using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    PlayerController controller;

    Rigidbody2D rigid;
    Animator anim;

    Transform checkGround;
    //Transform hookSphere;

    public float speed = 3;
    public float jumpSpeed = 8;
    public int maxHp = 5;

    float originXspeed;
    public Hook hook;

    [HideInInspector]
    public bool flag = false;


    int hp;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool isGround = false;
    //bool faceLeft = true;//脸朝左为true朝右为false
    //失去控制时间（物理帧）用于生效弹飞
    float outControlTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hook = transform.Find("HookSphere").GetComponent<Hook>();

        //hookSphere = transform.Find("HookSphere");
        checkGround = transform.Find("CheckGround");//在脚本所在游戏物体中寻找
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.jump)
        {
            jump = true;
        }

        //更新动画状态
        anim.SetBool("IsGround", isGround);
        anim.SetFloat("Speed", Mathf.Abs(controller.h));
        if (controller.attack)
        {
            anim.SetTrigger("Attack");
        }
        if (flag)
        {
            MoveToCar(hook.car);
            //Debug.Log(hook);
        }
        

    }

    bool IsAttacking()
    {
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);
        //IsName判断当前动画名称
        originXspeed = rigid.velocity.x;
        rigid.velocity = new Vector2(0,rigid.velocity.y);
        return asi.IsName("Attack1") || asi.IsName("Attack2") || asi.IsName("Attack3");
    }
    private void FixedUpdate()//刚体的移动在Fixed Update中调用更好
    {
        CheckGround();
        if (!IsAttacking())
        {
            rigid.velocity = new Vector2(originXspeed, rigid.velocity.y);
            Move(controller.h);
        }
        
        jump = false;
        outControlTime--;
    }

    private void Move(float h)
    {
        if (outControlTime > 0)
        {
            return;
        }
        Flip(h);
        float vy = rigid.velocity.y;
        if (jump && isGround)
        {
            anim.SetTrigger("Jump");
            vy = jumpSpeed;
        }
        rigid.velocity = new Vector2(h * speed, vy);

    }

    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(checkGround.position, 0.1f,~LayerMask.GetMask("Player"));//对除了player层的所有层有效，
    }
    private void Flip(float h)
    {
        Vector3 scaleLeft = new Vector3(1, 1, 1);
        Vector3 scaleRight = new Vector3(-1, 1, 1);

        if (h > 0.1f)
        {
            //faceLeft = false;
            transform.localScale = scaleRight;
        }
        else if(h < -0.1f)
        {
            //faceLeft = true;
            transform.localScale = scaleLeft;
        }
    }

    private void OnDrawGizmos()//辅助球，辅助查看状态
    {
        if (Application.isPlaying)//Gizmos.DrawSphere在编辑状态下也能运行，添加游戏是否运行的判断
        {
            Gizmos.color = Color.white;
            if (isGround)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(checkGround.position, 0.1f);//该函数在编辑状态下也能执行
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter");
        if (collision.transform.CompareTag("CarRight"))
        {
            GetHit(1, "right");
        }
        if (collision.transform.CompareTag("CarLeft"))
        {
            GetHit(1, "left");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionStay:"+collision.gameObject.tag);
        /*if (collision.transform.CompareTag("CarRight"))
        {
            GetHit(1,"right");
        }*/        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       /* Debug.Log("OnTriggerEnter");
        if (collision.transform.CompareTag("Car"))
        {
            GetHit(1);
        }*/
    }
    private void GetHit(int damage,string side)
    {
        hp -= damage;
        if(hp < 0) { hp = 0; }

        //UIManager.Instance.setPlayerHp(hp, maxHp);

        //受伤动画
        anim.SetTrigger("GetHit");

        //受击朝反方向飞出
        Vector2 force = new Vector2(3,6);
        if (side.Equals("right"))
        {
            force.x *= -8;
        }
        if (side.Equals("left"))
        {
            force.x *= 8;
        }
        //rigid.AddForce(force);//rigid.velocity
        rigid.velocity = force;
        outControlTime = 30;
    }
    public void MoveToCar(Transform car)
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, car.transform.position, 0.2f);
    }
}

