using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBeetle : Enemy
{
   

    
    // Start is called before the first frame update
    void Start()
    {
        Base = GameObject.Find("GameManager").GetComponent<GameBase>();
        Money = GameObject.Find("Money").GetComponent<Text>();
        Award = 20;
        canvas = GameObject.FindGameObjectWithTag("canvas");
        InitHealth();
        //hp = 2000;
        //speed = 2.5f;
        //life = 2000;
        //life += (int)(life * (GameObject.Find("GameManager").GetComponent<EnemySpawner>().Wave_num * 0.6f + 0.1));
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("Walk Forward", true);
        //end = GameObject.Find("point_ed").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLive();
        if (life <= 0)
        {
            Dead();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
}
