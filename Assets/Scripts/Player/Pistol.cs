using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Text bulletCounter;
    private int bullets = 20;
    private int fireRate = 1;
    private float nextFire = 0;
    private float[] attackDetails = new float[2];

    private void Update()
    {
        if (Input.GetMouseButtonDown(2) && Time.time > nextFire && bullets > 0)
        {
            bullets--;
            bulletCounter.text = bullets.ToString();
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot ()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 25);

        attackDetails[0] = 15;
        attackDetails[1] = transform.position.x;

        if(hitInfo && hitInfo.transform.tag == "Enemy")
        {
            hitInfo.transform.parent.SendMessage("Damage", attackDetails);
        }
    }
}
