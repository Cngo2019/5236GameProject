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
    GameObject levelController;
    bool standStill;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        standStill = false;
        levelController = GameObject.Find("LevelController");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Character") != null && !standStill) {
            // This will be replaced with A star soon
            Vector2 charPosition = GameObject.Find("Character").transform.position;
            chaseCharacter(charPosition);
        }

        if (standStill) {
            if (timer >= 0) {
            timer -= Time.deltaTime;;
            } else {
                standStill = false;
            }
        }


         if (hp <= 0) {
            levelController.GetComponent<LevelController>().reduceKillCount();
            GameObject.Destroy(gameObject);
        }
        
        if (levelController.GetComponent<LevelController>().getKillRequirement() <= 0) {
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

        if (obj.tag == "Player" && GameObject.Find("Character") != null) {
            standStill = true;
            rb.velocity = Vector2.zero;
            timer = standStillSeconds;
            obj.GetComponent<Character>().reduceHealth(playerDamage);
        }

    }
}
