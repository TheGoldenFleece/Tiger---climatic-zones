using System;
using UnityEngine;

public class RainZoneController : MonoBehaviour
{
    [SerializeField] private Transform tigerTransform;
    [SerializeField] private Transform startCorner;
    [SerializeField] private Transform endCorner;

    private RandomMovementController tigerRMC;

    private void Start() {
        GameManager.Instance.OnRainZoneStarted += GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled += GameManager_OnRainZoneCanceled;

        tigerRMC = tigerTransform.GetComponent<RandomMovementController>();
        tigerRMC.SetCorners(startCorner.position, endCorner.position);
        tigerRMC.enabled = false;
    }

    private void GameManager_OnRainZoneCanceled(object sender, EventArgs e) {
        tigerRMC.enabled = false;
    }

    private void GameManager_OnRainZoneStarted(object sender, EventArgs e) {
        tigerRMC.enabled = true;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnRainZoneCanceled;
    }

}
