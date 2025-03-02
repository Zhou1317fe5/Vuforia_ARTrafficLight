using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
   
    public void LoadS(string str)
    {
        QuanJu.ScreenName = str;
        SceneManager.LoadScene(3);
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}
