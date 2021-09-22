using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を生成するコンポーネント
/// m_enemyPrefabs にアサインされたプレハブを m_spawnTimesInWave 回ずつ生成し、全部生成したら最後に m_bossPrefab にアサインされたプレハブを生成する
/// Wave の仕様は以下の通り。
/// 1. m_enemyPrefabs 配列の要素としてアサインされているプレハブを、m_spawnIntervalInWave 秒ごとに m_spawnTimesInWave 回生成する。これを１ウェーブとする。
/// 2. ウエーブが終わったら、画面（シーン）から敵がいなくなるまで待つ
/// 3. m_enemyPrefabs 配列から次の要素のプレハブを、１ウェーブぶん生成する
/// 4. m_enemyPrefabs 配列の全ての要素に対して１ウェーブずつの生成が終わったら、シーンから敵がいなくなるのを待つ
/// 5. m_bossPrefab にアサインされたプレハブを生成する
/// </summary>
public class EnemyGenerateControl : MonoBehaviour
{
    /// <summary>ウエーブとして生成するプレハブの配列</summary>
    [SerializeField] GameObject[] m_enemyPrefabs = null;
    //　次に敵が出現するまでの時間
    [SerializeField] float m_appearNextTime;
    //　この場所から出現する敵の数
    [SerializeField] int m_maxNumOfEnemys;
    //　今何人の敵を出現させたか（総数）
    private int m_numberOfEnemys;
    //　待ち時間計測フィールド
    private float m_elapsedTime;

    GameObject m_object = default;
    GameObject m_enemyGenerator = default;


    private void Start()
    {
        m_numberOfEnemys = 0;
        m_elapsedTime = 0f;
        m_enemyGenerator = GameObject.Find("EnemyGenerator");
    }
    void Update()
    {
        if (m_numberOfEnemys >= m_maxNumOfEnemys)
        {
            return;
        }

        //　経過時間を足す
        m_elapsedTime += Time.deltaTime;

        //　経過時間が経ったら
        if (m_elapsedTime > m_appearNextTime)
        {
            m_elapsedTime = 0f;

            AppearEnemy();
        }
    }
    void AppearEnemy()
    {
        //　出現させる敵をランダムに選ぶ
        var randomValue = Random.Range(0, m_enemyPrefabs.Length);

        GameObject.Instantiate(m_enemyPrefabs[randomValue], transform.position, Quaternion.identity);

        m_numberOfEnemys++;
        m_elapsedTime = 0f;
    }
}
