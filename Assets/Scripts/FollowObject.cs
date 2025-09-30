using UnityEngine;

public class FollowObject : MonoBehaviour {
    [SerializeField] private GameObject m_followTarget;

    private void LateUpdate() {
        transform.position = new Vector3(m_followTarget.transform.position.x, m_followTarget.transform.position.y, transform.position.z);
    }
}
