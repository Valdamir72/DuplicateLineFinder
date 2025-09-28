// Form1.cs (финальная версия)
using System.Windows.Forms;
using System.IO; // Важно для Path.ChangeExtension

namespace DuplicateLineFinder
{
    public partial class Form1 : Form
    {
        private FileProcessor _fileProcessor = new FileProcessor();
        private XmlFileProcessor _xmlProcessor = new XmlFileProcessor(); // Новый процессор
        private string? _currentFilePath;
        private bool _isUpdatingChecks = false;
        private bool _isOriginalBackupCreated = false;

        public Form1()
        {
            InitializeComponent();
            btnSaveChanges.Enabled = false;
            btnExport.Enabled = false;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Все файлы (*.*)|*.*|Текстовые файлы (*.txt, *.cs, *.py)|*.txt;*.cs;*.py|XML файлы (*.xml)|*.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _currentFilePath = ofd.FileName;
                    lblFilePath.Text = _currentFilePath;
                    _isOriginalBackupCreated = false;

                    try
                    {
                        // Запускаем оба процессора
                        _fileProcessor.ProcessFile(_currentFilePath);
                        _xmlProcessor.ProcessFile(_currentFilePath);

                        // Заполняем все вкладки
                        PopulateDuplicatesTab();
                        PopulateCommentsTab();
                        PopulateEmptyLinesTab();
                        PopulateStructuralDuplicatesTab(); // Новая вкладка

                        btnExport.Enabled = true; // Активируем экспорт после анализа
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnExport.Enabled = false;
                    }
                }
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;

            // Собираем данные со всех вкладок
            var duplicateIndexesToDelete = GetSelectedDuplicateIndexes();
            var commentIndexesToClean = GetSelectedCommentIndexes();
            var emptyLineIndexesToDelete = GetSelectedEmptyLineIndexes();
            var structuralIndexesToDelete = GetSelectedStructuralIndexes();

            // ПРАВИЛО: либо структурные правки, либо текстовые
            if (structuralIndexesToDelete.Any() && (duplicateIndexesToDelete.Any() || commentIndexesToClean.Any() || emptyLineIndexesToDelete.Any()))
            {
                MessageBox.Show("Нельзя одновременно сохранять изменения из вкладки 'Структурные дубликаты' и других вкладок.\n\nПожалуйста, снимите выделение на текстовых вкладках, чтобы сохранить изменения XML, или наоборот.", "Конфликт операций", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool hasTextChanges = duplicateIndexesToDelete.Any() || commentIndexesToClean.Any() || emptyLineIndexesToDelete.Any();
            bool hasStructuralChanges = structuralIndexesToDelete.Any();

            if (!hasTextChanges && !hasStructuralChanges)
            {
                MessageBox.Show("Нет отмеченных элементов для изменения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Логика бэкапов
                if (!_isOriginalBackupCreated)
                {
                    string originalBackupPath = Path.ChangeExtension(_currentFilePath, null) + "_orig.bak";
                    File.Copy(_currentFilePath, originalBackupPath, true);
                    _isOriginalBackupCreated = true;
                }
                string regularBackupPath = _currentFilePath + ".bak";
                File.Copy(_currentFilePath, regularBackupPath, true);

                // Выбираем, какой процессор использовать для сохранения
                string newContent;
                if (hasStructuralChanges)
                {
                    newContent = _xmlProcessor.GetModifiedContent(_currentFilePath, structuralIndexesToDelete);
                }
                else // hasTextChanges
                {
                    var newLines = _fileProcessor.GetModifiedContent(duplicateIndexesToDelete, commentIndexesToClean, emptyLineIndexesToDelete);
                    newContent = string.Join(Environment.NewLine, newLines);
                }

                File.WriteAllText(_currentFilePath, newContent);
                MessageBox.Show($"Файл успешно сохранен!\nПоследняя версия сохранена в: {regularBackupPath}", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Перезагружаем все
                _fileProcessor.ProcessFile(_currentFilePath);
                _xmlProcessor.ProcessFile(_currentFilePath);
                PopulateAllTabs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовый отчет (*.txt)|*.txt|JSON отчет (*.json)|*.json";
                sfd.FileName = $"{Path.GetFileNameWithoutExtension(_currentFilePath)}_report";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string reportContent = "";
                    string currentTabName = tabControl.SelectedTab.Name;
                    // Определяем формат по расширению файла
                    string format = Path.GetExtension(sfd.FileName).ToLower() == ".json" ? "json" : "txt";

                    if (currentTabName == "tabPageStructuralDuplicates")
                    {
                        reportContent = _xmlProcessor.GenerateReport(format);
                    }
                    else
                    {
                        reportContent = _fileProcessor.GenerateReport(currentTabName, format);
                    }

                    File.WriteAllText(sfd.FileName, reportContent);
                    MessageBox.Show($"Отчет успешно сохранен в: {sfd.FileName}", "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // --- Методы для отображения ---

        private void PopulateAllTabs()
        {
            PopulateDuplicatesTab();
            PopulateCommentsTab();
            PopulateEmptyLinesTab();
            PopulateStructuralDuplicatesTab();
        }

        private void PopulateDuplicatesTab()
        {
            treeViewResults.BeginUpdate();
            treeViewResults.Nodes.Clear();
            var duplicates = _fileProcessor.Duplicates;
            if (!duplicates.Any())
            {
                treeViewResults.Nodes.Add("Дубликаты не найдены.");
                tabControl.TabPages["tabPageDuplicates"].Text = "Дубликаты";
            }
            else
            {
                tabControl.TabPages["tabPageDuplicates"].Text = $"Дубликаты ({duplicates.Count})";
                foreach (var kvp in duplicates)
                {
                    var parentNode = new TreeNode($"'{kvp.Key}' (встречается {kvp.Value.Count} раз)");
                    foreach (var lineIndex in kvp.Value)
                    {
                        var childNode = new TreeNode($"Строка {lineIndex + 1}: {_fileProcessor.OriginalLines[lineIndex]}");
                        childNode.Tag = lineIndex;
                        parentNode.Nodes.Add(childNode);
                    }
                    treeViewResults.Nodes.Add(parentNode);
                }
            }
            treeViewResults.EndUpdate();
            UpdateSaveChangesButtonState();
        }
        private void PopulateCommentsTab()
        {
            listViewComments.BeginUpdate();
            listViewComments.Items.Clear();
            var comments = _fileProcessor.Comments;
            if (!comments.Any())
            {
                listViewComments.Items.Add("Комментарии не найдены.");
                tabControl.TabPages["tabPageComments"].Text = "Комментарии";
            }
            else
            {
                tabControl.TabPages["tabPageComments"].Text = $"Комментарии ({comments.Count})";
                foreach (var commentInfo in comments)
                {
                    var item = new ListViewItem(new[] { (commentInfo.LineIndex + 1).ToString(), commentInfo.LineText });
                    item.Tag = commentInfo.LineIndex;
                    listViewComments.Items.Add(item);
                }
            }
            listViewComments.EndUpdate();
            UpdateSaveChangesButtonState();
        }
        private void PopulateEmptyLinesTab()
        {
            listViewEmptyLines.BeginUpdate();
            listViewEmptyLines.Items.Clear();
            var emptyIndexes = _fileProcessor.EmptyLineIndexes;
            if (!emptyIndexes.Any())
            {
                listViewEmptyLines.Items.Add("Пустые строки не найдены.");
                tabControl.TabPages["tabPageEmptyLines"].Text = "Пустые строки";
            }
            else
            {
                tabControl.TabPages["tabPageEmptyLines"].Text = $"Пустые строки ({emptyIndexes.Count})";
                foreach (var lineIndex in emptyIndexes)
                {
                    var item = new ListViewItem((lineIndex + 1).ToString());
                    item.Tag = lineIndex;
                    listViewEmptyLines.Items.Add(item);
                }
            }
            listViewEmptyLines.EndUpdate();
            UpdateSaveChangesButtonState();
        }

        private void PopulateStructuralDuplicatesTab()
        {
            treeViewStructuralDuplicates.BeginUpdate();
            treeViewStructuralDuplicates.Nodes.Clear();
            var duplicates = _xmlProcessor.StructuralDuplicates;
            if (!duplicates.Any())
            {
                treeViewStructuralDuplicates.Nodes.Add("Структурные дубликаты не найдены.");
                tabControl.TabPages["tabPageStructuralDuplicates"].Text = "Структурные дубликаты (XML)";
            }
            else
            {
                tabControl.TabPages["tabPageStructuralDuplicates"].Text = $"Структурные дубликаты (XML) ({duplicates.Count})";
                foreach (var kvp in duplicates)
                {
                    // НОВЫЙ ФОРМАТ ОТОБРАЖЕНИЯ: (NN раз) <object...>
                    var parentNode = new TreeNode($"({kvp.Value.Count} раз) {kvp.Key}");
                    foreach (var blockInfo in kvp.Value)
                    {
                        var childNode = new TreeNode($"Блок на строке {blockInfo.StartLine}");
                        childNode.Tag = blockInfo.StartLine; // Сохраняем начальную строку блока
                        parentNode.Nodes.Add(childNode);
                    }
                    treeViewStructuralDuplicates.Nodes.Add(parentNode);
                }
            }
            treeViewStructuralDuplicates.EndUpdate();
            UpdateSaveChangesButtonState();
        }

        // --- Методы для сбора отмеченных элементов ---

        private HashSet<int> GetSelectedDuplicateIndexes()
        {
            var indexes = new HashSet<int>();
            var checkedNodes = GetCheckedChildNodes(treeViewResults.Nodes);
            foreach (var node in checkedNodes)
            {
                if (node.Tag is int lineIndex) indexes.Add(lineIndex);
            }
            return indexes;
        }
        private HashSet<int> GetSelectedCommentIndexes()
        {
            var indexes = new HashSet<int>();
            foreach (ListViewItem item in listViewComments.CheckedItems)
            {
                if (item.Tag is int lineIndex) indexes.Add(lineIndex);
            }
            return indexes;
        }
        private HashSet<int> GetSelectedEmptyLineIndexes()
        {
            var indexes = new HashSet<int>();
            foreach (ListViewItem item in listViewEmptyLines.CheckedItems)
            {
                if (item.Tag is int lineIndex) indexes.Add(lineIndex);
            }
            return indexes;
        }

        private HashSet<int> GetSelectedStructuralIndexes()
        {
            var indexes = new HashSet<int>();
            var checkedNodes = GetCheckedChildNodes(treeViewStructuralDuplicates.Nodes);
            foreach (var node in checkedNodes)
            {
                if (node.Tag is int startLine) indexes.Add(startLine);
            }
            return indexes;
        }

        private List<TreeNode> GetCheckedChildNodes(TreeNodeCollection nodes)
        {
            var checkedNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked && node.Tag != null)
                {
                    checkedNodes.Add(node);
                }
                checkedNodes.AddRange(GetCheckedChildNodes(node.Nodes));
            }
            return checkedNodes;
        }

        // --- Обработчики событий и вспомогательные методы ---

        private void UpdateSaveChangesButtonState()
        {
            bool hasChanges = GetSelectedDuplicateIndexes().Any() ||
                              GetSelectedCommentIndexes().Any() ||
                              GetSelectedEmptyLineIndexes().Any() ||
                              GetSelectedStructuralIndexes().Any();
            btnSaveChanges.Enabled = hasChanges;
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateSaveChangesButtonState();
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_isUpdatingChecks) return;
            try
            {
                _isUpdatingChecks = true;
                TreeNode node = e.Node;
                if (node.Parent == null) // Родитель
                {
                    foreach (TreeNode child in node.Nodes) child.Checked = node.Checked;
                }
                else // Ребенок
                {
                    int checkedCount = node.Parent.Nodes.Cast<TreeNode>().Count(n => n.Checked);
                    node.Parent.Checked = (checkedCount == node.Parent.Nodes.Count);
                }
            }
            finally { _isUpdatingChecks = false; }
            UpdateSaveChangesButtonState();
        }

        // ... (остальные методы: GetCheckedChildNodes, deleteCheckedMenuItem, listView_ItemChecked, SetAllListViewChecks и т.д. без изменений)
        // ... НО нужно привязать treeView_AfterCheck к новому TreeView!

        // НОВЫЕ ОБРАБОТЧИКИ ДЛЯ НОВОГО КОНТЕКСТНОГО МЕНЮ
        private void selectAllStructuralMenuItem_Click(object sender, EventArgs e)
        {
            SetAllTreeViewChecks(treeViewStructuralDuplicates, true);
        }

        private void deselectAllStructuralMenuItem_Click(object sender, EventArgs e)
        {
            SetAllTreeViewChecks(treeViewStructuralDuplicates, false);
        }

        // Новый вспомогательный метод для TreeView
        private void SetAllTreeViewChecks(TreeView treeView, bool isChecked)
        {
            treeView.BeginUpdate();
            foreach (TreeNode parentNode in treeView.Nodes)
            {
                parentNode.Checked = isChecked;
                // 'AfterCheck' сработает и отметит всех детей
            }
            treeView.EndUpdate();
        }
        private void CollapseAllNodes(TreeView treeView)
        {
            treeView.BeginUpdate();
            foreach (TreeNode node in treeView.Nodes)
            {
                node.Collapse();
            }
            treeView.EndUpdate();
        }
        private void deleteCheckedMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var node in GetCheckedChildNodes(treeViewResults.Nodes))
            {
                node.NodeFont = new Font(treeViewResults.Font, FontStyle.Strikeout);
                node.ForeColor = Color.Gray;
            }
            UpdateSaveChangesButtonState();
        }
        private void selectAllCommentsMenuItem_Click(object sender, EventArgs e)
        {
            SetAllListViewChecks(listViewComments, true);
        }
        private void deselectAllCommentsMenuItem_Click(object sender, EventArgs e)
        {
            SetAllListViewChecks(listViewComments, false);
        }
        private void selectAllEmptyLinesMenuItem_Click(object sender, EventArgs e)
        {
            SetAllListViewChecks(listViewEmptyLines, true);
        }
        private void deselectAllEmptyLinesMenuItem_Click(object sender, EventArgs e)
        {
            SetAllListViewChecks(listViewEmptyLines, false);
        }
        private void SetAllListViewChecks(ListView listView, bool isChecked)
        {
            listView.BeginUpdate();
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = isChecked;
            }
            listView.EndUpdate();
        }
        // --- Обработчики для контекстного меню обычных дубликатов ---
        private void expandAllMenuItem_Click(object sender, EventArgs e)
        {
            treeViewResults.ExpandAll();
        }

        private void collapseAllMenuItem_Click(object sender, EventArgs e)
        {
            CollapseAllNodes(treeViewResults);
        }

        // --- Обработчики для контекстного меню структурных дубликатов ---
        private void expandAllStructuralMenuItem_Click(object sender, EventArgs e)
        {
            treeViewStructuralDuplicates.ExpandAll();
        }

        private void collapseAllStructuralMenuItem_Click(object sender, EventArgs e)
        {
            CollapseAllNodes(treeViewStructuralDuplicates);
        }
    }
}