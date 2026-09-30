using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private RectTransform windowParent; // the Canvas or a "WindowLayer" panel windows spawn into
    [SerializeField] private TaskbarManager taskbar;
    [SerializeField] private RectTransform taskbarRect;

    private List<WindowController> openWindows = new List<WindowController>();
    private WindowController activeWindow;

    private int spawnCount = 0;

    private void Awake()
    {
        // Simple singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public WindowController SpawnWindow(GameObject windowPrefab, string titleText)
    {
        GameObject newWindowObj = Instantiate(windowPrefab, windowParent);
        RectTransform rt = newWindowObj.GetComponent<RectTransform>();

        // Cascade offset so new windows don't spawn exactly on top of each other
        Vector2 offset = new Vector2(30, -30) * (spawnCount % 8);
        rt.anchoredPosition = offset;
        spawnCount++;

        WindowController controller = newWindowObj.GetComponent<WindowController>();
        controller.SetTitle(titleText);

        return controller; // RegisterWindow gets called from WindowController's own Start()
    }

    public void RegisterWindow(WindowController window)
    {
        if (!openWindows.Contains(window))
            openWindows.Add(window);

        taskbar.AddTaskbarButton(window);
        SetActiveWindow(window);
    }

    public void UnregisterWindow(WindowController window)
    {
        openWindows.Remove(window);
        taskbar.RemoveTaskbarButton(window);

        if (activeWindow == window)
            activeWindow = null;
    }
    
    public float GetTaskbarHeight()
    {
        return taskbarRect.rect.height;
    }

    public void SetActiveWindow(WindowController window)
    {
        activeWindow = window;
        taskbar.SetActiveTaskbarButton(window);
    }

    public WindowController GetActiveWindow()
    {
        return activeWindow;
    }

    public void OnWindowMinimized(WindowController window)
    {
        if (activeWindow == window)
            activeWindow = null;

        taskbar.SetTaskbarButtonMinimized(window);
    }

    public List<WindowController> GetOpenWindows()
    {
        return openWindows;
    }
}