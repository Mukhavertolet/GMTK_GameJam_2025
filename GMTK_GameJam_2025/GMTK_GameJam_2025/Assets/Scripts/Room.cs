using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomNumber = 0;

    [SerializeField]
    private List<GameObject> enemyPrefab;



    public int enemyQuantity = 1;

    public int enemyPointsMax = 15;
    public int enemyPointsCurrent = 15;

    public int lowestEnemyCost = 1000;

    //items placed in the room
    public List<Item> items = new List<Item>();


    //enemies alive in the room
    public List<EntityStats> enemies = new List<EntityStats>();





    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRoom()
    {
        enemyPointsCurrent = enemyPointsMax;

        foreach(GameObject enemy in enemyPrefab)
        {
            EntityStats e = enemy.GetComponent<EntityStats>();
            if(e.pointCost < lowestEnemyCost) lowestEnemyCost = e.pointCost;
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject enemyToSpawn in SelectEnemies())
        {
            yield return new WaitForSeconds(0.1f);
            enemies.Add(Instantiate(enemyToSpawn, new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)), Quaternion.identity).GetComponent<EntityStats>());
            enemyToSpawn.GetComponent<EntityStats>().room = this;
            enemyToSpawn.GetComponent<EntityStats>().currentHP = enemyToSpawn.GetComponent<EntityStats>().maxHP;
        }
    }

    private List<GameObject> SelectEnemies()
    {
        List<GameObject> enemiesSelected = new List<GameObject>();

        while (enemyPointsCurrent > 0)
        {
            if (enemyPointsCurrent <= lowestEnemyCost)
                break;
            int rng = Random.Range(0, enemyPrefab.Count());
            if (enemyPointsCurrent <= lowestEnemyCost * 2 && Random.Range(0, 5) <= 3)
                rng = 0;
            Debug.Log(rng);
            GameObject enemyToAdd = enemyPrefab[rng];
            enemiesSelected.Add(enemyToAdd);
            enemyPointsCurrent -= enemyToAdd.GetComponent<EntityStats>().pointCost;
        }

        if (enemyPointsCurrent > 0)
        {
            GameManager.gameManager.cumulativePointRemaining += enemyPointsCurrent;
        }


        Debug.Log(enemyPointsCurrent);
        return enemiesSelected;

    }

}
