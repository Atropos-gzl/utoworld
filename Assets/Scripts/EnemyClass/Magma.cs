using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magma : Enemy
{
    public float circle_radius = 5.0f;
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
            MagicDefend(circle_radius);
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


    public void MagicDefend(float range)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                EnemyBuff_21201 eb = hit.gameObject.GetComponent<EnemyBuff_21201>();
                if (eb == null)
                {
                    eb.gameObject.AddComponent<EnemyBuff_21201>();
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
            EnemyBuff_21201 buff = other.gameObject.GetComponent<EnemyBuff_21201>();
            if (buff != null)
            {
                Destroy(buff);
            }
        }
    }
}
