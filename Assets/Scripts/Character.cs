using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bullet;
    private Vector2 positionChange;
    // Start is called before the first frame update
    void Start() 
    {
        positionChange = Vector2.zero;
    }

    // Update is called once per frame

    void Update () {

        rotateCharacter();

        if (Input.GetKey(KeyCode.W)) {
            positionChange = new Vector2(0f, .1f);
            rb.MovePosition(rb.position + positionChange);
        }

        if (Input.GetKey(KeyCode.S)) {
           positionChange = new Vector2(0f, -.1f);
           rb.MovePosition(rb.position + positionChange);
        }

        if (Input.GetKey(KeyCode.A)) {
            positionChange = new Vector2(-.1f, 0f);
            rb.MovePosition(rb.position + positionChange);
        }

        if (Input.GetKey(KeyCode.D)) {
            positionChange = new Vector2(.1f, 0f);
            rb.MovePosition(rb.position + positionChange);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            Vector2 characterFacing = transform.forward;
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
    
        }

    }

    private void rotateCharacter() {
        Vector3 mouseWorldPos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        transform.right = mouseWorldPos - transform.position;
    }
}
