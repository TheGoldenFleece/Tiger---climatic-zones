using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private const string MENU_SCENE_NAME = "Menu";

    [SerializeField] private Button homeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private void Start() {
        homeButton.onClick.AddListener(() => Home());
        pauseButton.onClick.AddListener(() => Pause());
    }

    private void Home() {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }

    private void Pause() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void OnDestroy() {
        homeButton.onClick.RemoveAllListeners();
        pauseButton.onClick.RemoveAllListeners();
    }
}
