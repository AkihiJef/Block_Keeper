using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSystem : MonoBehaviour {
    public int levelNum = 1;
    public int progressStyle = 0;
    float progressTimeCount = 0;
    public float prepareTime = 15;
    public float gameTime = 60;
    public float playerCount= 295;
    public int specialBlockLive=0;
    public Text countText;
    public List<GameObject> stars;
    public GameObject star;
    public GameObject ball;
    public GameObject progressBar;
    public float progressBarScaleY;
    public Color progressBarGamingColor;
    public float countIncreaseTime = 3;
    GameObject platform;
	// Use this for initialization
    void Awake()
    {
        stars = new List<GameObject>();

    }
    void Start () {
        progressTimeCount = prepareTime;
        ball.SetActive(false);
        progressBarScaleY = progressBar.transform.localScale.y;
        platform = GameObject.Find("Platform");
        platform.SetActive(false);
	}
	public void addSpecialBlockLive()
    {
        specialBlockLive++;
        GameObject last = Instantiate(star, new Vector3(7.25851f - 0.8f * (specialBlockLive - 1), 4.40811f, 0), transform.rotation) as GameObject;
        stars.Add(last);
    }
    public void declineSpecialBlockLive()
    {
        specialBlockLive--;
        GameObject last = stars[specialBlockLive];
        stars.Remove(last);
        Destroy(last);
        if (specialBlockLive == 0)
        {
            PlayerDead();
        }
    }
    void PlayerDead()
    {
        PlayerPrefs.SetInt("LevelNum", levelNum);
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame
    void StartGame()
    {
        platform.SetActive(true);
        ball.SetActive(true);
        progressBar.GetComponent<SpriteRenderer>().color = progressBarGamingColor;
    }
    void NextLevel()
    {
        SceneManager.LoadScene(3);
    }
    void Update () {
        if(progressStyle==1)playerCount += specialBlockLive * Time.deltaTime * countIncreaseTime;
        countText.text = ((int)playerCount).ToString();
        if (progressTimeCount > 0)
        {
            progressTimeCount -= Time.deltaTime;
            if (progressStyle == 0)
            {
                progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x, progressBarScaleY * progressTimeCount / prepareTime,0);
            }else if (progressStyle == 1)
            {
                progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x, progressBarScaleY * progressTimeCount / gameTime, 0);

            }

        }
        else
        {
            if (progressStyle == 0)
            {
                
                progressStyle = 1;
                progressTimeCount = gameTime;
                StartGame();
            }else if (progressStyle == 1)
            {
                NextLevel();
            }
        }
        

    }
}
