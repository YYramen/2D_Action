using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [SerializeField] float m_lifetime = 0.7f;

    void Start()
    {
        // Destroy 関数の第二引数に「何秒後に破棄するか」を指定できる
        Destroy(this.gameObject, m_lifetime);
    }
}
