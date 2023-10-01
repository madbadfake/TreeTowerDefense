using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Button[] upgradeButtons;
    private int buttonIndex = 0;

    private GameObject wolpertinger;
    private GameObject tower; 
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
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
        if (tower != null)
        {
            tower.GetComponent<Tower>().UpgradeSelf(index);
            Debug.Log("upgrading");
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
