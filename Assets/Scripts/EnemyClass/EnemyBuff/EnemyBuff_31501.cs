using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff_31501 : EnemyBuff
{
    public bool done = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        timer = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (timer > 0)
        {
            this.movespeed_percentage = -0.5f;
            timer -= Time.deltaTime;    
        }
        else
        {
            timer = 0;
            this.movespeed_percentage -= 0;
        }
    }
}
