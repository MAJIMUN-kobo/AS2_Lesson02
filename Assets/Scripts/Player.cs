using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;     // 弾丸のプレハブ
    public GameObject shotPoint;        // 弾丸の発射位置オブジェクト

    public AudioSource audioSource;     // AudioSourceコンポーネント
    public AudioClip nShotSe;           // 効果音ファイル

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // PlayerInputから[Move]アクションを呼び出すメソッド
    public void OnMove(InputValue value)
    {
        // 第一引数にPlayerInputから渡された値(InputValue)を取得する
        Debug.Log($"移動[{ value.Get<Vector2>() }]");

        // 移動のベクトルを作成する
        Vector3 move = new Vector3(
            value.Get<Vector2>().x,
            value.Get<Vector2>().y,
            0 );

        // X軸の移動量を制限
        if (transform.position.x + value.Get<Vector2>().x < -8
            || transform.position.x + value.Get<Vector2>().x > 8)
            return;

        // Y軸の移動量を制限
        if (transform.position.y + value.Get<Vector2>().y < -4
            || transform.position.y + value.Get<Vector2>().y > 6)
            return;

        // 値を整数に丸める
        move.x = Mathf.Round(move.x);
        move.y = Mathf.Round(move.y);

        // プレイヤーを移動させる
        transform.Translate(move);
    }

    // PlayerInputから[Attack]アクションを呼び出すメソッド
    public void OnAttack(InputValue value)
    {
        // 第一引数にPlayerInputから渡された値(InputValue)を取得する
        Debug.Log($"攻撃アクション [{ value.Get<float>() }]");

        // 弾丸を生成する
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, transform.rotation);
        
        // 弾丸に力を加える
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * 25, ForceMode.Impulse);

        // 効果音を再生する
        audioSource.PlayOneShot(nShotSe);

        // 5秒後に弾丸を破壊する
        Destroy(bullet, 5f);
    }
}
