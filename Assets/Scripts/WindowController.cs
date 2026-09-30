using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class WindowController : MonoBehaviour, IPointerDownHandler
{
    [Header("References")]
    [SerializeField] private RectTransform windowRect;
    [SerializeField] private Button BarRect;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button minimizeButton;
    [SerializeField] private Button maximizeButton;

    [Header("State")]
    public bool isMaximized = false;
    public bool isMinimized = false;

    // Stored values so we can restore after maximize/minimize
    private Vector2 restoredSize;
    private Vector2 restoredPosition;

    private void Awake()
    {
        if (windowRect == null)
            windowRect = GetComponent<RectTransform>();

        closeButton.onClick.AddListener(CloseWindow);
        minimizeButton.onClick.AddListener(MinimizeWindow);
        maximizeButton.onClick.AddListener(ToggleMaximize);
    }

    private void Start()
    {
        // Register with the manager so the taskbar knows about this window
        WindowManager.Instance.RegisterWindow(this);
        Focus();
    }

    // Clicking anywhere on the window (not just title bar) brings it to front
    public void OnPointerDown(PointerEventData eventData)
    {
        Focus();
    }
    
    [SerializeField] private TMPro.TextMeshProUGUI titleText;

    public void SetTitle(string title)
    {
        if (titleText != null)
            titleText.text = title;
    }

    public void Focus()
    {
        windowRect.SetAsLastSibling();
        WindowManager.Instance.SetActiveWindow(this);
    }
    


    public void CloseWindow()
    {
        WindowManager.Instance.UnregisterWindow(this);
        Destroy(gameObject);
    }

    public void MinimizeWindow()
    {
        isMinimized = true;
        gameObject.SetActive(false); // hides it, taskbar button stays via WindowManager
        WindowManager.Instance.OnWindowMinimized(this);
    }

    public void RestoreFromTaskbar()
    {
        isMinimized = false;
        gameObject.SetActive(true);
        Focus();
    }

    public void ToggleMaximize()
    {
        if (!isMaximized)
        {
            restoredSize = windowRect.sizeDelta;
            restoredPosition = windowRect.anchoredPosition;

            float taskbarHeight = WindowManager.Instance.GetTaskbarHeight();

            windowRect.anchorMin = Vector2.zero;
            windowRect.anchorMax = Vector2.one;
            windowRect.offsetMin = new Vector2(0, taskbarHeight); // dynamic now
            windowRect.offsetMax = Vector2.zero;

            isMaximized = true;
        }
        else
        {
            windowRect.anchorMin = new Vector2(0.5f, 0.5f);
            windowRect.anchorMax = new Vector2(0.5f, 0.5f);
            windowRect.sizeDelta = restoredSize;
            windowRect.anchoredPosition = restoredPosition;

            isMaximized = false;
        }
    }
}