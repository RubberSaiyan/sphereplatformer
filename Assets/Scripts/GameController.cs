using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public Vector3 lastCheckPoint;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
