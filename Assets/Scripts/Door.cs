using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;

    public bool doorOpen;

    public Button button;

    public float ping, pong;

    void Start()
    {
        
    }

    void Update()
    {
        anim.SetBool("isPressed", doorOpen);

        if(button.isPressed == true)
        {
            doorOpen = true;
            Debug.Log("Open");
        }
        else
        {
            doorOpen = false;
        }

        //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, pong) + ping, transform.position.z);
    }
}
