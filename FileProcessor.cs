// FileProcessor.cs
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DuplicateLineFinder
{
    public record CommentInfo(int LineIndex, string LineText);

    public class FileProcessor
    {
        public List<string> OriginalLines { get; private set; } = new List<string>();
        public Dictionary<string, List<int>> Duplicates { get; private set; } = new Dictionary<string, List<int>>();
        public List<CommentInfo> Comments { get; private set; } = new List<CommentInfo>();
        public List<int> EmptyLineIndexes { get; private set; } = new List<int>(); // НОВОЕ

        private const string CommentMarker = "//";

        public void ProcessFile(string filePath)
        {
            OriginalLines = File.ReadAllLines(filePath).ToList();
            FindDuplicates();
            FindComments();
            FindEmptyLines(); // НОВОЕ
        }

        private void FindDuplicates()
        {
            // ... без изменений
            Duplicates.Clear();
            var linesMap = new Dictionary<string, List<int>>();
            for (int i = 0; i < OriginalLines.Count; i++)
            {
                var line = OriginalLines[i];
                if (!linesMap.ContainsKey(line))
                {
                    linesMap[line] = new List<int>();
                }
                linesMap[line].Add(i);
            }
            Duplicates = linesMap.Where(kvp => kvp.Value.Count > 1)
                                 .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private void FindComments()
        {
            // ... без изменений
            Comments.Clear();
            for (int i = 0; i < OriginalLines.Count; i++)
            {
                if (OriginalLines[i].Contains(CommentMarker))
                {
                    Comments.Add(new CommentInfo(i, OriginalLines[i]));
                }
            }
        }

        // НОВЫЙ МЕТОД
        private void FindEmptyLines()
        {
            EmptyLineIndexes.Clear();
            for (int i = 0; i < OriginalLines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(OriginalLines[i]))
                {
                    EmptyLineIndexes.Add(i);
                }
            }
        }

        // ОБНОВЛЕННЫЙ МЕТОД СОХРАНЕНИЯ
        public List<string> GetModifiedContent(HashSet<int> duplicateIndexesToDelete,
                                               HashSet<int> commentIndexesToClean,
                                               HashSet<int> emptyLineIndexesToDelete)
        {
            var modifiedLines = new List<string>(OriginalLines);

            // Шаг 1: Удаляем комментарии
            foreach (var index in commentIndexesToClean)
            {
                var line = modifiedLines[index];
                int commentStartIndex = line.IndexOf(CommentMarker);
                if (commentStartIndex != -1)
                {
                    modifiedLines[index] = line.Substring(0, commentStartIndex).TrimEnd();
                }
            }

            // Шаг 2: Собираем ВСЕ индексы строк, которые нужно полностью удалить
            var allIndexesToDelete = new HashSet<int>(duplicateIndexesToDelete);
            allIndexesToDelete.UnionWith(emptyLineIndexesToDelete);

            // Шаг 3: Формируем финальный список, исключая ненужные строки
            var finalLines = new List<string>();
            for (int i = 0; i < modifiedLines.Count; i++)
            {
                // Пропускаем строки, которые были помечены для полного удаления
                if (allIndexesToDelete.Contains(i))
                {
                    continue;
                }
                finalLines.Add(modifiedLines[i]);
            }
            return finalLines;
        }

        public string GenerateReport(string tabName, string format)
        {
            object reportData = null;
            switch (tabName)
            {
                case "tabPageDuplicates":
                    reportData = Duplicates.Select(kvp => new
                    {
                        Content = kvp.Key,
                        Count = kvp.Value.Count,
                        Lines = kvp.Value.Select(i => i + 1)
                    });
                    break;
                case "tabPageComments":
                    reportData = Comments.Select(c => new
                    {
                        Line = c.LineIndex + 1,
                        Text = c.LineText
                    });
                    break;
                case "tabPageEmptyLines":
                    reportData = new
                    {
                        Count = EmptyLineIndexes.Count,
                        Lines = EmptyLineIndexes.Select(i => i + 1)
                    };
                    break;
            }

            if (reportData == null) return "Нет данных для экспорта.";

            if (format == "json")
            {
                // --- ВОТ ИЗМЕНЕНИЕ ---
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Для красивого форматирования с отступами
                                          // Эта опция говорит сериализатору НЕ кодировать кириллицу и символы вроде < > "
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                return JsonSerializer.Serialize(reportData, options);
            }
            else // txt format
            {
                // ... (эта часть остается без изменений)
                var sb = new StringBuilder();
                switch (tabName)
                {
                    case "tabPageDuplicates":
                        sb.AppendLine("ОТЧЕТ ПО ДУБЛИКАТАМ СТРОК");
                        sb.AppendLine("============================");
                        foreach (var group in (IEnumerable<dynamic>)reportData)
                        {
                            sb.AppendLine($"\nГруппа: '{group.Content}'");
                            sb.AppendLine($"Количество: {group.Count}");
                            sb.AppendLine($"На строках: {string.Join(", ", group.Lines)}");
                        }
                        break;
                    case "tabPageComments":
                        sb.AppendLine("ОТЧЕТ ПО КОММЕНТАРИЯМ");
                        sb.AppendLine("=========================");
                        foreach (var comment in (IEnumerable<dynamic>)reportData)
                        {
                            sb.AppendLine($"Строка {comment.Line}: {comment.Text}");
                        }
                        break;
                    case "tabPageEmptyLines":
                        dynamic emptyData = reportData;
                        sb.AppendLine("ОТЧЕТ ПО ПУСТЫМ СТРОКАМ");
                        sb.AppendLine("=========================");
                        sb.AppendLine($"Всего найдено: {emptyData.Count}");
                        sb.AppendLine($"На строках: {string.Join(", ", emptyData.Lines)}");
                        break;
                }
                return sb.ToString();
            }
        }
    }
}