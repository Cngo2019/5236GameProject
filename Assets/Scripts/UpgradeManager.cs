using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    private static UpgradeManager upgradeManager;

    public static UpgradeManager Instance { get; private set;}

    public float firingRateCoolDown = .5f;
    public bool bulletPenetration = false;
    public float ammoNum = 30;

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
        bulletPenetration = false;
        ammoNum = 30;
    }
}
