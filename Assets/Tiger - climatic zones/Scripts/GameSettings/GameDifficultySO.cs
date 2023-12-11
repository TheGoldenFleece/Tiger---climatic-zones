using UnityEngine;

[CreateAssetMenu(fileName = "GameDifficulty", menuName = "ScriptableObjects/GameDifficulty", order = 1)]
public class GameDifficultySO: ScriptableObject {

    public Transform umbrellaPrefab;
    public Difficulty difficulty;
    public float zoneDuration;
    public int scorePerSecond;
    public int damagePerSecond;
    public float moneyCoefficient;
    public float tigerSpeed;

    [Header("Sunray Zone")]
    public int sunrayAmount;
    public float sunraySpawnRate;
    public float sunraySpeed;
    
}
