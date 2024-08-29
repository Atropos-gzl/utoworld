using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target = null;
    private Transform Canvas;

    void Start()
    {
        Canvas = GameObject.Find("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
