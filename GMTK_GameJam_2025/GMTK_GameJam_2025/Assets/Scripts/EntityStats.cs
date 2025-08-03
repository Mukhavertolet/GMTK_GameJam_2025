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
    public List<GameObject> bulletPattern = new List<GameObject>();
    public GameObject bulletPatternSpecial;
    public GameObject bulletPatternUltimate;



    //GAMEPLAY PARAMS

    public int maxHP = 5;
    public int currentHP = 5;

    public int damage = 1;
    public float attackSpd = 1f;

    public float bulletSpd = 3f;
    public float bulletSize = 1f;

    public int pierces = 0;

    public float moveSpd = 5f;

    public float InvisibilityTime = 0.1f;
    public bool isVulnerable = true;

    public int pointCost = 5;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {


        GameObject firedBulletPattern = Instantiate(bulletPattern[Random.Range(0, bulletPattern.Count)], shootPos.transform.position, Quaternion.identity);


        Vector2 dir = shootPos.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firedBulletPattern.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        firedBulletPattern.GetComponent<BulletPattern>().SetBulletParams(bulletSize, damage, bulletSpd, entityName, pierces);
    }

    public void SpecialAttack()
    {
        GameObject firedBulletPattern = Instantiate(bulletPatternSpecial, shootPos.transform.position, Quaternion.identity);


        Vector2 dir = shootPos.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firedBulletPattern.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        firedBulletPattern.GetComponent<BulletPattern>().SetBulletParams(bulletSize, damage, bulletSpd, entityName, pierces);
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


    public void SetStatsDefault(int maxHP_, int currentHP_, int damage_, float attackSpd_, float moveSpd_, float bulletSpd_, float bulletSize_, int pierces_)
    {
        maxHP = maxHP_;
        currentHP = currentHP_;
        damage = damage_;
        attackSpd = attackSpd_;
        moveSpd = moveSpd_;
        bulletSpd = bulletSpd_;
        bulletSize = bulletSize_;
        pierces = pierces_;

    }
}
