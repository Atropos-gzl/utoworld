using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuff_20601 : EnemyBuff
{
    public int heal_value = 20;
    public GameObject owner;
    // Start is called before the first frame update
    new void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        
        if (owner != null)
        {
            Debug.Log("buff 20601 is working");
            life = (int)(Time.deltaTime / heal_value);
        }
        else
        {
            Debug.Log("buff 20601 is destroyed");
            Destroy(this);
        }
    }
}
