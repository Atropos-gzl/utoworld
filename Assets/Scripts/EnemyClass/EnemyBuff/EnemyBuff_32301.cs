using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff_32301 : EnemyBuff
{
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
            timer -= Time.deltaTime;
            
        }
        else {
            timer = 0;
        }
    }
}
