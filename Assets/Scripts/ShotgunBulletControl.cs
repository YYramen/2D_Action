using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunBulletControl : MonoBehaviour
{
    [SerializeField] float m_bulletSpeed = 5f;
    GameObject m_enemy = default;
    [SerializeField] GameObject m_effectPrefab = default;

    Rigidbody2D m_rb = default;
    GameObject m_player;
    PlayerControl m_playerScript;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player");
        m_playerScript = m_player.GetComponent<PlayerControl>();

        m_rb.velocity = Vector3.forward * m_bulletSpeed;
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 向きの生成（Z成分の除去と正規化）
        Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

        GetComponent<Rigidbody2D>().velocity = shotForward * m_bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            return;
        }

        if (m_effectPrefab && collision.CompareTag("Enemy"))
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("PowerUp"))
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
