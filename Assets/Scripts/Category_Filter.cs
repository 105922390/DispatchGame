using UnityEngine;
using UnityEngine.UI;

public class Category_Filter : MonoBehaviour
{

    [SerializeField] private RectTransform graphContainer;

    
    [SerializeField] Toggle Emergency_Health;
    [SerializeField] Toggle Education_Community;
    [SerializeField] Toggle Parks_Recreation;
    [SerializeField] Toggle Infrastructure_Services;
    [SerializeField] Toggle Commercial_Residential;
    [SerializeField] Toggle Landmarks_Navigation;
    
    /*
    🔴 Emergency and health
    🔵 Education and community
    🟢 Parks and recreation
    🟣 Infrastructure and services
    🟠 Commercial and residential
    ⚫ Landmarks and navigation
    */
    
    Color32 red = new Color32(204, 66, 66, 255);
    Color32 blue = new Color32(50, 120, 199, 255);
    Color32 green = new Color32(71, 141, 75, 255);
    Color32 purple = new Color32(124, 55, 166, 255);
    Color32 orange = new Color32(240, 126, 31, 255);
    Color32 grey = new Color32(92, 110, 119, 255);


    private void Awake()
    {
        Emergency_Health.isOn = true;
        Education_Community.isOn = true;
        Parks_Recreation.isOn = true;
        Infrastructure_Services.isOn = true;
        Commercial_Residential.isOn = true;
        Landmarks_Navigation.isOn = true;

        FilterCategories();
    }

    void Update()
    {
        FilterCategories();
    }

    public void FilterCategories()
    {
        foreach (Transform child in graphContainer)
        {
            Image image = child.GetComponent<Image>();

            if (child.name == "background") {
                continue;
            }

            if (image != null)
            {
                bool show = false;

                if (Emergency_Health.isOn && image.color == red)
                    show = true;

                if (Education_Community.isOn && image.color == blue)
                    show = true;

                if (Parks_Recreation.isOn && image.color == green)
                    show = true;

                if (Infrastructure_Services.isOn && image.color == purple)
                    show = true;

                if (Commercial_Residential.isOn && image.color == orange)
                    show = true;

                if (Landmarks_Navigation.isOn && image.color == grey)
                    show = true;

                child.gameObject.SetActive(show);
            }
        }
    }

    public void ShowAll()
    {
        foreach (Transform child in graphContainer)
        {
            child.gameObject.SetActive(true);
        }
    }
}
