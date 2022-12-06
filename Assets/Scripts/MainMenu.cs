using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMainGame() {
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

    public void loadInstructions() {
        SceneManager.LoadScene("Instructions");
    }

    public void LoadStartMenu() {
        SceneManagerData.Instance.resetInstance();
        UpgradeManager.Instance.resetInstance();
        SceneManager.LoadScene("Start");
    }
}
