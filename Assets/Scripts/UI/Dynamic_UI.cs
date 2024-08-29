using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Disappear();
        }
    }

    public void Disappear()
    {
        GameObject select_ui = GameObject.FindGameObjectWithTag("Select_UI");
        GameObject upgrade_ui = GameObject.FindGameObjectWithTag("Upgrade_UI");
        Destroy(upgrade_ui);
        Destroy(select_ui);
    }
}
