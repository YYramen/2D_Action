using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpControl : MonoBehaviour
{
    [SerializeField] GameObject m_effectPrefab = default;
    [SerializeField] float m_bubbleSpeed = 5f;
    [SerializeField] float m_increaseSlider = 0.05f;

    Slider m_slider = default;

    GameObject m_player = default;
    Rigidbody2D m_rb = default;
    // Start is called before the first frame update
    void Start()
    {
        m_slider = GameObject.Find("Slider2").GetComponent<Slider>();
        m_player = GameObject.Find("Player");
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player)
        {
            Vector3 dir = m_player.transform.position - transform.position;

            m_rb.velocity = dir.normalized * m_bubbleSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bulet")
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //m_slider.value += m_increaseSlider;
        }
    }
}
