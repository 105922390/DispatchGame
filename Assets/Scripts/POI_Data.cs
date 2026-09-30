using System;

[Serializable]
public class POIData
{
    public int id;
    public string name;
    public string game_category;
    public string feature_type;
    public string feature_subtype;

    public float longitude;
    public float latitude;

    public float game_x;
    public float game_y;

    public string parent_name;
    public int source_feature_id;
    public string source_verified_date;
    public string scenario_data_issue;
    public string scenario_notes;
}