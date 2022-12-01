using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private float spawnIntervalSec;
    [SerializeField] GameObject enemy;

    [SerializeField] string nextRoom;

    [SerializeField] private WorldDecomposer wd;


    private GameObject player;
    
    [SerializeField] private float killRequirement;
    private float spawnTimer;

    private bool hudInstantiated;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnIntervalSec;
        player = GameObject.Find("Character");
        wd = GameObject.Find("WorldDecomposer").GetComponent<WorldDecomposer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= 0) {
             spawnTimer -= Time.deltaTime;
         } else {
            // If we successfuly spawn a zombie reset the timer.
            if (spawnEnemyZombie()) {
                spawnTimer = spawnIntervalSec;
            }
                    
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

    private bool spawnEnemyZombie() {

        Node[,] worldData = wd.getWorldData();
        int spawnCoordinateX = Random.Range(0, wd.getColNum());
        int spawnCoordinateY = Random.Range(0, wd.getRowNum());

        if (worldData[spawnCoordinateY, spawnCoordinateX].getIsPathable()) {
            
            GameObject zombieInstance = Instantiate(
                    enemy,
                    new Vector3(
                        worldData[spawnCoordinateY, spawnCoordinateX].getWorldX(), worldData[spawnCoordinateY, spawnCoordinateX].getWorldZ(), 0f)
                    ,
                    Quaternion.identity
            );
            return true;
        }

        return false;
            
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
