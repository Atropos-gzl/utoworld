using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    private Toggle m_toggle;
    private RectTransform bg;

    // Start is called before the first frame update
    void Start()
    {
        m_toggle = gameObject.GetComponent<Toggle>();
        bg = m_toggle.GetComponentInChildren<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter()
    {
        bg.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void OnPointerExit()
    {
        bg.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void OnCrossBowSelected()
    {
        GameObject.Find("UIManager").SendMessage("Disappear");
        GameObject.Find("GameManager").SendMessage("OnCrossBowSelected");
    }

    public void OnFattyPlasmaSelected()
    {
        GameObject.Find("UIManager").SendMessage("Disappear");
        GameObject.Find("GameManager").SendMessage("OnFattyPlasmaSelected");
    }

    public void OnCulverinSelected()
    {
        GameObject.Find("UIManager").SendMessage("Disappear");
        GameObject.Find("GameManager").SendMessage("OnCulverinSelected");
    }

    public void OnUpgradeSelected()
    {
        GameObject.Find("UIManager").SendMessage("Disappear");
        GameObject.Find("GameManager").SendMessage("OnUpgradeSelected");
    }

    public void OnRemoveSelected()
    {
        GameObject.Find("UIManager").SendMessage("Disappear");
        GameObject.Find("GameManager").SendMessage("RemoveTurret");
    }
}
