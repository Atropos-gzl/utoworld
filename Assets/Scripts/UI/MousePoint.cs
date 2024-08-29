using System;
using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Text = UnityEngine.UI.Text;

public class MousePoint : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Invertory invertory;
    private GameObject infoBox;
    public GameObject invertoryManager;
    public GameObject ItemInfoBox;
    void Start()
    {
        invertory = invertoryManager.GetComponent<Invertory>();
    }

    private void Update()
    {
        invertory = invertoryManager.GetComponent<Invertory>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (eventData.pointerCurrentRaycast.gameObject.name == "delete")
        {
            Debug.Log("进入物体");
            GameObject prefab = Resources.Load("UI/upgradetInfo") as GameObject;
            infoBox = Instantiate(prefab);
            infoBox.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position + new Vector3(150, 0, 150);
            infoBox.transform.SetParent(canvas.transform);
            infoBox.transform.GetChild(0).GetComponent<Text>().text="点击拆除塔获得" + (invertory.Selection.cost[invertory.Selection.level] * 0.6).ToString() + "金币";
        }else if (eventData.pointerCurrentRaycast.gameObject.name == "upgrade"&&invertory.Selection.level<=2)
        {
            GameObject prefab = Resources.Load("UI/upgradetInfo") as GameObject;
            infoBox = Instantiate(prefab);
            infoBox.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position + new Vector3(150, 0, 150);
            infoBox.transform.SetParent(canvas.transform);
            infoBox.transform.GetChild(0).GetComponent<Text>().text =
                "消耗" + invertory.Selection.cost[invertory.Selection.level + 1].ToString() + "金币升级塔";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       Destroy(infoBox);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
