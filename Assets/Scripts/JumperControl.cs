using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_enemyBulletPrefab = default;
    [SerializeField] Transform m_muzzle1 = null;
    [SerializeField] Transform m_muzzle2 = null;
    [SerializeField] Transform m_muzzle3 = null;
    [SerializeField] GameObject m_death = default;

    [SerializeField] float m_lifeTime = 10f;
    [SerializeField] float m_enemyHealth = 1f;
    [SerializeField] float m_targetTime = 1.0f;
    [SerializeField] float m_currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime;
        if (m_targetTime < m_currentTime)
        {
            Instantiate(m_enemyBulletPrefab, m_muzzle1.position, Quaternion.identity);
            Instantiate(m_enemyBulletPrefab, m_muzzle2.position, Quaternion.identity);
            Instantiate(m_enemyBulletPrefab, m_muzzle3.position, Quaternion.identity);
            m_currentTime = 0;
        }

        if (m_enemyHealth == 0)
        {
            Instantiate(m_death, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            m_enemyHealth -= 1;
        }
    }
}
