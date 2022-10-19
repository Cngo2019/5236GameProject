using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float canShootCoolDown;

    private bool canShoot;
    private float timer;

    private float health;
    

    // Start is called before the first frame update
    void Start() 
    {
        timer = 0;
        canShoot = true;
        health = 100;
    }

    // Update is called once per frame

    void Update () {
        rotateCharacterTowardsMouse();
        checkForMovementInput();
        checkForFiringInput();
        clampPlayer();

        if (health <= 0) {
            GameObject.Destroy(gameObject);
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


    /**
    In each statement, we:
    1. Record the basic WASD key inputs
    2. Set the amount we want to travel
    3. Move to that position
    **/
    private void checkForMovementInput() {
        
        if (Input.GetKey(KeyCode.W)) {
            Vector2 positionChange = new Vector2(0f, movementSpeed);
            rb.MovePosition(rb.position + positionChange);
            return;
        }

        if (Input.GetKey(KeyCode.S)) {
           Vector2 positionChange = new Vector2(0f, movementSpeed * -1f);
           rb.MovePosition(rb.position + positionChange);
           return;
        }

        if (Input.GetKey(KeyCode.A)) {
            Vector2 positionChange = new Vector2(movementSpeed * -1f , 0f);
            rb.MovePosition(rb.position + positionChange);
            return;
        }

        if (Input.GetKey(KeyCode.D)) {
            Vector2 positionChange = new Vector2(movementSpeed, 0f);
            rb.MovePosition(rb.position + positionChange);
            return;
        }

        rb.velocity = Vector2.zero;
        return;
    }

    private void checkForFiringInput() {

        // If player presses space bar and is able to shoot
        if (Input.GetKey(KeyCode.Space) && canShoot) {
            // Create a bullet on top of the player
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            // set the timer value to start at the cooldown's seconds
            timer = canShootCoolDown;
            // set the can shoot state to false so they can't shoot while on cooldown
            canShoot = false;
        }

        // If timer is greater than zero then subtract it down. otherwise this means we can shoot again.
        if (timer >= 0) {
            timer -= Time.deltaTime;
        } else {
            canShoot = true;
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
