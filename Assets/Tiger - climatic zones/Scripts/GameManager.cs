using Scripts;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static float Counter;

    public const float ZONE_CHANGE_DELAY = 1.5f;
    public const float ZONE_DURATION = 15f;

    public event EventHandler OnRainZoneStarted;
    public event EventHandler OnRainZoneCanceled;
    public event EventHandler OnSunZoneStarted;
    public event EventHandler OnSunZoneCanceled;
    public event EventHandler OnZoneChanged;

    private Zone zone;
    public Zone GetZone => zone;
    public bool IsGameOver { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one GameManager instance");
        }
        Instance = this;

        Counter = 0;

        IsGameOver = false;

        zone = Zone.Sun;

        Time.timeScale = 1f;
    }

    private void Start() {
        StartCoroutine(ChangeZoneCoroutine());
    }

    private void Update() {
        Debug.Log(zone);
    }

    public static void UpdateCounter(float amount) {
        Counter += amount;
    }

    private IEnumerator ChangeZoneCoroutine() {
        while (!IsGameOver) {
            switch (zone) {
                case Zone.Rain: {
                        zone = Zone.Sun;
                        OnRainZoneCanceled?.Invoke(this, EventArgs.Empty);
                        OnZoneChanged?.Invoke(this, EventArgs.Empty);

                        yield return new WaitForSeconds(ZONE_CHANGE_DELAY);

                        OnSunZoneStarted?.Invoke(this, EventArgs.Empty);

                        break;
                    }
                case Zone.Sun: {
                        zone = Zone.Rain;

                        OnSunZoneCanceled?.Invoke(this, EventArgs.Empty);
                        OnZoneChanged?.Invoke(this, EventArgs.Empty);

                        yield return new WaitForSeconds(ZONE_CHANGE_DELAY);
                        OnRainZoneStarted?.Invoke(this, EventArgs.Empty);

                        break;
                    }
            }

            yield return new WaitForSeconds(ZONE_DURATION);


        }
        yield break;
    }


}
