using Scripts;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color rainZoneColor;
    [SerializeField] private Color sunZoneColor;

    private void Start() {
        GameManager.Instance.OnZoneChanged += GameManager_OnZoneChanged;
    }

    private void GameManager_OnZoneChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.GetZone == Zone.Sun) {
            backgroundImage.color = sunZoneColor;
        }
        else {
            backgroundImage.color = rainZoneColor;
        }
    }

    private void OnDestroy() {
        GameManager.Instance.OnZoneChanged -= GameManager_OnZoneChanged;
    }
}
