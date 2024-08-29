using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_animator.SetTrigger("Turn Left");
            Debug.Log("l");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_animator.SetTrigger("Turn Right");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetBool("Run Forward", true);
            Debug.Log("run");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_animator.SetBool("Run Forward", false);
        }
    }
}
