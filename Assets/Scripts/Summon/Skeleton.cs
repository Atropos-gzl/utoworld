using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skeleton : Summon
{
    // Start is called before the first frame update
    //private new Dictionary<Animations, string> animations;
    protected override void Awake()
    {
        base.Awake();
        m_animator = GetComponent<Animator>();
        Debug.Log("is awake");
        Base = GameObject.Find("GameManager").GetComponent<GameBase>();
        //Money = GameObject.Find("Money").GetComponent<Text>();
        canvas = GameObject.FindGameObjectWithTag("canvas");
        InitHealth();
        //animations = new Dictionary<Animations, string>
        //{
        //    {Animations.Forward, "Fly Forward" },
        //    //"Move Forward Slow", 
        //    //"Claw Attack", 
        //    {Animations.Attack, "Sting Attack" },
        //    {Animations.CastSpell, "Cast Spell" },
        //    {Animations.TakeDamage,"Take Damage"},
        //    {Animations.Die, "Die"}
        //};
    }

    protected override void Start()
    {
        base.Start();
        AssemblePos = transform.position;
        
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override int TakeDamage(float[] damage)
    {
        return base.TakeDamage(damage);
    }

    public override void ReturnHome()
    {
        base.ReturnHome();
    }
}
