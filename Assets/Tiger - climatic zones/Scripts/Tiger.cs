using UnityEngine;
using Scripts;

public class Tiger : MonoBehaviour {
    public static Tiger Instance;
    private static Tiger tiger;

    public static Tiger GetTiger => tiger;
    public static void SetTiger(TigerState tigerState) {

        if (tiger != null) {
            Destroy(tiger.gameObject);
        }

        Tiger tigerToSet = GameManager.Instance.GetTigerType(tigerState);
        tiger = Instantiate(tigerToSet, tigerToSet.transform.position, Quaternion.identity);
    }

    [SerializeField] private TigerState tigerState;
    public TigerState TigerState => tigerState;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There should be only one Tiger Instance");
        }
        Instance = this;

        if (tigerState == TigerState.Walk) {
            
            SetMovableZone();
        }
    }

    public void SetMovableZone() {
        RandomMovementController moveController = this.transform.GetComponent<RandomMovementController>();

        moveController.SetCorners(RainZoneController.Instance.GetStartCorner.position, RainZoneController.Instance.GetEndCorner.position);

        moveController.enabled = false;
        moveController.SetSpeed(GameManager.Instance.GDSO.tigerSpeed);
    }
}
