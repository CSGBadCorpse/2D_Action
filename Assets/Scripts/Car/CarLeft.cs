using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLeft : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 3;

    public GameObject DeathParticle;

    public int maxHp = 1;

    bool hit = false;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vy = rigid.velocity.y;
        rigid.velocity = new Vector2(speed, vy);
    }
    private void FixedUpdate()
    {
        
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
        force *= 3;
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

    public virtual void OnDie()
    {
        var particle = Instantiate(DeathParticle);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        Destroy(gameObject);
    }
}
