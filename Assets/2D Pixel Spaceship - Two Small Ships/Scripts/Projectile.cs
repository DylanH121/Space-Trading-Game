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
}
