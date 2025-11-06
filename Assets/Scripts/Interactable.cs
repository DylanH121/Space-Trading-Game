using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField] private TMP_Text m_UIPrompt;
    [SerializeField] private GameObject m_UIPanel;

    private GameObject m_playerObject;
    private PlayerController m_playerController;

    void Start() {
        m_playerObject = GameObject.FindGameObjectWithTag("Player");
        m_playerController = m_playerObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Player entered trigger");
            m_UIPrompt.gameObject.SetActive(true);
            m_playerController.m_interactableObject = this;
            m_playerController.m_inInteractArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Player exited trigger");
            m_UIPrompt.gameObject.SetActive(false);
            m_playerController.m_interactableObject = null;
            m_playerController.m_inInteractArea = false;
        }
    }

    public void OnInteract() {
        m_UIPanel.SetActive(true);
    }
    
}
