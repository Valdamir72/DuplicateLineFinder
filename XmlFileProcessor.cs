// XmlFileProcessor.cs
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.Json;

namespace DuplicateLineFinder
{
    // Структура для хранения информации о XML-блоке
    public record XmlBlockInfo(int StartLine, int EndLine, XElement Element);

    public class XmlFileProcessor
    {
        // Словарь: Ключ - первая строка блока <object>, Значение - список всех блоков с таким же ключом
        public Dictionary<string, List<XmlBlockInfo>> StructuralDuplicates { get; private set; } = new();

        public void ProcessFile(string filePath)
        {
            StructuralDuplicates.Clear();
            try
            {
                var doc = XDocument.Load(filePath, LoadOptions.SetLineInfo);
                var allObjects = doc.Descendants("object")
                                    .Select(el => new XmlBlockInfo(((IXmlLineInfo)el).LineNumber, 0, el))
                                    .ToList();

                // Шаг 1: Группируем все блоки по их открывающему тегу.
                var groupsByOpeningTag = allObjects.GroupBy(block => GetOpeningTag(block.Element));

                var finalDuplicates = new Dictionary<string, List<XmlBlockInfo>>();

                // Шаг 2: Проходим по каждой группе с одинаковым открывающим тегом.
                foreach (var group in groupsByOpeningTag)
                {
                    // Шаг 3: Внутри этой группы ищем настоящие дубликаты по ПОЛНОМУ совпадению.
                    var innerGroupsByFullContent = group.GroupBy(block => block.Element.ToString(SaveOptions.DisableFormatting));

                    // Шаг 4: Отбираем только те "подгруппы", где больше одного элемента (это и есть наши дубли).
                    foreach (var innerGroup in innerGroupsByFullContent)
                    {
                        if (innerGroup.Count() > 1)
                        {
                            // Ключ - это открывающий тег.
                            // Значение - это список блоков, которые полностью идентичны.
                            string key = group.Key;

                            // Если вдруг такой ключ уже есть (из-за другой "подгруппы"), просто добавляем к нему новые блоки.
                            if (finalDuplicates.ContainsKey(key))
                            {
                                finalDuplicates[key].AddRange(innerGroup.ToList());
                            }
                            else
                            {
                                finalDuplicates[key] = innerGroup.ToList();
                            }
                        }
                    }
                }

                StructuralDuplicates = finalDuplicates;
            }
            catch (XmlException)
            {
                StructuralDuplicates.Clear();
            }
        }

        // Вспомогательный метод для получения первой строки из текста элемента
        private string GetFirstLine(string elementText)
        {
            return elementText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
        }
        private string GetOpeningTag(XElement element)
        {
            // Этот метод надежно извлекает только открывающий тег, даже если XML в одну строку
            string fullText = element.ToString(SaveOptions.DisableFormatting);
            int closingBracketIndex = fullText.IndexOf('>');
            if (closingBracketIndex > 0)
            {
                return fullText.Substring(0, closingBracketIndex + 1);
            }
            return GetFirstLine(fullText); // Запасной вариант
        }


        public string GetModifiedContent(string filePath, HashSet<int> startLinesToDelete)
        {
            var doc = XDocument.Load(filePath, LoadOptions.SetLineInfo);

            // Находим и удаляем узлы по их начальной строке
            doc.Descendants("object")
               .Where(el => startLinesToDelete.Contains(((IXmlLineInfo)el).LineNumber))
               .Remove();

            // Сохраняем в строку с правильным форматированием
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }

        public string GenerateReport(string format)
        {
            var reportData = StructuralDuplicates.Select(kvp => new
            {
                Content = kvp.Key,
                Count = kvp.Value.Count,
                Lines = kvp.Value.Select(b => b.StartLine)
            });

            if (format == "json")
            {
                // --- ТО ЖЕ САМОЕ ИЗМЕНЕНИЕ ---
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                return JsonSerializer.Serialize(reportData, options);
            }
            else // txt format
            {
                // ... (эта часть остается без изменений)
                var sb = new StringBuilder();
                sb.AppendLine("ОТЧЕТ ПО СТРУКТУРНЫМ ДУБЛИКАТАМ (XML)");
                sb.AppendLine("========================================");
                foreach (var group in reportData)
                {
                    sb.AppendLine($"\nГруппа: {group.Content}");
                    sb.AppendLine($"Количество: {group.Count}");
                    sb.AppendLine($"На строках: {string.Join(", ", group.Lines)}");
                }
                return sb.ToString();
            }
        }
    }
}