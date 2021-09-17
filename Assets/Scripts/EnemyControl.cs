using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] GameObject m_enemyBulletPrefab = default;
    [SerializeField] Transform m_muzzle = null;

    float m_lifeTime = 10f;
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

        if (m_currentTime > m_lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
