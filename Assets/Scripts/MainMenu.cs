using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMainGame() {
        SceneManager.LoadScene("Room_1");
    }

    public void LoadStartMenu() {
        SceneManager.LoadScene("Start");
    }
}
