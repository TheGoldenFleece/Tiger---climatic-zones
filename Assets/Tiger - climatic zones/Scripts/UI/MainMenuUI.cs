using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const string MAIN_SCENE_NAME = "Main";
    private const string SCORE_PREFS = "Score";

    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        playButton.onClick.AddListener(() => Play());
        exitButton.onClick.AddListener(() => Exit());

        scoreText.text = "Best try: " + PlayerPrefs.GetInt(SCORE_PREFS, 0).ToString();
    }

    private void Play() {
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }

    private void Exit() {
        Application.Quit();
    }

    private void OnDestroy() {
        playButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

}
