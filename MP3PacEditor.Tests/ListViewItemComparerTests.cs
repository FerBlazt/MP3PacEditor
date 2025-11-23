using System.Windows.Forms;
using NUnit.Framework;

namespace MP3PacEditor.Tests
{
    public class ListViewItemComparerTests
    {
        [Test]
        public void Compare_UsesNumericComparisonWhenPossible()
        {
            var comparer = new ListViewItemComparer(1, true);
            var slower = new ListViewItem(new[] { "SongA", "128 kbps" });
            var faster = new ListViewItem(new[] { "SongB", "256 kbps" });

            Assert.That(comparer.Compare(slower, faster), Is.LessThan(0));
            Assert.That(comparer.Compare(faster, slower), Is.GreaterThan(0));
        }

        [Test]
        public void Compare_HandlesDateValues()
        {
            var comparer = new ListViewItemComparer(0, true);
            var older = new ListViewItem(new[] { "01/01/2024 10:00:00" });
            var newer = new ListViewItem(new[] { "01/02/2024 10:00:00" });

            Assert.That(comparer.Compare(older, newer), Is.LessThan(0));
        }

        [Test]
        public void Compare_UsesDescendingOrderForText()
        {
            var comparer = new ListViewItemComparer(0, false);
            var alpha = new ListViewItem(new[] { "Alpha" });
            var beta = new ListViewItem(new[] { "Beta" });

            Assert.That(comparer.Compare(alpha, beta), Is.GreaterThan(0));
        }
    }
}