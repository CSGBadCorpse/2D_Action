using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public GameObject player;
    PlayerCharacter character;

    // Start is called before the first frame update
    void Start()
    {
        character = player.GetComponent<PlayerCharacter>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("CarRight") || collision.transform.CompareTag("CarLeft"))
        {
            character.flag = false;
        }
    }

}
