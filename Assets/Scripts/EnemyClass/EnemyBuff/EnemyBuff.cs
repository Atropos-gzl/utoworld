using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff : MonoBehaviour
{
    //加算部分
    public int life = 0;
    public int health = 0;
    public float defence = 0;
    public float magicdefence = 0;
    public float movespeed = 0;
    public float resistancerate = 0;
    public int toPlayerDamage = 0;
    public int coindropAmount = 0;
    public float attack = 0;
    public float magicAttack = 0;
    public float attackSpeed = 0;
    public float attackRange = 0;
    public int damage_manipulation = 0;

    //乘算部分
    public float life_percentage = 0;
    public float health_percentage = 0;
    public float defence_percentage = 0;
    public float magicdefence_percentage = 0;
    public float movespeed_percentage = 0;
    public float resistancerate_percentage = 0;
    public float toPlayerDamage_percentage = 0;
    public float coindropAmount_percentage = 0;
    public float attack_percentage = 0;
    public float magicAttack_percentage = 0;
    public float attackSpeed_percentage = 0;
    public float attackRange_percentage = 0;
    public float damage_manipulation_percentage = 0;

    

    //计时器
    public float timer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
