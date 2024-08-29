using CSV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    public int WaveNum;
    public int RouteNum;
    public int MonsterNum;
    private CSVData data;
    public static EnemyData[] enemies;
    public static Dictionary<int, EnemyData> enemyMap;
    public static WaveData[] waves;
    public static Dictionary<int, WaveData> wavesMap;
    public static RouteData[] routes;
    public static Dictionary<int, RouteData> routesMap;
    // Start is called before the first frame update
    void Awake()
    {
        data = new CSVData();   
        data.OpenCSV(Application.dataPath + "/Config/ConfigFile_MonsterData.csv");
        enemies = new EnemyData[MonsterNum];
        enemyMap = new Dictionary<int, EnemyData>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = data.DatafromCSV<EnemyData>(i);
            //Debug.Log("read Enemy Speed:   "+enemies[i].move_speed);
            enemyMap.Add(enemies[i].ID, enemies[i]);
        }
        data.OpenCSV(Application.dataPath + "/Config/ConfigFile_WaveData.csv");
        waves = new WaveData[WaveNum];
        wavesMap = new Dictionary<int, WaveData>();
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i] = data.DatafromCSV<WaveData>(i);
            //Debug.Log("read Enemy Speed:   " + waves[i].prefabs);
            wavesMap.Add(waves[i].ID, waves[i]);
        }
        data.OpenCSV(Application.dataPath + "/Config/ConfigFile_RouteData.csv");
        routes = new RouteData[RouteNum];
        routesMap = new Dictionary<int, RouteData>();
        for (int i = 0; i < routes.Length; i++)
        {
            routes[i] = data.DatafromCSV<RouteData>(i);
            //Debug.Log("read RouteData ID:   " + routes[i].ID);
            routesMap.Add(routes[i].ID, routes[i]);
        }
    }

    void Start()
    {
        
    }

    public static List<string> getStarts(int id)
    {
        List<string> Starts = new List<string>();
        string[] start = wavesMap[id].starts.Split(';');
        foreach(string point in start)
        {
            Starts.Add(point);
        }
        return Starts;
    }

    public static string getPrefabs(int id)
    {
        string prefab = wavesMap[id].prefabs;

        return prefab;
    }

    public static int getNum(int id)
    {
        string num = wavesMap[id].num;
        return int.Parse(num);
    }

    public static List<int> getRouteID(int id)
    {
        List<int> Routes = new List<int>();
        string[] route = wavesMap[id].route.Split(';');
        foreach (string point in route)
        {
            Routes.Add(int.Parse(point));
        }
        return Routes;
    }

    public static List<int> getRoute(int id)
    {
        //Debug.Log("routID : " + id.GetType());
        List<int> Routes = new List<int>();
        string[] route = routesMap[id].points.Split(';');
        foreach (string point in route)
        {
            Routes.Add(int.Parse(point));
        }
        return Routes;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

public class EnemyData
{
    public int ID { set; get; }
    public int health { set; get; }
    public float defence { set; get; }
    public float magic_defence { set; get; }
    public float move_speed { set; get; }
    public float resistance_rate { set; get; }
    public int toplayer_damage { set; get; }
    public int coindrop_amount { set; get; }
    public float attack { set; get; }
    public float magic_attack { set; get; }
    public float attack_speed { set; get; }
    public float attack_range { set; get; }
    public int monster_level { set; get; }
    public int difficulty_level { set; get; }

    public EnemyData(int id, int health, float defence, float magicdefence, float movespeed, float resistancerate, int toPlayerDamage, int coindropAmount, float attack, float magicAttack, float attackSpeed, float attackRange, int monsterLevel, int difficultyLevel)
    {
        this.ID = id;
        this.health = health;
        this.defence = defence;
        this.magic_defence = magicdefence;
        this.move_speed = movespeed;
        this.resistance_rate = resistancerate;
        this.toplayer_damage = toPlayerDamage;
        this.coindrop_amount = coindropAmount;
        this.attack = attack;
        this.magic_attack = magicAttack;
        this.attack_speed = attackSpeed;
        this.attack_range = attackRange;
        this.monster_level = monsterLevel;
        this.difficulty_level = difficultyLevel;
    }

    public EnemyData() { }
}

public class WaveData
{
    public int ID { get; set; }
    public string starts { get; set; }
    public string prefabs { get; set; }
    public string num { get; set; }
    public string route { get; set; }

    public WaveData(int id, string starts, string prefabs, string num, string route)
    {
        this.ID = id;
        this.starts = starts;
        this.prefabs = prefabs;
        this.num = num;
        this.route = route;
    }
    public WaveData() { }
}

public class RouteData
{
    public int ID { get; set; }
    public string points { get; set; }

    public RouteData(int id, string points)
    {
        this.ID = id;
        this.points = points;
    }

    public RouteData() { }
}