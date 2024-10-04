using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject win;

    private GameController gc;
    private Player player;

    public AudioSource source;
    public AudioClip point;

    void Start()
    {
        DontDestroyOnLoad(win);

        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.transform.position = gc.lastCheckPoint;
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "level select")
        {
            player.transform.position = Vector3.zero;
        }

        if (win.activeInHierarchy == true)
        {
            gc.lastCheckPoint = Vector3.zero;
        }

        //Debug.Log(gc.lastCheckPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Checkpoint");
            source.PlayOneShot(point);
            //pointLight.GetComponent<Light>().enabled = true;
            pointLight.GetComponent<Renderer>().material.color = Color.green;

            gc.lastCheckPoint = transform.position;
        }
    } 
}
