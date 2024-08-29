using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    public Transform originParent;
    private Invertory inventoryManager;
    private Turret turret;
    private Vector3 oripos;
    public GameObject itemInfoBox;
    public GameObject itemNameBox;
    public int id;

    public void Start()
    {
        inventoryManager = GameObject.Find("InvertoryManager").GetComponent<Invertory>();
        itemInfoBox = GameObject.Find("ItemInfo");
        itemNameBox = GameObject.Find("ItemName");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oripos = transform.position;
        //originParent = transform.parent;
        //transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        turret = inventoryManager.turret.GetComponent<Turret>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = oripos;
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        {

                Transform[] children = transform.GetComponentsInChildren<Transform>();
                int itemid = 0;
                foreach (Transform child in children) {   
 
                    if (child.name == "ItemImage")
                    {
                        itemid = Convert.ToInt32(child.gameObject.GetComponent<Image>().sprite.name);
                    }
                }
                Transform target = eventData.pointerCurrentRaycast.gameObject.transform.parent;
                int originId= transform.GetComponent<ItemOnDrag>().id;
                int targetId = target.GetComponent<ItemOnDrag>().id;
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite!=null&&eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite.name.StartsWith("3"))
                {
                    
                    int targetItemId = Convert.ToInt32(eventData.pointerCurrentRaycast.gameObject
                        .GetComponent<Image>().sprite.name);
                    string systhesisName;
                    if (itemid < targetItemId)
                    {
                        systhesisName = "[" + itemid.ToString() + "，" + targetItemId.ToString() + "]";
                    }
                    else
                    {
                        systhesisName = "[" + targetItemId.ToString() + "，" + itemid.ToString() + "]";
                    }
                    if (!inventoryManager.synthesis.ContainsKey(systhesisName))
                    {
                        Debug.Log("meiyouzhege");
                        transform.position = oripos;
                        GetComponent<CanvasGroup>().blocksRaycasts = true;
                        inventoryManager.updateBag();
                        inventoryManager.updateTowerBag();
                        return;
                    }
                    int newItemId = Convert.ToInt32(inventoryManager.synthesis[systhesisName]);
                    if (target.name == "gamerItem"&&transform.name=="towerItem")
                    {
                        turret.removeEquipment(originId);
                        inventoryManager.removeEquipment(targetId);
                        inventoryManager.addEquipment(newItemId,targetId);
                    }else if (target.name == "towerItem" && transform.name == "gamerItem")
                    {
                        inventoryManager.removeEquipment(originId);
                        turret.removeEquipment(targetId);
                        turret.addEquipment(newItemId,targetId);
                    }else if (target.name == transform.name &&target.name == "towerItem")
                    {
                        turret.removeEquipment(originId);
                        turret.removeEquipment(targetId);
                        turret.addEquipment(newItemId,targetId);
                    }else if (target.name== transform.name && target.name== "gamerItem")
                    {
                        inventoryManager.removeEquipment(originId);
                        inventoryManager.removeEquipment(targetId);
                        inventoryManager.addEquipment(newItemId, targetId);
                    }
                }
                else
                {
                   if (target.name == "gamerItem"&&transform.name=="towerItem")
                   {
                        turret.removeEquipment(originId);
                        inventoryManager.addEquipment(itemid,targetId);
                        
                   }else if (target.name == "towerItem" && transform.name == "gamerItem")
                   {
                            inventoryManager.removeEquipment(originId);
                            turret.addEquipment(itemid,targetId);
                            
                   }else if (target.name == transform.name &&target.name == "towerItem")
                   {
                            turret.removeEquipment(originId);
                            turret.addEquipment(itemid,targetId);
                        
                   }else if (target.name== transform.name && target.name== "gamerItem")
                   {
                            inventoryManager.removeEquipment(originId);
                            inventoryManager.addEquipment(itemid, targetId);

                   } 
                }
        }
        //transform.SetParent(originParent);
        inventoryManager.updateBag();
        inventoryManager.updateTowerBag();
        transform.position = oripos;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("点击了");
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage"&&eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite.name.StartsWith("3"))
        {
            string spriteName = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite.name;
            Debug.Log(spriteName);
            itemInfoBox.GetComponent<Text>().text = inventoryManager.ItemInfo[spriteName].description;
            //itemNameBox.GetComponent<Text>().text=inventoryManager.ItemInfo[spriteName].
            
        }
    }
}