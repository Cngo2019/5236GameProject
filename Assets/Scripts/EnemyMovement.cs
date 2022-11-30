using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float hp;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float standStillSeconds;
    [SerializeField] private float playerDamage;
    [SerializeField] private WorldDecomposer wd;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && !flag) {
            // This will be replaced with A star soon
            Vector2 charPosition = GameObject.Find("Character").transform.position;
            computePath(GameObject.Find("Character"));
            flag = true;
        }

        if (flag) {
            chaseCharacter();
        }

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

    private void chaseCharacter() {
        //Vector2 velocityOutput = KinematicArrive.getSteering(transform.position, charPosition, maxSpeed, maxSpeed);
        //rb.velocity = velocityOutput;

         if (path.Count > 0) {
            // If our leader is already at the current node location then move on to the next location in the path list
            if (transform.position.Equals(currentNodeLocation)) {
                //Debug.Log(currentNodeLocation);
                // Then go to the next node in the path list
                currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
                Debug.Log(currentNodeLocation);
                path.RemoveAt(0);
            }

            // Just continue lerping to our current target location.
            transform.position = Vector2.Lerp(transform.position, currentNodeLocation, .5f);
           // Debug.Log("Curr position " + currentNodeLocation);

        } else {
            flag = false;
        }
        

    }

    private void computePath(GameObject player) {
        //Debug.Log("X, Y Enemy position is :"  + transform.position);
        int startRow = (int) (transform.position.y + 6 - .5f);
        int startCol = (int) (transform.position.x + 11 - .5f);
        //Debug.Log("Y This has been mapped to :"  + wd.getWorldData()[startRow, startCol].getWorldZ());
        //Debug.Log("X This has been mapped to :"  + wd.getWorldData()[startRow, startCol].getWorldX());

        int playerLocationRow = (int) (player.transform.position.y + 6 -.5f);
        int playerLocationCol = (int) (player.transform.position.x + 11 - .5f);
        //Debug.Log("Player row number: " + playerLocationRow);
        //Debug.Log("Player col number: " + playerLocationCol);

       // Debug.Log("player position: " + player.transform.position);

        // Debug.Log("Player world Y position: " + wd.getWorldData()[playerLocationRow, playerLocationCol].getWorldZ());
        // Debug.Log("Player world X position: " + wd.getWorldData()[playerLocationRow, playerLocationCol].getWorldX());
        

        path = PathFinding.generatePath(
            wd.getWorldData(),
            (int) Mathf.Floor(startRow),
            (int) Mathf.Floor(startCol),
            (playerLocationRow),
            (playerLocationCol)
        );

        if (path.Count > 0) {
            // Set the currentNodeLocation to be the first node to travel to from the set of path locations.
            currentNodeLocation = new Vector2(path[0].getWorldX(), path[0].getWorldZ());
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        GameObject obj = collision.gameObject;

        if (obj.tag == "Bullet") {
            hp -= 50;
        }

        if (obj.tag == "Player" && GameObject.Find("Character") != null) {
            standStill = true;
            rb.velocity = Vector2.zero;
            timer = standStillSeconds;
            obj.GetComponent<Character>().reduceHealth(playerDamage);
        }

    }
}
