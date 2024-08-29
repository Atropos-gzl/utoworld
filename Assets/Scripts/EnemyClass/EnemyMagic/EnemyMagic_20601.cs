using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagic_20601 : EnemyMagic
{
    // Start is called before the first frame update
    public float range;
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        timer += Time.deltaTime;
        if(timer >= magic_interval && !owner.substates[Enemy.SubState.Silence])
        {
            Loadbuff();
            timer = 0;
        }
    }

    public override void Loadbuff()
    {
        Transform trans = owner.GetComponent<Transform>();
        Collider[] hits = Physics.OverlapSphere(trans.position, range);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                EnemyBuff_20601 eb = hit.gameObject.GetComponent<EnemyBuff_20601>();
                if (eb == null)
                {
                    eb.gameObject.AddComponent<EnemyBuff_20601>();
                    eb.enabled = true;
                    targets.Add(hit.gameObject);
                }
            }
        }
    }
}
