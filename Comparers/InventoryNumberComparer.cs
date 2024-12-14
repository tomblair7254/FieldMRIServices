using FieldMRIServices.Model.Abstractions;

namespace FieldMRIServices.Comparers;

public class InventoryNumberComparer : IComparer<object>
{
    public int Compare(object xObj, object yObj)
    {
        var x = xObj as ISortableModel;
        var y = yObj as ISortableModel;
        
        if (x is null && y is null)
        {
            return 0;
        }

        if (x is null)
        {
            return 1;
        }

        if (y is null)
        {
            return -1;
        }

        if (int.TryParse(x.InventoryNumber, out var xValue) &&
            int.TryParse(y.InventoryNumber, out var yValue))
        {
            return xValue.CompareTo(yValue);
        }
        
        return string.Compare(x.InventoryNumber, y.InventoryNumber, StringComparison.Ordinal);
    }
}
