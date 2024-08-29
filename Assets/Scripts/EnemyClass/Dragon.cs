using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : Enemy
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
        magic_interval = 8.0f;
        //end = GameObject.Find("point_ed").GetComponent<Transform>();
        //animations = new Dictionary<Animations, string>
        //{
        //    {Animations.Forward, "Fly Forward" },
        //    //"Move Forward Slow", 
        //    //"Claw Attack", 
        //    {Animations.Attack, "Bite Attack" },
        //    {Animations.CastSpell, "Cast Spell" },
        //    {Animations.Transfer, "Fly Down" },
        //    {Animations.TakeDamage,"Take Damage"},
        //    {Animations.Die, "Die"}
        //};
    }



    // Update is called once per frame
    public override void Update()
    {
        calcAttrib();
        if (life <= 0)
        {
            state = EnemyState.Dead;
        }
        magic_timer += Time.deltaTime;
        if (magic_timer >= magic_interval && !substates[SubState.Silence])
        {
            CreateBabyDragon();
            magic_timer = 0;
        }
        switch (state)
        {
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Tracking:
                TrackTarget();
                break;
            case EnemyState.Dead:
                Dead();
                break;
            case EnemyState.Transfer:
                Transfer();
                break;
            default:
                CheckLive();
                if (life > 0)
                {
                    Move();
                }
                break;
        }
    }

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

    public override void Transfer()
    {

        base.Transfer();
    }

    public void CreateBabyDragon()
    {
        StartCoroutine(PlayTriggerCurtoon(animations[Animations.CastSpell]));
        Vector3 pos = transform.position;
        pos -= transform.forward * 0.3f;
        GameObject monster = Resources.Load<GameObject>("Prefabs/" + "21001");
        Quaternion q = Quaternion.Euler(0, 270, 0);
        GameObject go = Instantiate(monster, pos, q);
        Enemy info = go.GetComponent<Enemy>();
        info.route = this.route;
        info.Index = this.Index;
    }
}
