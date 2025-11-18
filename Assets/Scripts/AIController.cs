using UnityEngine;

public class AIController : MonoBehaviour {
    // Movement
    [SerializeField] private float m_thrusterForce;
    [SerializeField] private float m_rotationForce;
    [SerializeField] private float m_maxMoveSpeed;
    [SerializeField] private float m_maxTurnSpeed;
    [SerializeField] private float m_brakeDragValue;
    [SerializeField] private float m_stopRadius;

    // Weapon projectile
    [SerializeField] private GameObject m_projectilePrefab;

    private Rigidbody2D m_rb;

    private bool m_hostileToPlayer = false;

    private Transform m_moveTarget = null;
    private Vector2 m_moveDirectionVector;
    private bool m_isAlignedWithMoveTarget = false;

    void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        ChooseRandomDirection();
    }

    private void ClampMaxSpeed() {
        if (m_rb.linearVelocity.magnitude > m_maxMoveSpeed) {
            m_rb.linearVelocity = m_rb.linearVelocity.normalized * m_maxMoveSpeed;
        }

        m_rb.angularVelocity = Mathf.Clamp(m_rb.angularVelocity, -m_maxTurnSpeed, m_maxTurnSpeed);
    }

    private void ChooseRandomDirection() {
        float directionDegrees = Random.Range(0.0f, 360.0f);
        float directionRadians = directionDegrees * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(directionRadians), Mathf.Sin(directionRadians));

        m_moveDirectionVector = direction;
        Debug.Log(m_moveDirectionVector);
    }

    // Get angle difference in degrees between current forward direction and target vector
    private float GetAngleDifference(Vector2 vectorToTarget) {
        return Mathf.DeltaAngle(transform.eulerAngles.z, Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f);
    }

    private void RotateToTargetVector(Vector2 vectorToTarget) {
        float angleDifference = GetAngleDifference(vectorToTarget);

        float turnForce = Mathf.Clamp(angleDifference * m_rotationForce, -m_maxTurnSpeed, m_maxTurnSpeed);
        m_rb.AddTorque(turnForce);

        if (angleDifference < 0.01) {
            m_isAlignedWithMoveTarget = true;
        }
    }

    private void MoveAlongVector(Vector2 moveVector) {
        RotateToTargetVector(moveVector);
        if (m_isAlignedWithMoveTarget) {
            m_rb.AddForce(transform.up * m_thrusterForce);
        }
    }

    // Move away from centre of scene and jump/despawn
    private void MoveToExit() {
        MoveAlongVector(m_moveDirectionVector);

        if (Vector2.Distance(transform.position, Vector2.zero) >= 25.0f) {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate() {
        MoveToExit();
        ClampMaxSpeed();
    }
}
