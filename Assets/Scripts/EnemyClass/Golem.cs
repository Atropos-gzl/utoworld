using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Golem : Enemy
{
    public float circle_radius = 5.0f;
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
        //    {Animations.Forward, "Walk Forward" },
        //    //"Move Forward Slow", 
        //    //"Claw Attack", 
        //    {Animations.Attack, "Punch Attack" },
        //    {Animations.CastSpell, "Cast Spell" },
        //    {Animations.Transfer, "Jump" },
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
            Defend(circle_radius);
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

    public void Defend(float range)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                EnemyBuff_21101 eb = hit.gameObject.GetComponent<EnemyBuff_21101>();
                if (eb == null)
                {
                    eb.gameObject.AddComponent<EnemyBuff_21101>();
                    eb.enabled = true;
                    eb.owner = this.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检查是否为友方单位
        if (other.CompareTag("Enemy"))
        {
            // 移除组件
            EnemyBuff_21101 buff = other.gameObject.GetComponent<EnemyBuff_21101>();
            if (buff != null)
            {
                Destroy(buff);
            }
        }
    }
}
