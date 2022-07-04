
namespace __Model_Studio
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FileNodeTree = new System.Windows.Forms.TreeView();
            this.ModelStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToCSMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToCSMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendABugReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EntryNodeTree = new System.Windows.Forms.TreeView();
            this.EntryStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewItemHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToJEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.EntryStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileNodeTree
            // 
            this.FileNodeTree.AllowDrop = true;
            this.FileNodeTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FileNodeTree.ContextMenuStrip = this.ModelStrip;
            this.FileNodeTree.FullRowSelect = true;
            this.FileNodeTree.Location = new System.Drawing.Point(13, 42);
            this.FileNodeTree.Name = "FileNodeTree";
            this.FileNodeTree.Size = new System.Drawing.Size(243, 576);
            this.FileNodeTree.TabIndex = 0;
            this.FileNodeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.FileNodeTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileNodeTree_DragDrop);
            this.FileNodeTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileNodeTree_DragEnter);
            // 
            // ModelStrip
            // 
            this.ModelStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.convertToCSMToolStripMenuItem,
            this.convertToCSMToolStripMenuItem1,
            this.convertToJEMToolStripMenuItem});
            this.ModelStrip.Name = "ModelStrip";
            this.ModelStrip.Size = new System.Drawing.Size(181, 136);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Enabled = false;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // convertToCSMToolStripMenuItem
            // 
            this.convertToCSMToolStripMenuItem.Enabled = false;
            this.convertToCSMToolStripMenuItem.Name = "convertToCSMToolStripMenuItem";
            this.convertToCSMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertToCSMToolStripMenuItem.Text = "Convert to JSON";
            this.convertToCSMToolStripMenuItem.Click += new System.EventHandler(this.convertToCSMToolStripMenuItem_Click);
            // 
            // convertToCSMToolStripMenuItem1
            // 
            this.convertToCSMToolStripMenuItem1.Name = "convertToCSMToolStripMenuItem1";
            this.convertToCSMToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.convertToCSMToolStripMenuItem1.Text = "Convert to CSM";
            this.convertToCSMToolStripMenuItem1.Click += new System.EventHandler(this.convertToCSMToolStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(728, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.changeLogToolStripMenuItem,
            this.sourceCodeToolStripMenuItem,
            this.sendABugReportToolStripMenuItem,
            this.tESTINGToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // changeLogToolStripMenuItem
            // 
            this.changeLogToolStripMenuItem.Name = "changeLogToolStripMenuItem";
            this.changeLogToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changeLogToolStripMenuItem.Text = "Change log";
            this.changeLogToolStripMenuItem.Click += new System.EventHandler(this.changeLogToolStripMenuItem_Click);
            // 
            // sourceCodeToolStripMenuItem
            // 
            this.sourceCodeToolStripMenuItem.Name = "sourceCodeToolStripMenuItem";
            this.sourceCodeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.sourceCodeToolStripMenuItem.Text = "Source Code";
            this.sourceCodeToolStripMenuItem.Click += new System.EventHandler(this.sourceCodeToolStripMenuItem_Click);
            // 
            // sendABugReportToolStripMenuItem
            // 
            this.sendABugReportToolStripMenuItem.Name = "sendABugReportToolStripMenuItem";
            this.sendABugReportToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.sendABugReportToolStripMenuItem.Text = "Send a bug report";
            this.sendABugReportToolStripMenuItem.Click += new System.EventHandler(this.sendABugReportToolStripMenuItem_Click);
            // 
            // tESTINGToolStripMenuItem
            // 
            this.tESTINGToolStripMenuItem.Name = "tESTINGToolStripMenuItem";
            this.tESTINGToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.tESTINGToolStripMenuItem.Text = "TESTING";
            this.tESTINGToolStripMenuItem.Click += new System.EventHandler(this.tESTINGToolStripMenuItem_Click);
            // 
            // EntryNodeTree
            // 
            this.EntryNodeTree.AllowDrop = true;
            this.EntryNodeTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EntryNodeTree.ContextMenuStrip = this.EntryStrip;
            this.EntryNodeTree.FullRowSelect = true;
            this.EntryNodeTree.Location = new System.Drawing.Point(262, 42);
            this.EntryNodeTree.Name = "EntryNodeTree";
            this.EntryNodeTree.Size = new System.Drawing.Size(454, 576);
            this.EntryNodeTree.TabIndex = 2;
            this.EntryNodeTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileNodeTree_DragDrop);
            this.EntryNodeTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileNodeTree_DragEnter);
            this.EntryNodeTree.DoubleClick += new System.EventHandler(this.EntryNodeTree_AfterSelect);
            // 
            // EntryStrip
            // 
            this.EntryStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewItemHexToolStripMenuItem});
            this.EntryStrip.Name = "contextMenuStrip1";
            this.EntryStrip.Size = new System.Drawing.Size(149, 26);
            // 
            // viewItemHexToolStripMenuItem
            // 
            this.viewItemHexToolStripMenuItem.Name = "viewItemHexToolStripMenuItem";
            this.viewItemHexToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.viewItemHexToolStripMenuItem.Text = "View item hex";
            this.viewItemHexToolStripMenuItem.Click += new System.EventHandler(this.viewItemHexToolStripMenuItem_Click);
            // 
            // convertToJEMToolStripMenuItem
            // 
            this.convertToJEMToolStripMenuItem.Name = "convertToJEMToolStripMenuItem";
            this.convertToJEMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertToJEMToolStripMenuItem.Text = "Convert to JEM";
            this.convertToJEMToolStripMenuItem.Click += new System.EventHandler(this.convertToJEMToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 630);
            this.Controls.Add(this.EntryNodeTree);
            this.Controls.Add(this.FileNodeTree);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Spark Model Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileNodeTree_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileNodeTree_DragEnter);
            this.ModelStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.EntryStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView FileNodeTree;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TreeView EntryNodeTree;
        private System.Windows.Forms.ContextMenuStrip EntryStrip;
        private System.Windows.Forms.ToolStripMenuItem viewItemHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ModelStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToCSMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tESTINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendABugReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToCSMToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem convertToJEMToolStripMenuItem;
    }
}

