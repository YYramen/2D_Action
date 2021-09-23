using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] float m_health = 3f;
    /// <summary>移動時の速度 </summary>
    [SerializeField] float m_speed = 5f;
    /// <summary>ジャンプ時にかける力 </summary>
    [SerializeField] float m_jumppower = 1f;
    /// <summary>接地フラグ </summary>
    bool m_jump = false;

    /// <summary>弾のプレハブ </summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    /// <summary>弾を発射する位置 </summary>
    [SerializeField] Transform m_muzzle1 = null;
    [SerializeField] Transform m_muzzle2 = null;
    [SerializeField] Transform m_muzzle3 = null;
    /// <summary>弾の上限</summary>
    [SerializeField] int m_bulletLimit;
    /// <summary>弾のカウント</summary>
    public int m_bulletCount = 0;

    [SerializeField] float m_reduceSlider = 0.05f;
    [SerializeField] float m_increaseSlider = 0.05f;
    Slider m_powerUpSlider = default;
    Slider m_healthSlider = default;
    float m_h;

    SpriteRenderer m_sprite;
    Rigidbody2D m_rb;
    Animator m_anim;

    bool m_isDamage = false;
    bool m_isRun = false;
    bool m_isJump = false;
    bool m_isRunShot = false;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        m_healthSlider = GameObject.Find("Slider3").GetComponent<Slider>();
        m_powerUpSlider = GameObject.Find("Slider2").GetComponent<Slider>();
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
            m_isRunShot = false;
        }

        else if (m_h < 0)
        {
            m_rb.velocity = new Vector2(-m_speed, m_rb.velocity.y);
            m_isRun = true;
            m_isRunShot = false;
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
            m_isRunShot = true;
            Fire();
            m_bulletCount++;
        }
        
        if(m_powerUpSlider.value >= 0.2f)
            if(Input.GetButtonDown("Fire2"))
            {
                m_isRunShot = true;
                Shotgun();
                m_bulletCount++;
            }

        if(m_health == 0)
        {
            Destroy(gameObject);
        }

        if (m_anim)
        {
            m_anim.SetBool("Run", m_isRun);
            m_anim.SetBool("Jump", m_isJump);
            m_anim.SetBool("RunShot", m_isRunShot);
        }

        if(m_isDamage)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            m_sprite.color = new Color(1f, 1f, 1f, level);
        }
    }

    void Fire()
    {
        if (m_bulletPrefab && m_muzzle1)
        {
            if (m_bulletCount < m_bulletLimit)
            {
                GameObject go = Instantiate(m_bulletPrefab, m_muzzle1.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                if (this.transform.localScale.x < 0)
                {
                    go.transform.Rotate(new Vector3(0, 0, 180f));
                }
            }
        }
    }

    void Shotgun()
    {
        if(m_powerUpSlider.value >= 0.2f)
        {
            if(m_bulletPrefab)
            {
                if(m_bulletCount < m_bulletLimit)
                {
                    GameObject go1 = Instantiate(m_bulletPrefab, m_muzzle1.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                    if (this.transform.localScale.x < 0)
                    {
                        go1.transform.Rotate(new Vector3(0, 0, 180f));
                    }

                    GameObject go2 = Instantiate(m_bulletPrefab, m_muzzle2.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                    if (this.transform.localScale.x < 0)
                    {
                        go2.transform.Rotate(new Vector3(0, 0, 180f));
                    }

                    GameObject go3 = Instantiate(m_bulletPrefab, m_muzzle3.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                    if (this.transform.localScale.x < 0)
                    {
                        go3.transform.Rotate(new Vector3(0, 0, 180f));
                    }
                }
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        m_jump = true;
        m_isJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_isDamage)
        {
            return;
        }

        if(collision.gameObject.tag == "Enemy")
        {
            m_isDamage = true;
            StartCoroutine(OnDamage());
            m_health -= 1;
            m_healthSlider.value -= m_reduceSlider;
            m_powerUpSlider.value -= m_reduceSlider;
            
        }

        if(collision.gameObject.tag == "PowerUp")
        {
            m_powerUpSlider.value += m_increaseSlider;
        }
    }

    public IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(4.0f);

        m_isDamage = false;
        m_sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
