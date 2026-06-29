using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    // === 非公開変数 === //
    private Rigidbody _rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbodyコンポーネントの取得
        TryGetComponent(out _rigidbody);
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();      // 線(Ray)の判定を作る
    }

    public void Raycast()
    {
        // 1フレーム当たりの飛距離
        Vector3 posA = transform.position + _rigidbody.linearVelocity;
        Vector3 posB = transform.position;
        float distance = Vector3.Distance(posA, posB);

        // 線(Ray)を作る
        // ※ Ray( 起点座標, 線の方向 )
        Ray ray = new Ray(transform.position, transform.forward);
        // デバッグ用の線を描画
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);

        // 👇 RayとRaycastHitを組み合わせる
        // ※ Physics.Raycast(線, out 衝突したオブジェクト情報, 衝突距離, レイヤーマスク);
        //    戻り値: 衝突フラグ
        RaycastHit hit;
        bool collision = Physics.Raycast(ray, out hit, distance, 1 >> 0);

        //Debug.Log($"layer => {}");
        if( collision )
        {
            Debug.Log($"線上にオブジェクトがあります => { hit.transform?.name }");
            if( hit.transform.name.Contains("Enemy"))
            {
                Debug.Log("敵オブジェクトに衝突しました。");
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
