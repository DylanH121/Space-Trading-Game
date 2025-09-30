using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D m_rb;

    [SerializeField] private float m_thrusterForce;
    [SerializeField] private float m_rotationForce;
    [SerializeField] private float m_maxMoveSpeed;
    [SerializeField] private float m_maxTurnSpeed;
    [SerializeField] private float m_movementBrakeForceMultiplier;
    [SerializeField] private float m_rotationBrakeForceMultiplier;
    
    [SerializeField] private GameObject m_projectilePrefab;

    private float m_moveInput;
    private float m_turnInput;
    private bool m_isBraking = false;

    void Start() {
        Application.targetFrameRate = 60;
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void ClampMaxSpeed() {
        if (m_rb.linearVelocity.magnitude > m_maxMoveSpeed) {
            m_rb.linearVelocity = m_rb.linearVelocity.normalized * m_maxMoveSpeed;
        }

        m_rb.angularVelocity = Mathf.Clamp(m_rb.angularVelocity, -m_maxTurnSpeed, m_maxTurnSpeed);
    }

    public void OnFireWeapon(InputAction.CallbackContext context) {
        GameObject projectile = Instantiate(m_projectilePrefab, transform.position, transform.rotation);

        // Assign projectile owner as player
        projectile.GetComponent<Projectile>().m_owner = gameObject;

        projectile.layer = 8;
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed && !m_isBraking) {
            m_moveInput = context.ReadValue<float>();
        }
        else if (context.canceled) {
            m_moveInput = 0.0f;
        }
    }

    public void OnTurn(InputAction.CallbackContext context) {
        if (context.performed && !m_isBraking) {
            m_turnInput = context.ReadValue<float>();
        }
        else if (context.canceled) {
            m_turnInput = 0.0f;
        }
    }

    public void OnBrake(InputAction.CallbackContext context) {
        if (context.performed) {
            m_isBraking = true;
        }
        else if (context.canceled) {
            m_isBraking = false;
        }
    }

    void FixedUpdate() {

        m_rb.AddForce(m_moveInput * m_thrusterForce * transform.up);
        m_rb.AddTorque(m_turnInput * m_rotationForce);

        if (m_isBraking) {
            m_rb.linearDamping = m_thrusterForce * m_movementBrakeForceMultiplier;
            m_rb.angularDamping = m_rotationForce * m_rotationBrakeForceMultiplier;
        }
        else {
            m_rb.linearDamping = 0f;
            m_rb.angularDamping = 0f;
        }

        ClampMaxSpeed();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 9) { // 9 = enemy projectile
            Debug.Log("Hit by projectile");
        }
    }

}
