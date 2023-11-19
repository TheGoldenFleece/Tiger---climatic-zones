using Scripts;
using System.Collections;
using UnityEngine;

public class RaindropSpawner : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform raindropPrefab;
    [SerializeField, Range(1f, 50f)] private float rate = 1.0f;

    private IEnumerator spawnCoroutine;

    private void Awake() {
        spawnCoroutine = SpawnCoroutine();
    }

    private void Start() {
        GameManager.Instance.OnRainZoneStarted += GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled += GameManager_OnRainZoneCanceled;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnRainZoneCanceled;
    }

    private void GameManager_OnRainZoneStarted(object sender, System.EventArgs e) {
        StartCoroutine(spawnCoroutine);
    }

    private void GameManager_OnRainZoneCanceled(object sender, System.EventArgs e) {
        StopCoroutine(spawnCoroutine);
    }

    IEnumerator SpawnCoroutine() {
        while (GameManager.Instance.GetZone == Zone.Rain) {
            
            SpawnOneRainDrop();

            yield return new WaitForSeconds(1f / rate);
        }

        yield break;
    }

    private void SpawnOneRainDrop() {
        float randomX = Random.Range(start.position.x, end.position.x);

        Vector2 posToSpawn = new Vector2(randomX, start.position.y);
        Transform raindropTransform = Instantiate(raindropPrefab, posToSpawn, Quaternion.identity);
        
        raindropTransform.SetParent(parent);
    }
}

