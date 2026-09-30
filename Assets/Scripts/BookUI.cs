using UnityEngine;

public class BookUI : MonoBehaviour
{
    [SerializeField] private RectTransform entryListContainer;
    [SerializeField] private GameObject entryRowPrefab;

    private void Start()
    {
        ShowCategory("Emergency and health");
    }

    public void ShowCategory(string category)
    {
        foreach (Transform child in entryListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (POIData data in POIManager.Instance.Database.features)
        {
            if (data.game_category != category) continue;

            GameObject row = Instantiate(entryRowPrefab, entryListContainer);
            TMPro.TextMeshProUGUI label = row.GetComponent<TMPro.TextMeshProUGUI>();
            label.text = $"{data.name}\n{data.feature_type} — {data.feature_subtype}" +
                         (string.IsNullOrEmpty(data.parent_name) ? "" : $" (near {data.parent_name})");
        }
    }
}
