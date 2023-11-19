using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaSetter : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private RectTransform panelSafeArea;
    private Rect currentSafeArea = new Rect();

    private void Start() {
        panelSafeArea = GetComponent<RectTransform>();

        //store current values
        currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    private void ApplySafeArea() {
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        currentSafeArea = safeArea;

    }
}
