using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceRelative : MonoBehaviour
{
    private void Start() {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
    }
    
}
