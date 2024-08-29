using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_30901 : EquipmentFunc
{
    public float timer = 0;
    public float interval = 2.0f;
    
    public bool done = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        gb = GameObject.Find("GameBase").GetComponent<GameBase>();
        buff = gameObject.GetComponent<TurretBuff>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            gb.ChangeMoney(1);
        }
        if (!done && gb.Money >= 1000)
        {
            buff.attack_perentage += 0.3f;
            buff.attack_speed += 0.3f;
            done = true;
        }
        else if (done && gb.Money < 1000) {
            buff.attack_perentage -= 0.3f;
            buff.attack_speed -= 0.3f;
            done = false;
        }

    }

    protected override void OnDestroy()
    {
        if (done) {
            buff.attack_perentage -= 0.3f;
            buff.attack_speed -= 0.3f;
        }
    }
}
