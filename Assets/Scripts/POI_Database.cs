using System;

[Serializable]
public class POIDatabase
{
    public string dataset;
    public string source_layer;
    public int record_count;

    public POIData[] features;
}