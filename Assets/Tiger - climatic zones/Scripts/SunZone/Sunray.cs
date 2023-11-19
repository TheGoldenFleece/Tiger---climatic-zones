using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunray : MonoBehaviour
{
    private const string RAINDROP_LAYER_NAME = "Raindrop";
    private const string UMBRELLA_LAYER_NAME = "Umbrella";
    private const string TIGER_LAYER_NAME = "Tiger";

    [SerializeField] private Collider2D sunrayCollider;

    [SerializeField] private int sps = 5;
    [SerializeField] private int dps = 7;

    private int raindropLayer;
    private int umbrellaLayer;
    private int tigerLayer;

    private bool isDamaging;

    private Collider2D tigerCollider;
    private void Awake() {
        isDamaging = true;

        raindropLayer = LayerMask.NameToLayer(RAINDROP_LAYER_NAME);
        umbrellaLayer = LayerMask.NameToLayer(UMBRELLA_LAYER_NAME);
        tigerLayer = LayerMask.NameToLayer(TIGER_LAYER_NAME);
    }

    private void Start() {
        tigerCollider = Tiger.Instance.GetComponent<Collider2D>();
    }

    private void FixedUpdate() {

        if (Physics2D.IsTouching(sunrayCollider, tigerCollider)) {
            GameManager.UpdateCounter(sps * Time.fixedDeltaTime);
            isDamaging = false;

        }
        else {
            HPUI.Instance.GetDamage(dps * Time.fixedDeltaTime);
            isDamaging = true;
        }

        Debug.Log(isDamaging);

    }

}