using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    /// <summary>移動時の速度 </summary>
    [SerializeField] float m_speed = 5f;
    /// <summary>ジャンプ時にかける力 </summary>
    [SerializeField] float m_jumppower = 1f;
    /// <summary>接地フラグ </summary>
    bool m_jump = false;

    /// <summary>弾のプレハブ </summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    /// <summary>弾を発射する位置 </summary>
    [SerializeField] Transform m_muzzle = null;
    /// <summary>弾の上限</summary>
    [SerializeField] int m_bulletLimit;
    /// <summary>弾のカウント</summary>
    public int m_bulletCount = 0;

    float m_h;

    //[SerializeField] float m_playerHealth = 1f;

    Rigidbody2D m_rb;
    Animator m_anim;

    bool m_isRun = false;
    bool m_isJump = false;
    bool m_isRunShot = false;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        m_h = Input.GetAxisRaw("Horizontal");

        //移動、ジャンプ処理
        if (m_h > 0)
        {
            m_rb.velocity = new Vector2(m_speed, m_rb.velocity.y);
            m_isRun = true;
        }

        else if (m_h < 0)
        {
            m_rb.velocity = new Vector2(-m_speed, m_rb.velocity.y);
            m_isRun = true;
        }
        else
        {
            m_isRunShot = false;
            m_isRun = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (m_jump == true)
            {
                m_rb.AddForce(Vector2.up * m_jumppower, ForceMode2D.Impulse);
                m_isJump = true;
                m_jump = false;
            }
        }
        //左右に移動時にスプライトを反転させる
        if (m_rb.velocity.x > 1)
        {
            scale.x = 1.868969f;
        }
        else if (m_rb.velocity.x < -1)
        {
            scale.x = -1.868969f;
        }
        transform.localScale = scale;

        if (Input.GetButtonDown("Fire1"))
        {
          
            Fire();
            m_bulletCount++;
            
        }

        if (m_anim)
        {
            m_anim.SetBool("Run", m_isRun);
            m_anim.SetBool("Jump", m_isJump);
            m_anim.SetBool("RunShot", m_isRunShot);
        }
    }

    void Fire()
    {
        if (m_bulletPrefab && m_muzzle)
        {
            if (m_bulletCount < m_bulletLimit)
            {
                GameObject go = Instantiate(m_bulletPrefab, m_muzzle.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                if (this.transform.localScale.x < 0)
                {
                    go.transform.Rotate(new Vector3(0, 0, 180f));
                }
                if(m_h > 0 || m_h < 0 )
                {
                    m_isRunShot = true;
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        m_jump = true;
        m_isJump = false;
    }
}
