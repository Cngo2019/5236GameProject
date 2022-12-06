using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    private static UpgradeManager upgradeManager;

    public static UpgradeManager Instance { get; private set;}

    public float firingRateCoolDown = .5f;
    public int bulletPenetrationLives = 1;
    public float ammoNum = 30;

    public float currentHealth = 150f;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject); 
    }

    public void resetInstance() {
        firingRateCoolDown = .5f;
        bulletPenetrationLives = 1;
        ammoNum = 30;
        currentHealth = 150f;
    }
}
