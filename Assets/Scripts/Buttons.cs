using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour {
    public int level = 1;
    public void onClick()
    {
        SceneManager.LoadScene(1);
    }
    public void onClick2()
    {
        Application.Quit();
    }
    public void onClick3()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNum") + 3);
    }
    public void onClick4()
    {
        SceneManager.LoadScene(0);
    }
    public void onClickLevel()
    {
        SceneManager.LoadScene(level + 3);
    }
}
