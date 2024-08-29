using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurpleBeetle : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Base = GameObject.Find("GameManager").GetComponent<GameBase>();
        Money = GameObject.Find("Money").GetComponent<Text>();
        Award = 20;
        canvas = GameObject.FindGameObjectWithTag("canvas");
        InitHealth();
        //hp = 1000;
        //life = 1000;
        //life += (int)(life  *  (GameObject.Find("GameManager").GetComponent<EnemySpawner>().Wave_num  *  0.6f + 0.1));
        //speed = 5.0f;
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
