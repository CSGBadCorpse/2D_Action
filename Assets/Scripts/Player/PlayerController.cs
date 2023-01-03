using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter player;
    [HideInInspector]
    public float h, v;
    [HideInInspector]
    public bool jump;
    [HideInInspector]
    public bool hook;
    [HideInInspector]
    public bool attack;
    // Start is called before the first frame update
    void Start()
    {
        player= GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        //Debug.Log("h:" + h);
        v = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        attack = Input.GetMouseButtonDown(0);
        
        hook = Input.GetButtonDown("Hook");
        //Debug.Log("hook" + hook);
    }
}
