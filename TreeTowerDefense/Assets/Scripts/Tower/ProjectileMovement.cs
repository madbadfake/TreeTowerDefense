using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 40;
    [SerializeField] private float damage = 1f;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroy if no target
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime); //move

        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //move


    }

    //seekTarget
    public void Seek(Transform _target)
    {
        target = _target.gameObject;
    }

    void HitTarget()
    {
        Debug.Log("WeHitSomething!!");
        Destroy(gameObject);
        EnemyMovement targetScript = target.GetComponent<EnemyMovement>();
        targetScript.TakeDamage(damage);
        //Destroy(target.gameObject);
    }


}
