using UnityEngine;

public class BaseParticle : MonoBehaviour
{
    // === 公開 変数 === //
    [HideInInspector] public ParticleSystem particle;
    [HideInInspector] public float lifeTime = 1.0f;

    // === 初期化処理メソッド === //
    public void Initialize(float lifeTime = 1.0f, bool looping = false)
    {
        this.particle = GetComponent<ParticleSystem>();
        this.lifeTime = lifeTime;

        if(!looping)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
