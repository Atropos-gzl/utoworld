using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserTurret : Turret
{
    
    protected override void Attack()
    {
        //gameObject.GetComponent<Animator>().SetTrigger("Fire");
        // gameObject.transform.LookAt(enemies.First().transform);
        //float[] damage = new float[3];
        //damage[0] = attack;
        //damage[1] = magic_attack;
        //int status = enemies.First().GetComponent<Enemy>().TakeDamage(damage);
        base.Attack();
    }

}
