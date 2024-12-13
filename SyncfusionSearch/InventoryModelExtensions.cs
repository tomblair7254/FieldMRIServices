using System.Text;
using FieldMRIServices.Model;

namespace FieldMRIServices.SyncfusionSearch;

public static class InventoryModelExtensions
{
    public static string ToSearchKeywords(this InventoryModel inventoryModel)
    {
        var keywords = new List<string>();

        var normalizationProcess = NormalizeSearch.Default;

        keywords.Add(normalizationProcess(inventoryModel.NameFirstPart));
        keywords.Add(normalizationProcess(inventoryModel.PinFirstPart) );
        keywords.Add(normalizationProcess(inventoryModel.LocationFirstPart));
        
        return string.Join(" ", keywords);
    }
}