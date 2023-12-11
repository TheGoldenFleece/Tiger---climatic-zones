using Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunraySpawner : MonoBehaviour {
    [SerializeField] private Transform leftStartCorner;
    [SerializeField] private Transform leftEndCorner;
    [SerializeField] private Transform rightStartCorner;
    [SerializeField] private Transform rightEndCorner;

    [SerializeField] private Transform parent;

    [SerializeField] private Transform leftSunrayPrefab;
    [SerializeField] private Transform rightSunrayPrefab;

    private IEnumerator spawnCoroutine;
    private Transform sunrayTransform;

    private int sunrayAmount;
    private float sunraySpawnRate;

    private float timer;

    private int maxSunrayAmount;
    private static List<Sunray> spawnedSunrayList;

    private void Awake() {
        timer = 0;

        spawnCoroutine = SpawnCoroutine();

        spawnedSunrayList = new List<Sunray>();
    }

    private void Start() {
        GameManager.Instance.OnSunZoneStarted += GameManager_OnSunZoneStarted;
        GameManager.Instance.OnSunZoneCanceled += GameManager_OnSunZoneCanceled;

        sunraySpawnRate = GameManager.Instance.GDSO.sunraySpawnRate;
        sunrayAmount = GameManager.Instance.GDSO.sunrayAmount;
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

        foreach (Sunray sunray in spawnedSunrayList) {
            if (sunray != null) {
                Destroy(sunray.gameObject);
            }
        }
        spawnedSunrayList.Clear();

    }

    IEnumerator SpawnCoroutine() {
        while (GameManager.Instance.GetZone == Zone.Sun) {

            int spawnAmount = sunrayAmount - spawnedSunrayList.Count;
            for (int i = 0; i < spawnAmount; i++) {
                SpawnSunray();
            }
            //timer = 0;
            yield return new WaitUntil(() => spawnedSunrayList.Count < sunrayAmount);
            //yield return new WaitUntil(() => Sunray.IsDestroyed || (1 / timer) > (1 / sunraySpawnRate));
        }

        yield break;
    }

    public static void RemoveSpawnedSunray(Sunray sunray) {
        spawnedSunrayList.Remove(sunray);
    }

    private void Update() {
        timer += Time.deltaTime;
    }

    private void SpawnSunray() {
        int randomDirection = Random.Range(0, 2);

        RandomMovementController sunrayMovementController;

        Vector2 spawnPosition;
        if (randomDirection == 0) {
            spawnPosition = GetRandomPosition(leftStartCorner.position, leftEndCorner.position);
            sunrayTransform = Instantiate(leftSunrayPrefab, spawnPosition, Quaternion.identity);

            sunrayMovementController = sunrayTransform.GetComponent<RandomMovementController>();
            sunrayMovementController.SetCorners(leftStartCorner.position, leftEndCorner.position);
        }
        else {
            spawnPosition = GetRandomPosition(rightStartCorner.position, rightEndCorner.position);
            sunrayTransform = Instantiate(rightSunrayPrefab, spawnPosition, Quaternion.identity);

            sunrayMovementController = sunrayTransform.GetComponent<RandomMovementController>();
            sunrayMovementController.SetCorners(rightStartCorner.position, rightEndCorner.position);
        }

        sunrayMovementController.SetSpeed(GameManager.Instance.GDSO.sunraySpeed);
        sunrayTransform.SetParent(parent);
        spawnedSunrayList.Add(sunrayTransform.GetComponent<Sunray>());
    }

    private Vector2 GetRandomPosition(Vector2 leftCorner, Vector2 rightCorner) {
        float randX = Random.Range(leftCorner.x, rightCorner.x);
        float randY = Random.Range(leftCorner.y, rightCorner.y);
        Vector2 position = new Vector2(randX, randY);

        return position;
    }

}
