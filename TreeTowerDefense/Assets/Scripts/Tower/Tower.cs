using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]

    [SerializeField] private float range = 15f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCountdown = 0f;

    [Header("UnitySetup")]

    private string enemyTag = "Enemy";
    [SerializeField] private GameObject projectile;

    GameObject nearestEnemy = null;

    //shooting

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        //if theres no target, dont do anything
        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; //if no enemy, distance is infinite
        

        //get closest enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distanceToEnemy < shortestDistance )
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy= enemy;
            }
        }

        if (nearestEnemy != null&& fireCountdown <= 0)
        {
            target = nearestEnemy.transform;
            Shoot();

        }
        else
        {
            target = null;
        }
    }

    //rangeindicator
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        target = nearestEnemy.transform;

        GameObject bulletGO = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        ProjectileMovement bullet = bulletGO.GetComponent<ProjectileMovement>();


        if (bullet != null )
        {

            bullet.Seek(target);
        }
        
        //Destroy(nearestEnemy);
        

       
    }
}
