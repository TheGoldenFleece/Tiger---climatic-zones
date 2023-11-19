using UnityEngine;

[CreateAssetMenu(fileName = "GameDifficulty", menuName = "ScriptableObjects/GameDifficulty", order = 1)]
public class GameDifficulty: ScriptableObject {
    public Transform umbrellaPrefab;
    public float zoneDuration;
    public float sunrayAmount;
    public int HP;
    public int scorePerSecond;
    public int damagePerSecond;
}
