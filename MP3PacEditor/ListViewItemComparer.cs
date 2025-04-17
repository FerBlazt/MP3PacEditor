using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

// Data structures from System.Collections or System.Collections.Generic are used (1 point)
// Use sealed or partial class (0.5 points)
public sealed class ListViewItemComparer : Comparer<ListViewItem>
{

    private int columnIndex;
    private bool ascending;

    public ListViewItemComparer(int columnIndex, bool ascending = true)
    {
        this.columnIndex = columnIndex;
        this.ascending = ascending;
    }

    public override int Compare(ListViewItem x, ListViewItem y)
    {
        if (x == null || y == null) return 0;

        string textX = x.SubItems[columnIndex].Text;
        string textY = y.SubItems[columnIndex].Text;

        int result;
        if (double.TryParse(textX.Replace(" MB", "").Replace(" kbps", ""), out double numX) &&
            double.TryParse(textY.Replace(" MB", "").Replace(" kbps", ""), out double numY))
        {
            result = numX.CompareTo(numY);
        }
        else if(IsValidDate(textX) && IsValidDate(textY))
        {
            DateTime dateX = DateTime.ParseExact(textX, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateY = DateTime.ParseExact(textY, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            result = dateX.CompareTo(dateY);
        }
        else
        {
            result = StringComparer.CurrentCultureIgnoreCase.Compare(textX, textY);
        }

        return ascending ? result : -result;
    }

    private bool IsValidDate(string dateStr)
    {
        return DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

}
