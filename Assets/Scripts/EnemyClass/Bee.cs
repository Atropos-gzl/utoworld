using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bee : Enemy
{
    private new Dictionary<Animations, string> animations;
    protected override void Awake()
    {
        base.Awake();
        m_animator = GetComponent<Animator>();
        Debug.Log("is awake");
        m_animator.SetBool("isCreated", true);
        Base = GameObject.Find("GameManager").GetComponent<GameBase>();
        Money = GameObject.Find("Money").GetComponent<Text>();
        canvas = GameObject.FindGameObjectWithTag("canvas");
        InitHealth();
        //hp = 200;
        //life = 200;
        //life += (int)(life * (GameObject.Find("GameManager").GetComponent<EnemySpawner>().Wave_num * 0.6f + 0.1));
        //speed = 10.0f;
        //Award = 10;
        //end = GameObject.Find("point_ed").GetComponent<Transform>();
        animations = new Dictionary<Animations, string>
        {
            {Animations.Forward, "Fly Forward" },
            //"Move Forward Slow", 
            //"Claw Attack", 
            {Animations.Attack, "Sting Attack" },
            {Animations.CastSpell, "Cast Spell" },
            {Animations.Transfer, "Dodge" },
            {Animations.TakeDamage,"Take Damage"},
            {Animations.Die, "Die"}
        };
    }

    void Update()
    {
        CheckLive();
        //Debug.Log("Enemy: " + life.ToString());
        if (life <= 0)
        {

            StartCoroutine(PlayBoolCurtoon(animations[Animations.Die], true, 1.0f));
            Dead();
        }

        Move();
    }

    private void FixedUpdate()
    {

    }

    protected override void Move()
    {
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], true));
        base.Move();
    }

    public override int TakeDamage(float[] damage)
    {
        //m_animator.SetBool("Walk Forward", false);
        m_animator.SetTrigger("takedamage");
        StartCoroutine(PlayTriggerCurtoon(animations[Animations.TakeDamage]));
        return base.TakeDamage(damage);

    }

    public override void Dead()
    {
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Die], true, 1.0f));
        base.Dead();

    }
}
