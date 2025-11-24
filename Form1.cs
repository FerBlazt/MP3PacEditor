using System.Drawing;
using System.IO;
using static ImageHelpers;

namespace MP3PacEditor
{
    public partial class MainWindowForm : Form
    {

        private const int ColumnWidthWide = 250;
        private const int ColumnWidthMedium = 150;
        private const int ColumnWidthNarrow = 100;

        private static string configFilePath;
        private static string lastUsedFolder;
        private Dictionary<string, byte[]> albumArtMap = new Dictionary<string, byte[]>();

        static MainWindowForm()
        {
            configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mp3editor.cfg");

            if (File.Exists(configFilePath))
            {
                lastUsedFolder = File.ReadAllText(configFilePath);
            }
        }

        public MainWindowForm()
        {
            InitializeComponent();
            albumArtBox.Cursor = Cursors.Hand;

            if (!string.IsNullOrEmpty(lastUsedFolder) && Directory.Exists(lastUsedFolder))
            {
                LoadFilesFromFolder(lastUsedFolder);
                WarningPopup.Show(this, $"Successfully loaded last used folder!", Color.LimeGreen);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    lastUsedFolder = folderDialog.SelectedPath;
                    File.WriteAllText(configFilePath, lastUsedFolder);
                    LoadFilesFromFolder(lastUsedFolder);
                }
            }
        }

        private void LoadFilesFromFolder(string selectedFolderPath)
        {
            var audioFiles = AudioFileHelper.GetSupportedAudioFiles(selectedFolderPath);

            folderListView.Columns.Clear();
            folderListView.Columns.Add("File Name", ColumnWidthWide);
            folderListView.Columns.Add("Title", ColumnWidthMedium);
            folderListView.Columns.Add("Artist", ColumnWidthMedium);
            folderListView.Columns.Add("Album", ColumnWidthNarrow);
            folderListView.Columns.Add("File Size", ColumnWidthNarrow);
            folderListView.Columns.Add("Bitrate", ColumnWidthNarrow);
            folderListView.Columns.Add("Date Modified", ColumnWidthMedium);

            folderListView.Items.Clear();

            //foreach (string file in mp3Files)
            foreach (string file in audioFiles)
            {
                try
                {
                    var fileTag = TagLib.File.Create(file);
                    var fileInfo = new FileInfo(file);

                    // For folderListView
                    string fileName = Path.GetFileName(file);
                    string title = fileTag.Tag.Title;
                    string artist = fileTag.Tag.Artists.Length > 0 ? string.Join(", ", fileTag.Tag.Artists) : null;
                    string album = fileTag.Tag.Album;
                    double fileSize = new FileInfo(file).Length / 1e+6;
                    fileSize = Math.Round(fileSize, 2);
                    int bitrate = fileTag.Properties.AudioBitrate;

                    // For infoListView
                    string duration = fileTag.Properties.Duration.ToString(@"hh\:mm\:ss");

                    string dateCreated = fileInfo.CreationTime.ToString(Formatting.DateTimeFormat);
                    string dateModified = fileInfo.LastWriteTime.ToString(Formatting.DateTimeFormat);

                    string format = fileTag.MimeType.Remove(0, 7).ToUpper();

                    // For PictureBox
                    var albumArt = fileTag.Tag.Pictures.Length > 0 ? fileTag.Tag.Pictures[0].Data.Data : null;

                    if (!albumArtMap.ContainsKey(file))
                        albumArtMap[file] = albumArt;

                    ListViewItem item = new ListViewItem(fileName)
                    {
                        Tag = file
                    };
                    item.SubItems.Add(title);
                    item.SubItems.Add(artist);
                    item.SubItems.Add(album);
                    item.SubItems.Add(fileSize.ToString() + " MB");
                    item.SubItems.Add(bitrate.ToString() + " kbps");
                    item.SubItems.Add(dateModified);
                    item.SubItems.Add(dateCreated);
                    item.SubItems.Add(duration);
                    item.SubItems.Add(format);

                    folderListView.Items.Add(item);
                }
                catch (Exception ex)
                {
                    var customEx = new MetadataReadException(file, "Error reading metadata.", ex);
                    WarningPopup.Show(this, $"Failed to read metadata from file.\n({Path.GetFileName(file)})", Color.DarkRed);
                }
            }
            // Global storage, naudojamas searchui)
            allItems = folderListView.Items.Cast<ListViewItem>().ToList();
        }

        private void folderListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (folderListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = folderListView.SelectedItems[0];

                fileNameBox.Text = Path.GetFileNameWithoutExtension(selectedItem.SubItems[0].Text);
                titleBox.Text = selectedItem.SubItems[1].Text;
                artistBox.Text = selectedItem.SubItems[2].Text;
                albumBox.Text = selectedItem.SubItems[3].Text;

                infoListView.Items.Clear();
                infoListView.Items.Add("Date Modified: " + selectedItem.SubItems[6].Text);
                infoListView.Items.Add("Date Created: " + selectedItem.SubItems[7].Text);
                infoListView.Items.Add("Duration: " + selectedItem.SubItems[8].Text);
                infoListView.Items.Add("Format: " + selectedItem.SubItems[9].Text);

                string filePath = selectedItem.Tag.ToString();

                var image = (albumArtMap.ContainsKey(filePath) && albumArtMap[filePath] != null)
                    ? ConvertIfValid(albumArtMap[filePath], data =>
                {
                    try
                    {
                        using var ms = new MemoryStream(data);
                        var original = Image.FromStream(ms);
                        return CropCenterSquare(original);
                    }
                    catch
                    {
                        return ImageHelpers.CropCenterSquare(MP3PacResources.noAlbumArt);
                    }
                })
                : ImageHelpers.CropCenterSquare(MP3PacResources.noAlbumArt);
                albumArtBox.Image = image;
            }
        }

        private int lastSortedColumn = -1;
        private bool ascending = true;
        private void folderListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lastSortedColumn)
            {
                ascending = !ascending;
            }
            else
            {
                ascending = true;
                lastSortedColumn = e.Column;
            }

            folderListView.ListViewItemSorter = new ListViewItemComparer(e.Column, ascending);
            folderListView.Sort();
            UpdateColumnHeaders();
        }

        private void UpdateColumnHeaders()
        {
            for (int i = 0; i < folderListView.Columns.Count; i++)
            {
                folderListView.Columns[i].Text = i switch
                {
                    _ when i == lastSortedColumn => (ascending ? "▲ " : "▼ ") + folderListView.Columns[i].Text.Trim('▲', '▼', ' '),
                    _ => folderListView.Columns[i].Text.Trim('▲', '▼', ' ')
                };
            }
        }

        private List<ListViewItem> allItems = new List<ListViewItem>();
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.Trim();

            var filteredItems = allItems
                .Where(item => string.IsNullOrEmpty(searchText) ||
                               item.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            folderListView.Items.Clear();
            folderListView.Items.AddRange(filteredItems.ToArray());
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            if (!TryGetSelectedFilePath(out string filePath))
                return;

            try
            {
                var fileTag = TagLib.File.Create(filePath);

                UpdateTagFromInputs(fileTag, titleBox.Text, artistBox.Text, albumBox.Text);

                fileTag.Save();

                string newFileName = Path.Combine(Path.GetDirectoryName(filePath), fileNameBox.Text + Path.GetExtension(filePath));

                RenameFileIfNeeded(ref filePath, newFileName);

                WarningPopup.Show(this, $"Successfully updated file metadata!\n({Path.GetFileName(filePath)})", Color.LimeGreen);

                LoadFilesFromFolder(Path.GetDirectoryName(filePath));
            }
            catch (Exception ex)
            {
                WarningPopup.Show(this,$"Error updating metadata: {ex.Message}",Color.DarkRed);
            }
        }

        private void fileNameMatchButton_Click(object sender, EventArgs e)
        {

            if (!TryGetSelectedFilePath(out string filePath))
                return;


            try
            {
                var fileTag = TagLib.File.Create(filePath);

                string title = fileTag.Tag.Title;
                string artist = fileTag.Tag.Performers.Length > 0 ? fileTag.Tag.Performers[0] : null;
                string album = fileTag.Tag.Album;

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(artist))
                {
                    WarningPopup.Show(this, $"The file must have both a title and an artist to rename it.", Color.Goldenrod);
                    return;
                }

                string newFileName;
                if (string.IsNullOrEmpty(album))
                {
                    newFileName = Path.Combine(Path.GetDirectoryName(filePath), $"{artist} - {title}{Path.GetExtension(filePath)}");
                }
                else
                {
                    newFileName = Path.Combine(Path.GetDirectoryName(filePath), $"{artist} - {title} {album}{Path.GetExtension(filePath)}");
                }

                RenameFileIfNeeded(ref filePath, newFileName);

                WarningPopup.Show(this, $"Successfully updated file name!\n({Path.GetFileName(filePath)})", Color.LimeGreen);

                LoadFilesFromFolder(Path.GetDirectoryName(filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void albumArtBox_Click(object sender, EventArgs e)
        {
            if (albumArtBox.Image != null)
            {
                var viewer = new AlbumArtViewerForm(albumArtBox.Image);
                viewer.ShowDialog();
            }
        }

        private void saveCopyButton_Click(object sender, EventArgs e)
        {

            if (!TryGetSelectedFilePath(out string filePath))
                return;

            try
            {
                string intendedBaseName = Path.GetFileNameWithoutExtension(fileNameBox.Text.Trim());
                var original = new AudioFileCopy(filePath);
                var clone = original.Clone(intendedBaseName);

                var fileTag = TagLib.File.Create(clone.FilePath);

                UpdateTagFromInputs(fileTag, titleBox.Text, artistBox.Text, albumBox.Text);

                fileTag.Save();

                WarningPopup.Show(this, $"Successfully saved metadata to cloned file!\n({Path.GetFileName(filePath)})", Color.LimeGreen);

                LoadFilesFromFolder(Path.GetDirectoryName(filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving metadata to copy: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            if (!TryGetSelectedFilePath(out string filePath))
                return;

            string fileName = Path.GetFileName(filePath);

            var result = MessageBox.Show($"Are you sure you want to delete:\n{fileName}?",
                                         "Confirm Deletion",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    File.Delete(filePath);
                    WarningPopup.Show(this, $"Successfully deleted file!\n({fileName})", Color.LimeGreen);
                    LoadFilesFromFolder(Path.GetDirectoryName(filePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static class Formatting
        {
            public const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        }

        private bool TryGetSelectedFilePath(out string filePath)
        {
            filePath = string.Empty;

            if (folderListView.SelectedItems.Count == 0)
            {
                WarningPopup.Show(this, "Please select a file from the list.", Color.Goldenrod);
                return false;
            }

            var selected = folderListView.SelectedItems[0];

            if (selected.Tag is string path && !string.IsNullOrWhiteSpace(path))
            {
                filePath = path;
                return true;
            }

            WarningPopup.Show(this, "Selected item does not contain a valid file path.", Color.DarkRed);
            return false;
        }

        private static void UpdateTagFromInputs(TagLib.File fileTag, string title, string artist, string album)
        {
            fileTag.Tag.Title = title;
            fileTag.Tag.Performers = new[] { artist };
            fileTag.Tag.Album = album;
        }

        private static void RenameFileIfNeeded(ref string filePath, string newFilePath)
        {
            if (!string.Equals(filePath, newFilePath, StringComparison.OrdinalIgnoreCase))
            {
                File.Move(filePath, newFilePath);
                filePath = newFilePath;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            titleBox.Clear();
            artistBox.Clear();
            albumBox.Clear();
            fileNameBox.Clear();
            folderListView.SelectedItems.Clear();
            infoListView.Items.Clear();
            albumArtBox.Image = null;
        }
    }
}