using UnityEngine;

public class DesktopIconManager : MonoBehaviour
{
    public static DesktopIconManager Instance { get; private set; }

    private DesktopIcon currentlySelected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SelectIcon(DesktopIcon icon)
    {
        if (currentlySelected != null && currentlySelected != icon)
            currentlySelected.SetHighlighted(false);

        currentlySelected = icon;
        icon.SetHighlighted(true);
    }
}