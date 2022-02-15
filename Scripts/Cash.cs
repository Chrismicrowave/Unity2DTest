using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : Collectable
{
    public Sprite emptyCash;
    public int yuanAmount = 5;
    private Transform playerTransform;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            //GetComponent<SpriteRenderer>().sprite = emptyCash;
            Destroy(gameObject);

            GameManager.instance.yuans += yuanAmount;

            // "playerTransform.position" causing NullReferenceException bug.
            //GameManager.instance.ShowText("捡到 " + yuanAmount + " 块！", 20, Color.black, playerTransform.position, Vector3.up * 5, 1f);

            GameManager.instance.ShowText("捡到 " + yuanAmount + " 块！", 20, Color.black, transform.position, Vector3.up * 5, 1f);


        }
        

    }

}
