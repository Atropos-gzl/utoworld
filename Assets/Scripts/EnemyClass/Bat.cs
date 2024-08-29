using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bat : Enemy
{
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
        //animations = new Dictionary<Animations, string>
        //{
        //    {Animations.Forward, "Fly Forward" },
        //    //"Move Forward Slow", 
        //    //"Claw Attack", 
        //    {Animations.Attack, "Melee Attack" },
        //    {Animations.CastSpell, "Cast Spell" },
        //    {Animations.Transfer, "Dodge" },
        //    {Animations.TakeDamage,"Take Damage"},
        //    {Animations.Die, "Die"}
        //};
    }



    // Update is called once per frame
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
