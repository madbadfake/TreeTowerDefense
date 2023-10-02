using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject target;

    [Header("Attributes")]

    [SerializeField] private float range = 5f;
    //[SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCD = 3f;
    [SerializeField] private float fireTimer = 0f;
    [SerializeField] private float damage = 1f;

    [Header("UnitySetup")]

    private string enemyTag = "Enemy";
    [SerializeField] private GameObject projectile;

    GameObject nearestEnemy = null;

    GameObject UpgradeUI;

    //upgrade

    public int[] upgradeCost = { 4, 20 };
    public int[] upggradeCostIncrease = { 4, 20 };

    //shooting

    // Start is called before the first frame update
    void Start()
    {
        UpgradeUI = GameObject.Find("UpgradeCanvas");
        fireTimer= 3f;
        InvokeRepeating("UpdateTarget", 0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime; 
        //if theres no target, dont do anything
        if (target == null)
            return;

        if (fireTimer <= 0f)
        {
            Shoot();
            
        }

        
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; //if no enemy, distance is infinite
        

        //get closest enemy
        foreach (GameObject enemy in enemies)
        {
            
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= range)
            {
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                    //Debug.Log("FOund enemy");
                }
            }
          
        }

        if (nearestEnemy != null&& fireTimer <= 0)
        {
            target = nearestEnemy.gameObject;


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
        Debug.Log("Shooting");

        GameObject bulletGO = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        ProjectileMovement bullet = bulletGO.GetComponent<ProjectileMovement>();
        bullet.damage = damage;


        if (bullet != null )
        {

            bullet.Seek(target.transform);
        }

        //Destroy(nearestEnemy);

        nearestEnemy = null;
        fireTimer= fireCD;
    }

    public void UpgradeSelf(int upgradePath)
    {
        if(upgradePath == 0) //dmg
        {
            damage += 2;
            gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            upgradeCost[upgradePath] += upggradeCostIncrease[upgradePath];

        }
        else
        {
            fireCD -= 0.5f;
            gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            upgradeCost[upgradePath] += upggradeCostIncrease[upgradePath];
        }
    }
}
