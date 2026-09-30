using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private GameObject windowPrefab; // the app's window prefab to spawn
    [SerializeField] private string appTitle = "Untitled";
    [SerializeField] private Image iconBackground; // for highlight visual
    [SerializeField] private Text label; // or TMPro.TextMeshProUGUI

    [Header("Highlight Colors")]
    [SerializeField] private Color normalColor = new Color(0, 0, 0, 0); // transparent
    [SerializeField] private Color highlightedColor = new Color(0.2f, 0.4f, 0.8f, 0.5f); // translucent blue

    [Header("Double-Click Settings")]
    [SerializeField] private float doubleClickThreshold = 0.3f; // seconds between clicks

    private float lastClickTime = -1f;

    public void OnPointerClick(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            // DOUBLE CLICK detected
            OpenWindow();
            lastClickTime = -1f; // reset so a third rapid click doesn't chain into another double-click
        }
        else
        {
            // SINGLE CLICK
            SelectIcon();
            lastClickTime = Time.time;
        }
    }

    private void SelectIcon()
    {
        DesktopIconManager.Instance.SelectIcon(this);
    }

    public void SetHighlighted(bool highlighted)
    {
        if (iconBackground != null)
            iconBackground.color = highlighted ? highlightedColor : normalColor;
    }

    private void OpenWindow()
    {
        WindowManager.Instance.SpawnWindow(windowPrefab, appTitle);
    }
}