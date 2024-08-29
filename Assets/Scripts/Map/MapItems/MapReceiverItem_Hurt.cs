using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReceiverItem_Hurt : MapReceiverItem
{
    public Animator animator;

    public int levelId = 0;
    private float attackLastTime = 0f;
    private float attackCd = 4.15f;
    private float attackTimer = 0f;


    private float input_attackTime = 0f;
    private float input_attackDamage = 0f;

    private List<Enemy> enemyList = new List<Enemy>();

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
        if (levelId % 10 == 0)
        {
            input_attackTime = 5f;
            input_attackDamage = 200f;
        }
        if (levelId % 10 == 1)
        {
            input_attackTime = 10f;
            input_attackDamage = 200f;
        }
        if (levelId % 10 == 2)
        {
            input_attackTime = 20f;
            input_attackDamage = 200f;
        }
    }

    public override void Receive()
    {
        attackLastTime = input_attackTime;
        attackTimer = 0f;
    }

    void TakeDamage()
    {
        foreach(Enemy enemy in enemyList)
        {
            float[] damages = { input_attackDamage, 0f, 0f };
            enemy.TakeDamage(damages);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null && !enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null && enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    void Update()
    {
        animator?.SetBool("work", (bool)(attackLastTime > 0f));
        if (attackLastTime > 0f)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackCd)
            {
                TakeDamage();
                attackTimer -= attackCd;
                attackLastTime -= attackCd;
            }
            if (attackLastTime < attackCd)
            {
                attackLastTime = 0;
            }
        }
    }
}
