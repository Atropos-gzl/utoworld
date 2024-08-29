using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy owner;
    public List<GameObject> targets;
    public EnemyBuff buff;
    public float magic_interval = 0;
    public float timer = 0;
    public void Start()
    {
        magic_interval = owner.magic_interval;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Loadbuff()
    {

    }

    public virtual void CreateMonster()
    {

    }
}
