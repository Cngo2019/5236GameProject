using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private float spawnIntervalSec;
    [SerializeField] GameObject enemy;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnIntervalSec;

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= 0) {
             spawnTimer -= Time.deltaTime;
         } else {
            //spawn enemy outside of camera view
            spawnEnemyZombie();
           // reset spawn timer
            spawnTimer = spawnIntervalSec;
         }

    }

    private void spawnEnemyZombie() {
        float cameraHorizontalBoundary = CameraBoundary.getCameraHorizontalBoundary("MainCamera");
        float cameraVerticalBoundary = CameraBoundary.getCameraVerticalBoundary("MainCamera");

        float spawnCoordinateX = randomizeSpawn(cameraHorizontalBoundary);
        float spawnCoordinateY = randomizeSpawn(cameraVerticalBoundary);


        GameObject bulletInstance = Instantiate(
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
}
