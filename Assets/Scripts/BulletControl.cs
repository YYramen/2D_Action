using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    //[SerializeField] float m_bulletPower = 1f;
    [SerializeField] float m_bulletSpeed = 15;
    [SerializeField] GameObject m_effectPrefab = default;

    Rigidbody2D rigidBody;
    GameObject m_player;
    PlayerControl m_playerScript;
    //[SerializeField] float m_near = 0.1f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // クリックした座標の取得（スクリーン座標からワールド座標に変換）
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 向きの生成（Z成分の除去と正規化）
        Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

        GetComponent<Rigidbody2D>().velocity = shotForward * m_bulletSpeed;

        m_player = GameObject.Find("Player");

        m_playerScript = m_player.GetComponent<PlayerControl>();
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemyタグをもつゲームオブジェクト当たったら消滅
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
        }
    }
    void Update()
    {
        // クリックした座標の取得（スクリーン座標からワールド座標に変換）
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 向きの生成（Z成分の除去と正規化）
        Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

        GetComponent<Rigidbody2D>().velocity = shotForward * m_bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_playerScript.m_bulletCount = 0;
        if (m_effectPrefab)
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
        }
        if (collision.CompareTag("Player"))
        {
            
            Debug.Log("プレイヤーに当たった");
        }
        Destroy(gameObject);
    }
}