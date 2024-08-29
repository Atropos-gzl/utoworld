using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSV;

public class Summon : MonoBehaviour
{
    protected EnemyData data;
    public EnemyState state;
    public GameObject health_Slider;   //血条预制体
    protected GameObject health_Slider_init;   //实例化血条UI
    public GameObject canvas;   //画布
    protected Slider m_Slider;
    protected Animator m_animator;    //该敌人的动画
    //protected Animator beat;  //心跳动画
    protected Text hp_text;  //血量显示
    protected Rigidbody m_rigidbody = null;
    //protected Text Money;  //玩家金币
    //protected float speed = 1;
    //protected int Award;
    //protected int Index = 1;  //路的下标
    //protected int damage = 2;  //对应玩家减少的血量
    protected GameBase Base;
    //public string Enemytag = "Untagged";
    //public string transportMethod = "jump";
    protected Dictionary<Animations, string> animations;
    public Vector3 AssemblePos;
    public GameObject targetEnemy;
    List<GameObject> targetCandidate;
    //public int hp;
    public int life;
    public int id;
    public int health;
    public float defence;
    public float magicdefence;
    public float movespeed;
    public float resistancerate;
    public int toPlayerDamage;
    public int coindropAmount;
    public float attack;
    public float magicAttack;
    public float attackSpeed;
    public float attackRange;
    public int monsterLevel;
    public int difficultyLevel;
    protected float timer = 0;
    protected float attack_interval = 0;
    //public List<Transform> route;

    public enum EnemyState
    {
        Idle,
        Tracking,
        Attacking,
        Returning,
        Live,
        Dead
    }

    protected enum Animations
    {
        Forward,
        Attack,
        CastSpell,
        TakeDamage,
        Die
    }
    protected virtual void Awake()
    {

        // beat = GameObject.Find("HP_Image").GetComponent<Animator>();
        //hp_text = GameObject.Find("HP_Text").GetComponent<Text>();

        animations = new Dictionary<Animations, string>
        {
            {Animations.Forward, "Move Forward" },
            {Animations.Attack, "Attack" },
            {Animations.CastSpell, "Cast Spell" },
            {Animations.TakeDamage,"Take Damage"},
            {Animations.Die, "Die"}
        };


    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log("Summon is Start");
        data = EnemyManager.enemyMap[id];
        health = data.health;
        defence = data.defence;
        magicdefence = data.magic_defence;
        movespeed = data.move_speed;
        resistancerate = data.resistance_rate;
        toPlayerDamage = data.toplayer_damage;
        coindropAmount = data.coindrop_amount;
        attack = data.attack;
        magicAttack = data.magic_attack;
        attackSpeed = data.attack_speed;
        attackRange = data.attack_range;
        monsterLevel = data.monster_level;
        difficultyLevel = data.difficulty_level;
        life = health;
        targetCandidate = new List<GameObject>();
        state = EnemyState.Live;
        attack_interval = 1.0f / attackSpeed;
        targetEnemy = null;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(life <= 0)
        {
            state = EnemyState.Dead;
        }
        switch (state)
        {
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Tracking:
                TrackTarget();
                break;
            case EnemyState.Returning:
                ReturnHome();
                break;
            case EnemyState.Dead:
                Dead();
                break;
            default:
                CheckLive();
                break;
        }
    }

    protected void InitHealth()
    {
        health_Slider_init = Instantiate(health_Slider);
        m_Slider = health_Slider_init.GetComponentInChildren<Slider>();
        health_Slider_init.GetComponent<Transform>().SetParent(canvas.transform);
        float width = health_Slider_init.GetComponent<RectTransform>().rect.width;
        float height = health_Slider_init.GetComponent<RectTransform>().rect.height;
        health_Slider_init.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
        health_Slider_init.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, height);
        health_Slider_init.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(transform.position.x - 0.5f, transform.position.y + 1, 0);
        health_Slider_init.GetComponent<HealthUI>().target = gameObject;
    }

    protected void CheckLive()
    {
        if (life > 0)
            state = EnemyState.Live;
        else
        {
            state = EnemyState.Dead;
            StartCoroutine(PlayBoolCurtoon(animations[Animations.Die], true, 0.5f));
            Destroy(gameObject);
        }

    }

    public virtual int TakeDamage(float[] damage)
    {
        int realDamage = (int)((damage[0] - defence) + (damage[1] - magicdefence) + damage[2]);
        this.life -= realDamage > 0 ? realDamage : 1;
        Debug.Log("current life: " + life.ToString());        
        if (life <= 0)
        {
            state = EnemyState.Dead;
            return 0;
        }
        else
        {
            StartCoroutine(PlayTriggerCurtoon(animations[Animations.TakeDamage]));
            m_Slider.value = (float)this.life / (float)this.health;
            return 1;
        }


    }

    public virtual void Dead()
    {
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Die], true, 1.0f));
        health_Slider_init.GetComponent<HealthUI>().Disappear();
        Destroy(gameObject);
        Base.ChangeMoney(coindropAmount);
        EnemySpawner.LiveCount--;
    }

    /*protected void Jump()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 20);
    }
    */

    public IEnumerator PlayTriggerCurtoon(string trigger, int changeSpeed = 0)
    {
        if (changeSpeed == 1)
        {
            movespeed += 10.0f;
            m_animator.SetTrigger(trigger);
            yield return new WaitForSeconds(0.7f);
            movespeed -= 10.0f;
        }
        else
        {
            m_animator.SetTrigger(trigger);
            yield break;
        }

    }

    public IEnumerator PlayBoolCurtoon(string boolparm, bool state, float time = 0.0f)
    {
        m_animator.SetBool(boolparm, state);
        if (time > 0.0f)
        {
            yield return new WaitForSeconds(time);
        }
        yield break;
    }

    public virtual void Attack()
    {

        if (targetEnemy != null)
        {
            timer += Time.deltaTime;
            Debug.Log("attck_interval is : " + attack_interval.ToString());
            if (timer >= attack_interval) {
                
                float[] damage = { attack, magicAttack, 0 };
                int status = targetEnemy.GetComponent<Enemy>().TakeDamage(damage);
                if (status != 0)
                {
                    StartCoroutine(PlayTriggerCurtoon(animations[Animations.Attack]));
                }
                timer = 0.0f;
            }
            
        }
        else
        {
            Debug.Log("No Enemy");
            state = EnemyState.Returning;
        }
    }

    public virtual void TrackTarget()
    {
        if(targetEnemy != null)
        {
            Debug.Log("Ready to track");
            Transform targetTransform = targetEnemy.GetComponent<Transform>();
            transform.LookAt(targetTransform.position);
            StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], true));
            transform.Translate((targetTransform.position - transform.position).normalized * Time.deltaTime * movespeed * 0.02f, Space.World);
            if(Vector3.Distance(targetTransform.position, transform.position) <= attackRange)
            {
                Debug.Log("Can do attack");
                targetEnemy.GetComponent<Enemy>().targetEnemy = this.gameObject;
                targetEnemy.GetComponent<Enemy>().state = Enemy.EnemyState.Tracking;
                StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
                state = EnemyState.Attacking;
            }
        }
        else
        {
            Debug.Log("No Enemy");
            state = EnemyState.Returning;
        }
    }

    public virtual void ReturnHome()
    {
        Debug.Log("Ready to Return");
        transform.LookAt(AssemblePos);
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], true));
        transform.Translate((AssemblePos - transform.position).normalized * Time.deltaTime * movespeed * 0.02f, Space.World);
        if (Vector3.Distance(AssemblePos, transform.position) <= 0.1f)
        {
            StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
            state = EnemyState.Live;
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy Enter");
        GameObject target = other.gameObject;
        if(target.CompareTag("Enemy"))
        {
            if(target.GetComponent<Enemy>().CanBeTracked())
            {
                if(targetEnemy == null)
                {
                    targetEnemy = target;
                    state = EnemyState.Tracking;
                }
                else
                {
                    targetCandidate.Add(target);
                }
            }
        }


    }

    void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if(targetCandidate.Contains(target))
        {
            targetCandidate.Remove(target);
        }
    }


    public virtual bool CanBeTracked()
    {
        if (state == EnemyState.Attacking || state == EnemyState.Tracking || state == EnemyState.Dead)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
