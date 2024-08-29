using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSV;
using UnityEngine.EventSystems;

public class Invertory : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private BuildManager _buildManager;
    public GameObject MyBag;

    private Collider _collider;

    public TurretData Selection;

    // Start is called before the first frame update
    public static Dictionary<int, int> itemDict;
    public GameObject[] Solts;
    public GameObject turret;
    GameObject[] towerSolts = new GameObject[9];

    private Dictionary<int, EquipmentData> dict;
    public Item[] Items;
    public CSVData data;
    public Dictionary<string, string> synthesis;
    public GameObject GamerBag;
    public Dictionary<string, Item> ItemInfo;
    public GameObject threeSelectOne;
    public GameObject canvas;
    private GameObject infoBox;


    private void Awake()
    {
        data = new CSVData();   
        data.OpenCSV(Application.dataPath + "/Config/ConfigFile_ComponentData.csv");
        Items = new Item[30];
        synthesis = new Dictionary<string, string>();
        ItemInfo = new Dictionary<string, Item>();
        for (int i = 0; i < 30; i++)
        {
            Debug.Log("du");
            Items[i] = data.DatafromCSV<Item>(i);
            ItemInfo.Add(Items[i].ID, Items[i]);
            if (Items[i].func=="null")continue;
            synthesis.Add(Items[i].func,Items[i].ID);
            
        }
        itemDict = new Dictionary<int, int>
        {

            { 1, 30101 },
            { 2, 30201 },
            { 3, 30301 },
            { 4, 30401 },
            { 5, 30501 },
            { 6, 30601 },
            { 7, 30701 },
            { 8, 30801 },
            { 9, 30901 },
            { 10, 31001 },
            { 11, 31101 },
            { 12, 31201 },
            { 13, 31301 },
            { 14, 31401 },
            { 15, 31501 },
            { 16, 31601 },
            { 17, 31701 },
            { 18, 31801 },
            { 19, 31901 },
            { 20, 32001 },
            { 21, 32101 },
            { 22, 32201 },
            { 23, 32301 },
            { 24, 32401 },
            { 25, 32501 },
            { 26, 32601 },
            { 27, 32701 },
            { 28, 32801 },
            { 29, 32901 },
            { 30, 33001 }
        };
    }

    void Start()
    {
        getSolts();
        _buildManager = GameObject.Find("GameManager").GetComponent<BuildManager>();
        
    }
    
    public void updateBag()
    
    {
        //getSolts();
        foreach (GameObject g in Solts)
        {
            g.GetComponent<Image>().sprite = null;
            g.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
        }
        foreach (int id in itemDict.Keys)
        {
            Sprite sp = GameResourceManager.getSprite(itemDict[id].ToString());
            Solts[id].GetComponent<Image>().sprite = sp;
            Image image = Solts[id].GetComponent<Image>();
            Color currentColor = image.color;
            currentColor.a = 1.0f;
            image.color = currentColor;
        }

    }

    public void updateTowerBag()
    {
        dict = turret.GetComponent<Turret>().equipments;
        foreach(GameObject g in  towerSolts)
        {
            g.GetComponent<Image>().sprite = null;
            g.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
        }
        foreach(int id in dict.Keys)
        {
            //获取图片
            Sprite sp = GameResourceManager.getSprite(dict[id].ID.ToString());
            towerSolts [id].GetComponent<Image>().sprite = sp;
            Image image = towerSolts[id].GetComponent<Image>();
            Color currentColor = image.color;
            currentColor.a = 1.0f;
            image.color = currentColor;
        }
    }
    

    public void closebag()
    {
        MyBag.GetComponent<CanvasGroup>().alpha = 0.0f;
        MyBag.GetComponent<CanvasGroup>().interactable = false;
        MyBag.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Time.timeScale = 1.0f;
    }

    public void UpgradeTurret()
    {
        _buildManager.OnUpgradeSelected(Selection, _collider);
    }

    public void DestroryTurret()
    {
        _buildManager.RemoveTurret(_collider);
        MyBag.GetComponent<CanvasGroup>().alpha = 0.0f;
        MyBag.GetComponent<CanvasGroup>().interactable = false;
        MyBag.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void GetTower(Collider cube_collider, TurretData turretData,GameObject tower)
    {
        _collider = cube_collider;
        Selection = turretData;
        turret = tower;
        dict = turret.GetComponent<Turret>().equipments;
    }

    void getSolts()
    {
        Solts = new GameObject[36];
        towerSolts = new GameObject[9];
        int i = 0;
        Transform[] children = MyBag.transform.GetComponentsInChildren<Transform>();
        GameObject gamerbag = new GameObject();
        foreach (Transform child in children)
        {
            if (child.name == "gamerbag")
            {
                gamerbag = child.gameObject;
            }

        }

        children = gamerbag.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "ItemImage")
            {
                Solts[i] = child.gameObject;
                i++;
            }

            if (i == 36)
            {
                break;
            }
        }

        //get towSlots
        i = 0;
        children = MyBag.transform.GetComponentsInChildren<Transform>();
        GameObject towerBag = new GameObject();
        foreach (Transform child in children)
        {
            if (child.name == "tower")
            {
                towerBag = child.gameObject;
            }

        }
        children = towerBag.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "ItemImage")
            {
                //Debug.Log("find Solt" + i);
                towerSolts[i] = child.gameObject;
                i++;
            }
            if (i == 9)
            {
                break;
            }
        }

    }
    public void addEquipment(int equipment_id, int dic_id)
    
    {
        Debug.Log("itemDict add "+dic_id.ToString());
        itemDict.Add(dic_id,equipment_id);
          
    }public void removeEquipment(int dic_id)
    {
        Debug.Log("itemDict remove "+dic_id.ToString());
        itemDict.Remove(dic_id);
            
    }

    public void GetItem(int [] items)
    {
        GameObject tSelectone = Instantiate(threeSelectOne);
        tSelectone.transform.SetParent(canvas.transform);
        tSelectone.transform.position = canvas.transform.position;
        ThreeSelectOne threeSelectOneui = tSelectone.GetComponent<ThreeSelectOne>();
        if (items == null)
        {
            threeSelectOneui.displayItem(new Item[]
            {
                Items[0],Items[1],Items[2]
            });
            Debug.Log("231");
        }
        else
        {
            
            Item[] itemgroup = new Item[3];
            itemgroup[0] = ItemInfo[items[0].ToString()];
            itemgroup[1] = ItemInfo[items[1].ToString()];
            itemgroup[2] = ItemInfo[items[2].ToString()];
            threeSelectOneui.displayItem(itemgroup);
        }
        
    }
    public void GetItems()
    {
        GameObject tSelectone = Instantiate(threeSelectOne);
        tSelectone.transform.SetParent(canvas.transform);
        tSelectone.transform.position = canvas.transform.position;
       
        ThreeSelectOne threeSelectOneui = tSelectone.GetComponent<ThreeSelectOne>();
       
            threeSelectOneui.displayItem(new Item[]
            {
                Items[0],Items[1],Items[2]
            });
        
        
    }

    public void addItem(int itemId)
    {   
        for (int i = 0; i <= 36; i++)
        {
            if (!itemDict.ContainsKey(i))
            {
                itemDict.Add(i,itemId);
                return;
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("射到了物体");
        if (eventData.pointerCurrentRaycast.gameObject.name == "delete")
        {
            Debug.Log("进入物体");
            GameObject prefab = Resources.Load("Assets/Prefabs/UI/upgradetInfo") as GameObject;
            infoBox = Instantiate(prefab);
            infoBox.transform.position = eventData.position + new Vector2(10, 10);
            infoBox.transform.GetChild(0).GetComponent<Text>().text =
                "点击拆除塔获得" + (Selection.cost[Selection.level] * 0.6).ToString() + "金币";
        }else if (eventData.pointerCurrentRaycast.gameObject.name == "upgrade"&&Selection.level<=2)
        {
            GameObject prefab = Resources.Load("Assets/Prefabs/UI/upgradetInfo") as GameObject;
            infoBox = Instantiate(prefab);
            infoBox.transform.position = eventData.position + new Vector2(10, 10);
            infoBox.transform.GetChild(0).GetComponent<Text>().text =
                "消耗" + Selection.cost[Selection.level + 1].ToString() + "金币升级塔";
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("退出物体");
        Destroy(infoBox);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("射到了物体");
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        {
            Debug.Log("点击了物体");
        }
    }
}