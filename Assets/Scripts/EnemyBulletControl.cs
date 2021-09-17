using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControl : MonoBehaviour
{
    [SerializeField] float m_bulletSpeed = 5f;
    GameObject m_player = default;
    [SerializeField] GameObject m_effectPrefab = default;

    float m_angleOffset = 0f;
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
            ////角度を取得
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //Vector3 euler = new Vector3(0, 0, angle + m_angleOffset);
            //transform.rotation = Quaternion.Euler(euler);
            //m_rb.velocity = dir.normalized * m_bulletSpeed;


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
        
    }
}
