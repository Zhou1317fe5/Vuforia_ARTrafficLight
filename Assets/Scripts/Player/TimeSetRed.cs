using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSetRed : MonoBehaviour
{
    public SetLight _sl;
    public Dropdown _dd;
    public CrossingController[] _cc;
    public GameObject _go;
    public TextMeshProUGUI _textInfo;
    private float current = 0;
    [HideInInspector]
    public int currentLuKou = 0;

    public TcpServer _ts;

    private void Start()
    {
        _go.SetActive(false);
    }
    private void OnEnable()
    {
        if (_cc[currentLuKou] != null)
        {
            _textInfo.text = ((int)(_cc[currentLuKou].duration - _cc[currentLuKou].timer)).ToString();
        }
      
    }
    private void Update()
    {
        if (!_go.activeSelf)
            return;
        current += Time.deltaTime;
        if (current >= 1)
        {
            current = 0;
            _textInfo.text = ((int)(_cc[currentLuKou].duration - _cc[currentLuKou].timer)).ToString();
        }
      
    }
    public void SetTime()
    {
        float _time = float.Parse(_dd.captionText.text);
        _cc[currentLuKou]. SetHLDNumber(_time);

        Debug.Log("本机路口#" + currentLuKou + "红绿灯时间为#" + _time);
        Debug.Log("开始发送设置的信息");
        string sendMessage = "1" + currentLuKou.ToString() + _time.ToString(); //发送设置红绿灯的信息
        Debug.Log("发送设置的信息为：" + sendMessage + "开始发送");
        _ts.inputMessage = sendMessage;
        _ts.isSendData = true;
    }
    public void CanelBtn()
    {
        gameObject.SetActive(false);
    }
    public void QH_Btn()
    {
        float _time = float.Parse(_dd.captionText.text);
        Debug.Log("切换本机红绿灯时间");
        _cc[currentLuKou].duration = _time;
        _cc[currentLuKou].SetHLD();
        gameObject.SetActive(false);

        Debug.Log("本机路口#" + currentLuKou + "切换红绿灯时间为#" + _time);
        Debug.Log("开始发送切换的信息");
        string sendMessage = "2" + currentLuKou.ToString() + _time.ToString(); //发送切换红绿灯的信息
        Debug.Log("发送切换的信息为：" + sendMessage + "开始发送");
        _ts.inputMessage = sendMessage;
        _ts.isSendData = true;
    }
    public void Btn_Click()
    {
        int cur = currentLuKou + 1;
        _sl.LoadSceenNumber(cur);
        SetTime();
        gameObject.SetActive(false);
    }

    public void TcpSetTime(int currentLuKou,float time) // 接收到来自电脑的设置信息时，设置红路灯时间
    {
        Debug.Log("成功调用TcpSetRed方法，开始设置红绿灯时间");
        Debug.Log("设置路口#"+currentLuKou+"#红绿灯时间为#"+time);
        int cur = currentLuKou + 1;
        _sl.LoadSceenNumber(cur);

        //SetTime();
        _cc[currentLuKou].SetHLDNumber(time);
        Debug.Log("时间：" + time);

        gameObject.SetActive(false);
    }

    public void Tcp_QH_Btn(int currentLuKou, float time) // 接收到来自电脑的切换信息时，切换红路灯时间
    {
        _cc[currentLuKou].duration = time;
        _cc[currentLuKou].SetHLD();
        gameObject.SetActive(false);
    }
}
