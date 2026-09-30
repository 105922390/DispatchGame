using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapWindowLoader : MonoBehaviour
{
    [SerializeField] private RawImage mapDisplay; // the RawImage inside ContentArea
    [SerializeField] private RenderTexture mapRenderTexture;
    [SerializeField] private string mapSceneName = "MapScene";

    private Camera mapCamera;

    private void Start()
    {
        StartCoroutine(LoadMapScene());
    }

    private IEnumerator LoadMapScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Additive);
        yield return op;

        Scene mapScene = SceneManager.GetSceneByName(mapSceneName);
        foreach (GameObject rootObj in mapScene.GetRootGameObjects())
        {
            Camera cam = rootObj.GetComponentInChildren<Camera>();
            if (cam != null)
            {
                mapCamera = cam;
                mapCamera.targetTexture = mapRenderTexture;
                break;
            }
        }

        mapDisplay.texture = mapRenderTexture;
    }

    public Camera GetMapCamera() => mapCamera;
}