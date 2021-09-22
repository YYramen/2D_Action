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
        Scroll();
    }

    public void Scroll()
    {
        transform.position -= new Vector3(Time.deltaTime * speed, 0);
        float width = m_sprite.bounds.size.x;
        if (transform.position.x < -width)
        {
            this.transform.position += Vector3.right * width * 2;
        }
    }
}
