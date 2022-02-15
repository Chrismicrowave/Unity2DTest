using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Figther : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    protected float immuneTime = 0.1f;
    protected float lastImmune;

    protected Vector3 pushDirection;

    protected int txtSize = 30;
    protected Color txtColor = Color.grey;
    

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            
            GameManager.instance.ShowText((dmg.damageAmount*-1).ToString(), txtSize, txtColor, transform.position, Vector3.zero, 0.5f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
