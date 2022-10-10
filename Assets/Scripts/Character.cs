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
    // Start is called before the first frame update
    void Start() 
    {
        timer = 0;
        canShoot = true;
    }

    // Update is called once per frame

    void Update () {
        rotateCharacterTowardsMouse();
        checkForMovementInput();
        checkForFiringInput();
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
            Vector2 positionChange = new Vector2(0f, .1f);
            rb.MovePosition(rb.position + positionChange);
            return;
        }

        if (Input.GetKey(KeyCode.S)) {
           Vector2 positionChange = new Vector2(0f, -.1f);
           rb.MovePosition(rb.position + positionChange);
           return;
        }

        if (Input.GetKey(KeyCode.A)) {
            Vector2 positionChange = new Vector2(-.1f, 0f);
            rb.MovePosition(rb.position + positionChange);
            return;
        }

        if (Input.GetKey(KeyCode.D)) {
            Vector2 positionChange = new Vector2(.1f, 0f);
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

}
