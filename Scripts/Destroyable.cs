using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : Figther
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}

