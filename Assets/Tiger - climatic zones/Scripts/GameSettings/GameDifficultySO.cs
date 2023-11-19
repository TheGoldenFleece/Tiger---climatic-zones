using UnityEngine;

[CreateAssetMenu(fileName = "GameDifficulty", menuName = "ScriptableObjects/GameDifficulty", order = 1)]
public class GameDifficultySO: ScriptableObject {
    public Transform umbrellaPrefab;
    public float zoneDuration;
    public int sunrayAmount;
    public int HP;
    public int scorePerSecond;
    public int damagePerSecond;
}
