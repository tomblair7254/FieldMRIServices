using FieldMRIServices.Model;

namespace FieldMRIServices.Comparers;

public class InventoryNumberComparer : IComparer<object>
{
    public int Compare(object x, object y)
    {
        if (x is not InventoryModel xData)
        {
            return 0;
        }

        if (y is not InventoryModel yData)
        {
            return 0;
        }

        // Attempt to parse the ShipName values as integers
        bool isXNumeric = int.TryParse(xData.InventoryNumber, out var xValue);
        bool isYNumeric = int.TryParse(yData.InventoryNumber, out var yValue);

        if (isXNumeric && isYNumeric)
        {
            // Compare numerically if both are numbers
            return xValue.CompareTo(yValue);
        }

        if (isXNumeric)
        {
            // If only x is numeric, it comes before y
            return -1;
        }

        if (isYNumeric)
        {
            // If only y is numeric, it comes before x
            return 1;
        }
            
        return string.Compare(xData.InventoryNumber, yData.InventoryNumber, StringComparison.Ordinal);
    }
}