using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected bool isRemove;

    protected new string name;

    public virtual void openPanel(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
    }

    public virtual void closePanel(string name)
    {
        isRemove = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}