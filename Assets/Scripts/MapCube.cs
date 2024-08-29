using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public bool used = true;
    private Renderer m_renderer;
    public TurretData memory;
    public int level;
    public GameObject turret;
    public int times = 0;
    private Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        colors = new Color[m_renderer.materials.Length];
        level = 0;
        //int i = 0;
        //foreach (Material m in m_renderer.materials)
        //{
        //    colors[i++] = m.color;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()==false&&Time.timeScale!=0)
        {
            //m_renderer.material.color = Color.red;
            //int i = 0;
            //foreach (Material m in m_renderer.materials)
            //{
            //    m.color = Color.red;
            //}
        }
    }

    private void OnMouseExit()
    {
        //m_renderer.material.color = Color.white;
        //int i = 0;
        //foreach (Material m in m_renderer.materials)
        //{
        //    m.color = colors[i++];
        //}

    }

    
}
