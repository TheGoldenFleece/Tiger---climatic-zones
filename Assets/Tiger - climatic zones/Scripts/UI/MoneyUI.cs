using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Update() {
        moneyText.text = PlayerStats.Money.ToString();
    }
}
