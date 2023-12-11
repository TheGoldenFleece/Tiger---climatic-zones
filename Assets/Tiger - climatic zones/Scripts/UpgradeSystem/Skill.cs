using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Skill
    {
    [field: SerializeField] public int Cost {get;private set;}
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Level { get; private set; }

    public Skill(SkillSO skillSO, int level = 0) {
        Cost = skillSO.Cost;
        Name = skillSO.Name;
        Description = skillSO.Description;
        Level = level;
    }

    public void UpgradeSkill() {
        Level++;
    }

    public string Serialize() {
        string mySkillToJson = "{" +
            $"\"_name\":\"{Name}\"," +
            $"\"_cost\":{Cost}," +
            $"\"_description\":\"{Description}\"," +
            $"\"_level\":{Level}" +
            "}";

        string skillToJson = JsonUtility.ToJson(this);

        PlayerPrefs.SetString($"Player_Skill_{Name}", skillToJson);
        PlayerPrefs.Save();

        return mySkillToJson;

    }

    public Skill Deserialize() {
        string skillFromJson = PlayerPrefs.GetString($"Player_Skill_{Name}", "");
        Skill newSkill = JsonUtility.FromJson<Skill>(skillFromJson);

        return newSkill;
        //PlayerPrefs.SetString($"Player_Skill_{Name}", skillToJson);
        //PlayerPrefs.Save();
    }
}
