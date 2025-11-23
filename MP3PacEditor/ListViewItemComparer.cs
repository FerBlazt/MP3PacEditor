using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

public sealed class ListViewItemComparer : Comparer<ListViewItem>
{
    private const string FileSizeSuffix = " MB";
    private const string BitrateSuffix = " kbps";
    private const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";

    private readonly int columnIndex;
    private readonly bool ascending;

    public ListViewItemComparer(int columnIndex, bool ascending = true)
    {
        this.columnIndex = columnIndex;
        this.ascending = ascending;
    }

    public override int Compare(ListViewItem x, ListViewItem y)
    {
        if (x == null || y == null)
        {
            return 0;
        }

        string textX = x.SubItems[columnIndex].Text;
        string textY = y.SubItems[columnIndex].Text;

        // skaitinis palyginimas (failo dydis, bitreitas ir t.t.)
        if (TryCompareAsNumber(textX, textY, out int numericResult))
        {
            return ApplySortDirection(numericResult);
        }

        // datos / laiko palyginimas
        if (TryCompareAsDate(textX, textY, out int dateResult))
        {
            return ApplySortDirection(dateResult);
        }

        // paprastas tekstinis palyginimas
        int textResult = StringComparer.CurrentCultureIgnoreCase.Compare(textX, textY);
        return ApplySortDirection(textResult);
    }

    private int ApplySortDirection(int result) => ascending ? result : -result;

    private static bool TryCompareAsNumber(string textX, string textY, out int result)
    {
        if (TryParseNumber(textX, out double numX) && TryParseNumber(textY, out double numY))
        {
            result = numX.CompareTo(numY);
            return true;
        }

        result = 0;
        return false;
    }

    private static bool TryParseNumber(string text, out double value)
    {
        string cleaned = text
            .Replace(FileSizeSuffix, string.Empty)
            .Replace(BitrateSuffix, string.Empty);

        return double.TryParse(
            cleaned,
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out value);
    }

    private static bool TryCompareAsDate(string textX, string textY, out int result)
    {
        bool parsedX = DateTime.TryParseExact(
            textX,
            DateTimeFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime dateX);

        bool parsedY = DateTime.TryParseExact(
            textY,
            DateTimeFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime dateY);

        if (parsedX && parsedY)
        {
            result = dateX.CompareTo(dateY);
            return true;
        }

        result = 0;
        return false;
    }
}
