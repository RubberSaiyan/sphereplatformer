using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    public float speed;
    public float yaw;
    public float pitch;

    private CinemachineFreeLook freeCam;

    void Start()
    {
        freeCam = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        //yaw += speed * Input.GetAxis("Mouse X");
        //pitch += speed * Input.GetAxis("Mouse Y");
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        freeCam.m_XAxis.Value = Input.GetAxis("Right Stick X");
        freeCam.m_YAxis.Value = Input.GetAxis("Right Stick Y");

    }
}
