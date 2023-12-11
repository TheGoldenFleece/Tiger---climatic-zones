using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "SkillsSO/Skill", order = 2)]
public class SkillSO : ScriptableObject
{
    public int Cost;
    public string Name;
    public string Description;
}
