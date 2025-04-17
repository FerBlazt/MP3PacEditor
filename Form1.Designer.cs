namespace MP3PacEditor
{
    partial class MainWindowForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fileNameMatchButton = new Button();
            clearButton = new Button();
            saveButton = new Button();
            selectFolderButton = new Button();
            titleBox = new TextBox();
            fileNameBox = new TextBox();
            albumBox = new TextBox();
            artistBox = new TextBox();
            folderListView = new ListView();
            fileNameLabel = new Label();
            titleLabel = new Label();
            artistLabel = new Label();
            albumLabel = new Label();
            label1 = new Label();
            infoListView = new ListView();
            infoLabel = new Label();
            searchBox = new TextBox();
            albumArtBox = new PictureBox();
            albumArtLabel = new Label();
            saveCopyButton = new Button();
            deleteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)albumArtBox).BeginInit();
            SuspendLayout();
            // 
            // fileNameMatchButton
            // 
            fileNameMatchButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            fileNameMatchButton.Location = new Point(12, 502);
            fileNameMatchButton.Name = "fileNameMatchButton";
            fileNameMatchButton.Size = new Size(117, 23);
            fileNameMatchButton.TabIndex = 0;
            fileNameMatchButton.Text = "Match File Name";
            fileNameMatchButton.UseVisualStyleBackColor = true;
            fileNameMatchButton.Click += fileNameMatchButton_Click;
            // 
            // clearButton
            // 
            clearButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            clearButton.Location = new Point(354, 530);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(117, 23);
            clearButton.TabIndex = 1;
            clearButton.Text = "Clear All Metadata";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            saveButton.Location = new Point(104, 474);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 2;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // selectFolderButton
            // 
            selectFolderButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectFolderButton.Location = new Point(1124, 530);
            selectFolderButton.Name = "selectFolderButton";
            selectFolderButton.Size = new Size(87, 23);
            selectFolderButton.TabIndex = 3;
            selectFolderButton.Text = "Select Folder";
            selectFolderButton.UseVisualStyleBackColor = true;
            selectFolderButton.Click += selectFolderButton_Click;
            // 
            // titleBox
            // 
            titleBox.Location = new Point(12, 68);
            titleBox.Name = "titleBox";
            titleBox.PlaceholderText = "Title";
            titleBox.Size = new Size(167, 23);
            titleBox.TabIndex = 4;
            // 
            // fileNameBox
            // 
            fileNameBox.Location = new Point(12, 24);
            fileNameBox.Name = "fileNameBox";
            fileNameBox.PlaceholderText = "Full file name";
            fileNameBox.Size = new Size(196, 23);
            fileNameBox.TabIndex = 5;
            // 
            // albumBox
            // 
            albumBox.Location = new Point(12, 156);
            albumBox.Name = "albumBox";
            albumBox.PlaceholderText = "Album";
            albumBox.Size = new Size(167, 23);
            albumBox.TabIndex = 6;
            // 
            // artistBox
            // 
            artistBox.Location = new Point(12, 112);
            artistBox.Name = "artistBox";
            artistBox.PlaceholderText = "Artist(-s)";
            artistBox.Size = new Size(167, 23);
            artistBox.TabIndex = 7;
            // 
            // folderListView
            // 
            folderListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            folderListView.Location = new Point(214, 24);
            folderListView.Name = "folderListView";
            folderListView.Size = new Size(997, 501);
            folderListView.Sorting = SortOrder.Ascending;
            folderListView.TabIndex = 8;
            folderListView.UseCompatibleStateImageBehavior = false;
            folderListView.View = View.Details;
            folderListView.ColumnClick += folderListView_ColumnClick;
            folderListView.SelectedIndexChanged += folderListView_SelectedIndexChanged;
            // 
            // fileNameLabel
            // 
            fileNameLabel.AutoSize = true;
            fileNameLabel.Location = new Point(12, 6);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new Size(107, 15);
            fileNameLabel.TabIndex = 9;
            fileNameLabel.Text = "Selected File Name";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(12, 50);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(29, 15);
            titleLabel.TabIndex = 10;
            titleLabel.Text = "Title";
            // 
            // artistLabel
            // 
            artistLabel.AutoSize = true;
            artistLabel.Location = new Point(12, 94);
            artistLabel.Name = "artistLabel";
            artistLabel.Size = new Size(35, 15);
            artistLabel.TabIndex = 11;
            artistLabel.Text = "Artist";
            // 
            // albumLabel
            // 
            albumLabel.AutoSize = true;
            albumLabel.Location = new Point(12, 138);
            albumLabel.Name = "albumLabel";
            albumLabel.Size = new Size(43, 15);
            albumLabel.TabIndex = 12;
            albumLabel.Text = "Album";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(214, 6);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 13;
            label1.Text = "Folder Files";
            // 
            // infoListView
            // 
            infoListView.Location = new Point(12, 200);
            infoListView.Name = "infoListView";
            infoListView.Size = new Size(196, 118);
            infoListView.TabIndex = 14;
            infoListView.UseCompatibleStateImageBehavior = false;
            infoListView.View = View.List;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.Location = new Point(12, 182);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new Size(107, 15);
            infoLabel.TabIndex = 15;
            infoLabel.Text = "Additional File Info";
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            searchBox.Location = new Point(214, 530);
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "File Name Search...";
            searchBox.Size = new Size(134, 23);
            searchBox.TabIndex = 16;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // albumArtBox
            // 
            albumArtBox.BorderStyle = BorderStyle.FixedSingle;
            albumArtBox.Location = new Point(12, 339);
            albumArtBox.Name = "albumArtBox";
            albumArtBox.Size = new Size(128, 128);
            albumArtBox.SizeMode = PictureBoxSizeMode.Zoom;
            albumArtBox.TabIndex = 17;
            albumArtBox.TabStop = false;
            albumArtBox.Click += albumArtBox_Click;
            // 
            // albumArtLabel
            // 
            albumArtLabel.AutoSize = true;
            albumArtLabel.Location = new Point(12, 321);
            albumArtLabel.Name = "albumArtLabel";
            albumArtLabel.Size = new Size(98, 15);
            albumArtLabel.TabIndex = 18;
            albumArtLabel.Text = "Album Art Image";
            // 
            // saveCopyButton
            // 
            saveCopyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            saveCopyButton.Location = new Point(12, 474);
            saveCopyButton.Name = "saveCopyButton";
            saveCopyButton.Size = new Size(86, 23);
            saveCopyButton.TabIndex = 19;
            saveCopyButton.Text = "Save as Copy";
            saveCopyButton.UseVisualStyleBackColor = true;
            saveCopyButton.Click += saveCopyButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(12, 530);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(75, 23);
            deleteButton.TabIndex = 20;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // MainWindowForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1222, 564);
            Controls.Add(deleteButton);
            Controls.Add(saveCopyButton);
            Controls.Add(albumArtLabel);
            Controls.Add(albumArtBox);
            Controls.Add(searchBox);
            Controls.Add(infoLabel);
            Controls.Add(infoListView);
            Controls.Add(selectFolderButton);
            Controls.Add(label1);
            Controls.Add(albumLabel);
            Controls.Add(artistLabel);
            Controls.Add(titleLabel);
            Controls.Add(fileNameLabel);
            Controls.Add(folderListView);
            Controls.Add(artistBox);
            Controls.Add(albumBox);
            Controls.Add(fileNameBox);
            Controls.Add(titleBox);
            Controls.Add(saveButton);
            Controls.Add(clearButton);
            Controls.Add(fileNameMatchButton);
            Name = "MainWindowForm";
            Text = "MP3PacEditor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)albumArtBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Button fileNameMatchButton;
        private Button clearButton;
        private Button saveButton;
        private Button selectFolderButton;
        private TextBox titleBox;
        private TextBox fileNameBox;
        private TextBox albumBox;
        private TextBox artistBox;
        private ListView folderListView;
        private Label fileNameLabel;
        private Label titleLabel;
        private Label artistLabel;
        private Label albumLabel;
        private Label label1;
        private ListView infoListView;
        private Label infoLabel;
        private TextBox searchBox;
        private PictureBox albumArtBox;
        private Label albumArtLabel;
        private Button saveCopyButton;
        private Button deleteButton;
    }
}