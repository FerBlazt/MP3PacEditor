using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MP3PacEditor
{
    public static class WarningPopup
    {
        private static readonly List<Form> ActivePopups = new List<Form>();
        private static readonly int VerticalSpacing = 5;

        public static void Show(Form parent, string message, Color? backgroundColor = null, int durationMs = 5000)
        {
            int secondsRemaining = durationMs / 1000;
            Color bgColor = backgroundColor ?? Color.Black;

            Form popupForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = bgColor,
                Opacity = 1.0,
                Size = new Size(300, 40)
            };

            Label label = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Text = $"{message} ({secondsRemaining}s)"
            };

            popupForm.Controls.Add(label);

            int topOffset = 10 + ActivePopups.Sum(p => p.Height + VerticalSpacing);
            Point location = parent.PointToScreen(new Point(
                parent.ClientSize.Width - popupForm.Width - 10,
                topOffset
            ));

            popupForm.Location = location;
            popupForm.Show();

            ActivePopups.Add(popupForm);

            System.Windows.Forms.Timer countdown = new System.Windows.Forms.Timer { Interval = 1000 };
            System.Windows.Forms.Timer fadeOut = new System.Windows.Forms.Timer { Interval = 50 };

            countdown.Tick += (s, e) =>
            {
                secondsRemaining--;
                if (secondsRemaining > 0)
                {
                    label.Text = $"{message} ({secondsRemaining}s)";
                }
                else
                {
                    countdown.Stop();
                    fadeOut.Start();
                }
            };

            fadeOut.Tick += (s, e) =>
            {
                popupForm.Opacity -= 0.05;
                if (popupForm.Opacity <= 0)
                {
                    fadeOut.Stop();
                    popupForm.Close();
                    ActivePopups.Remove(popupForm);
                    RepositionPopups(parent);
                }
            };

            countdown.Start();
        }

        private static void RepositionPopups(Form parent)
        {
            int currentY = 10;
            foreach (Form popup in ActivePopups)
            {
                Point location = parent.PointToScreen(new Point(
                    parent.ClientSize.Width - popup.Width - 10,
                    currentY
                ));
                popup.Location = location;
                currentY += popup.Height + VerticalSpacing;
            }
        }
    }
}
