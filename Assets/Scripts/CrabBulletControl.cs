using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_bulletSpeed = 5f;
    [SerializeField] GameObject m_effectPrefab = default;

    Rigidbody2D m_rb = default;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = this.gameObject.transform.forward;
        m_rb.AddForce(dir * m_bulletSpeed, ForceMode2D.Impulse);
    }
}
