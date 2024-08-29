using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDevice_TPDoorSelf : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject teleportTarget; // 传送目标 GameObject
    public GameObject ParticleSystem;
    Coroutine WaitTriggerCoroutine;
    void Start()
    {
        ParticleSystem.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player")) // 碰撞对象是玩家
        {
            
             WaitTriggerCoroutine = StartCoroutine(WaitTrigger(other, 1f));
            // 将玩家传送到指定的 GameObject 上
            
        }
    }

    IEnumerator WaitTrigger(Collider other, float Seconds)
    {
        ParticleSystem.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(Seconds);
        if(other != null){
            other.transform.position = teleportTarget.transform.position;
            
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Player")) // 碰撞对象是玩家
        {
            StopCoroutine( WaitTriggerCoroutine);
            ParticleSystem.GetComponent<ParticleSystem>().Stop();
        }
    }

}
