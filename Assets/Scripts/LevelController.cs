using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private float spawnIntervalSec;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;

    private GameObject player;
    
    private float killRequirement;
    private float spawnTimer;
    private int level;

    private bool hudInstantiated;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        spawnTimer = spawnIntervalSec;
        killRequirement = 10;
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

            
            player.GetComponent<Character>().decreaseShootCD();
            player.GetComponent<Character>().increaseMS();
            startNextLevel();
         }
    }

    private void spawnEnemyZombie() {
        float cameraHorizontalBoundary = CameraBoundary.getCameraHorizontalBoundary("MainCamera");
        float cameraVerticalBoundary = CameraBoundary.getCameraVerticalBoundary("MainCamera");

        float spawnCoordinateX = randomizeSpawn(cameraHorizontalBoundary);
        float spawnCoordinateY = randomizeSpawn(cameraVerticalBoundary);

        if (level == 1) {
            GameObject zombieInstance = Instantiate(
                enemy,
                new Vector3(spawnCoordinateX, spawnCoordinateY, 0f),
                Quaternion.identity
            );
        }

        if (level == 2) {
            GameObject zombieInstance = Instantiate(
                enemy2,
                new Vector3(spawnCoordinateX, spawnCoordinateY, 0f),
                Quaternion.identity
            );
        }

        if (level == 3) {
            GameObject zombieInstance = Instantiate(
                enemy3,
                new Vector3(spawnCoordinateX, spawnCoordinateY, 0f),
                Quaternion.identity
            );
        }
            

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
        Debug.Log(killRequirement);
    }

    private void startNextLevel() {
        level += 1;
        spawnIntervalSec -= 1;
        killRequirement = level * 5;
        player.GetComponent<Character>().setHealth(100);
    }

    public float getKillRequirement() {
        return this.killRequirement;
    }
}
