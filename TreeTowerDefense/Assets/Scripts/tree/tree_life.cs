using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tree_life : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    // ---------------- UI ----------------

    [SerializeField] private Image hpBarFill;


    // Start is called before the first frame update
    void Start()
    {
        maxHP = 20;
        currentHP = maxHP;

        hpBarFill.fillAmount = currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHPBar()
    {
        float fillAmount = (float)currentHP / maxHP;
        hpBarFill.fillAmount = fillAmount;
        Debug.Log("HP Bar updated. fillAmount: " + fillAmount);
    }
}
