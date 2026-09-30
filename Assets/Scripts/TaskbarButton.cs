using UnityEngine;
using UnityEngine.UI;

public class TaskbarButton : MonoBehaviour
{
    [SerializeField] private Text label; // or TMPro.TextMeshProUGUI
    [SerializeField] private Image background;
    [SerializeField] private Color normalColor = new Color(0.83f, 0.83f, 0.83f);
    [SerializeField] private Color highlightedColor = new Color(0.6f, 0.7f, 0.9f);

    private WindowController linkedWindow;
    private TaskbarManager manager;

    public bool IsHighlighted { get; private set; }

    public void Setup(WindowController window, TaskbarManager taskbarManager)
    {
        linkedWindow = window;
        manager = taskbarManager;

        label.text = window.name;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        manager.OnTaskbarButtonClicked(linkedWindow);
    }

    public void SetHighlighted(bool highlighted)
    {
        IsHighlighted = highlighted;
        background.color = highlighted ? highlightedColor : normalColor;
    }
}