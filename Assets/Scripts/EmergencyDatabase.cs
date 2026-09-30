using System.Collections.Generic;

public static class EmergencyDatabase
{
    public static List<EmergencyData> Emergencies = new List<EmergencyData>
    {
        new EmergencyData
        {
            emergencyType = "Heart attack",
            correctService = "Ambulance",
            generic = true,

            callerMessages = new string[]
            {
                 "Someone is having a heart attack.",
                "I think someone is having a heart attack.",
                "There's someone here having a heart attack."
            }
        },

        new EmergencyData
        {
            emergencyType = "Fire",
            correctService = "Fire",
            generic = true
        },

        new EmergencyData
        {
            emergencyType = "Broken leg",
            correctService = "Ambulance",
            generic = false,
            suitableCategories = new string[]
            {
                "Parks and recreation"
            },
            callerMessages = new string[]
            {
                "Someone has broken their leg.",
                "I think someone has broken their leg.",
                "There's someone here with a broken leg."
            }
        },

        new EmergencyData
        {
            emergencyType = "Robbery",
            correctService = "Police",
            generic = false,
            suitableCategories = new string[]
            {
                "Commercial and residential"
            }
        }
    };
}