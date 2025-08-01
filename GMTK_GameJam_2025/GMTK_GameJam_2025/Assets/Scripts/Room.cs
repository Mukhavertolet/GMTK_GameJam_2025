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
        StartCoroutine(SpawnEnemies(enemyQuantity));
    }

    private IEnumerator SpawnEnemies(int enemyQuantity)
    {
        for (int i = 0; i < enemyQuantity; i++)
        {
            yield return new WaitForSeconds(0.1f);
            enemies.Add(Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)), Quaternion.identity).GetComponent<EntityStats>());
            enemies[i].room = this;
        }
    }

}
