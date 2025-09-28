// Form1.Designer.cs
namespace DuplicateLineFinder
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnOpenFile = new Button();
            btnSaveChanges = new Button();
            lblFilePath = new Label();
            tabControl = new TabControl();
            tabPageDuplicates = new TabPage();
            treeViewResults = new TreeView();
            contextMenu = new ContextMenuStrip(components);
            deleteCheckedMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            expandAllMenuItem = new ToolStripMenuItem();
            collapseAllMenuItem = new ToolStripMenuItem();
            tabPageComments = new TabPage();
            listViewComments = new ListView();
            colLineNumber = new ColumnHeader();
            colLineText = new ColumnHeader();
            commentsContextMenu = new ContextMenuStrip(components);
            selectAllCommentsMenuItem = new ToolStripMenuItem();
            deselectAllCommentsMenuItem = new ToolStripMenuItem();
            tabPageEmptyLines = new TabPage();
            listViewEmptyLines = new ListView();
            colEmptyLineNumber = new ColumnHeader();
            emptyLinesContextMenu = new ContextMenuStrip(components);
            selectAllEmptyLinesMenuItem = new ToolStripMenuItem();
            deselectAllEmptyLinesMenuItem = new ToolStripMenuItem();
            tabPageStructuralDuplicates = new TabPage();
            treeViewStructuralDuplicates = new TreeView();
            structuralDuplicatesContextMenu = new ContextMenuStrip(components);
            selectAllStructuralMenuItem = new ToolStripMenuItem();
            deselectAllStructuralMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            expandAllStructuralMenuItem = new ToolStripMenuItem();
            collapseAllStructuralMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            btnExport = new Button();
            tabControl.SuspendLayout();
            tabPageDuplicates.SuspendLayout();
            contextMenu.SuspendLayout();
            tabPageComments.SuspendLayout();
            commentsContextMenu.SuspendLayout();
            tabPageEmptyLines.SuspendLayout();
            emptyLinesContextMenu.SuspendLayout();
            tabPageStructuralDuplicates.SuspendLayout();
            structuralDuplicatesContextMenu.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnOpenFile
            // 
            btnOpenFile.Dock = DockStyle.Top;
            btnOpenFile.Location = new Point(0, 15);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(800, 23);
            btnOpenFile.TabIndex = 0;
            btnOpenFile.Text = "Открыть файл...";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Dock = DockStyle.Bottom;
            btnSaveChanges.Location = new Point(0, 427);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(800, 23);
            btnSaveChanges.TabIndex = 1;
            btnSaveChanges.Text = "Сохранить изменения и создать .bak";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // lblFilePath
            // 
            lblFilePath.AutoSize = true;
            lblFilePath.Dock = DockStyle.Top;
            lblFilePath.Location = new Point(0, 0);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(97, 15);
            lblFilePath.TabIndex = 2;
            lblFilePath.Text = "Файл не выбран";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageDuplicates);
            tabControl.Controls.Add(tabPageComments);
            tabControl.Controls.Add(tabPageEmptyLines);
            tabControl.Controls.Add(tabPageStructuralDuplicates);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 61);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(800, 366);
            tabControl.TabIndex = 4;
            // 
            // tabPageDuplicates
            // 
            tabPageDuplicates.Controls.Add(treeViewResults);
            tabPageDuplicates.Location = new Point(4, 24);
            tabPageDuplicates.Name = "tabPageDuplicates";
            tabPageDuplicates.Padding = new Padding(3);
            tabPageDuplicates.Size = new Size(792, 338);
            tabPageDuplicates.TabIndex = 0;
            tabPageDuplicates.Text = "Дубликаты";
            tabPageDuplicates.UseVisualStyleBackColor = true;
            // 
            // treeViewResults
            // 
            treeViewResults.CheckBoxes = true;
            treeViewResults.ContextMenuStrip = contextMenu;
            treeViewResults.Dock = DockStyle.Fill;
            treeViewResults.Location = new Point(3, 3);
            treeViewResults.Name = "treeViewResults";
            treeViewResults.Size = new Size(786, 332);
            treeViewResults.TabIndex = 3;
            treeViewResults.AfterCheck += treeView_AfterCheck;
            // 
            // contextMenu
            // 
            contextMenu.Items.AddRange(new ToolStripItem[] { deleteCheckedMenuItem, toolStripSeparator1, expandAllMenuItem, collapseAllMenuItem });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(232, 76);
            // 
            // deleteCheckedMenuItem
            // 
            deleteCheckedMenuItem.Name = "deleteCheckedMenuItem";
            deleteCheckedMenuItem.Size = new Size(231, 22);
            deleteCheckedMenuItem.Text = "Удалить отмеченные строки";
            deleteCheckedMenuItem.Click += deleteCheckedMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(228, 6);
            // 
            // expandAllMenuItem
            // 
            expandAllMenuItem.Name = "expandAllMenuItem";
            expandAllMenuItem.Size = new Size(231, 22);
            expandAllMenuItem.Text = "Развернуть всё";
            expandAllMenuItem.Click += expandAllMenuItem_Click;
            // 
            // collapseAllMenuItem
            // 
            collapseAllMenuItem.Name = "collapseAllMenuItem";
            collapseAllMenuItem.Size = new Size(231, 22);
            collapseAllMenuItem.Text = "Свернуть всё";
            collapseAllMenuItem.Click += collapseAllMenuItem_Click;
            // 
            // tabPageComments
            // 
            tabPageComments.Controls.Add(listViewComments);
            tabPageComments.Location = new Point(4, 24);
            tabPageComments.Name = "tabPageComments";
            tabPageComments.Padding = new Padding(3);
            tabPageComments.Size = new Size(792, 338);
            tabPageComments.TabIndex = 1;
            tabPageComments.Text = "Комментарии";
            tabPageComments.UseVisualStyleBackColor = true;
            // 
            // listViewComments
            // 
            listViewComments.CheckBoxes = true;
            listViewComments.Columns.AddRange(new ColumnHeader[] { colLineNumber, colLineText });
            listViewComments.ContextMenuStrip = commentsContextMenu;
            listViewComments.Dock = DockStyle.Fill;
            listViewComments.FullRowSelect = true;
            listViewComments.Location = new Point(3, 3);
            listViewComments.Name = "listViewComments";
            listViewComments.Size = new Size(786, 332);
            listViewComments.TabIndex = 0;
            listViewComments.UseCompatibleStateImageBehavior = false;
            listViewComments.View = View.Details;
            listViewComments.ItemChecked += listView_ItemChecked;
            // 
            // colLineNumber
            // 
            colLineNumber.Text = "№ строки";
            colLineNumber.Width = 80;
            // 
            // colLineText
            // 
            colLineText.Text = "Текст строки";
            colLineText.Width = 500;
            // 
            // commentsContextMenu
            // 
            commentsContextMenu.Items.AddRange(new ToolStripItem[] { selectAllCommentsMenuItem, deselectAllCommentsMenuItem });
            commentsContextMenu.Name = "commentsContextMenu";
            commentsContextMenu.Size = new Size(212, 48);
            // 
            // selectAllCommentsMenuItem
            // 
            selectAllCommentsMenuItem.Name = "selectAllCommentsMenuItem";
            selectAllCommentsMenuItem.Size = new Size(211, 22);
            selectAllCommentsMenuItem.Text = "Выделить всё";
            selectAllCommentsMenuItem.Click += selectAllCommentsMenuItem_Click;
            // 
            // deselectAllCommentsMenuItem
            // 
            deselectAllCommentsMenuItem.Name = "deselectAllCommentsMenuItem";
            deselectAllCommentsMenuItem.Size = new Size(211, 22);
            deselectAllCommentsMenuItem.Text = "Снять выделение со всех";
            deselectAllCommentsMenuItem.Click += deselectAllCommentsMenuItem_Click;
            // 
            // tabPageEmptyLines
            // 
            tabPageEmptyLines.Controls.Add(listViewEmptyLines);
            tabPageEmptyLines.Location = new Point(4, 24);
            tabPageEmptyLines.Name = "tabPageEmptyLines";
            tabPageEmptyLines.Padding = new Padding(3);
            tabPageEmptyLines.Size = new Size(792, 338);
            tabPageEmptyLines.TabIndex = 2;
            tabPageEmptyLines.Text = "Пустые строки";
            tabPageEmptyLines.UseVisualStyleBackColor = true;
            // 
            // listViewEmptyLines
            // 
            listViewEmptyLines.CheckBoxes = true;
            listViewEmptyLines.Columns.AddRange(new ColumnHeader[] { colEmptyLineNumber });
            listViewEmptyLines.ContextMenuStrip = emptyLinesContextMenu;
            listViewEmptyLines.Dock = DockStyle.Fill;
            listViewEmptyLines.FullRowSelect = true;
            listViewEmptyLines.Location = new Point(3, 3);
            listViewEmptyLines.Name = "listViewEmptyLines";
            listViewEmptyLines.Size = new Size(786, 332);
            listViewEmptyLines.TabIndex = 0;
            listViewEmptyLines.UseCompatibleStateImageBehavior = false;
            listViewEmptyLines.View = View.Details;
            listViewEmptyLines.ItemChecked += listView_ItemChecked;
            // 
            // colEmptyLineNumber
            // 
            colEmptyLineNumber.Text = "№ строки";
            colEmptyLineNumber.Width = 120;
            // 
            // emptyLinesContextMenu
            // 
            emptyLinesContextMenu.Items.AddRange(new ToolStripItem[] { selectAllEmptyLinesMenuItem, deselectAllEmptyLinesMenuItem });
            emptyLinesContextMenu.Name = "emptyLinesContextMenu";
            emptyLinesContextMenu.Size = new Size(212, 48);
            // 
            // selectAllEmptyLinesMenuItem
            // 
            selectAllEmptyLinesMenuItem.Name = "selectAllEmptyLinesMenuItem";
            selectAllEmptyLinesMenuItem.Size = new Size(211, 22);
            selectAllEmptyLinesMenuItem.Text = "Выделить всё";
            selectAllEmptyLinesMenuItem.Click += selectAllEmptyLinesMenuItem_Click;
            // 
            // deselectAllEmptyLinesMenuItem
            // 
            deselectAllEmptyLinesMenuItem.Name = "deselectAllEmptyLinesMenuItem";
            deselectAllEmptyLinesMenuItem.Size = new Size(211, 22);
            deselectAllEmptyLinesMenuItem.Text = "Снять выделение со всех";
            deselectAllEmptyLinesMenuItem.Click += deselectAllEmptyLinesMenuItem_Click;
            // 
            // tabPageStructuralDuplicates
            // 
            tabPageStructuralDuplicates.Controls.Add(treeViewStructuralDuplicates);
            tabPageStructuralDuplicates.Location = new Point(4, 24);
            tabPageStructuralDuplicates.Name = "tabPageStructuralDuplicates";
            tabPageStructuralDuplicates.Padding = new Padding(3);
            tabPageStructuralDuplicates.Size = new Size(792, 338);
            tabPageStructuralDuplicates.TabIndex = 3;
            tabPageStructuralDuplicates.Text = "Структурные дубликаты (XML)";
            tabPageStructuralDuplicates.UseVisualStyleBackColor = true;
            // 
            // treeViewStructuralDuplicates
            // 
            treeViewStructuralDuplicates.CheckBoxes = true;
            treeViewStructuralDuplicates.ContextMenuStrip = structuralDuplicatesContextMenu;
            treeViewStructuralDuplicates.Dock = DockStyle.Fill;
            treeViewStructuralDuplicates.Location = new Point(3, 3);
            treeViewStructuralDuplicates.Name = "treeViewStructuralDuplicates";
            treeViewStructuralDuplicates.Size = new Size(786, 332);
            treeViewStructuralDuplicates.TabIndex = 0;
            treeViewStructuralDuplicates.AfterCheck += treeView_AfterCheck;
            // 
            // structuralDuplicatesContextMenu
            // 
            structuralDuplicatesContextMenu.Items.AddRange(new ToolStripItem[] { selectAllStructuralMenuItem, deselectAllStructuralMenuItem, toolStripSeparator2, expandAllStructuralMenuItem, collapseAllStructuralMenuItem });
            structuralDuplicatesContextMenu.Name = "structuralDuplicatesContextMenu";
            structuralDuplicatesContextMenu.Size = new Size(212, 98);
            // 
            // selectAllStructuralMenuItem
            // 
            selectAllStructuralMenuItem.Name = "selectAllStructuralMenuItem";
            selectAllStructuralMenuItem.Size = new Size(211, 22);
            selectAllStructuralMenuItem.Text = "Выделить всё";
            selectAllStructuralMenuItem.Click += selectAllStructuralMenuItem_Click;
            // 
            // deselectAllStructuralMenuItem
            // 
            deselectAllStructuralMenuItem.Name = "deselectAllStructuralMenuItem";
            deselectAllStructuralMenuItem.Size = new Size(211, 22);
            deselectAllStructuralMenuItem.Text = "Снять выделение со всех";
            deselectAllStructuralMenuItem.Click += deselectAllStructuralMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(208, 6);
            // 
            // expandAllStructuralMenuItem
            // 
            expandAllStructuralMenuItem.Name = "expandAllStructuralMenuItem";
            expandAllStructuralMenuItem.Size = new Size(211, 22);
            expandAllStructuralMenuItem.Text = "Развернуть всё";
            expandAllStructuralMenuItem.Click += expandAllStructuralMenuItem_Click;
            // 
            // collapseAllStructuralMenuItem
            // 
            collapseAllStructuralMenuItem.Name = "collapseAllStructuralMenuItem";
            collapseAllStructuralMenuItem.Size = new Size(211, 22);
            collapseAllStructuralMenuItem.Text = "Свернуть всё";
            collapseAllStructuralMenuItem.Click += collapseAllStructuralMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnExport);
            panel1.Controls.Add(btnOpenFile);
            panel1.Controls.Add(lblFilePath);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 61);
            panel1.TabIndex = 5;
            // 
            // btnExport
            // 
            btnExport.Dock = DockStyle.Top;
            btnExport.Enabled = false;
            btnExport.Location = new Point(0, 38);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(800, 23);
            btnExport.TabIndex = 3;
            btnExport.Text = "Экспорт содержимого текущего окна";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl);
            Controls.Add(panel1);
            Controls.Add(btnSaveChanges);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поиск дубликатов строк 1.0.1";
            tabControl.ResumeLayout(false);
            tabPageDuplicates.ResumeLayout(false);
            contextMenu.ResumeLayout(false);
            tabPageComments.ResumeLayout(false);
            commentsContextMenu.ResumeLayout(false);
            tabPageEmptyLines.ResumeLayout(false);
            emptyLinesContextMenu.ResumeLayout(false);
            tabPageStructuralDuplicates.ResumeLayout(false);
            structuralDuplicatesContextMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenFile;
        private Button btnSaveChanges;
        private Label lblFilePath;
        private TabControl tabControl;
        private TabPage tabPageDuplicates;
        private TreeView treeViewResults;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem deleteCheckedMenuItem;
        private TabPage tabPageComments;
        private ListView listViewComments;
        private ColumnHeader colLineNumber;
        private ColumnHeader colLineText;
        private ContextMenuStrip commentsContextMenu;
        private ToolStripMenuItem selectAllCommentsMenuItem;
        private ToolStripMenuItem deselectAllCommentsMenuItem;
        private TabPage tabPageEmptyLines;
        private ListView listViewEmptyLines;
        private ColumnHeader colEmptyLineNumber;
        private ContextMenuStrip emptyLinesContextMenu;
        private ToolStripMenuItem selectAllEmptyLinesMenuItem;
        private ToolStripMenuItem deselectAllEmptyLinesMenuItem;
        private Panel panel1;
        private Button btnExport;
        private TabPage tabPageStructuralDuplicates;
        private TreeView treeViewStructuralDuplicates;
        private ContextMenuStrip structuralDuplicatesContextMenu;
        private ToolStripMenuItem selectAllStructuralMenuItem;
        private ToolStripMenuItem deselectAllStructuralMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem expandAllMenuItem;
        private ToolStripMenuItem collapseAllMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem expandAllStructuralMenuItem;
        private ToolStripMenuItem collapseAllStructuralMenuItem;
    }
}