using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static Bullets;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

public class Turret : MonoBehaviour
{
    
    public List<GameObject> enemies;
    public TurretData Selection;
    protected float timer;
    public int ID;
    public float rate;
    public float attack;
    public float magic_attack;
    public float crit_rate;
    public float crit_dmg;
    public float attack_range;
    public Dictionary<int,EquipmentData> equipments;
    public List<int> buffs;
    protected float attack_interval;
    public GameObject bullet;
    public GameBase gb;
    public TurretBuff tb;
    private Transform ShootPoint;
    private float range_tmp = 0;

    public delegate void IncreasePower(Turret turret, GameObject bullet, GameBase gb);
    public delegate void OnCritAttack(Turret turret, GameObject bullet, GameBase gb);
    public delegate void OnEnemyEnter(GameObject go);
    public event IncreasePower OnIncreasePowerEvent;
    public event OnCritAttack OnCritAttackEvent;
    public event OnEnemyEnter OnEnemyEnterEvent;
    

    public void IncreasePowerCompleted(Turret turret, GameObject bullet, GameBase gb)
    {
        OnIncreasePowerEvent?.Invoke(turret, bullet, gb);
    }

    public void OnCritAttackCompleted(Turret turret, GameObject bullet, GameBase gb)
    {
        OnCritAttackEvent?.Invoke(turret, bullet, gb);
    }

    public void OnEnemyEnterCompleted(GameObject go) { 
        OnEnemyEnterEvent?.Invoke(go);
    }

    // Start is called before the first frame update
    protected void Start()
    {
        range_tmp = GetComponent<SphereCollider>().radius;
        gb = GameObject.Find("GameManager").GetComponent<GameBase>();
        enemies =  new List<GameObject>();
        equipments = new Dictionary<int,EquipmentData>();
        timer = attack_interval;
        tb = gameObject.AddComponent<TurretBuff>();
        tb.enabled = true;
        tb = GetComponent<TurretBuff>();
        resetAttrib();
        //addEquipment(30101, 0);
    }

    // Update is called once per frame
    protected void Update()
    {
        //改变炮塔朝向  处理List
        GameObject target = enemies.FirstOrDefault();
        if (target == null && enemies.Count > 0)
        {
            enemies.RemoveAt(0);
        }
        if (target != null)
        {
            gameObject.GetComponent<Transform>().LookAt(target.transform);
        }
        else
        {
            if (timer > attack_interval)
                timer = attack_interval;
        }
        timer += UnityEngine.Time.deltaTime;

        calcuAttrib();

        if (range_tmp != this.attack_range * 10)
        {
            GetComponent<SphereCollider>().radius = this.attack_range * 10;
            range_tmp = this.attack_range * 10;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log(gameObject.name + " get enemy " + other.name);
            OnEnemyEnterCompleted(other.gameObject);
            enemies.Add(other.gameObject);

        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && enemies.Count == 0)
        {
            enemies.Add(other.gameObject);
        }
        if (timer > attack_interval && enemies.Count>0)
        {
            timer -= attack_interval;
            Attack();
            if (Selection.type == TurretType.Culverin)
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Reload");
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }


    protected virtual void Attack()
    {

        if (enemies.First() == null)
        {
            return;
        }

        //gameObject.GetComponentInChild<Animator>().SetTrigger("Fire");
        float randnum = UnityEngine.Random.Range(0, 1.0f);
        float[] damage = new float[3];
        damage[0] = attack;
        damage[1] = magic_attack;
        damage[2] = 0;

        //处理子弹
        if (ShootPoint == null)
        {
            ShootPoint = GameUtility.FindChildWithName(transform, "ShootPoint");
        }

        //GameObject go = Instantiate(bullet, transform.position+new Vector3(0,1.6f,0), Quaternion.identity);
        Vector3 target = enemies.First().transform.position;
        gameObject.GetComponent<Transform>().LookAt(target);

        GameObject go = Instantiate(bullet, ShootPoint.position, Quaternion.identity);
        go.transform.SetParent(transform);
        //设置暴击
        //randnum = -0.1f;
        if (randnum < crit_rate)
        {
            damage[0] *= crit_dmg;
            damage[1] *= crit_dmg;
            go.GetComponent<Bullets>().isCrit = true;
            //GameObject vfx_crit = go.transform.Find("VFX_Crit").gameObject;
            //go.transform.GetChild(0).position = go.transform.position;
            //if (vfx_crit != null)
            //{
            //    vfx_crit.SetActive(true);
            //    go.transform.GetChild(0).position = new Vector3(0, 0, 0);
            //}
            //else
            //{
            //    Debug.Log("cant find vfx");
            //}

            //GameObject vfx_crit = GameResourceManager.getPrefabs("VFX_Crit");
            //Instantiate(vfx_crit, new Vector3(0,0,0),  Quaternion.identity, go.transform);
            OnCritAttackCompleted(this, go, gb);
            Debug.Log("crit");
        }
        else
        {
            go.transform.Find("VFX_Crit").gameObject.SetActive(false);
        }
        //设定子弹初始化
        go.GetComponent<Bullets>().datatype = Selection;
        go.transform.LookAt(enemies.First().transform);
        
        if (Selection.type==TurretType.Culverin)
        go.GetComponent<Rigidbody>().AddForce(Force(target)*0.6f,ForceMode.Impulse);
        else
        {
            go.GetComponent<Rigidbody>().AddForce((target - transform.position) * 100);
        }
        go.GetComponent<Bullets>().target = enemies.First();
        
        go.GetComponent<Bullets>().damage = damage;
        IncreasePowerCompleted(this, go, gb);
        //播放动画
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }



    public Vector3 Force(Vector3 target)
    {
        float dx = target.x - transform.position.x;
        float dz = target.z - transform.position.z;
        float dis = Mathf.Pow((dx * dx + dz * dz), 0.5f);
        float speed_value = Mathf.Pow(9.8f * dis, 0.5f);
        float y = speed_value * Mathf.Pow(2.0f, 0.5f) / 2;
        Vector3 b = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 a = new Vector3(1, 0, 0);
        float sin = Vector3.Cross(a, b).magnitude / b.magnitude;
        float cos = Vector3.Dot(a, b) / b.magnitude;
        Vector3 ans = new Vector3(cos * b.magnitude, y, sin * b.magnitude);
        return ans;
    }



    public void resetAttrib()
    {
        ID = Selection.ID[Selection.level];
        rate = Selection.rate[Selection.level];
        attack = Selection.attack[Selection.level];
        magic_attack = Selection.magic_attack[Selection.level];
        crit_dmg = Selection.crit_dmg[Selection.level];
        crit_rate = Selection.crit_rate[Selection.level];
        attack_range = Selection.range[Selection.level];
        attack_interval = 1.0f / rate;
        bullet = Selection.bullet_Prefab;
        
    }



    public void calcuAttrib()
    {
        //重置
        resetAttrib();
        // 加算部分
        //if (equipments == null)
        //{
        //    return;
        //}
        
        if (equipments!=null && equipments.Count != 0)
        {
            foreach (EquipmentData data in equipments.Values)
            {
                this.rate += data.attack_speed;
                this.attack += data.attack;
                this.magic_attack += data.magic_attack;
                this.crit_rate += data.crit_rate;
                this.crit_dmg += data.crit_dmg;
                this.attack_range += data.attack_range;
            }
        }
        //buff calc
        
        this.attack = this.attack * tb.attack_perentage + tb.attack;
        this.magic_attack = this.magic_attack * tb.magic_attack_percentage + tb.magic_attack;
        //Debug.Log("attck_range: " + attack_range + " " + tb.attack_range_percentage + " " + tb.attack_range);
        this.attack_range = this.attack_range * tb.attack_range_percentage + tb.attack_range;
        
        crit_rate = crit_rate * tb.crit_rate_percentage + tb.crit_rate;
        this.crit_dmg = this.crit_dmg * tb.crit_dmg_percentage + tb.crit_dmg;
        this.rate = this.rate * tb.attack_speed_percentage + tb.attack_speed;
        attack_interval = 1.0f / rate;
        
    }



    public void addEquipment(int equipment_id, int dic_id)
    {
        Debug.Log("添加装备 " + equipment_id + " to " + dic_id);
        equipments.Add(dic_id, EquipmentManager.equipmentMap[equipment_id]);
        try
        {
            EquipmentFunc ef = gameObject.AddComponent(Type.GetType("EquipFunc_" + equipment_id.ToString())) as EquipmentFunc;
            ef.enabled = true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Log("cant find component " + equipment_id.ToString());
        }
        calcuAttrib();

    }



    public void removeEquipment(int dic_id)
    {
        EquipmentData data = equipments[dic_id];
        int equipment_id = data.ID;
        Debug.Log("移除装备" + equipment_id);
        try
        {
            EquipmentFunc ef = gameObject.GetComponent(Type.GetType("EquipFunc_" + equipment_id.ToString())) as EquipmentFunc;
            Destroy(ef);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Log("cant remove component " + equipment_id.ToString());
        }
        equipments.Remove(dic_id);
        calcuAttrib();
    }


}
