using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] GameObject m_enemyBulletPrefab = default;
    [SerializeField] Transform m_muzzle = null;
    [SerializeField] GameObject m_death = default;
    [SerializeField] GameObject m_powerUp = default;
    [SerializeField] float m_reduceSlider = 0.05f;

    Slider m_slider = default;
    [SerializeField] float m_lifeTime = 10f;
    float m_enemyHealth = 2f;
    float m_targetTime = 1.0f;
    float m_currentTime = 0;

    private void Start()
    {
        if(m_enemyHealth != 2)
        {
            m_enemyHealth = 2;
        }
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime;
        if (m_targetTime < m_currentTime)
        {
            Instantiate(m_enemyBulletPrefab, m_muzzle.position, this.transform.rotation);
            m_currentTime = 0;
        }

        if (m_enemyHealth < 0)
        {
            Instantiate(m_death, this.transform.position, Quaternion.identity);
            Instantiate(m_powerUp, this.transform.position, Quaternion.identity);
            m_slider.value -= m_reduceSlider;
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
