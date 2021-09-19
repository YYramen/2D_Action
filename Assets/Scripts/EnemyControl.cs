using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] GameObject m_enemyBulletPrefab = default;
    [SerializeField] Transform m_muzzle = null;
    [SerializeField] GameObject m_Death = default;
    [SerializeField] GameObject m_powerUp = default;

    [SerializeField] float m_lifeTime = 10f;
    [SerializeField] float m_enemyHealth = 1f;
    float m_targetTime = 1.0f;
    float m_currentTime = 0;

    Animator m_anim = default;
    
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime;
        if (m_targetTime < m_currentTime)
        {
            Instantiate(m_enemyBulletPrefab, m_muzzle.position, m_enemyBulletPrefab.transform.rotation);
            m_currentTime = 0;
        }

        if (m_enemyHealth == 0)
        {
            Instantiate(m_Death, this.transform.position, Quaternion.identity);
            Instantiate(m_powerUp, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            m_enemyHealth -= 1;
        }

        if (collision.gameObject.tag == ("Lava"))
        {
            Destroy(this.gameObject);
        }
    }
}
