using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //object references
    [SerializeField]
    public static EntityStats playerInstance;
    [SerializeField]
    public static GameManager gameManager;

    public UIManager uiManager;


    //prefabs
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private GameObject enemyPrefab;


    [SerializeField]
    private GameObject roomPrefab;




    //gameplay params

    public List<Room> rooms;

    public Room currentRoom;



    public int cycleCounter = 1;


    public int enemiesCount = 0;


    public List<GameObject> BulletPatternInstances;



    //default player stats

    private int DEFAULTmaxHP = 5;
    private int DEFAULTcurrentHP = 5;
    private int DEFAULTdamage = 2;
    private float DEFAULTattackSpd = 0.5f;
    private float DEFAULTmoveSpd = 4f;
    private float DEFAULTbulletSpd = 12f;
    private float DEFAULTbulletSize = 1f;
    private int DEFAULTpierces = 0;



    public int maxHP = 5;
    public int currentHP = 5;

    public int damage = 2;
    public float attackSpd = 2f;

    public float moveSpd = 5f;
    public float bulletSpd = 6f;
    public float bulletSize = 1f;

    public int pierces = 0;


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

    private void RefreshEffects()
    {
        maxHP = DEFAULTmaxHP;
        currentHP = DEFAULTcurrentHP;

        damage = DEFAULTdamage;
        attackSpd = DEFAULTattackSpd;

        moveSpd = DEFAULTmoveSpd;
        bulletSpd = DEFAULTbulletSpd;
        bulletSize = DEFAULTbulletSize;

        pierces = DEFAULTpierces;




    }


    private IEnumerator StartGame()
    {
        for (int i = 0; i < 4; i++)
        {
            rooms.Add(Instantiate(roomPrefab).GetComponent<Room>());
            rooms[i].roomNumber = i + 1;
            rooms[i].enemyQuantity += i + cycleCounter;
        }
        StartCoroutine(StartNewRoom());


        yield return null;
    }


    public IEnumerator StartNewRoom()
    {
        foreach (GameObject bulletPattern in BulletPatternInstances)
        {
            Destroy(bulletPattern);
        }

        yield return new WaitForSeconds(2);

        Debug.Log(currentHP, playerInstance);

        if (playerInstance != null)
        {
            DEFAULTcurrentHP = playerInstance.currentHP;

            Destroy(playerInstance.gameObject);
        }



        if (currentRoom != null && rooms.IndexOf(currentRoom) < 3)
        {
            currentRoom.gameObject.SetActive(false);

            currentRoom = rooms[rooms.IndexOf(currentRoom) + 1];
        }
        else if (currentRoom != null)
        {
            currentRoom.gameObject.SetActive(false);
            currentRoom = rooms[0];
            cycleCounter += 1;
        }
        else
            currentRoom = rooms[0];

        uiManager.SetRoomNumberText(currentRoom.roomNumber);

        currentRoom.gameObject.SetActive(true);
        currentRoom.StartRoom();

        Debug.Log(currentHP, playerInstance);

        RefreshEffects();

        Debug.Log(currentHP, playerInstance);

        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<EntityStats>();
        playerInstance.SetStatsDefault(maxHP, currentHP, damage, attackSpd, moveSpd, bulletSpd, bulletSize, pierces);

        uiManager.playerInstance = playerInstance;

    }


    public void RemoveEntity(EntityStats entity)
    {
        currentRoom.enemies.Remove(entity);
        if (currentRoom.enemies.Count <= 0)
        {
            StartCoroutine(GameManager.gameManager.StartNewRoom());
        }
    }
}
