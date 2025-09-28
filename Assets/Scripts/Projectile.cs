using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject m_owner;

    private Rigidbody2D m_rb;

    [SerializeField] private float m_projectileForce;

    void Start() {
        m_rb = GetComponent<Rigidbody2D>();

        // Add an impulse force when spawned
        m_rb.AddForce(transform.up * m_projectileForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Destroy the projectile when it collides with something other than its owner
        if (!collision.gameObject.Equals(m_owner)) {
            Destroy(this.gameObject);
        }
    }

    private void FaceForward() {
        // Rotate projectile to match its direction
        if (m_rb.linearVelocity.sqrMagnitude > 0f) {
            // Calculate direction in degrees based on velocity
            float angle = Mathf.Atan2(m_rb.linearVelocity.y, m_rb.linearVelocity.x) * Mathf.Rad2Deg;

            // Rotate to forward direction
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    private void FixedUpdate() {
        FaceForward();
    }
}
