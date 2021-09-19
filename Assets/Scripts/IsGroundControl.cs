using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_isGround;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_isGround = true;
    }
}
