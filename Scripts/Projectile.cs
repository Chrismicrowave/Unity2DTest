using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 directionToMove;
    public float lifetime;
    private float lifetimeSeconds;
    private Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;

    }

    private void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if(lifetimeSeconds<=0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel)
    {
        myRigidbody.velocity = initialVel * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
