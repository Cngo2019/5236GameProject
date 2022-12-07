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

    public string[] roomSequence;
    private int currScene;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            this.initialize();
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void initialize() {
        this.roomSequence = new string[] {"Room_1", "Upgrade_Room", "Room_2", "Upgrade_Room", "Room_3", "Win"};
        this.currScene = 0;
    }


    public string getNextScene() {
        return this.roomSequence[currScene++];
    }

    public void resetInstance() {
        currScene = 0;
    }
}
