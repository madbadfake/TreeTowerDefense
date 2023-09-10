using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerSpeed;

    private CinemachineVirtualCamera virtualCamera;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 cameraForward = virtualCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = virtualCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 movement = cameraForward * moveZ + cameraRight * moveX;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000);
        }

        rb.MovePosition(transform.position + movement.normalized * playerSpeed * Time.deltaTime);
    }
}
