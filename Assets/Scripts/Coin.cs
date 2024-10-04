using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float ping, pong;
    public float rotateSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, pong) + ping, transform.position.z);
    }
}
