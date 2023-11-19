using UnityEngine;

public class Sunray : MonoBehaviour
{
    [SerializeField] private Collider2D sunrayCollider;

    private float lifeTime = 3.0f;

    private int sps;
    private int dps;

    private Collider2D tigerCollider;

    private void Start() {
        sps = GameManager.Instance.GDSO.scorePerSecond;
        dps = GameManager.Instance.GDSO.damagePerSecond;

        tigerCollider = Tiger.Instance.GetComponent<Collider2D>();

        Destroy(this.gameObject, lifeTime);
    }

    private void FixedUpdate() {
        lifeTime -= Time.fixedDeltaTime;
        lifeTime = Mathf.Clamp(lifeTime, 0, Mathf.Infinity);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == tigerCollider) {
            GameManager.UpdateCounter(sps * lifeTime);

            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() {
        HPUI.Instance.GetDamage(dps);

    }

}