using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour

{

    [SerializeField] private float hp;
    [SerializeField] private float standStillSeconds;
    [SerializeField] private float playerDamage;
    

    [SerializeField] private float pathFindingTime;

    [SerializeField] private float t;

    [SerializeField] private GameObject ammo;

    private WorldDecomposer wd;
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


        if (hp <= 0) {
            levelController.GetComponent<LevelController>().reduceKillCount();
            levelController.GetComponent<LevelController>().reduceCurrent();
            spawnAmmo();
            GameObject.Destroy(gameObject);
        }

        if (character != null) {
            Vector2 direction = character.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));
        }
        
        if (levelController.GetComponent<LevelController>().getKillRequirement() <= 0) {
            GameObject.Destroy(gameObject);
        }


        
    }

    private void handleMovement() {
        if (computeTimer <= 0 || path.Count <= pathIndex) {
            computePath(character);
        }
        else {
            chaseCharacter();
        }
    }
    private void chaseCharacter() {
        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        if (Vector2.Distance(current, currentNodeLocation) <= .1f) {
            pathIndex++;
            if (pathIndex < path.Count)
                currentNodeLocation = new Vector2(path[pathIndex].getWorldX(), path[pathIndex].getWorldZ());
        }
        transform.position = Vector2.MoveTowards(transform.position, currentNodeLocation, t * Time.deltaTime);
        computeTimer -= Time.deltaTime;
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

        if (path.Count > 0) {
            pathIndex = 0;
            currentNodeLocation = new Vector2(path[pathIndex].getWorldX(), path[pathIndex].getWorldZ());
            computeTimer = pathFindingTime * Time.deltaTime;
        } else {
            // Means I have spawned somewhere that is not pathable to player
            GameObject.Destroy(gameObject);
        }
        

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
                obj.GetComponent<Character>().reduceHealth(playerDamage);
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

    private void spawnAmmo() {
        Vector3 worldPosition = new Vector3(getNearestWorldNode(transform.position.x), getNearestWorldNode(transform.position.y), 0f);
        GameObject ammoInstance = Instantiate(ammo, worldPosition, Quaternion.identity);
    }
}
