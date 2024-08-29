using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff_21201 : EnemyBuff
{
    // Start is called before the first frame update
    public GameObject owner;
    new void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        
        if (owner != null)
        {
            Debug.Log("buff 21201 is working");
            magicdefence_percentage = 0.8f;
        }
        else
        {
            Debug.Log("buff 21201 is destroyed");
            Destroy(this);
        }
    }
}
