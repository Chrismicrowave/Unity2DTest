using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private PolygonCollider2D poly2d;

    // Start is called before the first frame update
    void Start()
    {
        poly2d = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.name == "Player")
        {
            //Debug.Log("11111");
        }

    }
}
