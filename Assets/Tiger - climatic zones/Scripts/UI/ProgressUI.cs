using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private Transform progressBar;
    [SerializeField] private Transform progressItem;
    [SerializeField] private int maxAmount;

    private int _amount;

    private void Awake() {
        //_amount = PlayerStats.;
    }
    private void Start() {
        
    }
}
