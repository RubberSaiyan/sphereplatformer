using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier;
    public float duration;
    public float respawn;

    public GameObject particle;

    void Start()
    {
        
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Speed")
        {
            StartCoroutine(SpeedPowerUp(other));
            Debug.Log("Speed");
        }
        if (other.gameObject.tag == "Player" && gameObject.tag == "Jump")
        {
            StartCoroutine(JumpPowerUp(other));
            Debug.Log("Jump");
        }
        if (other.gameObject.tag == "Player" && gameObject.tag == "Shrink")
        {
            StartCoroutine(ShrinkPowerUp(other));
            Debug.Log("Shrink");
        }

    }

    IEnumerator SpeedPowerUp(Collider player)
    {
        Player playerStats = player.GetComponent<Player>();

        playerStats.speedSet *= playerStats.speedMultiplier;
        playerStats.maxSpeed *= playerStats.speedMultiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        particle.SetActive(true);

        yield return new WaitForSeconds(duration);

        playerStats.speedSet /= playerStats.speedMultiplier;
        playerStats.maxSpeed /= playerStats.speedMultiplier;

        yield return new WaitForSeconds(respawn);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        particle.SetActive(false);
    }

    IEnumerator JumpPowerUp(Collider player)
    {
        Player playerStats = player.GetComponent<Player>();

        if (playerStats.jumpPowerup == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(respawn);

            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

    IEnumerator ShrinkPowerUp(Collider player)
    {
        Player playerStats = player.GetComponent<Player>();

        player.transform.localScale /= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.transform.localScale *= multiplier;

        yield return new WaitForSeconds(respawn);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
