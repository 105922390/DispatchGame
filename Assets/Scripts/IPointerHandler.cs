using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class WindowDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private RectTransform windowRoot; // the whole window, not just the title bar
    private Canvas canvas;
    private Vector2 dragOffset;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        if (windowRoot == null)
            windowRoot = transform.parent.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Bring window to front
        windowRoot.SetAsLastSibling();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            windowRoot.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPos);

        dragOffset = (Vector2)windowRoot.localPosition - localPointerPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            windowRoot.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPos);

        windowRoot.localPosition = localPointerPos + dragOffset;

        ClampWindowToDesktopBounds(); // NEW - keeps window inside desktop area
    }

    private void ClampWindowToDesktopBounds()
    {
        RectTransform parentRect = windowRoot.parent as RectTransform;
        Vector2 desktopSize = parentRect.rect.size;

        Vector2 size = windowRoot.sizeDelta;
        Vector2 pos = windowRoot.anchoredPosition;

        float halfWidth = size.x / 2f;
        float halfHeight = size.y / 2f;

        float minX = -desktopSize.x / 2f + halfWidth;
        float maxX = desktopSize.x / 2f - halfWidth;
        float minY = -desktopSize.y / 2f + halfHeight;
        float maxY = desktopSize.y / 2f - halfHeight;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        windowRoot.anchoredPosition = pos;
    }
}