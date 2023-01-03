using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject player;
    PlayerController controller;
    [HideInInspector]
    public Transform car;
    //public TimeController timeController;

    public bool hookFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = player.GetComponent<PlayerController>();
        //timeController = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.hook && hookFlag)
        {
            Debug.Log("HOOK!");
            //计算角色和汽车的距离，每帧计算
            //绘制角色到汽车的线，每帧绘制，这样可以一边移动，线一边消失
            //Vector2 vec = car.transform.position - player.transform.position;
            //player.transform.position = car.transform.position;
            player.transform.GetComponent<PlayerCharacter>().flag = true;
            player.transform.GetComponent<PlayerCharacter>().isGround = true;
            player.transform.GetComponent<PlayerCharacter>().jump = false;

            //player.gameObject.GetComponent<PlayerCharacter>().MoveToCar(car.transform);

        }

        //按下钩锁键{
        //调用汽车减缓下落的函数

        //角色沿着线移动
        //如果角色的safeZone接触到汽车碰撞体就停止移动
        //给定时间，如果没有攻击，则恢复汽车下落速度
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("CarRight")|| collision.transform.CompareTag("CarLeft"))
        {
            if (collision.gameObject.GetComponent<CarRight>().state == CarState.flying)
            {
                //Debug.Log("CarFlying");
                hookFlag = true;
                car = collision.transform;
                //汽车缓落
                //TimeController._instance.SlowTime();
            }
            else if(collision.gameObject.GetComponent<CarRight>().state == CarState.running)
            {
                //Debug.Log("CarRunning");
                hookFlag = false;
                car = collision.transform;
                //TimeController._instance.NormaliseTime();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("CarRight") || collision.transform.CompareTag("CarLeft"))
        {
            hookFlag = false;

            if (collision.gameObject.GetComponent<CarRight>().state == CarState.flying)
            {
                //Debug.Log("EnterHook");
                //汽车缓落
                //TimeController._instance.SlowTime();
            }
        }
        
    }
}
