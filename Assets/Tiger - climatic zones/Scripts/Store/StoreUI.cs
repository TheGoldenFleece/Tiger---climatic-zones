using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start() {
        saveButton.onClick.AddListener(() => PlayerStats.SaveSkills());
        mainMenuButton.onClick.AddListener(() => MainMenu());
    }

    private void Update() {
        moneyText.text = PlayerStats.Money.ToString();
    }

    private void MainMenu() {
        SceneManager.LoadScene(GameScene.Menu.ToString());
    }

    private void OnDestroy() {
        saveButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

}