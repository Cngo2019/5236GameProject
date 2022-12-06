using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* This scene manager class is only used wehn the player is actively in the game and playing.
**/
public class SceneManagerData : MonoBehaviour
{
    private static SceneManagerData sceneManagerData;

    public static SceneManagerData Instance { get; private set;}

    public string[] nextLevel;
    private int currScene = 0;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            Instance.nextLevel = new string[] {"Room_1", "Upgrade_Room", "Room_2", "Upgrade_Room", "Room_3", "Win"};
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public string getNextScene() {
        Debug.Log(nextLevel[currScene]);
        return nextLevel[currScene++];
    }

    public void resetInstance() {
        currScene = 0;
    }
}
