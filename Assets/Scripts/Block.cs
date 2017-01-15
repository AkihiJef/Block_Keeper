using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    public int hitTime = 1;
    public bool blockActive = false;
    public Color specialColor;
    protected float initTime = 0.5f;
    protected float timeCount;
    public bool special = false;
    public GameObject levelSystem;
    protected GameObject Se2;
    protected GameObject ball;
    // Use this for initialization
    protected virtual void Start () {
        Se2 = GameObject.Find("Se2");
        timeCount = initTime;
        if (special)
        {
            levelSystem = GameObject.Find("LevelSystem");
            levelSystem.GetComponent<LevelSystem>().addSpecialBlockLive();
            setSpecial();
        }
        
    }
    public void setSpecial()
    {
        special = true;
        GetComponent<SpriteRenderer>().color = specialColor;
    }
    public virtual void Dead()
    {
        if (special)
        {
            levelSystem.GetComponent<LevelSystem>().declineSpecialBlockLive();
        }
        ball = GameObject.Find("Ball");
        print(ball);
        ball.GetComponent<Ball>().randStatus();
        Destroy(gameObject);
    }
	public virtual void Hit()
    {
        Se2.GetComponent<AudioSource>().Play();
        if (timeCount <= 0)
        {
            hitTime--;
            if (hitTime == 0) Dead();
        }
    }
    // Update is called once per frame
    protected void Update () {
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
        }
	}
}
