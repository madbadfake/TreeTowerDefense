using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //particle
    [SerializeField] ParticleSystem impactLeaves;

    //enemy
    public float speed = 10f;
    public float damage = 1;
    public float health = 1;

    //waypoints
    private Transform target;
    private int waypointIndex = 0;

    public GameObject tree;

    //Expierence

    [SerializeField] GameObject xp;
    [SerializeField] private float xpValue = 5f;


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

    void dealDamage(float damage)
    {
        GameObject particleArea = GameObject.Find("ParticleArea");
        if (particleArea != null)
        {
            Vector3 particleAreaPosition = particleArea.transform.position; // Position des "ParticleArea"-Objekts holen
            ParticleSystem newImpactLeaves = Instantiate(impactLeaves, particleAreaPosition, particleArea.transform.rotation); // Partikelsystem am "ParticleArea" spawnen
            newImpactLeaves.Play(); // Partikelsystem starten
        }
        tree.GetComponent<tree_life>().currentHP -= damage;
        tree.GetComponent<tree_life>().UpdateHPBar();
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        if(health <= 0)
        {

            Destroy(gameObject);
            SpawnXP();
        }



    }

    private void SpawnXP()
    {
        for(float i = 5; i >= 0; i--)
        {
            Instantiate(xp, transform.position, Quaternion.identity);
        }
    }
}