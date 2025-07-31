using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EntityStats entityStats;

    public RotationPoint rotationPoint;

    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rotationPoint.targetPos = target.transform.position;
    }
}
