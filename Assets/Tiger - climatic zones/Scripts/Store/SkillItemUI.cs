using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private RectTransform maxLevelPanel;
    [SerializeField] private ProgressBarUI progressBarUI;

    private int maxSkillLevel = 4;
    private Skill skill;

    private void Awake() {
        costText.text = skillSO.Cost.ToString();
        nameText.text = skillSO.Name;
        descriptionText.text = skillSO.Description;
    }

    private void Start() {
        upgradeButton.onClick.AddListener(() => UpgradeSkill());

        upgradeButton.gameObject.SetActive(true);
        maxLevelPanel.gameObject.SetActive(false);

        skill = PlayerStats.SkillList.FirstOrDefault(skill => skill.Name == skillSO.Name);

        DisplayProgress();
    }

    private void UpgradeSkill() {
        int cost = skill.Level == 0? skill.Cost : skill.Cost * skill.Level;
        if (PlayerStats.Money < cost) return;

        PlayerStats.AddMoney(cost * -1);
        skill.UpgradeSkill();

        DisplayProgress();
    }

    private void DisplayProgress() {
        if (skill.Level == maxSkillLevel) {
            upgradeButton.gameObject.SetActive(false);
            maxLevelPanel.gameObject.SetActive(true);

        }
        else {
            upgradeButton.gameObject.SetActive(true);
            maxLevelPanel.gameObject.SetActive(false);
        }

        progressBarUI.Display(skill.Level);
    }

    private void OnDestroy() {
        upgradeButton.onClick.RemoveAllListeners();
    }
}
