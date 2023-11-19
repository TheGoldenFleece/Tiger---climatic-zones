using Scripts;
using System.Collections;
using UnityEngine;

public class SunraySpawner : MonoBehaviour
{
    [SerializeField] private Transform leftStartCorner;
    [SerializeField] private Transform leftEndCorner;
    [SerializeField] private Transform rightStartCorner;
    [SerializeField] private Transform rightEndCorner;

    [SerializeField] private Transform parent;

    [SerializeField] private Transform leftSunrayPrefab;
    [SerializeField] private Transform rightSunrayPrefab;

    [SerializeField, Range(1f, 5f)] private float lifeDuration = 3.0f;

    private IEnumerator spawnCoroutine;
    private Transform sunrayTransform;

    private void Awake() {
        spawnCoroutine = SpawnCoroutine();
    }

    private void Start() {
        GameManager.Instance.OnSunZoneStarted += GameManager_OnSunZoneStarted;
        GameManager.Instance.OnSunZoneCanceled += GameManager_OnSunZoneCanceled;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnSunZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnSunZoneCanceled;
    }

    private void GameManager_OnSunZoneStarted(object sender, System.EventArgs e) {
        StartCoroutine(spawnCoroutine);
    }

    private void GameManager_OnSunZoneCanceled(object sender, System.EventArgs e) {
        if (sunrayTransform != null) {
            Destroy(sunrayTransform.gameObject);
        }

        StopCoroutine(spawnCoroutine);
    }

    IEnumerator SpawnCoroutine() {
        while (GameManager.Instance.GetZone == Zone.Sun) {

            SpawnSunray();

            yield return new WaitForSeconds(lifeDuration);
        }

        yield break;
    }

    private void SpawnSunray() {
        int randomDirection = Random.Range(0, 2);

        Vector2 spawnPosition;
        if (randomDirection == 0) {
            spawnPosition = GetRandomPosition(leftStartCorner.position, leftEndCorner.position);
            sunrayTransform = Instantiate(leftSunrayPrefab, spawnPosition, Quaternion.identity);

            sunrayTransform.GetComponent<RandomMovementController>().SetCorners(leftStartCorner.position, leftEndCorner.position);

        }
        else {
            spawnPosition = GetRandomPosition(rightStartCorner.position, rightEndCorner.position);
            sunrayTransform = Instantiate(rightSunrayPrefab, spawnPosition, Quaternion.identity);
            
            sunrayTransform.GetComponent<RandomMovementController>().SetCorners(rightStartCorner.position, rightEndCorner.position);
        }
        sunrayTransform.SetParent(parent);
        Destroy(sunrayTransform.gameObject, lifeDuration);
    }

    private Vector2 GetRandomPosition(Vector2 leftCorner, Vector2 rightCorner) {
        float randX = Random.Range(leftCorner.x, rightCorner.x);
        float randY = Random.Range(leftCorner.y, rightCorner.y);
        Vector2 position = new Vector2(randX, randY);

        return position;
    }

}