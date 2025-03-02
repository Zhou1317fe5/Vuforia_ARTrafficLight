using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Vuforia;

public class SelectText : MonoBehaviour
{
    public string[] _info;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _btnText;
    public GameObject _panel;
    public GameObject _LkSheZhi;
    
    void Start()
    {
        _LkSheZhi.SetActive(false);
        _panel.SetActive(false);
    }
    public IEnumerator startGameShow(GameObject _go, float time, Action _ac)
    {
        yield return new WaitForSeconds(time);
        _go.SetActive(true);
        if (_ac != null)
        {
            _ac.Invoke();
        }
    }
    IEnumerator startGameShow(GameObject _go, float time)
    {
        yield return new WaitForSeconds(time);
        _go.SetActive(true);
    }

    //TcpServer调用，用来启动红绿灯
    public void Tcp_ButNameClick_test02()
    {
        Debug.Log("调用成功");
        _panel.SetActive(false);
        _LkSheZhi.SetActive(true);
    }

    public void StopLKSheZhi() //TcpServer调用，禁用路口设置,恢复初始状态
    {
        Debug.Log("StopLKSheZhi");
        _panel.SetActive(false);

        GameObject LuKouBS = GameObject.Find("LuKouBS");
        LuKouBS.SetActive(false);
        _LkSheZhi.SetActive(false);


        // 获取CrossingCollection 
        GameObject crossingCollection = GameObject.Find("TrafficSystem/CrossingCollection");

        // 循环获取所有Crossing对象
        for (int i = 1; i <= 7; i++)
        {

            // 拼接对象名称
            string name = "Crossing" + i;

            // 获取对应对象
            GameObject crossing = crossingCollection.transform.Find(name).gameObject;
            // 设置为不激活
            crossing.SetActive(false);
            /*
            // 获取CrossingController脚本并设置duration变量为10
            CrossingController controller = crossing.GetComponent<CrossingController>();
            if (controller != null)
            {
                controller.duration = 10;
            }

            Transform trafficLight = crossing.transform.Find("traffic lights (" + (i-1) + ")");
            if (trafficLight != null)
            {
                RedLightCtro redLightController = trafficLight.GetComponent<RedLightCtro>();
                if (redLightController != null)
                {
                    redLightController.SetLeftOrRight(1);
                    redLightController.SetLeftOrRight(0);
                }
            }*/


        }
    }
}
