using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float move_speed = 50;
    public float zoom_speed = 600;
    private GameObject UI_Delete;

    // Update is called once per frame
    void Update()
    {
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 towards = gameObject.transform.forward;
        Vector3 right = gameObject.transform.right;
        //Vector3 dir = towards * h + right * h;
        gameObject.transform.Translate(new Vector3(0, mouse * zoom_speed, 0) * move_speed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(towards * move_speed * Time.deltaTime, Space.World);
            UI_Delete = GameObject.FindGameObjectWithTag("Select_UI");
            Destroy(UI_Delete);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(-towards * move_speed * Time.deltaTime, Space.World);
            UI_Delete = GameObject.FindGameObjectWithTag("Select_UI");
            Destroy(UI_Delete);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-right * move_speed * Time.deltaTime, Space.World);
            UI_Delete = GameObject.FindGameObjectWithTag("Select_UI");
            Destroy(UI_Delete);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(right * move_speed * Time.deltaTime, Space.World);
            UI_Delete = GameObject.FindGameObjectWithTag("Select_UI");
            Destroy(UI_Delete);
        }
        if(Input.GetKey(KeyCode.E))
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0), 4.0f * Time.deltaTime);
        }
       
        
        

        
    }
}
