using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    public string shooterName;

    public float bulletSize;
    public float bulletDamage;
    public float bulletSpeed;

    public int pierces;

    public List<Bullet> bulletPrefab;




    private void Start()
    {
        
        bulletPrefab = new List<Bullet>();
        foreach (Transform child in transform)
        {
            bulletPrefab.Add(child.GetComponent<Bullet>());
        }

            StartCoroutine(LaunchBullets());
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


            yield return new WaitForSeconds(bullet.delay);
        }
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
