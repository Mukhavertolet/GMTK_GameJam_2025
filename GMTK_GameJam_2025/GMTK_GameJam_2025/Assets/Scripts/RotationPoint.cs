using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPoint : MonoBehaviour
{
    public Vector3 targetPos;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 rotation = targetPos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
