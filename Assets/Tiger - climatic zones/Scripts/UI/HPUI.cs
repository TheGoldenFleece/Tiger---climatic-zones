using System;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public static HPUI Instance;

    [SerializeField] private Color unharmedColor;
    [SerializeField] private Color harmedColor;

    [SerializeField] private Image[] heartImagesArray;

    public event EventHandler OnNoHealthLeft;

    private float HP;
    private float maxHP;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There should be only one HPUI Instance");
        }
        Instance = this;
    }

    private void Start() {
        maxHP = GameManager.Instance.GDSO.HP;
        HP = maxHP;

        foreach (Image image in heartImagesArray) {
            image.color = unharmedColor;
        }
    }

    private void Update() {
        Debug.Log(HP);
    }

    public void GetDamage(float damage) {
        HP -= damage;

        DisplayHP();

        if (HP <= 0) {
            OnNoHealthLeft?.Invoke(this, EventArgs.Empty);
            this.enabled = false;
        }

    }

    private void DisplayHP() {
        int unharmedHeart = (int)Math.Ceiling(HP * heartImagesArray.Length / maxHP);
        unharmedHeart = Math.Clamp(unharmedHeart, 0, heartImagesArray.Length);
        for (int i = heartImagesArray.Length - 1; i > unharmedHeart - 1; i--) {
            heartImagesArray[i].color = harmedColor;
        }
    }
}
