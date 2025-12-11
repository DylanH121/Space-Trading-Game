using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public float m_maxCapacity { get; private set; } = 20.0f;
    public float m_currentCapacity { get; private set; } = 0.0f;

    [SerializeField] Dictionary<string, float> m_currentCargo = new Dictionary<string, float>() {
        { "Food", 0.0f },
        { "Ice", 0.0f },
        { "Medical Supplies", 0.0f},
        { "Consumer Electronics", 0.0f },
        { "Raw Ores", 0.0f },
        { "Refined Metals", 0.0f },
        { "Luxury Goods", 0.0f }
    };

    public void AddCargo(string cargo, float volume) {
        if (m_currentCapacity + volume <= m_maxCapacity) {
            m_currentCargo[cargo] += volume;
        }
        else {
            Debug.Log("Cannot add cargo: Over capacity");
        }
    }

    public void RemoveCargo(string cargo, float volume) {
        if (m_currentCargo[cargo] - volume < 0.0f) {
            Debug.Log(volume + " of " + cargo + " was not available. Removed " + m_currentCargo[cargo] + " instead.");
            m_currentCargo[cargo] = 0.0f;
        }
        else {
            m_currentCargo[cargo] -= volume;
        }
    }

}
