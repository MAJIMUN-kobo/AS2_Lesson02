using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab;     // スポーンするオブジェクトのプレハブ

    private float spawnInterval = 2f;     // スポーンの間隔（秒）
    private float spawnTimer = 0f;        // スポーンのタイマー

    void Start()
    {
        
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;       // タイマーを更新(毎秒)

        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();      // オブジェクトを生成
            spawnTimer = 0f;    // タイマーをリセット
        }
    }

    // オブジェクトを生成するメソッド
    private void SpawnObject()
    {
        // プレイヤーのZ座標を取得
        Player player = GameObject.FindAnyObjectByType<Player>();
        float playerZ = player.transform.position.z;

        Vector3 randomPos = Vector3.zero;
        randomPos.x = Random.Range(-8, 8);   // X軸のランダムな位置
        randomPos.z = playerZ + 100;         // プレイヤーの前方に生成
        Instantiate(spawnPrefab, randomPos, transform.rotation);
    }
}
