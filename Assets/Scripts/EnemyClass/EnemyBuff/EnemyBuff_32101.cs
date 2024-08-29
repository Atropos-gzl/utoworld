using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff_32101 : EnemyBuff
{
    // Start is called before the first frame update
    protected override void Start()
    {
        timer = 4;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (timer > 0) { 
            timer -= Time.deltaTime;
            this.damage_manipulation_percentage = 0.4f;
        }
        else
        {
            this.damage_manipulation_percentage = 0.0f;
        }
    }
}
