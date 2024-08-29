using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSV;

public class EnemySpawner : MonoBehaviour
{
    public static int LiveCount = 0;
    public Wave[] waves;
    private int id;
    //public Transform start;
    public float WaveRate = 0.6f;
    public int Wave_num = 0;
    

    Quaternion q = Quaternion.Euler(0, 270, 0);
    // Start is called before the first frame update
    void Start()
    {
        LiveCount = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void GameEnd()
    {
        StopCoroutine(SpawnEnemy());
    }



    IEnumerator SpawnEnemy()
    {
        Wave_num = 1;
        //Transform t = GameObject.Find("Start").transform;
        for(int i=0; i < waves.Length; i++)
        {
            int cur = 0;
            GameObject.Find("Wave_num").GetComponent<Text>().text = Wave_num.ToString();
            Wave wave = waves[i];
            //Debug.Log("Current wave : " + i.ToString());
            foreach (int id in wave.waveIDs)
            {
                //Debug.Log("Current waveID : " + id.ToString());
                List<string> starts = EnemyManager.getStarts(id);
                int num = EnemyManager.getNum(id);
                List<int> routeIDs = EnemyManager.getRouteID(id);
                int finalJ = 0;
                List<Transform> routes = new List<Transform>();
                for (int j = 0; j < starts.Count; j++)
                {
                    bool flag = true;
                    int routeID = routeIDs[j];
                    
                    List<int> route = EnemyManager.getRoute(routeID);
                    List<Transform> temproutes = new List<Transform>();
                    foreach (int routeid in route)
                    {
                        //Debug.Log("Routeid : " + routeid.ToString());
                        Transform routepoint = null;
                        try
                        {
                            routepoint = GameObject.Find("point (" + routeid.ToString() + ")").GetComponent<Transform>();
                        }
                        catch(System.NullReferenceException e)
                        {
                            flag = false;
                            finalJ = j;
                            break;
                        }
                        finally
                        {
                            if(routepoint != null)
                            {
                                temproutes.Add(routepoint);
                            }
                            
                        }
                    }
                    if(!flag)
                    {
                        continue;
                    }
                    //Debug.Log("Starts: " + starts[j]);
                    
                    //GameObject monster = Resources.Load<GameObject>("Enemy/" + prefab);
                    
                    if (flag)
                    {
                        routes = temproutes;
                        break;
                    }
                    
                }
                Transform starttrans = GameObject.Find(starts[finalJ]).GetComponent<Transform>();
                string prefab = EnemyManager.getPrefabs(id);
                GameObject monster = Resources.Load<GameObject>("Prefabs/" + prefab);
                //Debug.Log("Prefab: " + monster.GetType());
                //Debug.Log("Starts: " + starttrans.GetType());
                for (int n = 0; n < num; n++)
                {
                    GameObject go = Instantiate(monster, starttrans.position, q);
                    go.GetComponent<Enemy>().route = routes;
                    yield return new WaitForSeconds(1.0f);
                    LiveCount++;
                }
                cur++;
                //monster = null;
                if (i == waves.Length - 1 && cur == wave.waveIDs.Length - 1)
                    continue;
                yield return new WaitForSeconds(waves[i].rate);
            }

            //Transform start_point = singlea.start;
            //int j = 0;
            //foreach(GameObject enemy in singlea.Enemy_Prefabs)
            //{
            //    Transform
            //    GameObject go = Instantiate(enemy, start_point.position, q);
                    
            //    yield return new WaitForSeconds(1.0f);
            //    LiveCount++;
            //    j++;
            //    i++;
            //    if (i == wave.Starts.Length - 1 && j == singlea.Enemy_Prefabs.Length - 1)
            //        continue;
            //    yield return new WaitForSeconds(wave.rate);
            //}
            
            //for (i = 0; i < wave.count; i++)
            //{
            //    for (int j = 0; j < wave.Starts.Length; j++)
            //    {
            //        public Start start = wave.
                   
            //    }
                //for(int j=0; j< wave.Enemy_Prefabs.Length; j++)
                //{
                //    GameObject go =  Instantiate(wave.Enemy_Prefabs[j], start.position, q);
                //    yield return new WaitForSeconds(1.0f);
                //    LiveCount++;
                //    if (i == wave.count - 1 && j == wave.Enemy_Prefabs.Length - 1)
                //        continue;
                //    yield return new WaitForSeconds(wave.rate);
                //}
            //}
            Debug.Log("Wave " + Wave_num + " End");
            Wave_num++;
            
            while (LiveCount > 0)
                yield return 0;
            yield return new WaitForSeconds(WaveRate);
            

        }

        CanvasGroup ui = GameObject.Find("Account_UI").GetComponent<CanvasGroup>();
        GameObject.Find("Account").GetComponent<Text>().color = Color.yellow;
        ui.alpha = 1;
        ui.blocksRaycasts = true;
        ui.interactable = true;
        Time.timeScale = 0;
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Pause();
    }
}
