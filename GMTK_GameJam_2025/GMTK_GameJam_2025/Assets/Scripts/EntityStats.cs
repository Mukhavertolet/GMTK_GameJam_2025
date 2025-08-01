using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public string entityName = "entity";

    //OBJECT REFERENCES

    [SerializeField]
    private GameManager gameManager;


    [SerializeField]
    private GameObject shootPos;

    public Room room;



    //PREFABS

    [SerializeField]
    private GameObject bulletPattern;



    //GAMEPLAY PARAMS

    public int maxHP = 5;
    public int currentHP = 5;

    public int damage = 1;
    public float attackSpd = 1f;

    public float bulletSpd = 3f;
    public float bulletSize = 1f;

    public float moveSpd = 5f;

    public float InvisibilityTime = 0.1f;
    public bool isVulnerable = true;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {

        Bullet firedBulletPattern = Instantiate(bulletPattern, shootPos.transform.position, Quaternion.identity).GetComponent<Bullet>();


        Vector2 dir = shootPos.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firedBulletPattern.gameObject.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        firedBulletPattern.gameObject.transform.localScale *= bulletSize;

        firedBulletPattern.shooterName = entityName;
        firedBulletPattern.moveSpd = bulletSpd;
        firedBulletPattern.damage = damage;
    }

    public IEnumerator TakeDamage(int dmg)
    {

        isVulnerable = false;
        currentHP -= dmg;

        if (currentHP <= 0)
            StartCoroutine(Die());

        yield return new WaitForSeconds(InvisibilityTime);
        isVulnerable = true;
    }

    public IEnumerator Die()
    {
        gameManager.RemoveEntity(this);

        Destroy(gameObject);
        yield return null;
    }


    public void SetStatsDefault(int maxHP_, int currentHP_, int damage_, float attackSpd_, float moveSpd_, float bulletSpd_, float bulletSize_)
    {
        maxHP = maxHP_;
        currentHP = currentHP_;
        damage = damage_;
        attackSpd = attackSpd_;
        moveSpd = moveSpd_;
        bulletSpd = bulletSpd_;
        bulletSize = bulletSize_;

        currentHP = maxHP;
    }
}
