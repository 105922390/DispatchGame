using UnityEngine;

public static class CategoryColors
{
    public static Color32 GetColor(string category)
    {
        switch (category)
        {
            case "Emergency and health": return new Color32(204, 66, 66, 255);
            case "Education and community": return new Color32(50, 120, 199, 255);
            case "Parks and recreation": return new Color32(71, 141, 75, 255);
            case "Infrastructure and services": return new Color32(124, 55, 166, 255);
            case "Commercial and residential": return new Color32(240, 126, 31, 255);
            case "Landmarks and navigation": return new Color32(92, 110, 119, 255);
            default: return Color.white;
        }
    }
}
