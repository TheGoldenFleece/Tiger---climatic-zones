using System;
using UnityEngine;

public class AwardSystem : MonoBehaviour
{
    public static AwardSystem Instance { get; private set; }
    public event EventHandler OnCombo;

    public int Combo { private set; get; }

    private float defaultMoneyAward = 1;
    private float difficultyMoneyCoefficient;
    private float timer;
    private float comboDelta = .5f;

    private void Awake() {

        if (Instance != null) {
            Debug.LogError("More than one AwardSystem instance");
        }
        Instance = this;

        Combo = 1;
        timer = 0;
    }

    private void Start() {

        Sunray.OnSunrayCatch += Sunray_OnSunrayCatch;

        difficultyMoneyCoefficient = GameManager.Instance.GDSO.moneyCoefficient;
    }

    private void Sunray_OnSunrayCatch(object sender, System.EventArgs e) {
        if (timer > 0) {

            Combo++;
            int money = (int)(defaultMoneyAward * difficultyMoneyCoefficient * Combo);
            Debug.Log(money);
            PlayerStats.AddMoney(money);

            //Debug.Log(0);

            OnCombo?.Invoke(this, EventArgs.Empty);
        }
        else {
            Combo = 1;
        }

        timer = comboDelta;
    }

    private void Update() {

        if (timer > 0) {

            timer -= Time.deltaTime;
        }
    }

    private void OnDestroy() {
        Sunray.OnSunrayCatch -= Sunray_OnSunrayCatch;
    }
}
