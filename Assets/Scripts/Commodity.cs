using UnityEngine;

[CreateAssetMenu(fileName = "Commodity", menuName = "Scriptable Objects/Commodity")]
public class Commodity : ScriptableObject {
    public string commodityName;
    public float baseValue;
    public float mass;
    public CommodityType type;
}

public enum CommodityType {
    Food,
    Minerals,
    Materials,
    Technology,
    Medicine,
    Cultural_Goods,
    Luxury_Goods,
    Contraband
}
