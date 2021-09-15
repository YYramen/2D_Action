using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 1;
    SpriteRenderer m_sprite = default;

    private void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //横方向にスクロール
        transform.position -= new Vector3(Time.deltaTime, 0 * speed);
        float width = m_sprite.bounds.size.x;
        if (transform.position.x < -width)
        {
            transform.position += Vector3.back * width * 2;
        }
    }
}
