using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // ---------------- MOVEMENT ----------------
    public float movespeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    // ---------------- KEY BINDS ----------------
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode buildKey= KeyCode.E;


    // ---------------- GROUND CHECK ----------------
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    Vector3 moveDirection;

    Rigidbody rb;

    // ---------------- Tower ----------------

    [SerializeField] GameObject towerPrefab;
    //building
    [SerializeField] GameObject towerIndicator;
    private bool isPlacingTower = false;
    private bool canPlaceTower = false;

    private string buildingZoneTag = "buildingZone";

    //upgrading
    private bool isUpgrading = false;
    private bool canUpgrade = false;

    GameObject upgradeUI;

    // ---------------- Camera ----------------

    private CinemachineVirtualCamera virtualCamera;
    private Camera mainCamera;

    // ---------------- Collectibles ----------------

    public float currentXp = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        // find camera

        //virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mainCamera = Camera.main;

        //setup toowerIndicator
        towerIndicator = Instantiate(towerIndicator);
        towerIndicator.SetActive(false);

        //upgradeUI 
        upgradeUI = GameObject.Find("UpgradeCanvas");
        upgradeUI.SetActive(false);



    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump

        if(Input.GetKey(jumpKey) && readyToJump && grounded) 
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

           
        }

        // build
        
        if (Input.GetKeyDown(buildKey)) //if pressing button
        {

            if (!isPlacingTower) // if building view not active, open building view
            {
                isPlacingTower = true; // open 
                towerIndicator.SetActive(true);
            }
            else // stop building view
            {
                
                isPlacingTower = false;
                towerIndicator.SetActive(false);

            }

        }

        if (isPlacingTower) //if building view active
        {
            //create raycast
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) //if hit
            {
                Vector3 towerPosition = hit.point;
                Terrain terrain = hit.transform.GetComponent<Terrain>();

                if (hit.transform.CompareTag("whatIsGround") | hit.transform.CompareTag("buildingZone")) //if hit ground
                {
                    towerIndicator.transform.position = towerPosition;

                    if (hit.transform.CompareTag("buildingZone"))
                    {
                        canPlaceTower = true;
                    }
                    else
                    {
                        canPlaceTower = false;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1)) //close when right mouse button
            {
                isPlacingTower = false;
                towerIndicator.SetActive(false);

            }

            if (canPlaceTower && Input.GetKeyDown(KeyCode.Mouse0)) //build if mouse left

            {
                placeTower(towerIndicator.transform);

            }
        }

        //Upgrade

        if (Input.GetKeyDown(KeyCode.Q))
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!isUpgrading)
            {
                if (Physics.Raycast(ray, out hit)) //if hit
                {
                    Vector3 towerPosition = hit.point;
                    //Terrain terrain = hit.transform.GetComponent<Terrain>();
                    float distanceToTower = towerPosition.x - gameObject.transform.position.x;

                    if (hit.transform.CompareTag("tower")) //if hit tower
                    {
                        if (distanceToTower <= 20 && distanceToTower >= -20)
                        {
                            upgradeUI.SetActive(true);
                            isUpgrading= true;

                        }

                    }

                }
            }
            else
            {
                upgradeUI.SetActive(false);
                isUpgrading= false;
            }
        }      
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);
        }

        // in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void InstantiateTower(Vector3 position)
    {
        Instantiate(towerPrefab, position, Quaternion.identity);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > movespeed)
        {
            Vector3 limitedVel = flatVel.normalized * movespeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void placeTower(Transform pos)
    {
        Instantiate(towerPrefab, pos.position, Quaternion.identity);

        isPlacingTower = false;
        towerIndicator.SetActive(false);
    }

    private void CollectXp()
    {
        currentXp += 1f;
        Debug.Log("Current Xp:" + currentXp);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            CollectXp();
            Destroy(other.gameObject);
        }
    }
    public float GetCurrentXp()
    {
        return currentXp;
    }

}
