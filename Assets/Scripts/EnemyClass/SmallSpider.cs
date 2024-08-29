using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallSpider : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }


    protected override void Awake()
    {
        base.Awake();
        m_animator = GetComponent<Animator>();
        //Debug.Log("is awake");
        Base = GameObject.Find("GameManager").GetComponent<GameBase>();
        Money = GameObject.Find("Money").GetComponent<Text>();
        canvas = GameObject.FindGameObjectWithTag("canvas");
        InitHealth();
        //end = GameObject.Find("point_ed").GetComponent<Transform>();
    }



    // Update is called once per frame
    //void Update()
    //{
    //    //CheckLive();
    //    ////Debug.Log("Enemy: " + life.ToString());
    //    //if (life <= 0)
    //    //{

    //    //    //StartCoroutine(PlayBoolCurtoon(animations[Animations.Die], true, 1.0f));
    //    //    Dead();
    //    //}

    //    //Move();
    //}

    private void FixedUpdate()
    {

    }

    protected override void Move()
    {

        base.Move();
    }

    public override int TakeDamage(float[] damage)
    {
        //m_animator.SetBool("Walk Forward", false);

        return base.TakeDamage(damage);

    }

    public override void Dead()
    {

        base.Dead();

    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
