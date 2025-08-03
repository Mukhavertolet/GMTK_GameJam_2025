using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public GameObject target;

    public GameObject trianglePointer;

    public bool isActivated = false;


    public string shooterName;

    public float damage = 1f;
    public float moveSpd = 3f;

    public float maxLifetime = 5f;
    public float currentLifeTime = 0f;

    public int pierces = 0;


    public float delay = 0f;

    public float followSpeed = 0;
    public bool followTarget = false;


    public Vector2 direction = Vector2.right;

    public string hitSound;

    public GameObject particles;


    // Start is called before the first frame update
    void Start()
    {
        trianglePointer.SetActive(false);


    }

    public void ActivateBullet()
    {
        isActivated = true;
        StartCoroutine(Timer());

        if (shooterName == "Player")
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
            transform.Translate(direction * moveSpd * Time.deltaTime, Space.Self);

        if (currentLifeTime >= maxLifetime)
            Destroy(gameObject);

        if (followTarget)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, followSpeed * Time.deltaTime);
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

            GameManager.gameManager.audioManager.Play(hitSound, Random.Range(0.9f, 1.1f));
            Instantiate(particles, transform.position, Quaternion.identity);

            pierces -= 1;

        }


    }

}
