using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI xpCounter;

    private GameObject player;
    private PlayerMovement playerScript;
    private float currentXp;
    
    void Start()
    {
        player = GameObject.Find("Wolpertinger");
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        currentXp = playerScript.GetCurrentXp();
        xpCounter.text= currentXp.ToString();
    }

    
}
