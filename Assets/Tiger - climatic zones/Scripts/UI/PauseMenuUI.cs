using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;

    private void Start() {
        resumeButton.onClick.AddListener(() => Resume());
        retryButton.onClick.AddListener(() => Retry());
        exitButton.onClick.AddListener(() => Exit());

        this.gameObject.SetActive(false);
    }

    private void Resume() {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    private void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Exit() {
        Application.Quit();
    }

    private void OnDestroy() {
        resumeButton.onClick.RemoveAllListeners();
        retryButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
