using System.Collections;
using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    private const string POPUP_ANIMATION_TRIGGER = "Popup";

    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Animator animator;

    private void Start() {

        AwardSystem.Instance.OnCombo += AwardSystem_OnCombo;
    }

    private void AwardSystem_OnCombo(object sender, System.EventArgs e) {
        Display();
    }

    private void Display() {

        comboText.text = "x" + AwardSystem.Instance.Combo.ToString();
        animator.SetTrigger(POPUP_ANIMATION_TRIGGER);
    }

    private void OnDestroy() {
        AwardSystem.Instance.OnCombo -= AwardSystem_OnCombo;
    }
}
