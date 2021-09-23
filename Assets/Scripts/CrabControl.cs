using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_bulletPrefab = default;
    [SerializeField] Transform m_muzzle = null;
    [SerializeField] GameObject m_death = default;
    [SerializeField] GameObject m_powerUp = default;

    [SerializeField] float m_moveSpeed = -5f;
    float m_enemyHealth = 2f;

    bool m_isGround;
    float m_targetTime = 1.0f;
    float m_currentTime = 0;

    Rigidbody2D m_rb = default;

    void Start()
    {
        if(m_enemyHealth != 2)
        {
            m_enemyHealth = 2;
        }
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime;
        if (m_isGround == true)
        {
            m_rb.velocity = new Vector3(-1 * m_moveSpeed, 0);

            if (m_targetTime < m_currentTime)
            {
                Instantiate(m_bulletPrefab, m_muzzle.position, m_bulletPrefab.transform.rotation);
                m_currentTime = 0;
            }
        }

        if (m_enemyHealth < 0 && m_death)
        {
            Instantiate(m_death, this.transform.position, Quaternion.identity);
            Instantiate(m_powerUp, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Bullet")
        {
            m_enemyHealth -= 1;
        }
    }
}
