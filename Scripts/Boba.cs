using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidbody;

    public float lifetime;
    private float lifetimeSeconds;

    private Animator animator;
    public int statesNumber;

    
    private float bornTime;
    private float enterTime;
    private float travelTime;
    public float airBorneTime;

    Collider2D myCollider;

    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;

        animator = GetComponent<Animator>();
        animator.SetBool("isLanded", false);

        bornTime = Time.time;
        
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);

        myRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        myCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blocking")
        {

            enterTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Igonre collision example

        //if (collision.gameObject.tag == "Player")
        //{
        //    Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());

        //}
        if (collision.gameObject.tag == "Blocking")
        {
            StartCoroutine(bobaEnterCollider());
            bobaEnterCollider();
        }
       
    }


    private IEnumerator bobaEnterCollider()
    {
        
        travelTime =  airBorneTime - (enterTime - bornTime);
        //Debug.Log(airBorneTime.ToString() + "|" + enterTime.ToString() + "|" + bornTime.ToString());
        //Debug.Log(travelTime.ToString());

        yield return new WaitForSeconds(travelTime);

        //myCollider.isTrigger = false;
        myRigidbody.velocity = Vector3.zero;
        animator.SetBool("isLanded", true);
        animator.SetInteger("bobaLandedRandom", Random.Range(0, statesNumber));
    }

    

}
