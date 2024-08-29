using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSV;
using System.Net.Http.Headers;

public class Enemy : MonoBehaviour
{
    protected EnemyData data;
    public EnemyState state;
    public GameObject health_Slider;   //血条预制体
    protected GameObject health_Slider_init;   //实例化血条UI
    public GameObject canvas;   //画布
    protected Slider m_Slider;
    protected Transform[] positions;  //路的数组
    //protected Transform end;  //终点
    protected Animator m_animator;    //该敌人的动画
    protected Animator beat;  //心跳动画
    protected Text hp_text;  //血量显示
    protected Rigidbody m_rigidbody = null;
    protected Text Money;  //玩家金币
    protected float speed = 1;
    protected int Award;
    public int Index = 1;  //路的下标
    //protected int damage = 2;  //对应玩家减少的血量
    protected GameBase Base;
    //public string Enemytag = "Untagged";
    //public string transportMethod = "jump";
    protected Dictionary<Animations, string> animations;
    public Dictionary<SubState, bool> substates;
    public GameObject targetEnemy;
    public float timer = 0.0f;
    public float magic_timer = 0.0f;
    public float attack_interval = 1.0f;
    public float magic_interval = 0.0f;
    public float jump_timer = 0.0f;
    public float jump_interval = 0.8f;
    public Transform startJumpTrans;
    protected bool transferOver = true;
    protected bool startunder = false;
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
    public List<Transform> route;
    public int transferType = 0;

    public enum EnemyState
    {
        Live,
        Tracking,
        Attacking,
        Returning,
        Transfer,
        Dead,
        Stun
    }

    public enum SubState
    {
        Silence,
        Accelerate,
        Slow
    }

    protected enum Animations
    {
        Forward,
        Attack,
        CastSpell,
        Transfer,
        TakeDamage,
        Spawn,
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
            {Animations.Transfer, "Jump" },
            {Animations.TakeDamage,"Take Damage"},
            {Animations.Die, "Die"},
            {Animations.Spawn, "Spawn" }

        };
        substates = new Dictionary<SubState, bool>
        {
            {SubState.Silence, false},
            {SubState.Accelerate, false},
            {SubState.Slow, false}
        };
        //substate = SubState.Normal;
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
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
        attack_interval = 1.0f / attackSpeed;
        targetEnemy = null;
        
        //Debug.Log("Prefab is start");
        //positions = GameObject.Find("RoutePoint").GetComponentsInChildren<Transform>();
        //gameObject.GetComponent<CharacterController>().Move((positions[Index].position - transform.position).normalized * Time.deltaTime * speed);
        //gameObject.GetComponentInChildren<Rigidbody>().MovePosition(transform.position + (positions[Index].position - transform.position).normalized * Time.deltaTime * speed);
        //route = new List<Transform>();
        //foreach (Transform pos in positions)
        //{
        //    GameObject point = pos.gameObject;
        //    //if (point.CompareTag(Enemytag))
        //    //{
               
        //    //}
        //    //Debug.Log(pos.name);
        //    route.Add(pos);
        //}
        //if (route.Count == 0)
        //{
        //    Debug.Log("Can not find routes");
        //    return;
        //}
        //else
        //{
        //    Debug.Log(route.Count);
        //}
    }

    // Update is called once per frame
    public virtual void Update()
    {
        calcAttrib();
        if (life <= 0)
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
            case EnemyState.Dead:
                Dead();
                break;
            case EnemyState.Transfer:
                Transfer();
                break;
            default:
                CheckLive();
                if(life > 0)
                {
                    Move();
                }
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
        health_Slider_init.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(transform.position.x-0.5f, transform.position.y+1, 0);
        health_Slider_init.GetComponent<HealthUI>().target = gameObject;
    }
   

    protected virtual void  Move()
    {
        if (route.Count == 0)
        {
            Debug.Log("Can not find routes");
            return;
        }
        
        if(Index == 1)
        {
            transform.LookAt(route[Index]);
        }
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], true));
        transform.Translate((route[Index].position - transform.position).normalized * Time.deltaTime * movespeed * 0.02f, Space.World);
        if (Vector3.Distance(route[Index].position, transform.position) < 0.1f)
        {
            Index++;
            if (Math.Abs(route[Index].position.y - transform.position.y) > 5.0f)
            {
                StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
                Debug.Log("Next index is " + Index.ToString());
                state = EnemyState.Transfer;
                startJumpTrans = transform;
                transferOver = false;
            }
            else
            {
                transform.LookAt(route[Index]);
            }
            
            
            
        }
        if (Vector3.Distance(route[route.Count  - 1].position, transform.position) < 0.5f)
        {
            //EnemySpawner.LiveCount--;
            StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
            Base.life -= toPlayerDamage;
            //beat.SetTrigger("beat");
            //hp_text.text = Base.life.ToString();
            //health_Slider_init.GetComponent<HealthUI>().Disappear();
            //Destroy(gameObject);
            state = EnemyState.Dead;
        }
    }

    protected void CheckLive()
    {
        if (life > 0)
            state = EnemyState.Live;
        else
        {
            state = EnemyState.Dead;
            Destroy(gameObject);
        }
            
    }

    public virtual int TakeDamage(float[] damage)
    {
        int realDamage = (int)((damage[0] - defence) + (damage[1] - magicdefence) + damage[2]);

        this.life -= realDamage > 0 ? realDamage : 1;
        if (life <= 0)
        {
            state = EnemyState.Dead;
            return 0;
        }
        else {
            StartCoroutine(PlayTriggerCurtoon(animations[Animations.TakeDamage]));
            m_Slider.value = (float)this.life / (float)this.health;
            return 1;
        }
        

    }

    public virtual void Dead()
    {
        health_Slider_init.GetComponent<HealthUI>().Disappear();
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Die], true, 1.0f));
        Destroy(gameObject);
        Base.ChangeMoney(coindropAmount);
        EnemySpawner.LiveCount--;
    }

    /*protected void Jump()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 20);
    }
    */

    public IEnumerator PlayTriggerCurtoon(string trigger, int changeSpeed=0, float time=0.0f)
    {
        if(changeSpeed == 1)
        {
            movespeed += 10.0f;
            m_animator.SetTrigger(trigger);
            yield return new WaitForSeconds(time);
            movespeed -= 10.0f;
        }
        else
        {
            m_animator.SetTrigger(trigger);
            yield return new WaitForSeconds(time);
        }
        
    }

    public IEnumerator PlayBoolCurtoon(string boolparm, bool state, float time=0.0f)
    {
        m_animator.SetBool(boolparm, state);
        if(time > 0.0f)
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
            if(timer >= attack_interval )
            {
                
                float[] damage = { attack, magicAttack, 0 };
                int status = targetEnemy.GetComponent<Summon>().TakeDamage(damage);
                if(status != 0)
                {
                    StartCoroutine(PlayTriggerCurtoon(animations[Animations.Attack]));
                }
                timer = 0.0f;
            }
           
        }
        else
        {
            state = EnemyState.Live;
        }
    }

    public void TrackTarget()
    {
        if (targetEnemy != null)
        {
            Transform targetTransform = targetEnemy.GetComponent<Transform>();
            transform.LookAt(targetTransform.position);
            StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], true));
            transform.Translate((targetTransform.position - transform.position).normalized * Time.deltaTime * movespeed * 0.02f, Space.World);
            if (Vector3.Distance(targetTransform.position, transform.position) <= attackRange)
            {
                StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
                state = EnemyState.Attacking;
            }
        }
        else
        {
            state = EnemyState.Returning;
        }
    }

    //public void ReturnHome()
    //{
    //    transform.LookAt(AssemblePos.position);
    //    StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], true));
    //    transform.Translate((AssemblePos.position - transform.position).normalized * Time.deltaTime * movespeed * 0.02f, Space.World);
    //    if (Vector3.Distance(AssemblePos.position, transform.position) <= attackRange)
    //    {
    //        StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
    //        state = EnemyState.Live;
    //    }
    //}

    public virtual void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Summon"))
        {
            if (target.GetComponent<Summon>().CanBeTracked() && targetEnemy == null)
            {
                Debug.Log("Enemy is tracking");
                targetEnemy = target;
                state = EnemyState.Tracking;
            }
        }


    }

    public virtual void Transfer()
    {
        
        //transferOver = false;
        if (transferType == 0)  //jump
        {
            StartCoroutine(PlayTriggerCurtoon(animations[Animations.Transfer]));
            transferOver = Jump(route[Index], 4.0f);
            
        }
        else if (transferType == 1)  // underground
        {
            if(!startunder)
            {
                startunder = true;
                StartCoroutine(transFromGround(route[Index]));
            }
            
        }
        if(transferOver)
        {
            Index++;
            transform.LookAt(route[Index].position);
            transferOver = false;
            state = EnemyState.Live;
        }          
    }

    public virtual bool Jump(Transform targetTrans, float jumpHeight)
    {
        timer += Time.deltaTime;
        float normalizedTime = timer / jump_interval;

        // 当跳跃时间结束时停止跳跃
        if (timer >= jump_interval)
        {
            //isJumping = false;
            timer = 0;
            transform.position = targetTrans.position;
            return true;
        }

        // 计算水平位置
        Vector3 horizontalPosition = Vector3.Lerp(startJumpTrans.position, targetTrans.position, normalizedTime);

        // 计算垂直位置
        float verticalPosition = jumpHeight * 4 * normalizedTime * (1 - normalizedTime);
        horizontalPosition.y += verticalPosition;

        transform.position = horizontalPosition;
        return false;
    }

    public virtual IEnumerator transFromGround(Transform targetTrans)
    {
        
        m_animator.SetTrigger("Underground");
        yield return new WaitForSeconds(0.3f);
        transform.position = targetTrans.position;
        m_animator.SetTrigger("Spawn");
        yield return new WaitForSeconds(0.8f);
        
        transferOver = true;
        startunder = false;
    }

    public bool CanBeTracked()
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

    public virtual void Transfer2Route(int delta)
    {
        int current_Index = Index;
        int next_Index = Index + 1;
        StartCoroutine(PlayBoolCurtoon(animations[Animations.Forward], false));
        if (delta == 1)
        {
            
            transform.position = route[next_Index].position;

        }
        else if(delta == -1)
        {
            transform.position = route[current_Index].position;
        }
    }

    protected virtual void fundamentalCircle(string targetAttrib, float value, GameObject monster)
    {
        Enemy monsterInfo = monster.GetComponent<Enemy>();
        System.Reflection.FieldInfo fieldInfo = monster.GetType().GetField("targetAttrib");
        //fieldInfo.SetValue
    }

    protected void calcAttrib()
    {
        resetAttrib();
        EnemyBuff[] buffs = GetComponents<EnemyBuff>();
        foreach (EnemyBuff buff in buffs)
        {
            Debug.Log(buff.name);
            life += (buff.life + (int)(life * buff.life_percentage)) >= 0 ? (buff.life + (int)(life * buff.life_percentage)) : (int)((buff.life + (life * buff.life_percentage)) * (1 - resistancerate));
            health += (buff.health + (int)(health * buff.health_percentage)) >= 0 ? (buff.health + (int)(health * buff.health_percentage)) : (int)((buff.health + (health * buff.health_percentage)) * (1 - resistancerate));
            defence += (buff.defence + (defence * buff.defence_percentage)) >= 0 ? (buff.defence + (defence * buff.defence_percentage)) : (buff.defence + (defence * buff.defence_percentage) * (1 - resistancerate));
            magicdefence += (buff.magicdefence + (magicdefence * buff.magicdefence_percentage)) >= 0 ? (buff.magicdefence + (magicdefence * buff.magicdefence_percentage)) : (buff.magicdefence + (magicdefence * buff.magicdefence_percentage) * (1 - resistancerate));
            movespeed += (buff.movespeed + (movespeed * buff.movespeed_percentage)) >= 0 ? (buff.movespeed + (movespeed * buff.movespeed_percentage)) : (buff.movespeed + (movespeed * buff.movespeed_percentage) * (1 - resistancerate));
            magicAttack += (buff.magicAttack + (magicAttack * buff.magicAttack_percentage)) >= 0 ? (buff.magicAttack + (magicAttack * buff.magicAttack_percentage)) : ((buff.magicAttack + (magicAttack * buff.magicAttack_percentage)) * (1 - resistancerate));
            defence += (buff.defence + (defence * buff.defence_percentage)) >= 0 ? (buff.defence + (defence * buff.defence_percentage)) : (buff.defence + (defence * buff.defence_percentage) * (1 - resistancerate));
            magicdefence += (buff.magicdefence + (int)(magicdefence * buff.magicdefence_percentage)) >= 0 ? (buff.magicdefence + (int)(magicdefence * buff.magicdefence_percentage)) : (buff.magicdefence + (magicdefence * buff.magicdefence_percentage) * (1 - resistancerate));
            attack += (buff.attack + (attack * buff.attack_percentage)) >= 0 ? (buff.attack + (attack * buff.attack_percentage)) : (buff.attack + (attack * buff.attack_percentage) * (1 - resistancerate));
            toPlayerDamage+= (buff.toPlayerDamage+ (int)(health * buff.health_percentage)) >= 0 ? (buff.toPlayerDamage+ (int)(toPlayerDamage* buff.health_percentage)) : (int)((buff.toPlayerDamage+ (toPlayerDamage* buff.toPlayerDamage_percentage)) * (1 - resistancerate));
            coindropAmount += (buff.coindropAmount + (int)(coindropAmount * buff.coindropAmount_percentage)) >= 0 ? (buff.coindropAmount + (int)(coindropAmount * buff.coindropAmount_percentage)) : (int)(buff.coindropAmount + (coindropAmount * buff.coindropAmount_percentage) * (1 - resistancerate));
            attackRange += (buff.attackRange + (attackRange * buff.attackRange_percentage)) >= 0 ? (buff.attackRange + (attackRange * buff.attackRange_percentage)) : (buff.attackRange + (attackRange * buff.attackRange_percentage) * (1 - resistancerate));
            attackSpeed += (buff.attackSpeed + (attackSpeed * buff.attackSpeed_percentage)) >= 0 ? (buff.attackSpeed + (attackSpeed * buff.attackSpeed_percentage)) : (buff.attackSpeed + (attackSpeed * buff.attackSpeed_percentage) * (1 - resistancerate));
        }
        life = life > health ? health : life;
    }
    protected void resetAttrib()
    {
        toPlayerDamage= data.health;
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
        attack_interval = 1.0f / attackSpeed;
    }

}



