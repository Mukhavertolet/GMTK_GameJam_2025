using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public string entityName = "entity";
    public EntityStats entity;

    // Start is called before the first frame update
    void Start()
    {
        entity = gameObject.GetComponentInParent<EntityStats>();
        entityName = entity.entityName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null && entity.isVulnerable && collision.GetComponent<Bullet>().shooterName != entityName)
        {
            StartCoroutine(entity.TakeDamage((int)collision.GetComponent<Bullet>().damage));
        }
    }


}
