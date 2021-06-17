using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Animator bodyAnimator;
    private int damage = 20;
    private float[] attackDetails = new float[2];
    private float nextDamage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        attackDetails[0] = damage;
        attackDetails[1] = transform.position.x;

        if (other.tag == "Enemy" && Time.time > nextDamage && IsAttacking())
        {
            nextDamage = Time.time + 0.5f;
            other.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private bool IsAttacking()
    {
        return bodyAnimator.GetCurrentAnimatorStateInfo(1).IsName("LightAttack") && bodyAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f; 
    }
}
