using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnpointerEnter()
    {
       gameObject.GetComponentInChildren<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnpointerExit()
    {
       gameObject.GetComponentInChildren<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
