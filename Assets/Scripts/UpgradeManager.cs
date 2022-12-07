using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    private static UpgradeManager upgradeManager;

    public static UpgradeManager Instance { get; private set;}

    public float firingRateCoolDown;
    public int bulletPenetrationLives;
    public float ammoNum;

    public float currentHealth;

    public float canWalkCoolDown;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            resetInstance();
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject); 
    }

    public void resetInstance() {
        this.firingRateCoolDown = .5f;
        this.bulletPenetrationLives = 1;
        this.ammoNum = 30;
        this.currentHealth = Mathf.Infinity;
    }
}
