using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private float spawnIntervalSec;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnIntervalSec;

    }

    // Update is called once per frame
    void Update()
    {
        // if (spawnTimer >= 0) {
        //     spawnTimer -= Time.deltaTime;
        // } else {
        //     // spawn enemy outside of camera view
        //     spawnEnemyZombie();
        //     // reset spawn timer
        //     spawnTimer = spawnIntervalSec;
        // }

        float x = 0f;
        float y = 0f;
        Vector3 origin = new Vector3(x, y, 0);

        
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit) {
            Debug.Log("Hit!");
        } 

    }

    private void spawnEnemyZombie() {
        
    }
}
