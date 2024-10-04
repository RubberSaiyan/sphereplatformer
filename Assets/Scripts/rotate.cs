using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotate : MonoBehaviour
{
    public float rotateSpeed;
    public float delayTime;

    public AudioSource source;
    public AudioClip clip;

    void Start()
    {
        
    }
    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            StartCoroutine(Begin(delayTime));
            source.PlayOneShot(clip);
        }

        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    IEnumerator Begin(float startDelay)
    {
        yield return new WaitForSeconds(delayTime);

        Debug.Log("Start");
        SceneManager.LoadScene("level select");
    }
}
