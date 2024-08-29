using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PlayerController : Singleton<PlayerController>
{

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetDestination(GetMousePos());
        }
    }

    Vector3 GetMousePos()
    {
        Vector3 ret = new Vector3(0, 0, 0);
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            ret = hitInfo.point;
        }
        return ret;
    }
    void SetDestination(Vector3 des)
    {
        GetComponent<NavMeshAgent>().SetDestination(des);
    }
}
