using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    
    private const string SCORE_PREFS = "Score";

    [SerializeField] private GameObject difficultyUI;
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        playButton.onClick.AddListener(() => Play());
        exitButton.onClick.AddListener(() => Exit());

        scoreText.text = "Best try: " + PlayerPrefs.GetInt(SCORE_PREFS, 0).ToString();
    }

    private void Play() {
        difficultyUI.SetActive(true);
    }

    private void Exit() {
        Application.Quit();
    }

    private void OnDestroy() {
        playButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

}
