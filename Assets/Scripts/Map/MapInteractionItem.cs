using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapInteractionItem : MonoBehaviour
{
    public int priority = 0;
    public GameObject tipPrefab;
    private GameObject tip;

    void Start()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        if (tipPrefab != null)
        {
            tip = Instantiate(tipPrefab, canvas);
            tip.SetActive(false);
        }
    }
    public abstract void InteractionAction();
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerEvent.Instance.InsertAction(this);
            tip?.SetActive(true);
        }


    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerEvent.Instance.DeleteAction(this);
            tip?.SetActive(false);
        }
    }

    void Update()
    {
        if (tip != null && tip.activeSelf == true)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            //Debug.Log(screenPosition);
            screenPosition.Set(screenPosition.x+180.0f , screenPosition.y  , screenPosition.z);
            if (screenPosition.x > 1920)
            {
                screenPosition.Set(screenPosition.x-360.0f , screenPosition.y  , screenPosition.z);
            }
            tip.transform.position = screenPosition;
        }
    }
}

/*
public class MapInterationItemComparer : IComparer<MapInteractionItem>
{
    public int Compare(MapInteractionItem x, MapInteractionItem y)
    {
        if(x == null || y == null)
        {
            return Comparer<MapInteractionItem>.Default.Compare(x, y);
        }
        return x.priority < y.priority ? 1 : -1;
    }
}
*/
