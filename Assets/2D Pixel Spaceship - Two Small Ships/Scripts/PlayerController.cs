using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D m_rb;

    [SerializeField] private float m_thrusterForce;
    [SerializeField] private float m_rotationForce;
    [SerializeField] private float m_maxMoveSpeed;
    [SerializeField] private float m_maxTurnSpeed;
    [SerializeField] private float m_movementBrakeForceMultiplier;
    [SerializeField] private float m_rotationBrakeForceMultiplier;
    
    [SerializeField] private GameObject m_projectilePrefab;

    void Start() {
        Application.targetFrameRate = 60;
        m_rb = GetComponent<Rigidbody2D>();
    }

    void ClampMaxSpeed() {
        if (m_rb.linearVelocity.magnitude > m_maxMoveSpeed) {
            m_rb.linearVelocity = m_rb.linearVelocity.normalized * m_maxMoveSpeed;
        }

        m_rb.angularVelocity = Mathf.Clamp(m_rb.angularVelocity, -m_maxTurnSpeed, m_maxTurnSpeed);
    }

    void Update() {
        /* Physics-Based Movement */

        // Increase forward velocity
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space)) {
            m_rb.AddForce(transform.up * m_thrusterForce);
        }

        // Decrease forward velocity
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Space)) {
            m_rb.AddForce(transform.up * -m_thrusterForce);
        }

        // Rotate right (clockwise)
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space)) {
            m_rb.AddTorque(-m_rotationForce);
        }

        // Rotate left (counter-clockwise)
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Space)) {
            m_rb.AddTorque(m_rotationForce);
        }

        // Brake
        if (Input.GetKey(KeyCode.Space)) {
            m_rb.linearDamping = m_thrusterForce * m_movementBrakeForceMultiplier;
            m_rb.angularDamping = m_rotationForce * m_rotationBrakeForceMultiplier;
        }
        else {
            m_rb.linearDamping = 0;
            m_rb.angularDamping = 0;
        }

        // Fire projectile on left-click
        if (Input.GetMouseButtonDown(0)) {
            GameObject projectile = Instantiate(m_projectilePrefab, transform.position, transform.rotation);

            // Assign projectile owner as player
            projectile.GetComponent<Projectile>().m_owner = gameObject;

            projectile.layer = 8; // 8 = Player projectile
        }

        ClampMaxSpeed();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 9) { // 9 = NPC projectile
            Destroy(this.gameObject);
        }
    }

}
