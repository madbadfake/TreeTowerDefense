using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_script : MonoBehaviour
{

    private NavMeshAgent enemyAgent;
    public GameObject objective;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent= GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAgent.destination = tree_life.targetPos;
    }
}
