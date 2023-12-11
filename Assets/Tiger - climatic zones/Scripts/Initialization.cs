using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public static Initialization Instance { get; private set; }

    private const string PLAYER_SKILLS = "Player_Skills";
    private const string PLAYER_MONEY = "Player_Money";

    [SerializeField] private SkillSO[] SkillSOArray;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There should be only one Initialization Instance");
        }
        Instance = this;

        Initialize();
    }

    public void Initialize() {
        InitializeMoney();
        InitializeSkills();
    }

    private void InitializeSkills() {
        List<Skill> skillList = new List<Skill>();

        string skillsJson = PlayerPrefs.GetString(PLAYER_SKILLS);

        if (string.IsNullOrEmpty(skillsJson)) {

            foreach (SkillSO skillSO in SkillSOArray) {
                Skill skill = new Skill(skillSO);
                skillList.Add(skill);
            }

            skillsJson = JsonUtility.ToJson(new SkillListWrapper { Skills = skillList });
            PlayerPrefs.SetString(PLAYER_SKILLS, skillsJson);
        }

        skillList = JsonUtility.FromJson<SkillListWrapper>(skillsJson).Skills;

        foreach (Skill skill in skillList) {
            PlayerStats.AddSkill(skill);
        }

    }

    private void InitializeMoney() {
        int money = PlayerPrefs.GetInt(PLAYER_MONEY, 0);

        if (money == 0 && PlayerStats.Money != 0) {
            money -= PlayerStats.Money;
        }

        PlayerStats.AddMoney(money);
    }
}


