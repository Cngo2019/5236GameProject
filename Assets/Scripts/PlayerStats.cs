using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerStats : MonoBehaviour
{

    TextMeshProUGUI Health;
    GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<TextMeshProUGUI>();
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = character.GetComponent<Character>().getHealth().ToString();
        Health.color = Color.red;
    }
}
