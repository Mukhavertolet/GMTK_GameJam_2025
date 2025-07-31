using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EntityStats entityStats;
    public RotationPoint rotationPoint;



    public Camera cam;
    public Vector3 mousePos;



    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

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

        if(Input.GetMouseButtonDown(0))
        {
            entityStats.Attack();
        }    


    }
}
