using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;     // 弾丸のプレハブ
    public GameObject shotPoint;        // 弾丸の発射位置オブジェクト

    public AudioSource audioSource;     // AudioSourceコンポーネント
    public AudioClip nShotSe;           // 効果音ファイル

    // 移動値の設定
    [Header("* * * 移動値の設定")]
    public float moveSpeed = 5.0f;      // 移動速度
    private Vector3 inputMoveVelocity;  // 移動ベクトルの入力値

    // 回転軸の設定
    [Header("* * * 回転軸の設定")]
    public bool tiltInvart = false; // 向きのチルト（上下）反転フラグ
    public GameObject lookAxis;     // 向きベクトル軸（オブジェクト）
    public GameObject gyroAxis;     // ジャイロ軸（オブジェクト）
    private Vector3 lookAngles;     // 向きベクトル（値）
    private float gyroAngle;        // ジャイロ回転（値）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zSpeed = moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, zSpeed);

        // 移動する方向に回転させる
        lookAngles.x += inputMoveVelocity.y * (tiltInvart ? -1 : 1);
        lookAngles.y += inputMoveVelocity.x;
        gyroAngle += inputMoveVelocity.x * -1;
        /* --- おまけポイント
         * 三項演算子[ 条件式 ? true : false ]
         * -- これと同じ -------------
         * if(tiletInvert)
         *      return -1;
         * else
         *      return 1;
         * ---------------------------
         */

        // 徐々に 0(目標値) に近づける
        lookAngles = Vector3.Lerp(lookAngles, Vector3.zero, Time.deltaTime * 3);
        gyroAngle = Mathf.Lerp(gyroAngle, 0, Time.deltaTime * 3);
        /* -----
         * 各軸に分けても OK
         * ----- */
        //lookAngles.x = Mathf.Lerp(lookAngles.x, 0, Time.deltaTime);
        //lookAngles.y = Mathf.Lerp(lookAngles.y, 0, Time.deltaTime);

        // 回転の制限
        // 👇[Mathf.Clamp(制限対象値, 最小値, 最大値);]
        lookAngles.x = Mathf.Clamp(lookAngles.x, -15, 15);
        lookAngles.y = Mathf.Clamp(lookAngles.y, -15, 15);
        gyroAngle = Mathf.Clamp(gyroAngle, -15, 15);

        // 角度の代入
        // [Transform.eulerAngles]で角度の変更ができる
        lookAxis.transform.eulerAngles = lookAngles;
        gyroAxis.transform.eulerAngles = new Vector3(0, 0, gyroAngle);

        /* これでもいい
        if(lookAngles.x <= -15)
        {
            lookAngles.x = -15;
        }
        */
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

        // 移動値を変数に保存
        inputMoveVelocity = move;
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
        if(nShotSe != null)
        {
            audioSource?.PlayOneShot(nShotSe);
        }

        // 5秒後に弾丸を破壊する
        Destroy(bullet, 5f);
    }
}
