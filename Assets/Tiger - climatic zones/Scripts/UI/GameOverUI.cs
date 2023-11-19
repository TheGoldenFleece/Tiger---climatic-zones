using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string MENU_SCENE_NAME = "Menu";
    private const string SCORE_PREFS = "Score";

    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        retryButton.onClick.AddListener(() => Retry());
        menuButton.onClick.AddListener(() => Menu());
        exitButton.onClick.AddListener(() => Exit());

        HPUI.Instance.OnNoHealthLeft += HPUI_OnNoHealthLeft;

        this.gameObject.SetActive(false);
    }

    private void HPUI_OnNoHealthLeft(object sender, System.EventArgs e) {
        Time.timeScale = 0.0f;
        this.gameObject.SetActive(true);

        int score = (int)GameManager.Counter;
        scoreText.text = "Score: " + score.ToString();
        int lastScore = PlayerPrefs.GetInt(SCORE_PREFS, 0);

        if (score > lastScore)
        {
            PlayerPrefs.SetInt(SCORE_PREFS, score);
            PlayerPrefs.Save();
        }
    }

    private void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Menu() {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }
    private void Exit() {
        Application.Quit();
    }

    private void OnDestroy() {
        retryButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
