using Scripts;
using System;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static float Counter;

    public const float ZONE_CHANGE_DELAY = 0.5f;

    [SerializeField] private GameDifficultySO[] gameDifficultySOArray;
    [SerializeField] private Tiger[] tigerArray;

    public Tiger GetTigerType(TigerState tigerState) {
        foreach (Tiger tiger in tigerArray) {
            if (tiger.TigerState == tigerState) {
                return tiger;
            }
        }

        return null;
    }

    public event EventHandler OnRainZoneStarted;
    public event EventHandler OnRainZoneCanceled;
    public event EventHandler OnSunZoneStarted;
    public event EventHandler OnSunZoneCanceled;
    public event EventHandler OnZoneChanged;

    public float ZoneDuration { get; private set; }

    public GameDifficultySO GDSO { get; private set; }

    private Zone zone;
    public Zone GetZone => zone;

    IEnumerator changeZoneCoroutine;
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one GameManager instance");
        }
        Instance = this;

        Counter = 0;

        zone = Zone.Rain;

        changeZoneCoroutine = ChangeZoneCoroutine();

        foreach (GameDifficultySO gameDifficultySO in gameDifficultySOArray) {
            if (gameDifficultySO.difficulty == PlayerStats.GameDifficulty) {
                GDSO = gameDifficultySO;
                break;
            }
        }

        ZoneDuration = GDSO.zoneDuration;

        Time.timeScale = 1f;
    }

    private void Start() {
        StartCoroutine(changeZoneCoroutine);
    }

    private void Update() {
        Debug.Log(zone);
    }

    public static void UpdateCounter(float amount) {
        Counter += amount;
    }

    private IEnumerator ChangeZoneCoroutine() {
        while (true) {
            Tiger.SetTiger(TigerState.Rest);

            switch (zone) {
                case Zone.Rain: {
                        zone = Zone.Sun;
                        OnRainZoneCanceled?.Invoke(this, EventArgs.Empty);
                        OnZoneChanged?.Invoke(this, EventArgs.Empty);

                        yield return new WaitForSeconds(ZONE_CHANGE_DELAY);

                        OnSunZoneStarted?.Invoke(this, EventArgs.Empty);

                        Tiger.SetTiger(TigerState.Chase);

                        break;
                    }
                case Zone.Sun: {
                        zone = Zone.Rain;

                        OnSunZoneCanceled?.Invoke(this, EventArgs.Empty);
                        OnZoneChanged?.Invoke(this, EventArgs.Empty);

                        yield return new WaitForSeconds(ZONE_CHANGE_DELAY);
                        OnRainZoneStarted?.Invoke(this, EventArgs.Empty);

                        Tiger.SetTiger(TigerState.Walk);

                        break;
                    }
            }

            yield return new WaitForSeconds(ZoneDuration);
        }
    }

    private void OnDestroy() {
        StopCoroutine(changeZoneCoroutine);
    }
    
}
