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
        for (int i = 0; i < sprRenderers.Length; i++)
        {
            sprRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    public override void BgMove(int index)
    {
        base.BgMove(index);

        int rand = Random.Range(0, 4);
        sprRenderers[(int)index].flipX = (rand & 0b_01) != 0;
        sprRenderers[(int)index].flipY = (rand & 0b_10) != 0;

    }
}
