using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UmbrellaController : MonoBehaviour
{
    public bool EnabledMovement { private set; get; }

    private int sps;
    private int dps;

    private Transform umbrellaTransform;
    private Collider2D umbrellaCollider;
    private Collider2D tigerCollider;


    private bool isDragging;
    private Vector2 screenPosition;
    private Vector2 worldPosition;

    private void Start() {
        GameManager.Instance.OnRainZoneStarted += GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled += GameManager_OnRainZoneCanceled;

        umbrellaTransform = Instantiate(GameManager.Instance.GDSO.umbrellaPrefab, Vector2.zero, Quaternion.identity);
        umbrellaCollider = umbrellaTransform.GetComponent<Umbrella>().SafeArea;

        sps = GameManager.Instance.GDSO.scorePerSecond;
        dps = GameManager.Instance.GDSO.damagePerSecond;

        Drop();
        this.enabled = false;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnRainZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnRainZoneCanceled;
    }

    private void GameManager_OnRainZoneStarted(object sender, EventArgs e) {
        this.enabled = true;
        tigerCollider = Tiger.GetTiger.GetComponent<Collider2D>();
    }
    private void GameManager_OnRainZoneCanceled(object sender, EventArgs e) {
        Drop();
        this.enabled = false;
    }

    private void Update() {
        if (Input.touchCount > 0) {

            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }

            screenPosition = Input.GetTouch(0).position;            

            if (isDragging) {
                Drag();
            }
            else {
                InitDrag();
            }
        }
        else {
            Drop();
        }
    }

    private void FixedUpdate() {
        if (umbrellaCollider.bounds.Contains(tigerCollider.bounds.min) && umbrellaCollider.bounds.Contains(tigerCollider.bounds.max)) {
            GameManager.UpdateCounter(sps * Time.fixedDeltaTime);
        }
        else {
            HPUI.Instance.GetDamage(dps * Time.fixedDeltaTime);
        }
    }

    private void InitDrag() {
        isDragging = true;
        umbrellaTransform.gameObject.SetActive(true);
    }

    private void Drop() {
        isDragging = false;
        umbrellaTransform.gameObject.SetActive(false);
    }

    void Drag() {
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        umbrellaTransform.position = new Vector2(worldPosition.x, worldPosition.y);
    }
}

