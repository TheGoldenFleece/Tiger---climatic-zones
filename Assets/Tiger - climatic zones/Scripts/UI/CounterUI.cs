using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    public static CounterUI Instance { private set; get; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There should be only one CounterUI Instance");
        }
        Instance = this;

    }

    private void Update() {
        counterText.text = ((int)GameManager.Counter).ToString();
    }

}
