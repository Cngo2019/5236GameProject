using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{

    [SerializeField] private float hp;
    [SerializeField] private float standStillSeconds;
    [SerializeField] private float playerDamage;
    [SerializeField] private WorldDecomposer wd;

    [SerializeField] private float pathFindingTime;

    [SerializeField] private float t;

    private int pathIndex;

    private GameObject character;
    private float standStillTimer;
    
    private float computeTimer;

    private List<Node> path;
    private Vector2 currentNodeLocation;
    GameObject levelController;
    bool standStill;

    // Start is called before the first frame update
    void Awake() {
        standStillTimer = 0;
        levelController = GameObject.Find("LevelController");
        path = new List<Node>();
        computeTimer = 0;
        wd = GameObject.Find("WorldDecomposer").GetComponent<WorldDecomposer>();
        character = GameObject.Find("Character");
    }
    // Update is called once per frame
    void Update()
    {
        
        if (standStillTimer <= 0) {
            if (character != null) {
                handleMovement();
            }
        }
        else {
            standStillTimer -= Time.deltaTime;
        }


        //  if (hp <= 0) {
        //     levelController.GetComponent<LevelController>().reduceKillCount();
        //     GameObject.Destroy(gameObject);
        // }
        
        // if (levelController.GetComponent<LevelController>().getKillRequirement() <= 0) {
        //     GameObject.Destroy(gameObject);
        // }
        
    }

    private void handleMovement() {
        if (computeTimer > 0) {
            chaseCharacter();
            computeTimer -= Time.deltaTime;
        } else {
            computePath(character);
        }
    }
    private void chaseCharacter() {
         if (pathIndex < path.Count) {
                Vector2 current = new Vector2(transform.position.x, transform.position.y);
                if (Vector2.Distance(current, currentNodeLocation) <= .1f) {
                    pathIndex++;
                    if (pathIndex < path.Count)
                        currentNodeLocation = new Vector2(path[pathIndex].getWorldX(), path[pathIndex].getWorldZ());
                }
                transform.position = Vector2.MoveTowards(transform.position, currentNodeLocation, t * Time.deltaTime);
        } else {
            computePath(character);
            if (path.Count > 0) {
                pathIndex = 0;
                currentNodeLocation = new Vector2(path[pathIndex].getWorldX(), path[pathIndex].getWorldZ());
            }
        }
    }


    private void computePath(GameObject player) {


        int startRow =  (int) (getNearestWorldNode(transform.position.y) + 6 - .5f);
        int startCol =  (int) (getNearestWorldNode(transform.position.x) + 11 - .5f);


        int playerLocationRow = (int) (player.transform.position.y + 6 -.5f);
        int playerLocationCol = (int) (player.transform.position.x + 11 - .5f);
        
        path = PathFinding.generatePath(
            wd.getWorldData(),
            startRow,
            startCol,
            playerLocationRow,
            playerLocationCol
        );

        computeTimer = pathFindingTime * Time.deltaTime;

    }

    private float getNearestWorldNode(float y) {
        float y1 = (int) y;
        float y_top_nearest = Mathf.Abs((y1 - .5f) - y);
        float y_bottom_nearest = Mathf.Abs((y1 + .5f) - y);

        if (y_top_nearest < y_bottom_nearest)
            return y1 - .5f;
        return y1 + .5f;
    }


    void OnTriggerEnter2D(Collider2D collision) {

        GameObject obj = collision.gameObject;

        Debug.Log(obj.tag);
        if (obj.tag == "Bullet") {
            hp -= 50;
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player") {
            if (standStillTimer <= 0) {
                standStillTimer = standStillSeconds * Time.deltaTime;
                obj.GetComponent<Character>().reduceHealth(2f);
            }
            return;
        }
    }

    public bool enemyCanDamagePlayer() {
        if (standStillTimer <= 0) {
            return true;
        }

        return false;

    }
}
