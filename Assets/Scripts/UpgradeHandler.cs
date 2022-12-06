using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeHandler : MonoBehaviour
{

    [SerializeField] Button bpUpgradeButton;
    public void UpgradeFiringRate() {
        UpgradeManager.Instance.firingRateCoolDown /= 1.5f;
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

    public void UpgradeBulletPenetration() {
        UpgradeManager.Instance.bulletPenetrationLives += 1;
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

    public void UpgradeAmmo() {
        UpgradeManager.Instance.ammoNum *= 2;
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

}
