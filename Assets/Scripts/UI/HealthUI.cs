using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject target = null;
    private Transform Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 WorldPositioin = new Vector3(target.transform.position.x, target.transform.position.y - 0.5f, target.transform.position.z);
        Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, WorldPositioin);
        Vector2 canvas_pos = Canvas.InverseTransformPoint(position);
        transform.localPosition = canvas_pos;
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
