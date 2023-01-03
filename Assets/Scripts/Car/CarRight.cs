using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CarState 
{
    flying,
    running
}

public class CarRight : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 3;
    public int maxHp = 1;

    Transform frontWheel;
    Transform backWheel;
    Transform button;

    public CarState state;

    public GameObject DeathParticle;

    bool isRunning = true;
    //public TimeController timeController;

    bool hit = false;
    int hp;
    // Start is called before the first frame update
    void Start()
    {
        //timeController = GetComponent<TimeController>();
        state = CarState.running;

        frontWheel = transform.Find("FrontWheel");
        backWheel = transform.Find("BackWheel");
        button = transform.Find("Button");

        hp = maxHp;
        rigid = GetComponent<Rigidbody2D>();

        
    }

    private void Update()
    {
        CheckRunning();
        if (!isRunning)
        {
            state = CarState.flying;
            TimeController._instance.SlowTime();
            button.gameObject.SetActive(true);
        }
        else if (isRunning)
        {
            state = CarState.running;
            TimeController._instance.NormaliseTime();
            button.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float vy = rigid.velocity.y;
        if (!hit)
        {
            rigid.velocity = new Vector2(-speed, vy);
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision:" + collision.transform.tag);
        if (collision.transform.CompareTag("PlayerHit"))
        {
            GetHit(1);
        }
    }

    private void GetHit(int damage)
    {
        hp -= damage;
        Debug.Log("hp:" + hp);
        

        //UIManager.Instance.setPlayerHp(hp, maxHp);

        //受伤动画
        //anim.SetTrigger("GetHit");

        //受击朝反方向飞出
        hit = true;
        Vector2 force = new Vector2(3, 6);
        force *= 1;
        /*if (side.Equals("right"))
        {
            force.x *= -8;
        }
        if (side.Equals("left"))
        {
            force.x *= 8;
        }*/
        //rigid.AddForce(force);//rigid.velocity
        rigid.velocity = force;
        if (hp <= 0) 
        { 
            hp = 0;
            //Destroy(gameObject);
            OnDie();
        }
    }

    private void CheckRunning()
    {
        if(Physics2D.OverlapCircle(frontWheel.position, 0.1f, ~LayerMask.GetMask("Car")&~LayerMask.GetMask("Player"))
        && Physics2D.OverlapCircle(backWheel.position, 0.1f, ~LayerMask.GetMask("Car") & ~LayerMask.GetMask("Player")))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        
    }
    public virtual void OnDie()
    {
        var particle = Instantiate(DeathParticle);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        Destroy(gameObject);
    }
    
}
