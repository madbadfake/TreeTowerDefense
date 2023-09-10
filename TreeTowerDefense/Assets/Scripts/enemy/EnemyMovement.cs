using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //enemy
    public float speed = 10f;
    public int damage = 1;
    public int health = 1;

    //waypoints
    private Transform target;
    private int waypointIndex = 0;

    public GameObject tree;


    private void Start()
    {
        target = waypoints.points[0]; //spawnpoint
        tree = GameObject.Find("Base");

    }

    private void Update()
    {
        //movement
        Vector3 direction = target.position - transform.position; //get position
        transform.Translate(direction.normalized * speed * Time.deltaTime); //move

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) //check if end
        {
            GetNextWaypoint();
        }
    }

    //functions

    void GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            dealDamage(damage);
            return;
        }
        waypointIndex++;
        target = waypoints.points[waypointIndex];
    }

    void dealDamage(int damage)
    {
        tree.GetComponent<tree_life>().currentHP -= damage;
    }
}