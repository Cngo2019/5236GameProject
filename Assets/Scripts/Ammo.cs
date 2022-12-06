using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("?");
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Character>().addAmmo();
            GameObject.Destroy(gameObject);
        }
        
    }
}
