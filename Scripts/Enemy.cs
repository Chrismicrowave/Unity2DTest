using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;

    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //is player in range?
        if(Vector3.Distance(playerTransform.position, startingPosition)< chaseLength)
        {
            if(chasing = Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
                GameManager.instance.enemychasing = chasing;
            }

            if (chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
                GameManager.instance.enemychasing = chasing;

            }

        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
            GameManager.instance.enemychasing = chasing;
        }

        //check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name =="Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }

    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 20, Color.black, playerTransform.position, Vector3.up *10, 0.5f);
    }

    
}

