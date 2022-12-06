using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private float spawnIntervalSec;
    [SerializeField] GameObject enemy;

    [SerializeField] private WorldDecomposer wd;


    private GameObject player;
    
    [SerializeField] private float killRequirement;
    [SerializeField] int spawnCap;
    private float spawnTimer;
    private int current;

    private bool hudInstantiated;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
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
            Debug.Log(spawnCap >= current);
            // If we successfuly spawn a zombie reset the timer.
            if (spawnCap >= current && spawnEnemyZombie()) {
                current += 1;
                spawnTimer = spawnIntervalSec;
            }
                    
        }

         if (killRequirement <= 0) {
            startNextLevel();
         }
    }

    private bool spawnEnemyZombie() {

        Node[,] worldData = wd.getWorldData();
        int spawnCoordinateX = Random.Range(0, wd.getColNum() - 1);
        int spawnCoordinateY = Random.Range(0, wd.getRowNum() - 1);

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

    public void reduceKillCount() {
        this.killRequirement -= 1;
    }

    public void reduceCurrent() {
        this.current -= 1;
    }
    private void startNextLevel() {
        // Go to next room.
        SceneManager.LoadScene(SceneManagerData.Instance.getNextScene());
    }

    public float getKillRequirement() {
        return this.killRequirement;
    }
}
