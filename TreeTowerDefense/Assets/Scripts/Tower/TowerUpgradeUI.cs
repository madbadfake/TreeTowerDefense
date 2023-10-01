using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Button[] upgradeButtons;
    private int buttonIndex = 0;

    public GameObject tower;
    // Start is called before the first frame update
    void Start()
    {

        SelectButton(buttonIndex);

    }

    // Update is called once per frame
    void Update()
    {
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
        if(index == 0) Debug.Log("upgrading dmg");
        else if(index == 1) Debug.Log("upgrading firerate");
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
