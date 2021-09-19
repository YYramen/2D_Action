﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_bulletPrefab = default;
    [SerializeField] Transform m_muzzle = null;
    [SerializeField] GameObject m_Death = default;
    [SerializeField] GameObject m_powerUp = default;

    [SerializeField] float m_moveSpeed = -5f;
    [SerializeField] float m_enemyHealth = 1f;

    bool m_isGround;
    float m_targetTime = 1.0f;
    float m_currentTime = 0;

    Rigidbody2D m_rb = default;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isGround == true)
        {
            m_rb.velocity = new Vector3(-1 * m_moveSpeed, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            m_isGround = true;
        }
    }
}
