using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    Material myMaterial;
    [SerializeField]
    Material snowMaterial;

    public Rigidbody rb;
    public Transform cam;
    public GameObject restart, win;

    private Vector3 move;
    Vector3 surfaceNormal = Vector3.zero;

    public float speedSet = 1500;
    public float speed;
    public float maxSpeed;
    public float speedMultiplier;
    public float gravity;
    private float jumpVelocity;
    public float jumpForce;
    public float airDrag;
    public float enemyBounce;
    public float coinCount;
    public float jumpTime;
    public float delay;
    public float sizeIncrease;
    public float maxSize;
    public float mph;
    public float delayTime;

    public bool jumpPowerup;
    public bool speedPowerup;
    public bool grounded;
    public bool snow;
    public bool canJump;

    public Text coinText;
    public Text gameTime;
    public Text speedText;

    public AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coins, bigcoin;
    public AudioClip speedPower, jumpPower, shrinkPower;
    public AudioClip enemyDeath;
    public AudioClip explosion;

    public ParticleSystem jumpParticle, rockParticle;

    void Start()
    {
        jumpPowerup = false;
        speedPowerup = false;

        coinCount = 0;

        restart.SetActive(false);
        win.SetActive(false);
    }

    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Joystick Axis Y"));

        float minutes = Mathf.FloorToInt(Time.time / 60);
        float seconds = Mathf.FloorToInt(Time.time % 60);

        mph = rb.velocity.magnitude;

        gameTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        speedText.text = mph.ToString("F2");

        float dot = Vector3.Dot(surfaceNormal, Vector3.up);
        jumpVelocity = -gravity * Time.deltaTime;

        delay -= Time.deltaTime;

        if (jumpPowerup == true)
        {
            jumpForce = 40;
        }
        if (jumpPowerup == false)
        {
            jumpForce = 20;
        }
        if (jumpPowerup == true && Input.GetButtonDown("Jump") && canJump == true)
        {
            StartCoroutine(JumpTime(0));
            Instantiate(jumpParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        }

        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (GetComponent<MeshRenderer>().enabled == false)
        {
            restart.SetActive(true);
        }

        if (dot >= 0.5f)
        {
            if (Input.GetButtonDown("Jump") && canJump == true)
            {
                jumpVelocity = jumpForce;
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                source.PlayOneShot(jumpClip);
            }
            else
            {
                jumpVelocity -= gravity * Time.deltaTime;
            }
        }

        if (pause.isPaused)
        {
            canJump = false;
        }

    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        //rb.AddForce(move * speed * Time.deltaTime);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        if (move.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(direction * speed * Time.deltaTime);
        }

        if (snow == true)
        {
            StartCoroutine(ChangeSize(sizeIncrease));
        }
        else if (snow == false)
        {
            StopCoroutine(ChangeSize(sizeIncrease));
            if (transform.localScale.x >= 2.0f)
            {
                transform.localScale /= sizeIncrease;
            }
        }

        if (transform.localScale.x <= 2.0f)
        {
            gameObject.GetComponent<Renderer>().material = myMaterial;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Destructable" && mph > 110 || collision.gameObject.tag == "Destructable" && transform.localScale.x >= 8)
        {
            source.PlayOneShot(explosion);
            Instantiate(rockParticle, transform.position + (Vector3.up * 10.0f), Quaternion.Euler(0, 0, 0));
            Destroy(GameObject.FindWithTag("Destructable"));
        }

        if (collision.gameObject.tag == "Platform")
        {
            grounded = true;
            speed = speedSet;
            canJump = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        surfaceNormal = collision.contacts[0].normal;

        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        if (collision.gameObject.tag == "Platform")
        {
            grounded = true;
            speed = speedSet;
            canJump = true;
        }
        if (collision.gameObject.tag == "Snow")
        {
            grounded = true;
            speed = speedSet;
            snow = true;
            canJump = true;
        }
        if (collision.gameObject.tag == "Slime")
        {
            grounded = true;
            speed = speedSet;
            canJump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        surfaceNormal = Vector3.zero;
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
            speed *= airDrag;
        }
        if (collision.gameObject.tag == "Snow")
        {
            grounded = false;
            speed *= airDrag;
            snow = false;
        }
        if (collision.gameObject.tag == "Slime")
        {
            grounded = false;
            speed *= airDrag;
            canJump = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy Bounce")
        {
            //    Destroy(other.gameObject);

            jumpVelocity = jumpForce;
            rb.AddForce(new Vector3(0, jumpForce * enemyBounce, 0), ForceMode.Impulse);
            source.PlayOneShot(enemyDeath);
        }

        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            coinCount++;
            coinText.text = coinCount.ToString();
            source.PlayOneShot(coins);
        }
        if (other.gameObject.tag == "BigCoin")
        {
            Destroy(other.gameObject);
            coinCount += 10;
            coinText.text = coinCount.ToString();
            source.PlayOneShot(bigcoin);
        }

        if (other.gameObject.tag == "Win")
        {
            win.SetActive(true);

            StartCoroutine(NextLevel(delayTime));
        }

        if (other.gameObject.tag == "Speed")
        {
            source.PlayOneShot(speedPower);
            speedPowerup = true;
        }
        if (other.gameObject.tag == "Jump")
        {
            source.PlayOneShot(jumpPower);
            jumpPowerup = true;
        }
        if (other.gameObject.tag == "Shrink")
        {
            source.PlayOneShot(shrinkPower);
        }
    }

    IEnumerator JumpTime(float jumpTime)
    {
        yield return new WaitForSeconds(jumpTime);
        jumpPowerup = false;
    }

    IEnumerator ChangeSize(float sizeIncrease)
    {
        if (snow == true && mph >= 10.0f)
        {
            transform.localScale *= sizeIncrease;
            gameObject.GetComponent<Renderer>().material = snowMaterial;
        }

        if (transform.localScale.x >= maxSize)
        {
            transform.localScale /= sizeIncrease;
        }

        yield return null;
    }

    IEnumerator NextLevel(float startDelay)
    {
        yield return new WaitForSeconds(delayTime);

        Debug.Log("Start");
        SceneManager.LoadScene("level select");
    }
}

