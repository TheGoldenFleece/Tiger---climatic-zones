using UnityEngine;

public class Tiger : MonoBehaviour
{
    public static Tiger Instance;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There should be only one Tiger Instance");
        }
        Instance = this;
    }

}
