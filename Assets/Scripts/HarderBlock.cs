using UnityEngine;
using System.Collections;

public class HarderBlock : Block {
    public Sprite hitSprite;
	public override void Hit()
    {
        Se2.GetComponent<AudioSource>().Play();
        hitTime--;
        if (hitTime == 1)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
        }else if (hitTime == 0)
        {
            Dead();
        }
        
    }


}
