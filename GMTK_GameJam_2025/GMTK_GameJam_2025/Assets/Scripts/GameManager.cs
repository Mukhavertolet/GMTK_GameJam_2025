using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static EntityStats playerInstance;
    [SerializeField]
    public static GameManager gameManager;

    //prefabs
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private GameObject enemyPrefab;


    [SerializeField]
    private GameObject roomPrefab;


    //object references


    //gameplay params

    public List<Room> rooms;

    public Room currentRoom;



    public int loopNumber = 1;


    public int enemiesCount = 0;



    //default player stats
    public int maxHP = 5;
    public int currentHP = 5;

    public int damage = 5;
    public float attackSpd = 1f;

    public float moveSpd = 5f;
    public float bulletSpd = 6f;


    private void Awake()
    {
        gameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator StartGame()
    {
        for (int i = 0; i < 4; i++)
        {
            rooms.Add(Instantiate(roomPrefab).GetComponent<Room>());

        }
        StartCoroutine(StartNewRoom());


        yield return null;
    }


    public IEnumerator StartNewRoom()
    {
        yield return new WaitForSeconds(2);

        if (playerInstance != null)
            Destroy(playerInstance.gameObject);

        if (currentRoom != null && rooms.IndexOf(currentRoom) < 3)
        {
            currentRoom.gameObject.SetActive(false);

            currentRoom = rooms[rooms.IndexOf(currentRoom) + 1];
        }
        else if (currentRoom != null)
        {
            currentRoom.gameObject.SetActive(false);
            currentRoom = rooms[0];
        }
        else
            currentRoom = rooms[0];

        currentRoom.gameObject.SetActive(true);
        currentRoom.StartRoom();


        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<EntityStats>();
        playerInstance.SetStatsDefault(maxHP, currentHP, damage, attackSpd, moveSpd, bulletSpd);

    }


}
