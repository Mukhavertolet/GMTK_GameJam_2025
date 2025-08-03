using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomNumber = 0;

    [SerializeField]
    private List<GameObject> enemyPrefab;

    [SerializeField]
    private List<GameObject> bossPrefab;


    public int enemyQuantity = 1;

    public int enemyPointsMax = 15;
    public int enemyPointsCurrent = 15;

    public int lowestEnemyCost = 1000;

    //items placed in the room
    public List<Item> items = new List<Item>();


    //enemies alive in the room
    public List<EntityStats> enemies = new List<EntityStats>();

    public List<GameObject> itemDrops;
    public List<GameObject> droppedItems;

    public GameObject leftItem;





    public GameObject leftItemParticleSystem;




    // Start is called before the first frame update
    void Start()
    {
        itemDrops = GameManager.gameManager.itemDrops;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRoom(bool boss)
    {
        if (leftItem != null)
            leftItem.SetActive(false);

        if (!boss)
        {
            enemyPointsCurrent = enemyPointsMax;

            foreach (GameObject enemy in enemyPrefab)
            {
                EntityStats e = enemy.GetComponent<EntityStats>();
                if (e.pointCost < lowestEnemyCost) lowestEnemyCost = e.pointCost;
            }

            StartCoroutine(SpawnEnemies());
        }
        else
        {
            StartCoroutine(SpawnBoss());
        }
    }
    public void DropItems()
    {
        if (leftItem != null)
        {
            droppedItems.Add(leftItem);
            leftItem.SetActive(true);
            leftItem.transform.position = new Vector3(-3, 0, 0);
            Instantiate(leftItemParticleSystem, leftItem.transform);
        }

        for (int i = -1; i <= 1; i++)
        {
            if (leftItem != null && i == -1)
                continue;

            droppedItems.Add(Instantiate(GameManager.gameManager.itemDrops[Random.Range(0, itemDrops.Count)], transform.position + new Vector3(i * 3, 0, 0), Quaternion.identity, transform));


        }
    }
    public void RemoveDroppedItemsExcept(GameObject chosenItem)
    {
        foreach (GameObject item in droppedItems)
        {
            if (item != chosenItem)
            {
                Destroy(item);
            }
        }
        droppedItems.Clear();
        InventoryManager.inventory.chosenItem = null;
        leftItem = chosenItem;

    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(0.1f);
        EntityStats bossEnemy = Instantiate(bossPrefab[Random.Range(0, bossPrefab.Count)], Vector2.zero, Quaternion.identity).GetComponent<EntityStats>();
        enemies.Add(bossEnemy);
        bossEnemy.room = this;
        bossEnemy.currentHP = bossEnemy.maxHP;

    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1f);
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
