using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage Structure
    public int[] damagePoint = { 1, 20 };
    public float[] pushForce = { 2.0f, 3.0f };

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing
    private Animator anim;

    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag =="Fighter")
        {
            if (coll.name == "Player")
                return;

            //create a new damage object,then send it to the fighter we hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage",dmg);

            //Debug.Log(coll.name);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");

    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // change stats

    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
