using System.Collections.Generic;
using UnityEngine;

public class CallManager : MonoBehaviour
{
    private CallData callData;
    private POIData selectedPOI; 

    private void CreateCall()
    {
        // Randomly Pick a POI
        POIData[] features = POIManager.Instance.Database.features;

        int randomIndex = Random.Range(0, features.Length);
        selectedPOI = features[randomIndex];

        Debug.Log("Selected POI: " + selectedPOI.name + ", ID: " + selectedPOI.id);

        // Pick an Emergency

        List<EmergencyData> possibleEmergencies = new List<EmergencyData>();
        foreach (EmergencyData emergency in EmergencyDatabase.Emergencies)
        {
            if (emergency.generic)
            {
                possibleEmergencies.Add(emergency);
                continue;
            }

            foreach (string category in emergency.suitableCategories)
            {
                if (category == selectedPOI.game_category)
                {
                    possibleEmergencies.Add(emergency);
                    break;
                }
            }
        }

        int randomEmergency = Random.Range(0, possibleEmergencies.Count);
        EmergencyData selectedEmergency = possibleEmergencies[randomEmergency];

        // Generate caller message & Correct service
        int randomMessage = Random.Range(0, selectedEmergency.callerMessages.Length);
        string callerMessage = selectedEmergency.callerMessages[randomMessage];

        // Store the currrent call
        callData = new CallData();
        callData.correctPOI = selectedPOI;
        callData.emergency = selectedEmergency;

        Debug.Log("Emergency: " + selectedEmergency.emergencyType);
        Debug.Log("POI: " + selectedPOI.name +
                    " | Category: " + selectedPOI.game_category +
                    " | Emergency: " + selectedEmergency.emergencyType +
                    " | Service: " + selectedEmergency.correctService +
                    " | Caller: " + callerMessage
                 );
    }

    private void Update()
    {

    }

    private void Awake()
    {
        CreateCall();
    }
}
