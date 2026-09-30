using UnityEngine;
[System.Serializable]
public class EmergencyData
{
    public string emergencyType; // E.g: Broken Leg
    public string correctService; // E.g: Ambulance
    public bool generic; // True = Applicable to every POI
    public string[] suitableCategories; // Applicable to specific categories
    public string[] callerMessages; // Dialogue of caller
    public DialogueData[] dialogue;
}
