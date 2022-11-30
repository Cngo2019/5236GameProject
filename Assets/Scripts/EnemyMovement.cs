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
    
    private float computeTimer;

    private List<Node> path;
    private Vector2 currentNodeLocation;
    GameObject levelController;
    bool standStill;
    private float timer;
    bool flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        standStill = false;
        levelController = GameObject.Find("LevelController");
        path = new List<Node>();
        computeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();

        if (standStill) {
            if (timer >= 0) {
            timer -= Time.deltaTime;;
            } else {
                standStill = false;
            }
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
        if (GameObject.Find("Character")) {
            if (computeTimer <= 0) {
                Vector2 charPosition = GameObject.Find("Character").transform.position;
                computePath(GameObject.Find("Character"));
                computeTimer = 160 * Time.deltaTime;
            } else {
                computeTimer -= Time.deltaTime;
            } 
        }

        chaseCharacter();
    }
    private void chaseCharacter() {
         if (path.Count > 0) {
            // If our leader is already at the current node location then move on to the next location in the path list
            if (Mathf.Approximately(transform.position.magnitude, currentNodeLocation.magnitude)) {
                //Debug.Log(currentNodeLocation);
                // Then go to the next node in the path list
                currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
                path.RemoveAt(0);
            }

            // Just continue lerping to our current target location.
            transform.position = Vector2.Lerp(transform.position, currentNodeLocation, 1);
           // Debug.Log("Curr position " + currentNodeLocation);

        } else {
            flag = false;
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
        Vector2 f = new Vector2(path[path.Count - 1].getWorldX(), path[path.Count - 1].getWorldZ());

        if (path.Count > 0) {
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

    }
}
