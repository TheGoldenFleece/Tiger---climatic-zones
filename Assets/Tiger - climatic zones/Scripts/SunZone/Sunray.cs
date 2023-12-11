using System;
using UnityEngine;

public class Sunray : MonoBehaviour
{
    [SerializeField] private Collider2D sunrayCollider;

    public static event EventHandler OnSunrayCatch;

    private float lifeTime = 3.0f;

    private int sps;
    private int dps;

    private float damageThreshold = 0.5f;
    private float timer = 0f;

    private Collider2D tigerCollider;

    private void Start() {
        sps = GameManager.Instance.GDSO.scorePerSecond;
        dps = GameManager.Instance.GDSO.damagePerSecond;

        tigerCollider = Tiger.Instance.GetComponent<Collider2D>();

        Destroy(this.gameObject, lifeTime);
    }

    private void Update() {
        timer += Time.deltaTime;
    }

    private void FixedUpdate() {
        lifeTime -= Time.fixedDeltaTime;
        lifeTime = Mathf.Clamp(lifeTime, 0, Mathf.Infinity);

        if (timer > damageThreshold)
        {
            HPUI.Instance.GetDamage(dps * Time.fixedDeltaTime);
            Debug.Log(dps * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == tigerCollider) {
            GameManager.UpdateCounter(sps * lifeTime);

            OnSunrayCatch?.Invoke(this, EventArgs.Empty);

            SunraySpawner.RemoveSpawnedSunray(this);
            Destroy(this.gameObject);
        }
    }

}