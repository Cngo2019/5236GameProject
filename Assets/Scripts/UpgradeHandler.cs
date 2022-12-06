using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeHandler : MonoBehaviour
{

    [SerializeField] Button bpUpgradeButton;
    void Awake() {
        if (UpgradeManager.Instance.bulletPenetration) {
            bpUpgradeButton.enabled = false;
        }
    }
    public void UpgradeFiringRate() {
        UpgradeManager.Instance.firingRateCoolDown /= 2;
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

    public void UpgradeBulletPenetration() {
        UpgradeManager.Instance.bulletPenetration = true;
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

    public void UpgradeAmmo() {
        UpgradeManager.Instance.ammoNum *= 2;
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

}
