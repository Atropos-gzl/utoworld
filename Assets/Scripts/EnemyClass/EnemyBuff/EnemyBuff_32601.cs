using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff_32601 : EnemyBuff
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
            this.defence_percentage = -0.8f;
            this.magicdefence_percentage = -0.8f;
        }
        else {
            this.defence_percentage = 0.0f;
            this.magicdefence_percentage = 0.0f;
            timer = 0;
        }
    }
}
