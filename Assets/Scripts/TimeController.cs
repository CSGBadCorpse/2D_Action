using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float fixedDeltaTime;
    public static TimeController _instance;
    public bool control = false;
    public Transform carList;

    private void Awake()
    {
        _instance = this;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAllCarsFlying();
    }


    public void CheckAllCarsFlying()
    {
        if (carList == null) 
        {
            NormaliseTime();
        }
        foreach(Transform index in carList)
        {
            
            if (index.gameObject.GetComponent<CarRight>().state == CarState.flying)
            {
                control = true;
                //Debug.Log("Control:"+control);
                break;
            }
            else if (index.gameObject.GetComponent<CarRight>().state == CarState.running)
            {
                control = false;
                //Debug.Log("Control:" + control);
            }
        }
    }

    public void SlowTime()
    {
        if (control)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        
    }

    public void NormaliseTime()
    {
        if (!control)
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        
    }
}
