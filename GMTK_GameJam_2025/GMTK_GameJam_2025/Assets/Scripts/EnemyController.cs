using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EntityStats entityStats;

    public RotationPoint rotationPoint;

    public GameObject target;

    public float currentLifeTime = 0f;
    public float timeSinceLastMove = 0f;
    public float timeSinceLastAttack = 0f;

    public bool isMoving = false;
    public Vector3 movement;
    private float randomMovementTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        randomMovementTime = Random.Range(0.8f, 2f);

        StartCoroutine(Timer());


    }

    // Update is called once per frame
    void Update()
    {
        rotationPoint.targetPos = target.transform.position;

        if (timeSinceLastAttack > entityStats.attackSpd)
        {
            entityStats.Attack();
            timeSinceLastAttack = 0f;
        }


        if (timeSinceLastMove > randomMovementTime && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Move());
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            randomMovementTime = Random.Range(0.8f, 3f);
        }

        transform.position += (movement * entityStats.moveSpd * Time.deltaTime);
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 0.7f));
        timeSinceLastMove = 0f;
        movement = Vector3.zero;
        isMoving = false;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            currentLifeTime += 0.1f;
            timeSinceLastAttack += 0.1f;
            timeSinceLastMove += 0.1f;
        }
    }


}
