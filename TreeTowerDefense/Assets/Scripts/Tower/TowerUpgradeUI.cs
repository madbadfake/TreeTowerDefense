using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Button[] upgradeButtons;
    private int buttonIndex = 0;

    private GameObject wolpertinger;
    private GameObject tower;

    [SerializeField] private TextMeshProUGUI buttonText1;
    [SerializeField] private TextMeshProUGUI buttonText2;



    private float playerXP;
    // Start is called before the first frame update
    void Start()
    {
        wolpertinger = GameObject.Find("Wolpertinger");

        SelectButton(buttonIndex);


    }

    // Update is called once per frame
    void Update()
    {
        tower = wolpertinger.GetComponent<PlayerMovement>().selectedTower;
        playerXP = wolpertinger.GetComponent<PlayerMovement>().currentXp;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //get upgrade costs
       
            buttonText1.text = "Cost:" + tower.GetComponent<Tower>().upgradeCost[0];
            buttonText2.text = "Cost:" + tower.GetComponent<Tower>().upgradeCost[1];

    
        if (scroll != 0)
        {
            buttonIndex += (int)Mathf.Sign(scroll);
            buttonIndex = Mathf.Clamp(buttonIndex, 0, upgradeButtons.Length - 1);
            SelectButton(buttonIndex);
        }

        if (Input.GetMouseButtonDown(0))
        {
            UpgradeTower(buttonIndex);
        }
    }

    public void UpgradeTower(int index)
    {
        int[] upgradeCost = tower.GetComponent<Tower>().upgradeCost;

        if (upgradeCost[index] <= playerXP && tower != null)
        {
            wolpertinger.GetComponent<PlayerMovement>().currentXp -= upgradeCost[index];
            tower.GetComponent<Tower>().UpgradeSelf(index);
            Debug.Log("upgrading");

        }
        else if (upgradeCost[index]>= playerXP && tower!= null)
        {
            Debug.Log("Not enough money");
        }
        else
        {
            return;
        }
        
    }

    public void SelectButton(int index)
    {
        foreach (Button button in upgradeButtons)
        {
            button.interactable = false;
        }

        upgradeButtons[index].interactable = true;
    }

    

}
