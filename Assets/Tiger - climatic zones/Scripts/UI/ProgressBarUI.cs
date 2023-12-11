using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform progressItemUnitPrefab;
    [SerializeField] private RectTransform parent;

    public void Display(int value) {
        foreach (RectTransform child in parent) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < value; i++) {
            Instantiate(progressItemUnitPrefab, parent);
        }
    }
}
