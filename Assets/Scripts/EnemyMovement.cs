using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{

    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float standStillSeconds;
    [SerializeField] private float playerDamage;
    [SerializeField] private WorldDecomposer wd;

    [SerializeField] private float pathFindingTimer;

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
                handleMovement();
            }
        }
        else {
            standStillTimer -= Time.deltaTime;
        }


         if (hp <= 0) {
            levelController.GetComponent<LevelController>().reduceKillCount();
            GameObject.Destroy(gameObject);
        }
        
        if (levelController.GetComponent<LevelController>().getKillRequirement() <= 0) {
            GameObject.Destroy(gameObject);
        }
        
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
            // If our leader is already at the current node location then move on to the next location in the path list
            Vector2 c = new Vector2(transform.position.x, transform.position.y);
            Vector2 ch = new Vector2(character.transform.position.x, character.transform.position.y);
            if (c == currentNodeLocation) {
                //Debug.Log(currentNodeLocation);
                // Then go to the next node in the path list
                currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
                path.RemoveAt(0);
            }

            if (Mathf.Abs((c - ch).magnitude) <= .99f) {
                transform.position = finalGoal;
                path = new List<Node>();
            } else {
                // Just continue lerping to our current target location.
                transform.position = Vector2.MoveTowards(c, currentNodeLocation, speed * Time.deltaTime);
            } 
        }
        

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
            finalGoal = new Vector2(path[path.Count - 1].getWorldX(), path[path.Count - 1].getWorldZ());
            // Set the currentNodeLocation to be the first node to travel to from the set of path locations.
            currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {

        GameObject obj = collision.gameObject;

        Debug.Log(obj.tag);
        if (obj.tag == "Bullet") {
            hp -= 50;
        }

        if (obj.tag == "Player") {
            Debug.Log("We bit the player");
            standStillTimer = standStillSeconds * Time.deltaTime;
        }

    }
}
