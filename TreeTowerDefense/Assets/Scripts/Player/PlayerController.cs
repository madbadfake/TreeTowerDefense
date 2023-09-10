using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerSpeed;
    [SerializeField] float rotationSensitivity = 1.0f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float flyForce = 5f;

    private CinemachineVirtualCamera virtualCamera;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isFlying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void FixedUpdate()
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000 * rotationSensitivity);
        }

        rb.MovePosition(transform.position + movement.normalized * playerSpeed * Time.deltaTime);

        if (!isGrounded)
        {
            // Deaktiviere die Gravitation
            rb.useGravity = false;

            // Wenn Leertaste gedrückt wird, füge Auftriebskraft hinzu
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * flyForce, ForceMode.Acceleration);
            }
            // Wenn Shift gedrückt wird, füge nach unten gerichtete Kraft hinzu
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.down * flyForce, ForceMode.Acceleration);
            }
        }
        else
        {
            // Der Spieler berührt den Boden, aktiviere die Gravitation wieder
            rb.useGravity = true;
        }
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        // Überprüfe, ob der Spieler auf einem Layer namens "Ground" steht
        isGrounded = collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        isGrounded = false;
    }
}


 

