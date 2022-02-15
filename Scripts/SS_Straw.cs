using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straw : Collidable
{
    // Damage Structure
    //public int[] damagePoint = { 1, 20 };
    //public float[] pushForce = { 2.0f, 3.0f };
    
    //Upgrade
    //public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing
    private Animator anim;

    private float cooldown = 0.5f;
    private float lastSwing;

    public int bobaCount;
    public int bobaCountMax;

    public GameObject projectile;

    private float x;
    private float y;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.instance.bobaCount = bobaCount;
        GameManager.instance.bobaCountMax = bobaCountMax;

    }

    protected override void Start()
    {
       

        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
        
    }

    protected override void Update()
    {
        //Debug.Log(GameManager.instance.bobaCount.ToString());

        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;

                if (GameManager.instance.bobaCount !=0)
                {
                    MakeBoba();
                }
                else
                {
                    RefillBoba();
                }

                //Shoot();
            }
        }
    }



    //shooting Boba
    private void MakeBoba()
    {

        if (GameManager.instance.bobaCount>0)
        {
            //get player direction
            Vector2 v2temp = GameManager.instance.playerFacing;

            //set shooting direction and rotation
            float zTemp = Mathf.Atan2(v2temp[1], v2temp[0]) * Mathf.Rad2Deg;
            Vector3 v3temp = new Vector3(0, 0, zTemp);

            Boba boba = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Boba>();

            //shoot
            boba.Setup(v2temp, v3temp);

            //update numbers
            GameManager.instance.bobaCount -= 1;
          

        }
    }
   
    private void RefillBoba()
    {
        GameManager.instance.bobaCount = bobaCountMax;
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log(other.name);
    //}

    protected override void OnCollide(Collider2D coll)
    {
        //if (coll.tag =="Fighter")
        //{
        //    if (coll.name == "Player")
        //        return;

        //    //create a new damage object,then send it to the fighter we hit
        //    Damage dmg = new Damage
        //    {
        //        damageAmount = damagePoint[weaponLevel],
        //        origin = transform.position,
        //        pushForce = pushForce[weaponLevel]
        //    };

        //    coll.SendMessage("ReceiveDamage",dmg);

        //    //Debug.Log(coll.name);
        //}

        //Debug.Log(coll.name);
    }



    //private void Shoot()
    //{
    //    anim.SetTrigger("Shoot");

    //}




    //public void UpgradeWeapon()
    //{
    //    weaponLevel++;
    //    spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    //    // change stats

    //}

    //public void SetWeaponLevel(int level)
    //{
    //    weaponLevel = level;
    //    spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    //}
}
