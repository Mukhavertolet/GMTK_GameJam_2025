using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trianglePointer;


    public string shooterName;

    public float damage = 1f;
    public float moveSpd = 3f;

    public float maxLifetime = 5f;
    public float currentLifeTime = 0f;

    public int pierces = 0;


    public float delay = 0f;


    public Vector2 direction = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        trianglePointer.SetActive(false);

        StartCoroutine(Timer());

        if (shooterName == "Player")
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(direction * moveSpd * Time.deltaTime, Space.Self);

        if (currentLifeTime >= maxLifetime)
            Destroy(gameObject);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            currentLifeTime += 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Wall>() != null || (collision.GetComponent<HurtBox>() != null && collision.GetComponent<HurtBox>().entityName != shooterName))
        {
            if (pierces <= 0)
                Destroy(gameObject);

            pierces -= 1;

        }


    }

}
