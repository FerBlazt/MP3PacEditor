using System.Drawing;
using System.Windows.Forms;

public partial class AlbumArtViewerForm : Form
{
    public AlbumArtViewerForm(Image image)
    {
        this.Text = "Album Art Viewer";

        PictureBox pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            Image = image,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Black
        };

        this.Controls.Add(pictureBox);
        this.ClientSize = new Size(600, 600);
    }

    private void InitializeComponent()
    {

    }
}