using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RainZoneController : MonoBehaviour
{
    [SerializeField] private Transform tigerTransform;
    [SerializeField] private Transform startCorner;
    [SerializeField] private Transform endCorner;

    private RandomMovementController tigerRMC;

    private Vector2 tigerDefaultPosition;

    private void Start() {
        GameManager.Instance.OnRainZoneStarted += GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled += GameManager_OnRainZoneCanceled;

        tigerDefaultPosition = tigerTransform.position;

        tigerRMC = tigerTransform.GetComponent<RandomMovementController>();
        tigerRMC.SetCorners(startCorner.position, endCorner.position);
        tigerRMC.enabled = false;
    }

    private void GameManager_OnRainZoneCanceled(object sender, EventArgs e) {
        tigerRMC.enabled = false;
        //StartCoroutine(PlaceTigerOnDefaultCoroutine());
    }

    IEnumerator PlaceTigerOnDefaultCoroutine() {
        //float step;
        //float speed = tigerRMC.GetSpeed;

        //while (!IsVectors2Equals(tigerTransform.position, tigerDefaultPosition)) {
        //    step = speed * Time.deltaTime;
        //    tigerTransform.position = Vector2.MoveTowards(tigerTransform.position, tigerDefaultPosition, step);

        //    yield return null;
        //}

        float transitionTime = GameManager.ZONE_CHANGE_DELAY;
        float timer = 0;
        while (timer < transitionTime) {

            Vector2 newPosition = Vector2.Lerp(tigerTransform.position, tigerDefaultPosition, timer / transitionTime);

            tigerTransform.position = newPosition;

            timer += Time.deltaTime;
            yield return null;
        }

        tigerRMC.enabled = false;
        yield break;
    }

    private bool IsVectors2Equals(Vector2 v1, Vector2 v2) {
        float delta = .05f;
        float minX = v1.x - delta;
        float maxX = v1.x + delta;

        float minY = v1.y - delta;
        float maxY = v1.y + delta;

        if (
            (minX < v2.x && v2.x < maxX)
            &&
            (minY < v2.y && v2.y < maxY)
        ) return true;

        return false;
    }

    private void GameManager_OnRainZoneStarted(object sender, EventArgs e) {
        tigerRMC.enabled = true;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnRainZoneCanceled;
    }

}
