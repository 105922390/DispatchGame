using UnityEngine;
using UnityEngine.EventSystems;

public class POIMarker : MonoBehaviour, IPointerClickHandler
{
    public POIData Data { get; private set; }

    public void Initialize(POIData data)
    {
        Data = data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked: {Data.name} ({Data.game_category})");
        // used to open book to this entry
    }
}
