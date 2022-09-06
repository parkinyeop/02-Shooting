using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackGroundStars : BackGround
{
    SpriteRenderer[] sprRenderers;
    protected override void Awake()
    {
        base.Awake();

        sprRenderers = new SpriteRenderer[transform.childCount];
        for(int i = 0; i < sprRenderers.Length; i++)
        {
            sprRenderers[i] = GetComponent<SpriteRenderer>();
        }
    }
}
