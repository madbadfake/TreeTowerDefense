using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerSpeed;

    private Rigidbody rb;
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Vertical");
        float moveY = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveY, 0.0f, moveX) * playerSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }


}
