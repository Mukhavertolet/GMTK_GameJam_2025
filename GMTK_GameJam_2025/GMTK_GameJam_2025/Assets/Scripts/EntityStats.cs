using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public string entityName = "entity";

    //OBJECT REFERENCES

    [SerializeField]
    private GameObject shootPos;


    //PREFABS

    [SerializeField]
    private GameObject bulletPattern;



    //GAMEPLAY PARAMS

    public int maxHP = 5;
    public int currentHP = 5;

    public int damage = 1;
    public float attackSpd = 1f;

    public float moveSpd = 5f;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Debug.Log($"{entityName} attack!");

        GameObject firedBulletPattern = Instantiate(bulletPattern, shootPos.transform.position, Quaternion.identity);

        Vector2 dir = shootPos.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firedBulletPattern.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }


}
