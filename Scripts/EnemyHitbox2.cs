using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox2 : Collidable
{
    public int damage;
    public float pushForce;

    private SpriteRenderer spriteRenderer;

    //swing
    private Animator anim;

    private float cooldown = 0.8f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (GameManager.instance.enemychasing == true)
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
            
        }
    }

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.tag == "Fighter" && coll.name == "Player")
        {

            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage",dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");

    }

}


