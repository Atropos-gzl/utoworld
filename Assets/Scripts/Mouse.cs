using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Mouse : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    public GameObject TurretSelection_Prefab;
    public GameObject UpAndRemove_Prefab;
    public GameObject TowerInfo;
    public GameObject EnemyInfo;
    private GameObject canvas;
    private GameObject go;
    private Sprite sprite;
    private LayerMask mask = ~(1 << 8);
    [HideInInspector] public Transform build_position;
    public Collider cube_collider;

    public TurretData Selection;

    // Start is called before the first frame update
    public GameObject myBag;

    //public bool isopen=false;
    public GameObject[] Solts;
    private Invertory inventoryManager;

    void Start()
    {
        TurretSelection_Prefab = Resources.Load<GameObject>("UI/TurretSelection");
        canvas = GameObject.FindGameObjectWithTag("canvas");
        // myBag = GameObject.Find("TowerBag");
        Debug.Log("测试inventoryManager");
        inventoryManager = GameObject.Find("InvertoryManager").GetComponent<Invertory>();
        TowerInfo = GameObject.Find("TowerInfo");
        // getSolts();

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.tag + " " + hit.transform.name);
                if (hit.transform.CompareTag("cube") && hit.collider.GetComponent<MapCube>().used)
                {
                    //获取建造炮塔的方块的组件
                    build_position = hit.transform;
                    cube_collider = hit.collider;


                    //生成选择炮塔的UI
                    Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    go = Instantiate(TurretSelection_Prefab);
                    go.GetComponent<Transform>().SetParent(canvas.transform);
                    float width = go.GetComponent<RectTransform>().rect.width;
                    float height = go.GetComponent<RectTransform>().rect.height;
                    go.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
                    go.GetComponent<RectTransform>()
                        .SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, height);

                    go.GetComponent<RectTransform>().anchoredPosition3D = pos;
                }

                else if (hit.transform.CompareTag("cube") && !hit.collider.GetComponent<MapCube>().used &&
                         hit.collider.GetComponent<MapCube>().memory.level <= 3)
                {
                    //获取建造炮塔的方块的组件
                    build_position = hit.transform;
                    cube_collider = hit.collider;
                    GameObject turret = hit.transform.GetComponent<MapCube>().turret;
                    TowerInfo.GetComponent<TowerInfo>().turret = turret;
                    TowerInfo.GetComponent<CanvasGroup>().alpha = 1.0f;
                    TowerInfo.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    TowerInfo.GetComponent<CanvasGroup>().interactable = true;
                    //生成炮塔升级与删除的UI
                    Selection = hit.collider.GetComponent<MapCube>().memory;
                    myBag.GetComponent<CanvasGroup>().alpha = 1.0f;
                    myBag.GetComponent<CanvasGroup>().interactable = true;
                    myBag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    inventoryManager.GetTower(cube_collider, Selection, turret);
                    inventoryManager.updateBag();
                    inventoryManager.updateTowerBag();

                    Time.timeScale = 0.0f;

                }
            }

        }
        if ((Input.GetMouseButtonUp(0))){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.tag + " " + hit.transform.name);
                if (hit.transform.CompareTag("Enemy") )
                {
                    Debug.Log(hit.transform.name);
                    EnemyInfo = GameObject.Find("MonsterInfo");
                    EnemyInfo.GetComponent<EnemyInfo>().enemy = hit.transform.gameObject;
                    EnemyInfo.GetComponent<CanvasGroup>().alpha = 1.0f;
                    EnemyInfo.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    EnemyInfo.GetComponent<CanvasGroup>().interactable = true;
                }
            }
        }


    }
}




