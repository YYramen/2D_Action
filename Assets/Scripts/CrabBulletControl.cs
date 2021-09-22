using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrabBulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_initialSpeed = 5f;
    [SerializeField] GameObject m_effectPrefab = default;

    [SerializeField] float m_reduceSlider = 0.05f;
    Slider m_slider = default;
    Rigidbody2D m_rb = default;
    void Start()
    {
        m_slider = GameObject.Find("Slider3").GetComponent<Slider>();
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = Vector3.left * m_initialSpeed;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_effectPrefab && collision.CompareTag("Player"))
        {
            Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
            m_slider.value -= m_reduceSlider;
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
