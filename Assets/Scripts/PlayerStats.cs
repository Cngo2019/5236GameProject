using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerStats : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI stats;
    GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<TextMeshProUGUI>();
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        stats.text = "Health: " + character.GetComponent<Character>().getHealth().ToString() + "\n" + "Ammo: " + character.GetComponent<Character>().getAmmo().ToString();
        stats.color = Color.red;
    }
}
