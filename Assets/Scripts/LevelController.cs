using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private float spawnIntervalSec;
    [SerializeField] GameObject enemy;

    [SerializeField] string nextRoom;

    private GameObject player;
    
    [SerializeField] private float killRequirement;
    [SerializeField] private float spawnTimer;

    private bool hudInstantiated;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnIntervalSec;
        player = GameObject.Find("Character");

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= 0) {
             spawnTimer -= Time.deltaTime;
         } else {
            //spawn enemy outside of camera view
            if (killRequirement > 0)
                spawnEnemyZombie();
           // reset spawn timer
            spawnTimer = spawnIntervalSec;
         }

         if (killRequirement <= 0) {
            /**
            Display HUD option 285, 193
            If they press a key then start the next level
            **/
            //player.GetComponent<Character>().decreaseShootCD();
            //player.GetComponent<Character>().increaseMS();
            startNextLevel();
         }
    }

    private void spawnEnemyZombie() {
        float cameraHorizontalBoundary = CameraBoundary.getCameraHorizontalBoundary("MainCamera");
        float cameraVerticalBoundary = CameraBoundary.getCameraVerticalBoundary("MainCamera");

        float spawnCoordinateX = randomizeSpawn(cameraHorizontalBoundary);
        float spawnCoordinateY = randomizeSpawn(cameraVerticalBoundary);

        GameObject zombieInstance = Instantiate(
                enemy,
                new Vector3(spawnCoordinateX, spawnCoordinateY, 0f),
                Quaternion.identity
            );
        
    }

    private float randomizeSpawn(float boundaryCoordinate) {
        int randInt = Random.Range(0, 2);
        int spawnArea = Random.Range(0, 10);
        if (randInt == 0) {
            boundaryCoordinate = -1f * boundaryCoordinate;
            boundaryCoordinate -= spawnArea * 1f;
        }

        boundaryCoordinate += spawnArea * 1f;

        return boundaryCoordinate;

    }

    public void reduceKillCount() {
        this.killRequirement -= 1;
    }

    private void startNextLevel() {
        // Go to next room.
        SceneManager.LoadScene(nextRoom);
    }

    public float getKillRequirement() {
        return this.killRequirement;
    }
}
