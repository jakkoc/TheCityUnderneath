using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private int damage = 10;
    private float nextDamage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;

        if (other.tag == "Player" && Time.time > nextDamage)
        {
            nextDamage = Time.time + 0.5f;
            other.transform.SendMessage("TakeDamage", damage);
        }
    }
}
