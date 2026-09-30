using UnityEngine;

public class POIManager : MonoBehaviour
{
    public static POIManager Instance { get; private set; }
    [SerializeField] private TextAsset jsonFile;
    public POIDatabase Database { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Database = JsonUtility.FromJson<POIDatabase>(jsonFile.text);
        Debug.Log("Loaded " + Database.features.Length + " POIs");
    }
}