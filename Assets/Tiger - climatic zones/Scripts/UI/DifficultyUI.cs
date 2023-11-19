using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    private const string MAIN_SCENE_NAME = "Main";

    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    private void Start() {
        easyButton.onClick.AddListener(() => Play(Difficulty.Easy));
        mediumButton.onClick.AddListener(() => Play(Difficulty.Medium));
        hardButton.onClick.AddListener(() => Play(Difficulty.Hard));

        this.gameObject.SetActive(false);
    }

    private void Play(Difficulty difficulty) {
        PlayerStats.GameDifficulty = difficulty;
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }

    private void OnDestroy() {
        easyButton.onClick.RemoveAllListeners();
        mediumButton.onClick.RemoveAllListeners();
        hardButton.onClick.RemoveAllListeners();
    }
}
