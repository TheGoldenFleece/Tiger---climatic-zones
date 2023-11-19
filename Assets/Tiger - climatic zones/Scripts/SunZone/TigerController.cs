using System;
using UnityEngine;

public class TigerController : MonoBehaviour
{
    public bool EnabledMovement { private set; get; }

    [SerializeField] private Transform tigerTransform;

    private Collider2D tigerCollider;

    private bool isDragging;
    private Vector2 screenPosition;
    private Vector2 worldPosition;

    private void Awake() {
        tigerCollider = tigerTransform.GetComponent<Collider2D>();
    }

    private void Start() {
        GameManager.Instance.OnSunZoneStarted += GameManager_OnSunZoneStarted;
        GameManager.Instance.OnSunZoneCanceled += GameManager_OnSunZoneCanceled;

        Drop();
    }

    private void GameManager_OnSunZoneStarted(object sender, EventArgs e) {
        this.enabled = true;
    }
    private void GameManager_OnSunZoneCanceled(object sender, EventArgs e) {
        Drop();
        this.enabled = false;
    }

    private void Update() {

        if (Input.touchCount > 0) {
            screenPosition = Input.GetTouch(0).position;

            worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            var col = Physics2D.OverlapPoint(worldPosition);

            if (tigerCollider != col) return;

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

    private void InitDrag() {
        isDragging = true;
    }

    private void Drop() {
        isDragging = false;
    }

    void Drag() {
        tigerTransform.position = worldPosition;
    }

    private void OnDestroy() {
        GameManager.Instance.OnRainZoneStarted -= GameManager_OnSunZoneStarted;
        GameManager.Instance.OnRainZoneCanceled -= GameManager_OnSunZoneCanceled;
    }
}
