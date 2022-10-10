using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float hp;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 charPosition = GameObject.Find("Character").transform.position;

        if (charPosition != null) {
            chaseCharacter(charPosition);
        }

        if (hp <= 0) {
            GameObject.Destroy(gameObject);
        }
    }

    private void chaseCharacter(Vector2 charPosition) {
        Vector2 velocityOutput = KinematicArrive.getSteering(transform.position, charPosition, maxSpeed, maxSpeed);
        rb.velocity = velocityOutput;
    }

    void OnCollisionEnter2D(Collision2D collision) {

        GameObject obj = collision.gameObject;

        if (obj.tag == "Bullet") {
            hp -= 50;
        }

    }
}
