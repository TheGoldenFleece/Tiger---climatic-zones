using System;
using UnityEngine;

public class RainZoneController : MonoBehaviour
{
    public static RainZoneController Instance { get; private set; }

    [SerializeField] private Transform startCorner;
    [SerializeField] private Transform endCorner;

    public Transform GetStartCorner => startCorner;
    public Transform GetEndCorner => endCorner;

    //private Transform tigerTransform;
    // private RandomMovementController tigerRMC;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one RainZoneController instance");
        }
        Instance = this;
    }

    //private void Start() {
    //    GameManager.Instance.OnRainZoneStarted += GameManager_OnRainZoneStarted;
    //    GameManager.Instance.OnRainZoneCanceled += GameManager_OnRainZoneCanceled;

    //    tigerRMC = tigerTransform.GetComponent<RandomMovementController>();
    //    tigerRMC.SetCorners(startCorner.position, endCorner.position);
    //    tigerRMC.enabled = false;
    //    tigerRMC.SetSpeed(GameManager.Instance.GDSO.tigerSpeed);
    //}

    //private void GameManager_OnRainZoneCanceled(object sender, EventArgs e) {
    //    tigerRMC.enabled = false;
    //}

    //private void GameManager_OnRainZoneStarted(object sender, EventArgs e) {
    //    tigerRMC.enabled = true;
    //}

    //private void OnDestroy() {
    //    GameManager.Instance.OnRainZoneStarted -= GameManager_OnRainZoneStarted;
    //    GameManager.Instance.OnRainZoneCanceled -= GameManager_OnRainZoneCanceled;
    //}

}
