using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31001 :  EquipmentFunc
{
    public float timer = 0;
    public float interval = 10.0f;
    public bool done = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        count = 0;
        max_count = 8;
        Turret turret = GetComponent<Turret>();
        buff = gameObject.GetComponent<TurretBuff>();
        buff.crit_rate -= 0.05f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!done) {
            timer += Time.deltaTime;
            if (timer > interval) { 
                done = true;
                buff.attack_speed += 10.0f;
                timer = 0;
            }
        }
    }

    protected override void IncreasePower(Turret turret, GameObject bullet, GameBase gb)
    {
        if (done) { 
            count++;
            if (count == max_count)
            {
                done = false;
                count = 0;
                buff.attack_speed -= 10.0f;
            }
        }
    }

    protected override void OnDestroy()
    {
        if (done) {
            buff.attack_speed -= 10.0f;
        }
        buff.crit_rate += 0.05f;
    }
}
