using UnityEngine;
using System.Collections;

public class HardestBlock : Block {
    public Sprite hitSprite;
    public Sprite hitSprite2;
    public override void Hit()
    {
        Se2.GetComponent<AudioSource>().Play();
        hitTime--;
        if (hitTime == 2)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
        }
        else if (hitTime == 1)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite2;
        }
        else if (hitTime == 0)
        {
            Dead();
        }

    }


}
