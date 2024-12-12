using System.Text;

namespace FieldMRIServices.SyncfusionSearch;

public static class NormalizeSearch
{
    public static NormalizeSearchProcess Default => searchString =>
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return string.Empty;
        }
        
        searchString = searchString.ToLowerInvariant();
        
        var searchStringBuilder = new StringBuilder();

        var wasSpecialCharacter = false;
        foreach (var character in searchString)
        {
            if (char.IsWhiteSpace(character) && wasSpecialCharacter)
            {
                wasSpecialCharacter = false;
                continue;
            }
            
            if (char.IsLetterOrDigit(character) || char.IsWhiteSpace(character))
            {
                searchStringBuilder.Append(character);
                wasSpecialCharacter = false;
                continue;
            }

            if (!wasSpecialCharacter)
            {
                searchStringBuilder.Append(' ');
                wasSpecialCharacter = true;
            }
        }
        
        return searchStringBuilder.ToString().Trim();
    };
}