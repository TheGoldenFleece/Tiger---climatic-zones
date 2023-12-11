using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public static HPUI Instance;

    private const string STRENGTH_SKILL_NAME = "Strength";

    [SerializeField] private Color unharmedColor;
    [SerializeField] private Color harmedColor;

    [SerializeField] private Image heartPrefab;

    public event EventHandler OnNoHealthLeft;

    private List<Image> heartImagesArray;

    private float HP;
    private float maxHP;
    private float defaultHP = 100;
    private float HPByHeart = 20;
    private int defaultHeartsAmount = 5;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There should be only one HPUI Instance");
        }

        Instance = this;
    }

    private void Start() {

        int heartsAmount = defaultHeartsAmount + PlayerStats.GetSkill(STRENGTH_SKILL_NAME).Level;

        HP = heartsAmount * HPByHeart;
        maxHP = HP;

        heartImagesArray = new List<Image>();
        for (int i = 0; i < heartsAmount; i++) {
            Image heartImage = Instantiate(heartPrefab, this.transform);
            heartImage.color = unharmedColor;
            heartImagesArray.Add(heartImage);
        }

    }

    private void Update() {

        //Debug.Log(HP);
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
        int heartsAmount = heartImagesArray.Count;
        int unharmedHeart = (int)Math.Ceiling(HP * heartsAmount / maxHP);

        unharmedHeart = Math.Clamp(unharmedHeart, 0, heartsAmount);
        for (int i = heartsAmount - 1; i >= unharmedHeart; i--) {
            heartImagesArray[i].color = harmedColor;
        }
    }

}
