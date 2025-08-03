using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletPattern : MonoBehaviour
{
    private GameObject target;


    public string shooterName;

    public float bulletSize;
    public float bulletDamage;
    public float bulletSpeed;

    public int pierces;

    public List<Bullet> bulletPrefab;

    public float rotationAmount = 0;

    public bool followTarget = false;
    public float followSpeed = 1f;

    public GameObject fireParticles;

    private void Start()
    {

        target = GameManager.playerInstance.gameObject;

        GameManager.gameManager.BulletPatternInstances.Add(this.gameObject);
        bulletPrefab = new List<Bullet>();
        foreach (Transform child in transform)
        {
            bulletPrefab.Add(child.GetComponent<Bullet>());
        }

        StartCoroutine(LaunchBullets());
    }

    private void Update()
    {
        if (rotationAmount != 0)
        {
            transform.Rotate(0, 0, 6f * rotationAmount * Time.deltaTime);
        }

        if (followTarget)
        {
            //transform.position += Vector3.Lerp(transform.position, GameManager.playerInstance.transform.position, 10);
        }

    }


    public IEnumerator LaunchBullets()
    {
        for (int i = 0; i < bulletPrefab.Count; i++)
        {
            Bullet bullet = bulletPrefab[i];
            //bullet.transform.position = transform.position;
            //bullet.transform.rotation *= transform.rotation;
            bullet.moveSpd = bulletSpeed;
            bullet.damage = bulletDamage;
            bullet.gameObject.transform.localScale *= bulletSize;
            bullet.shooterName = shooterName;
            bullet.pierces = pierces;
            bullet.target = target;

            bullet.ActivateBullet();

            if (bullet.delay > 0)
                yield return new WaitForSeconds(bullet.delay);

        }

        Instantiate(fireParticles, transform.position, transform.rotation * fireParticles.transform.rotation, transform);
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);


    }
    public void SetBulletParams(float size, float damage, float speed, string shooter, int pierceAmount)
    {
        bulletSize = size;
        bulletDamage = damage;
        bulletSpeed = speed;
        shooterName = shooter;
        pierces = pierceAmount;
    }
}
