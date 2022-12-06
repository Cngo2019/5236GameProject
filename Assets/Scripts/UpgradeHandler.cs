using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeHandler : MonoBehaviour
{

    [SerializeField] Button bpUpgradeButton;
    [SerializeField] string nextRoom;
    void Awake() {
        if (UpgradeManager.Instance.bulletPenetration) {
            bpUpgradeButton.enabled = false;
        }
    }
    public void UpgradeFiringRate() {
        UpgradeManager.Instance.firingRateCoolDown /= 2;
        SceneManager.LoadScene(nextRoom);
    }

    public void UpgradeBulletPenetration() {
        UpgradeManager.Instance.bulletPenetration = true;
        SceneManager.LoadScene(nextRoom);
    }

    public void UpgradeAmmo() {
        UpgradeManager.Instance.ammoNum *= 2;
        SceneManager.LoadScene(nextRoom);
    }
}
