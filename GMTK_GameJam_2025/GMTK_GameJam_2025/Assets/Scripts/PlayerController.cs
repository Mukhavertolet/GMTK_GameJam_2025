using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EntityStats entityStats;
    public RotationPoint rotationPoint;



    public Camera cam;
    public Vector3 mousePos;


    public float currentLifeTime = 0f;
    public float timeSinceLastMove = 0f;
    public float timeSinceLastAttack = 0f;

    public bool canAttack = true;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        //look
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotationPoint.targetPos = mousePos;

        //walk
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 movement = inputDirection.normalized * entityStats.moveSpd;
        transform.position += (movement * Time.deltaTime);

        if (timeSinceLastAttack > entityStats.attackSpd)
            canAttack = true;

        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            entityStats.Attack();
            canAttack = false;
            timeSinceLastAttack = 0f;
        }    

        if(Input.GetKeyDown(KeyCode.Space))
        {
            EffectManager.effectManager.OnSpace();
        }



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
