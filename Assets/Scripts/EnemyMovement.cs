using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{

    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float standStillSeconds;
    [SerializeField] private float playerDamage;
    [SerializeField] private WorldDecomposer wd;

    [SerializeField] private float pathFindingTimer;

    [SerializeField] private float t;

    private float moveTimer;
    private GameObject character;
    private float standStillTimer;

    private Vector2 finalGoal;
    
    private float computeTimer;

    private List<Node> path;
    private Vector2 currentNodeLocation;
    GameObject levelController;
    bool standStill;

    // Start is called before the first frame update
    void Awake() {
        moveTimer = 0f;
        finalGoal = new Vector2();
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
                try {
                    handleMovement();
                } catch(Exception e) {
                    Debug.Log(e);
                }
                
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
        if (computeTimer <= 0) {
            computePath(character);
            computeTimer = pathFindingTimer * Time.deltaTime;
        } else {
            chaseCharacter();
            computeTimer -= Time.deltaTime;
        }
        
    }
    private void chaseCharacter() {
         if (path.Count > 0) {
            if (moveTimer <= 0) {
                Vector2 current = new Vector2(transform.position.x, transform.position.y);
                if (current.Equals(currentNodeLocation)) {
                    // Just continue lerping to our current target location.
                    path.RemoveAt(0);
                    currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
                    transform.position = currentNodeLocation;
                }
                
                moveTimer =  t * Time.deltaTime;
            } else {
                moveTimer -= Time.deltaTime;
            }
                
        } else {
            computePath(character);
        }
    }

    private void moveToNextNode() {
        
        

    }
    private void computePath(GameObject player) {
        int startRow = (int) (transform.position.y + 6 - .5f);
        int startCol = (int) (transform.position.x + 11 - .5f);

        int playerLocationRow = (int) (player.transform.position.y + 6 -.5f);
        int playerLocationCol = (int) (player.transform.position.x + 11 - .5f);
        
        path = PathFinding.generatePath(
            wd.getWorldData(),
            startRow,
            startCol,
            playerLocationRow,
            playerLocationCol
        );

        if (path.Count > 0) {
            currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
        }
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
