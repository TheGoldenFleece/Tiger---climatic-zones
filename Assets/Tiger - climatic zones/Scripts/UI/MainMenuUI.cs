using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const string SCORE_PREFS = "Score";

    [SerializeField] private GameObject difficultyUI;
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        playButton.onClick.AddListener(() => Play());
        exitButton.onClick.AddListener(() => Exit());
        storeButton.onClick.AddListener(() => Store());
        settingsButton.onClick.AddListener(() => Settings());

        scoreText.text = "Best try: " + PlayerPrefs.GetInt(SCORE_PREFS, 0).ToString();
    }

    private void Store() {
        SceneManager.LoadScene(GameScene.Store.ToString());
    }

    private void Settings() {
        SceneManager.LoadScene(GameScene.Settings.ToString());
    }

    private void Play() {
        this.gameObject.SetActive(false);
        difficultyUI.SetActive(true);
    }

    private void Exit() {
        Application.Quit();
    }

    private void OnDestroy() {
        playButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        storeButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
    }

}
