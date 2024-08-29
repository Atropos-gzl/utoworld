using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CrossBow : Turret
{
    bool isMultiple;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        isMultiple = Selection.level > 1;

    }

    // Update is called once per frame
    protected  new void Update()
    {
        base .Update();
        //GameObject target = enemies.FirstOrDefault();
        //if (target == null && enemies.Count > 0)
        //{
        //    enemies.RemoveAt(0);
        //}
        //if (target != null)
        //{
        //    gameObject.GetComponent<Transform>().LookAt(target.transform);
        //}
        //else
        //{
        //    timer = rate;
        //}
        //timer += Time.deltaTime;



    }

    protected new void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && enemies.Count == 0)
        {
            enemies.Add(other.gameObject);
        }
        if (timer > attack_interval && enemies.Count > 0)
        {
            timer -= attack_interval;
            Attack();

        }

    }

    protected override void Attack()
    {
        //三支箭
        if (isMultiple)
        {
            //播放动画
            gameObject.GetComponent<Animator>().SetTrigger("Fire");

            //处理子弹
            Vector3 pos = transform.position;
            GameObject go2 = Instantiate(Selection.bullet_Prefab, pos + new Vector3(0.6f, 1.14f, 0), Quaternion.identity);
            Quaternion ori = go2.transform.rotation;
            go2.transform.LookAt(enemies[0].transform);
            Quaternion lookat = go2.transform.rotation;
            GameObject go1 = Instantiate(Selection.bullet_Prefab, pos + new Vector3(0.6f,1.14f,0), Quaternion.Euler(0,  Quaternion.Angle(ori,lookat), 0)); 
            GameObject go3 = Instantiate(Selection.bullet_Prefab, pos + new Vector3(-0.6f, 1.14f, 0), Quaternion.Euler(0, Quaternion.Angle(ori, lookat), 0));
            go1.transform.LookAt(enemies[0].transform);
            go3.transform.LookAt(enemies[0].transform);
            go1.GetComponent<Bullets>().datatype = Selection;
            go2.GetComponent<Bullets>().datatype = Selection;
            go3.GetComponent<Bullets>().datatype = Selection;

            

            Vector3 target = enemies.First().transform.position;
            go1.GetComponent<Rigidbody>().AddForce((target - transform.position) * 150);
            go2.GetComponent<Rigidbody>().AddForce((target - transform.position) * 150);
            go3.GetComponent<Rigidbody>().AddForce((target - transform.position) * 150);
            go1.GetComponent<Bullets>().isMultiple = true;
            go2.GetComponent<Bullets>().isMultiple = true;
            go3.GetComponent<Bullets>().isMultiple = true;

            go1.GetComponent<Bullets>().target = enemies.First(); 
            go2.GetComponent<Bullets>().target = enemies.First();
            go3.GetComponent<Bullets>().target = enemies.First();

        }
        //一支箭
        else
        {
            //播放动画
            //gameObject.GetComponent<Animator>().SetTrigger("Fire");

            ////处理子弹
            //GameObject go = Instantiate(Selection.bullet_Prefab, transform.position + new Vector3(0, 1.6f, 0), Quaternion.identity);
            //go.GetComponent<Bullets>().datatype = Selection;
            //go.transform.LookAt(enemies.First().transform);
            //Vector3 target = enemies.First().transform.position;
            //if (Selection.type == TurretType.Culverin)
            //    go.GetComponent<Rigidbody>().AddForce(Force(target) * 0.7f, ForceMode.Impulse);
            //else if (Selection.type == TurretType.CrossBow)
            //{
            //    go.GetComponent<Rigidbody>().AddForce((target - transform.position) * 150);
            //}
            //go.GetComponent<Bullets>().target = enemies.First();
            base.Attack();
        }
       
    }

}
