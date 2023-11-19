using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [SerializeField] private Collider2D safeArea;
    public Collider2D SafeArea => safeArea;
}
