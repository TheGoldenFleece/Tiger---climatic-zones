using Unity.VisualScripting;
using UnityEngine;

public class Raindrop : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)] private float speed;
    private const string RAINDROPS_REMOVER_LAYER_NAME = "RaindropsRemover";
    private const string UMBRELLA_LAYER_NAME = "Umbrella";
    int raindropsRemoverLayer;
    int umbrellaLayer;

    private void Awake() {
        raindropsRemoverLayer = LayerMask.NameToLayer(RAINDROPS_REMOVER_LAYER_NAME);
        umbrellaLayer = LayerMask.NameToLayer(UMBRELLA_LAYER_NAME);
    }

    private void Start() {
        GameManager.Instance.OnRainZoneCanceled += GameManager_OnRainZoneCanceled;
    }

    private void GameManager_OnRainZoneCanceled(object sender, System.EventArgs e) {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        int layer = collision.gameObject.layer;

        if (layer == raindropsRemoverLayer) {
            Destroy(this.gameObject);
        }
        else if (layer == umbrellaLayer) {
            Destroy(this.gameObject);
        }
        
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnRainZoneCanceled;
    }
}
