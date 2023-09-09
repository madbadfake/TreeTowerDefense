using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_life : MonoBehaviour
{
    public static Vector3 targetPos;
    public int maxHP;
    public int currentHP;


    // Start is called before the first frame update
    void Start()
    {
        targetPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
