using System;
using UnityEngine;

public class SunZoneController : MonoBehaviour
{
    private TigerController tigerController;

    private void Start() {
        GameManager.Instance.OnSunZoneStarted += GameManager_OnSunZoneStarted;
        GameManager.Instance.OnSunZoneCanceled += GameManager_OnSunZoneCanceled;

        tigerController.enabled = false;
    }

    private void GameManager_OnSunZoneCanceled(object sender, EventArgs e) {
        tigerController.transform.position = Vector2.zero;
        tigerController.enabled = false;
    }

    private void GameManager_OnSunZoneStarted(object sender, EventArgs e) {
        tigerController.enabled = true;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnSunZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnSunZoneCanceled;
    }
}
