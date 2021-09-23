using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletEffectControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_lifetime = 0.7f;

    void Start()
    {
        Destroy(this.gameObject, m_lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PowerUp")
        {
            return;
        }
    }
}
