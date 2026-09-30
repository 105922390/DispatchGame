using System.Collections.Generic;
using UnityEngine;

public class TaskbarManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform taskbarButtonContainer; // has a Horizontal Layout Group
    [SerializeField] private GameObject taskbarButtonPrefab;

    // Maps each window to its corresponding taskbar button instance
    private Dictionary<WindowController, TaskbarButton> buttonMap = new Dictionary<WindowController, TaskbarButton>();

    public void AddTaskbarButton(WindowController window)
    {
        if (buttonMap.ContainsKey(window))
            return; // already has one, don't duplicate

        GameObject buttonObj = Instantiate(taskbarButtonPrefab, taskbarButtonContainer);
        TaskbarButton taskbarButton = buttonObj.GetComponent<TaskbarButton>();
        taskbarButton.Setup(window, this);

        buttonMap.Add(window, taskbarButton);
    }

    public void RemoveTaskbarButton(WindowController window)
    {
        if (buttonMap.TryGetValue(window, out TaskbarButton button))
        {
            Destroy(button.gameObject);
            buttonMap.Remove(window);
        }
    }

    public void SetActiveTaskbarButton(WindowController activeWindow)
    {
        foreach (var pair in buttonMap)
        {
            bool isActive = pair.Key == activeWindow;
            pair.Value.SetHighlighted(isActive);
        }
    }

    public void SetTaskbarButtonMinimized(WindowController window)
    {
        if (buttonMap.TryGetValue(window, out TaskbarButton button))
        {
            button.SetHighlighted(false);
        }
    }

    public void OnTaskbarButtonClicked(WindowController window)
    {
        if (window.isMinimized)
        {
            window.RestoreFromTaskbar();
        }
        else if (WindowManager.Instance.GetActiveWindow() == window)
        {
            window.MinimizeWindow();
        }
        else
        {
            window.Focus();
        }
    }
}