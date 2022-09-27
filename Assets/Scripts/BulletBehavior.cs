using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        Vector3 direction = GameObject.Find("Character").transform.right;
        direction.Normalize();
        rb.velocity = direction * 15f;

    }

    // Update is called once per frame
    void Update() {
        if (isOffScreen()) {
            GameObject.Destroy(gameObject);
        }
    }

    private bool isOffScreen() {

        float cameraHorizontalBoundary = CameraBoundary.getCameraHorizontalBoundary("Main Camera");
        float cameraVerticalBoundary = CameraBoundary.getCameraVerticalBoundary("Main Camera");

        return 
        transform.position.x > cameraHorizontalBoundary || 
        transform.position.x < cameraHorizontalBoundary * -1 || 
        transform.position.y > cameraVerticalBoundary || 
        transform.position.y < cameraHorizontalBoundary * -1;
    }


}
