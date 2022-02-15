using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    umbrellaAttack,
    broomAttack,
    bobaAttack,
    interact
}

public class Player : Mover
{ 
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector3 change;
    public PlayerState currentState;

    //facing direction
    private Vector2 playerFacing = new Vector2 (0,-1);

    //weapon cooldown
    private float cooldown = 0.5f;
    private float lastAction;

    //boba tea
    public int bobaCount;
    public int bobaCountMax;
    public GameObject projectile;


    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentState = PlayerState.walk;

        //for bbtea UI count
        GameManager.instance.bobaCount = bobaCount;
        GameManager.instance.bobaCountMax = bobaCountMax;

    }

    private bool actionCoolDown()
    {
        if (Time.time - lastAction > cooldown)
        {
            lastAction = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        //bbtea attack
        if (Input.GetButtonDown("ButtonA") || Input.GetKeyDown("space"))    
        {
            if (currentState != PlayerState.bobaAttack)
            {

                if (actionCoolDown())
                {
                    StartCoroutine(BobaAttackCo());

                    if (GameManager.instance.bobaCount != 0)
                    {
                        ShootBoba();
                    }
                    else
                    {
                        RefillBoba();
                    }
                }
            }
        }

        //walk
        else if (currentState == PlayerState.walk)
        {
            UpdateMotor(new Vector3(change.x, change.y, 0));
            UpdateAnimationAndMove();
        }
        
    }


    //update animation and Move
    private void UpdateAnimationAndMove()
    {

        if (change.x != 0 | change.y != 0)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isWalking", true);

            playerFacing = new Vector2(Mathf.Round(animator.GetFloat("moveX")), Mathf.Round(animator.GetFloat("moveY")));
            //GameManager.instance.playerFacing = playerFacing;
            
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        

    }


    //bbtea codes
    private IEnumerator BobaAttackCo()
    {
        animator.SetBool("isBobaAttacking", true);
        currentState = PlayerState.bobaAttack;
        yield return null;
        animator.SetBool("isBobaAttacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    private void ShootBoba()
    {

        if (GameManager.instance.bobaCount > 0)
        {

            //set shooting direction and rotation

            //set rotation
            //float zTemp = Mathf.Atan2(playerFacing[1], playerFacing[0]) * Mathf.Rad2Deg;
            //Vector3 v3temp = new Vector3(0, 0, zTemp);

            Vector3 v3temp = Vector3.zero;


            Boba boba = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Boba>();

            //shoot
            boba.Setup(playerFacing, v3temp);

            //update numbers
            GameManager.instance.bobaCount -= 1;


        }
    }

    private void RefillBoba()
    {
        GameManager.instance.bobaCount = bobaCountMax;
    }



    protected override void ReceiveDamage(Damage dmg)
    {
        txtColor = Color.red;
        txtSize = 30;
        base.ReceiveDamage(dmg);
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
        playerFacing[0] = -playerFacing[0];
    }

    //entering exiting bridge sorting
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Bld_Bridge")
        {
            spriteRenderer.sortingLayerName = "HighLevel";
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Bld_Bridge")
        {
            spriteRenderer.sortingLayerName = "Objects";
        }
    }


    public void OnLevelUp()
    {
        maxHitpoint += 2;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

}

