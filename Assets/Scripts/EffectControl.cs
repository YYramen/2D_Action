using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    [SerializeField] GameObject m_powerUp = default;
    [SerializeField] float m_lifetime = 0.7f;

    void Start()
    {
        if (m_powerUp)
        {
            StartCoroutine(Timer());
            // Destroy 関数の第二引数に「何秒後に破棄するか」を指定できる
        }
        Destroy(this.gameObject, m_lifetime);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(m_lifetime - 0.2f);
        Instantiate(m_powerUp, transform.position, Quaternion.identity);
    }
}
