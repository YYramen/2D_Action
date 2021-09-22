using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBulletControl : MonoBehaviour
{
    //[SerializeField] float m_bulletPower = 0.5f;
    [SerializeField] float m_bulletSpeed = 5f;
    GameObject m_player = default;
    [SerializeField] GameObject m_effectPrefab = default;

    Rigidbody2D m_rb = default;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");
        //m_effectPrefab = GameObject.Find("OctopusBulletEffect");

        if (m_player)
        {
            Vector3 dir = m_player.transform.position - transform.position;
            
            m_rb.velocity = dir.normalized * m_bulletSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_effectPrefab && collision.CompareTag("Player"))
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }
}
