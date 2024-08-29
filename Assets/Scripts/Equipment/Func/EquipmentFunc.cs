using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentFunc : MonoBehaviour
{
    public GameObject Summoned_Creature;
    public TurretBuff buff;
    public Turret turret;
    public Bullets bullet;
    public GameBase gb;
    public string creature_name = "Cube";
    public int count = 0;
    public int total_count = 0;
    public int max_count = 0;
    public int attack_interval = 4;
    public float value = 0; 

    protected virtual void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        buff = GetComponent<TurretBuff>();
        gb = GameBase.getInstance();
        //bullet.onKilleds.Add(KillEnemy);
        //bullet.OnKilledEvent += KillEnemy;
        //turret.OnIncreasePowerEvent += IncreasePower;
        //turret.OnCritAttackEvent += OnCritAttack;
        //CreateMonster();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnDestroy()
    {
        DestroyMonster();
    }

    protected virtual void CreateMonster()
    {
        Debug.Log(gameObject.name+" create "+ creature_name);
        Summoned_Creature = Instantiate(GameResourceManager.getPrefabs(creature_name), transform.position, Quaternion.identity, transform);

    }

    protected virtual void DestroyMonster() {
        Debug.Log(gameObject.name + " destroy " + creature_name);
        Destroy(Summoned_Creature);
    }

    protected virtual void IncreasePower(Turret turret, GameObject bullet, GameBase gb)
    {
        count++;
        Debug.Log(turret.ID + " Attack " + count);
        count %= attack_interval;
    }

    protected virtual void KillEnemy(Turret turret, GameBase gamebase)
    {
        Debug.Log(turret.ID + " Kill Enemy");
    }

    protected virtual void OnCritAttack(Turret turret, GameObject bullet, GameBase gb)
    {

    }

    protected virtual void OnMoneyIncrease()
    {

    }

    protected virtual void OnHit(Bullets bt, GameObject go)
    {

    }

    protected virtual void OnEnemyEnter(GameObject go)
    {

    }




}
