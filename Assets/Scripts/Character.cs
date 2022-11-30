using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
   
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float canShootCoolDown;
    
    [SerializeField] private bool canShoot;
    

    [SerializeField] private float walkCooldown;
    private float walkTimer;
    private float shootTimer;


    private float health;
    

    // Start is called before the first frame update
    void Start() 
    {
        shootTimer = 0;
        walkTimer = 0;
        health = 100;

    }

    // Update is called once per frame

    void Update () {
        rotateCharacterTowardsMouse();
        checkForMovementInput();
        checkForFiringInput();
        //clampPlayer();

        if (health <= 0) {
            GameObject.Destroy(gameObject);
            SceneManager.LoadScene("DeadMenu");
        }

    }

    private void rotateCharacterTowardsMouse() {
        // Get the world position of the mouse 
        Vector3 mouseWorldPos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        // set z to 0
        mouseWorldPos.z = 0f;
        // Turn the right vector 
        transform.right = mouseWorldPos - transform.position;
    }

    private bool isObstacle(Vector2 position) {
        Vector2 charPosition = new Vector2(transform.position.x, transform.position.y);
        int layerMask = 1 << 8;
        Vector2 direction = position;
        direction.Normalize();
        direction *= 1f;
        RaycastHit2D hit = Physics2D.Raycast(charPosition, direction, direction.magnitude, layerMask);
        if (hit.collider) {
            if (hit.collider.gameObject.tag == "Obstacle") {
                Debug.Log("hit an osbtacle");
                return true;
            }
            
        }
        walkTimer = walkCooldown * Time.deltaTime;
        return false;
    }
    /**
    In each statement, we:
    1. Record the basic WASD key inputs
    2. Set the amount we want to travel
    3. Move to that position
    **/
    private void checkForMovementInput() {
        
        if (walkTimer <= 0) {
            if (Input.GetKey(KeyCode.W)) {
                Vector3 positionChange = new Vector3(0f, 1, 0);
                if (!isObstacle(positionChange)) {
                    transform.position = (transform.position + positionChange);
                }
                return;
            }

            if (Input.GetKey(KeyCode.S)) {
                Vector3 positionChange = new Vector3(0f, -1f);
                if (!isObstacle(positionChange)) {
                    transform.position = (transform.position + positionChange);
                }
                return;
            }

            if (Input.GetKey(KeyCode.A)) {
                Vector3 positionChange = new Vector3(-1f , 0f);
                if (!isObstacle(positionChange)) {
                    transform.position = (transform.position + positionChange);
                }
                return;
            }

            if (Input.GetKey(KeyCode.D)) {
                Vector3 positionChange = new Vector3(1, 0f);
                if (!isObstacle(positionChange)) {
                    transform.position = (transform.position + positionChange);
                }
                return;
            }
        } else {
            walkTimer -= Time.deltaTime;
        }

        return;
    }

    private void checkForFiringInput() {

        

        // If timer is greater than zero then subtract it down. otherwise this means we can shoot again.
        if (shootTimer >= 0) {
            shootTimer -= Time.deltaTime;
        } else {
            // If player presses space bar and is able to shoot
            if (Input.GetKey(KeyCode.Space)) {
                // Create a bullet on top of the player
                GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
                // set the timer value to start at the cooldown's seconds
                shootTimer = canShootCoolDown;

            }
        }
    }

    private void clampPlayer() {
        Vector2 charPosition = transform.position;
        float horizontalBoundary = CameraBoundary.getCameraHorizontalBoundary("MainCamera");
        float verticalBoundary = CameraBoundary.getCameraVerticalBoundary("MainCamera");
        charPosition.x = Mathf.Clamp(charPosition.x, -horizontalBoundary + .25f, horizontalBoundary - .25f);
        charPosition.y = Mathf.Clamp(charPosition.y, -verticalBoundary + .25f, verticalBoundary - .25f);

        transform.position = new Vector2(charPosition.x, charPosition.y);
    }

        void OnCollisionEnter2D(Collision2D collision) {
            GameObject obj = collision.gameObject;

            if (obj.tag == "Enemy") {
                health -= 2;
            }
        }

    public void reduceHealth(float amount) {
        this.health = health - amount;
    }

    public void setHealth(float health) {
        this.health = health;
    }

    public void increaseMS() {
        this.movementSpeed += .05f;
    }

    public void decreaseShootCD() {
        this.canShootCoolDown -= .15f;
    }
}
