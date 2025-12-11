using UnityEngine;

public class PlanetUI : MonoBehaviour {
    [SerializeField] GameObject m_UIPanel;

    public void ClosePanel() {
        m_UIPanel.SetActive(false);
    }
}
