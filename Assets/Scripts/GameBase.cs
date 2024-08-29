using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameBase : MonoBehaviour
{
    public int life = 20;
    public int Money = 500;
    public float rate = 5;
    public float timer = 0;
    public Text Moneytxt;
    public Text Bloodtxt;
    public Animator anim_Blood;
    public bool isRun = false;
    public delegate void onMoneyIncrease();
    public event onMoneyIncrease onMoneyIncreaseEvent;
    private static GameBase instance;

    public void MoneyIncreaseCompleted()
    {
        onMoneyIncreaseEvent?.Invoke();
    }

    private void Awake()
    {
        instance = this;
        GameObject[] routepoints = GameObject.FindGameObjectsWithTag("RoutePoint");
        foreach (GameObject routepoint in routepoints)
        {
            if (routepoint != null)
            {
                Ray ray = new Ray(routepoint.transform.position, new Vector3(0, -1, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~((1 << 2) + (1 << 9)) ) )
                {
                    routepoint.transform.position = hit.point;
                }

            }
        }
    }

    private void Start()
    {
        Moneytxt = GameObject.Find("Money").GetComponent<Text>();
        Bloodtxt = GameObject.Find("HP_Text").GetComponent<Text>();
        anim_Blood = GameObject.Find("HP_Image").GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRun)
        {
            timer += Time.deltaTime;
            if (timer > rate)
            {
                timer -= rate;
                Money += 5;
                Moneytxt.text = "$" + Money.ToString();
            }
            Bloodtxt.text = life.ToString();
        }
        

        if (life <= 0)
        {
            Time.timeScale = 0;
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Pause();
            CanvasGroup ui = GameObject.Find("Account_UI").GetComponent<CanvasGroup>();
            GameObject.Find("Account").GetComponent<Text>().color = Color.gray;
            GameObject.Find("Account").GetComponent<Text>().text = "Fail";
            ui.alpha = 1;
            ui.blocksRaycasts = true;
            ui.interactable = true;
        }
    }


    public bool Buy(int cost)
    {
        return Money >= cost; 
    }

    public void ChangeMoney(int Delta)
    {
        Money += Delta;
        if (Delta > 0)
        {
            MoneyIncreaseCompleted();
        }
        Moneytxt = GameObject.Find("Money").GetComponent<Text>();
        Moneytxt.text = "$" + Money.ToString();
    }

    public void ChangeHP(int Delta)
    {
        life += Delta;
        if (Delta < 0)
        {
            anim_Blood.SetTrigger("beat");
        }
    }

    public static GameBase getInstance()
    {
        if (instance == null)
        {
            instance = new GameBase();
            Debug.Log("error 存在两个GameBase");
        }
        return instance;
    }
}
