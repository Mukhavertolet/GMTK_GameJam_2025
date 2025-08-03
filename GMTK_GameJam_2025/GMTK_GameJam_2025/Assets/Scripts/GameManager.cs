using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //object references
    [SerializeField]
    public static EntityStats playerInstance;
    [SerializeField]
    public static GameManager gameManager;

    public UIManager uiManager;
    public AudioManager audioManager;


    //prefabs
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private GameObject enemyPrefab;


    [SerializeField]
    private GameObject roomPrefab;

    public GameObject tutRoom;
    public bool tut;


    //gameplay params

    public List<Room> rooms;

    public Room currentRoom;

    private IEnumerator startRoomCor;

    public bool boss = false;


    public int cycleCounter = 1;

    public int cumulativePointRemaining = 0;


    public int enemiesCount = 0;


    public List<GameObject> BulletPatternInstances;


    public List<GameObject> itemDrops;




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
        audioManager = FindObjectOfType<AudioManager>();

        if (SceneManager.GetActiveScene().name != "SampleScene")
            StartCoroutine(Tutorial());
        else
            StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Tab))
            Restart();
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

    private IEnumerator Tutorial()
    {
        tut = true;
        GameObject room = Instantiate(tutRoom);

        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<EntityStats>();
        playerInstance.currentHP = currentHP;




        uiManager.playerInstance = playerInstance;

        yield return new WaitForSeconds(3f);

        room.SetActive(true);
        room.GetComponent<Room>().StartRoomTutorial();

        currentRoom = room.GetComponent<Room>();


        yield return null;
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
        if (tut)
        {
            SceneManager.LoadScene("SampleScene");
        }

        foreach (GameObject bulletPattern in BulletPatternInstances)
        {
            Destroy(bulletPattern);
        }

        yield return new WaitForSeconds(0.5f);

        //Debug.Log(currentHP, playerInstance);

        if (playerInstance != null)
        {
            currentRoom.DropItems();
            InventoryManager.inventory.choice = true;

            yield return new WaitUntil(() => !InventoryManager.inventory.choice);

            yield return new WaitForSeconds(3);

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

        currentRoom.enemyPointsMax = (int)(rooms.IndexOf(currentRoom) * 1.5f + cycleCounter * 8 + 10);
        if (currentRoom.roomNumber == 3)
        {
            currentRoom.enemyPointsMax += cumulativePointRemaining;
            cumulativePointRemaining = 0;
        }

        if (currentRoom.roomNumber == 4)
            boss = true;
        else
            boss = false;

        currentRoom.StartRoom(boss);

        //Debug.Log(currentHP, playerInstance);

        RefreshEffects();

        //Debug.Log(currentHP, playerInstance);

        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<EntityStats>();
        //playerInstance.SetStatsDefault(maxHP, currentHP, damage, attackSpd, moveSpd, bulletSpd, bulletSize, pierces);
        playerInstance.currentHP = currentHP;

        foreach (IEffect effect in EffectManager.effectManager.effects)
        {
            if (effect.GetCondition() == "")
                effect.ApplyEffect();
        }



        uiManager.playerInstance = playerInstance;

    }


    public void RemoveEntity(EntityStats entity)
    {
        if (entity != playerInstance)
        {
            currentRoom.enemies.Remove(entity);
            if (currentRoom.enemies.Count <= 0)
            {
                if (tut)
                {
                    Destroy(currentRoom.gameObject);
                    currentRoom = null;
                }
                StartCoroutine(startRoomCor = GameManager.gameManager.StartNewRoom());
            }
        }
        else
        {
            StartCoroutine(StopGame());
        }
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(2f);

        uiManager.deathText.SetActive(true);
        uiManager.gameUI.SetActive(false);

        yield return new WaitForSeconds(3f);
        uiManager.restartText.SetActive(true);


        yield return null;
    }

    private void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
