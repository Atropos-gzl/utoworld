using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject threeSelectOne;
    
    private void Awake()
    {
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject MyBag = GameObject.Find("TowerBag");
        Transform[] children = MyBag.transform.GetComponentsInChildren<Transform>();
        GameObject towerBag = new GameObject();
        GameObject gamerBag = new GameObject();
        foreach (Transform child in children) { 
            if(child.name == "tower")
            {
                towerBag = child.gameObject;
            }else if (child.name == "gamerbag")
            {
                gamerBag = child.gameObject;
            }
            
        }
        children = towerBag.transform.GetComponentsInChildren<Transform>();
        int i = 0;
        foreach (Transform child in children) { 
            if(child.name == "towerItem")
            { 
                child.GetComponent<ItemOnDrag>().id = i;
                i++;
            }
                
        }
        children = gamerBag.transform.GetComponentsInChildren<Transform>();
         i = 0;
        foreach (Transform child in children) { 
            if(child.name == "gamerItem")
            { 
                child.GetComponent<ItemOnDrag>().id = i;
                i++;
            }
                
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Disappear();
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Time.timeScale = 0;
        //    GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Pause();
        //    GameObject.Find("End_UI").GetComponent<CanvasGroup>().alpha = 1;
        //    GameObject.Find("End_UI").GetComponent<CanvasGroup>().blocksRaycasts = true;
        //    GameObject.Find("End_UI").GetComponent<CanvasGroup>().interactable = true;
        //}
    }

    public void Disappear()
    {
        GameObject select_ui = GameObject.FindGameObjectWithTag("Select_UI");
        GameObject upgrade_ui = GameObject.FindGameObjectWithTag("Upgrade_UI");
        Destroy(upgrade_ui);
        Destroy(select_ui);
    }

    public void Continue()
    {
        GameObject.Find("End_UI").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("End_UI").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("End_UI").GetComponent<CanvasGroup>().interactable = false;
        Time.timeScale = 1;
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Play();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnpointerEnter()
    {
       GameObject.Find("End_UI").GetComponentInChildren<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnpointerExit()
    {
        GameObject.Find("End_UI").GetComponentInChildren<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void Restart()
    {
        CanvasGroup ui = GameObject.Find("Account_UI").GetComponent<CanvasGroup>();
        ui.alpha = 0;
        ui.blocksRaycasts = false;
        ui.interactable = false;
        GameObject.Find("GameManager").SendMessage("GameEnd");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToStart()
    {
        Debug.Log("enter start");
        GameObject obj = GameObject.Find("Go_UI");
        if (obj == null) {
            Debug.Log("cant find");
        }
        GameObject.Find("Start_UI").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Start_UI").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Start_UI").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("Go_UI").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Go_UI").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("Go_UI").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
        Debug.Log("test: "+GameObject.Find("Go_UI").GetComponent<CanvasGroup>().alpha);

    }

    public void Go()
    {
        GameObject.Find("GameManager").GetComponent<GameBase>().isRun = true;
        GameObject.Find("Wave").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Wave").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("Wave").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("Go_UI").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Go_UI").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Go_UI").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("GameManager").SendMessage("GameStart");
    }
    
    









}
