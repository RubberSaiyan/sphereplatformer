using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    GameObject selector, loading;

    [SerializeField]
    GameObject[] row1;

    [SerializeField]
    GameObject[] row2;

    [SerializeField]
    GameObject[] row3;

    const int columns = 3;
    const int rows = 3;

    Vector2 positionIndex;
    GameObject currentSlot;

    public GameController gc;

    bool isMoving = false;

    public GameObject[,] grid = new GameObject[columns, rows];
    public Player player;

    public AudioSource source;
    public AudioClip select;

    void Start()
    {
        AddRow(0, row1);
        AddRow(1, row2);
        AddRow(2, row3);

        positionIndex = new Vector2(0, 0);
        currentSlot = grid[0, 0];

        loading.SetActive(false);
    }

    void Update()
    {
        gc.lastCheckPoint = Vector3.zero;

        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return))
        {
            loading.SetActive(true);
            string levelID = currentSlot.GetComponent<LevelID>().levelID;
            SceneManager.LoadScene(levelID);
        }

        if (xAxis > 0)
        {
            MoveSelector("right");
        }
        else if (xAxis < 0)
        {
            MoveSelector("left");
        }
        else if (yAxis > 0)
        {
            MoveSelector("up");
        }

        else if (yAxis < 0)
        {
            MoveSelector("down");
        }
    }

    void AddRow(int index, GameObject[] row)
    {
        for (int i = 0; i < 3; i++)
        {
            grid[index, i] = row[i];
        }
    }

    void MoveSelector(string direction)
    {
        if (isMoving == false)
        {
            isMoving = true;

            if (direction == "right")
            {
                if (positionIndex.x < columns - 1)
                {
                    positionIndex.x += 1;

                    source.PlayOneShot(select);
                }
            }

            else if (direction == "left")
            {
                if (positionIndex.x > 0)
                {
                    positionIndex.x -= 1;

                    source.PlayOneShot(select);
                }
            }

            else if (direction == "up")
            {
                if (positionIndex.y > 0)
                {
                    positionIndex.y -= 1;

                    source.PlayOneShot(select);
                }
            }

            else if (direction == "down")
            {
                if (positionIndex.y < rows - 1)
                {
                    positionIndex.y += 1;

                    source.PlayOneShot(select);
                }
            }

            currentSlot = grid[(int)positionIndex.y, (int)positionIndex.x];
            selector.transform.position = currentSlot.transform.position;

            Invoke("ResetMovement", 0.2f);
        }
    }

    void ResetMovement()
    {
        isMoving = false;     
    }
}
