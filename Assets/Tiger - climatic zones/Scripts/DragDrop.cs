using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour {
    private Vector2 screenPosition;
    private Vector2 worldPosition;
    private bool isDragging;

    Draggable lastDragged;

    private void Awake() {
        isDragging = false;
        lastDragged = null;
    }

    private void Update() {

        //screenPosition = UmbrellaInputManager.Instance.GetInput();

        if (Input.GetMouseButton(0)) {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0) {
            screenPosition = Input.GetTouch(0).position;
        }
        else {
            return;
        }


        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isDragging) {
            Drag();
        }
        else {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null) {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();

                if (draggable != null) {
                    lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    void InitDrag() {
        isDragging = true;
    }

    void Drag() {
        lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
    }

    void Drop() {

    }
}
