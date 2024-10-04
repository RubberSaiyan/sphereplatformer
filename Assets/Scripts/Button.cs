using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isPressed;

    public float maxSize;

    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (isPressed == false)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (isPressed == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.transform.localScale.x >= maxSize)
        {
            isPressed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPressed = false;
        }
    }
}
