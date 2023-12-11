using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class PlayerStats {

    private const string PLAYER_SKILLS = "Player_Skills";
    private const string PLAYER_MONEY = "Player_Money";

    public static Difficulty GameDifficulty { get; set; } = Difficulty.Medium;

    public static int Score { get; private set; } = 0;
    public static void ChangeScore(int value) {
        Score += value;
    }
    public static int Money { get; private set; } = 0;
    public static void AddMoney(int value) {

        Money += value;
    }
    public static void SaveMoney() {

        PlayerPrefs.SetInt(PLAYER_MONEY, Money);
    }

    public static List<Skill> SkillList { private set; get; } = new List<Skill>();
    public static void SaveSkills() {

        string skillsJson = JsonUtility.ToJson(new SkillListWrapper { Skills = SkillList });
        PlayerPrefs.SetString(PLAYER_SKILLS, skillsJson);

        SaveMoney();

        PlayerPrefs.Save();
    }
    public static void AddSkill(Skill skill) {

        Skill updatedSkill = GetSkill(skill.Name);

        if (updatedSkill != null) {

            SkillList[SkillList.IndexOf(updatedSkill)] = skill;

            return;

        }

        SkillList.Add(skill);
    }

    public static Skill GetSkill(string name) {
        return SkillList.Find(skill => skill.Name == name);
    }
}
