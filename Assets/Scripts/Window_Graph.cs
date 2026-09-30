using System;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;


    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }

    private void Start()
    {
        POIData[] features = POIManager.Instance.Database.features;
        ShowGraph(features);
    }

    private void CreateCircle(POIData data)
    {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(graphContainer, false);
        circle.GetComponent<Image>().sprite = circleSprite;
        circle.name = data.name;

        RectTransform rectTransform = circle.GetComponent<RectTransform>();
        float padding = 0.03f; // space between the edges of the map and the dots
        float mapX = (padding + (data.game_x / 100f) * (1f - padding * 2f)) * graphContainer.rect.width;
        float mapY = (padding + (data.game_y / 100f) * (1f - padding * 2f)) * graphContainer.rect.height;
        rectTransform.anchoredPosition = new Vector2(mapX, mapY);
        rectTransform.sizeDelta = new Vector2(15, 15); // POI dot size
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;

        circle.GetComponent<Image>().color = CategoryColors.GetColor(data.game_category);

        circle.AddComponent<POIMarker>().Initialize(data);
    }

    private void ShowGraph(POIData[] features)
    {
        foreach (POIData data in features)
        {
            CreateCircle(data);
        }
    }
}