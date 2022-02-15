using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : Collidable
{
    public string message;
    public float cooldown = 4.0f;
    private float lastShow;

    protected override void Start()
    {
        base.Start();
        lastShow = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShow > cooldown)
        {
            lastShow = Time.time;
            GameManager.instance.ShowText(message, 25, Color.black, transform.position + new Vector3(0, GetComponent<BoxCollider2D>().bounds.extents.y, 0), Vector3.zero, 4.0f);

        }

    }
}
