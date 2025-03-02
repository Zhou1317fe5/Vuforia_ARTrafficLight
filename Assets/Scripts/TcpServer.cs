using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class TcpServer : MonoBehaviour
{
    public TimeSetRed _tsr;
    public SelectText _st;


    public string recMes = "NULL";                        //接收到的信息
    private int recTimes = 0;                              //接收到的信息次数 

    private string inputIp;                //ip地址
    private string inputPort = "6000";                     //端口值
    public string inputMessage;                  //用以发送的信息   



    private Socket socketWatch;                            //用以监听的套接字
    private Socket socketSend;                             //用以和客户端通信的套接字

    public bool isSendData = false;                       //是否点击发送数据按钮
    private bool ifConnect = false;                  //是否点击监听按钮

    private bool getNew = false;    //是否接收到新信息
                                    //显示本机IP
    public Text displayselfAddress;
    /*
    // 路口编号到比特映射  
    Dictionary<int, string> crossingMap = new Dictionary<int, string>{
  {0, "000"},
  {1, "001"},
  {2, "010"},
  {3, "011"},
  {4, "100"},
  {5, "101"},
  {6, "110"}
};

    // 时间到比特映射
    Dictionary<float, string> timeMap = new Dictionary<float, string> {
  {10f, "00"},
  {20f, "01"},
  {30f, "10"}
};*/

    void Start()
    {

        //定义本机服务器的ip和端口对

        //inputIp = GetAndIPv4(); //手机端
        //inputIp = GetPCIP(); //电脑端
        inputIp = GetIOSIP3();//ISO

        inputIp = inputIp.Trim();

        Debug.Log("获取IP为：" + inputIp);
        // 显示本机IP
        displayselfAddress.text = inputIp;
        Connect();

    }

    // Update is called once per frame
    void Update()
    {

        if (getNew == true)
        {
            getNew = false;


            string recType;  // 设置时间还是修改时间。‘0’设置，‘1’修改。
            int recCurrentLuKou = -1;// 路口编号
            float recTime = -1; // 红绿灯时间


            /*
            //解析类型信息
            recType = recMes.Substring(0, 1);
            Debug.Log("recType:"+ recType);
   
            //解析路口信息
                string crossingBits = recMes.Substring(1, 1);  //.SubString(x, y) 
                int crossingId = -1;
                foreach (var kv in crossingMap)
                {
                    if (kv.Value == crossingBits)
                    {
                        crossingId = kv.Key;
                        break;
                    }
                }

                recCurrentLuKou = crossingId;


                //解析时间信息
                string timeBits = recMes.Substring(2); // 1111

                float time = -1;
                foreach (var kv in timeMap)
                {
                    if (kv.Value == timeBits)
                    {
                        time = kv.Key;
                        break;
                    }
                }

                recTime = time;
            */

            recType = recMes.Substring(0, 1);
            recCurrentLuKou = int.Parse(recMes.Substring(1, 1));
            recTime = float.Parse(recMes.Substring(2));
            
            //根据类型处理
            if (recType == "0") //开启红绿灯
            {
                Debug.Log("接收到#0,开始调用SelectText脚本启动红绿灯");
                _st.Tcp_ButNameClick_test02();
            }
            else if (recType == "1"){ //设置
                Debug.Log("接收到设置红绿灯的信息,开始设置红绿灯");
                Debug.Log("接收到的路口为: " + recCurrentLuKou + ",接收到的时间为: " + recTime);
                //调用方法 设置红路灯时间
                _tsr.TcpSetTime(recCurrentLuKou, recTime);
            }

            else if (recType == "2") //修改
            {
                Debug.Log("接收到切换红绿灯的信息,开始切换红绿灯");
                Debug.Log("路口" + recCurrentLuKou + "红绿灯时间切换为" + recTime);
                //调用方法 切换红路灯时间
                _tsr.Tcp_QH_Btn(recCurrentLuKou, recTime);
            }

            else if (recType == "3") //重启
            {
                Debug.Log("接收到#3,开始重启场景");
                _st.StopLKSheZhi();
            }

            else
            {
                
            }
        }

    }




    /// <summary>
    /// <span style="font-family: Arial, Helvetica, sans-serif;">获取局域网的IP</span>
    /// </summary>
    /// <returns></returns>
    public string GetIOSIP()
    {
        string AddressIP = string.Empty;

        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        for (int i = 0; i < adapters.Length; i++)
        {
            if (adapters[i].Supports(NetworkInterfaceComponent.IPv4))
            {
                UnicastIPAddressInformationCollection uniCast = adapters[i].GetIPProperties().UnicastAddresses;
                if (uniCast.Count > 0)
                {
                    for (int j = 0; j < uniCast.Count; j++)
                    {
                        //得到IPv4的地址。 AddressFamily.InterNetwork指的是IPv4
                        if (uniCast[j].Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            AddressIP = uniCast[j].Address.ToString();
                        }
                    }
                }
            }
        }
        return AddressIP;
    }


    /// <summary>
    /// 获取局域网的IP
    /// </summary>
    /// <returns></returns>
    public string GetIOSIP2()
    {
        string AddressIP = string.Empty;
#if UNITY_IPHONE
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces(); ;
        foreach (NetworkInterface adapter in adapters)
        {
            if (adapter.Supports(NetworkInterfaceComponent.IPv4))
            {
                UnicastIPAddressInformationCollection uniCast = adapter.GetIPProperties().UnicastAddresses;
                if (uniCast.Count > 0)
                {
                    foreach (UnicastIPAddressInformation uni in uniCast)
                    {
                        //得到IPv4的地址。 AddressFamily.InterNetwork指的是IPv4
                        if (uni.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            AddressIP = uni.Address.ToString();
                        }
                    }
                }
            }
        }
#endif
#if UNITY_STANDALONE_WIN
        ///获取本地的IP地址
        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
            {
                AddressIP = _IPAddress.ToString();
            }
        }
#endif
        return AddressIP;
    }


    public string GetIOSIP3()
    {
        string ipAddress = "";
        IPHostEntry host;
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = ip.ToString();
                break;
            }
        }
        return ipAddress;
    }



/// <summary>
/// 获取手机IP
/// </summary>
// IPV4
public static string GetAndIPv4()
    {
        string ipAddress = "";
        try
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("IP 获取失败");
        }
        return ipAddress;
    }


    /// <summary>
    /// 获取电脑IP
    /// </summary>

    public static string GetPCIP()
    {
        try
        {
            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug.Log(IpEntry.AddressList[i].ToString());
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "noIp";
        }
        catch
        {
            Debug.Log("ipGetFailed");
            return ("ipGetFailed");
        }
    }


    //建立tcp通信链接
    private void Connect()
    {
        try
        {
            int _port = Convert.ToInt32(inputPort);         //获取端口号
            string _ip = inputIp;                           //获取ip地址

            Debug.Log(" ip 地址是 ：" + _ip);
            Debug.Log(" 端口号是 ：" + _port);

            ifConnect = true;                         //点击了监听按钮

            //info = "ip地址是 ： " + _ip + "端口号是 ： " + _port;

            //点击开始监听时 在服务端创建一个负责监听IP和端口号的Socket
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);   //创建对象端口

            socketWatch.Bind(point);                        //绑定端口号

            Debug.Log("监听成功!");
            //info = "监听成功";

            socketWatch.Listen(10);                         //设置监听，最大同时连接10台

            //创建监听线程
            Thread thread = new Thread(Listen);
            thread.IsBackground = true;
            thread.Start(socketWatch);
        }
        catch { }
    }

    /// <summary>
    /// 等待客户端的连接 并且创建与之通信的Socket
    /// </summary>
    void Listen(object o)
    {
        try
        {
            Socket socketWatch = o as Socket;
            while (true)
            {
                socketSend = socketWatch.Accept();           //等待接收客户端连接

                Debug.Log(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功!");
                //info = socketSend.RemoteEndPoint.ToString() + "  连接成功!";

                Thread r_thread = new Thread(Received);      //开启一个新线程，执行接收消息方法
                r_thread.IsBackground = true;
                r_thread.Start(socketSend);

                Thread s_thread = new Thread(SendMessage);   //开启一个新线程，执行发送消息方法
                s_thread.IsBackground = true;
                s_thread.Start(socketSend);
            }
        }
        catch { }
    }

    /// <summary>
    /// 服务器端不停的接收客户端发来的消息
    /// </summary>
   
    void Received(object o)
    {
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                Thread.Sleep(1);//加延迟！！！#######
                //byte[] messageBytes = new byte[1024 * 6];         //客户端连接服务器成功后，服务器接收客户端发送的消息
                byte[] messageBytes = new byte[1024];
                int len = socketSend.Receive(messageBytes);       //实际接收到的有效字节数
                if (len == 0)
                {
                    break;
                }

                string str = Encoding.UTF8.GetString(messageBytes, 0, len);
                Debug.Log("接收到的消息：" + socketSend.RemoteEndPoint + ":" + str);
                recMes = str;
                getNew = true;

                recTimes++;
                //info = "接收到一次数据，接收次数为：" + recTimes;
                Debug.Log("接收数据次数：" + recTimes);

            }
        }
        catch { }
    }

       /*
    void Received(object o)
    {
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                Thread.Sleep(1);//加延迟！！！#######
                byte[] buffer = new byte[1024];         //客户端连接服务器成功后，服务器接收客户端发送的消息
                int len = socketSend.Receive(buffer);       //实际接收到的有效字节数
                if (len == 0)
                {
                    break;  //有问题？
                }

                //取第一个字节作为消息字节
                byte messageByte = buffer[0];


                //解析信息类型比特
                string typeBits = Convert.ToString(messageByte, 2).PadLeft(8, '0').Substring(0, 1);
                //int messageType = int.Parse(typeBits);
                recType = typeBits;



                //解析路口比特
                string crossingBits = Convert.ToString(messageByte, 2).PadLeft(8, '0').Substring(1, 3);
                //int crossingId = Array.IndexOf(crossingMap.Values, crossingBits);
                int crossingId = -1;
                foreach (var kv in crossingMap)
                {
                    if (kv.Value == crossingBits)
                    {
                        crossingId = kv.Key;
                        break;
                    }
                }

                recCurrentLuKou = crossingId;


                //解析时间比特
                string timeBits = Convert.ToString(messageByte, 2).PadLeft(8, '0').Substring(4, 2);
                //float time = Array.IndexOf(timeMap.Values, timeBits);

                float time = -1;
                foreach (var kv in timeMap)
                {
                    if (kv.Value == timeBits)
                    {
                        time = kv.Key;
                        break;
                    }
                }

                recTime = time;


                Debug.Log("接收到消息，消息为：");
                Debug.Log("类型为："+ typeBits + ",路口为："+ crossingId +"，时间为："+time);

            
                getNew = true;

                recTimes++;
                //info = "接收到一次数据，接收次数为：" + recTimes;
                Debug.Log("接收数据次数：" + recTimes);


            }
        }
        catch { }
    }*/


    /// <summary>
    /// 服务器端不停的向客户端发送消息
    /// </summary>
    void SendMessage(object o)
    {
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                if (isSendData)
                {
                    isSendData = false;

                    byte[] sendByte = Encoding.UTF8.GetBytes(inputMessage);

                    Debug.Log("发送的数据为 :" + inputMessage);
                    Debug.Log("发送的数据字节长度 :" + sendByte.Length);

                    socketSend.Send(sendByte);
                }
            }
        }
        catch { }
    }

    private void OnDisable()
    {
        Debug.Log("begin OnDisable()");

        if (ifConnect)
        {
            try
            {
                socketWatch.Shutdown(SocketShutdown.Both);    //禁用Socket的发送和接收功能
                socketWatch.Close();                          //关闭Socket连接并释放所有相关资源

                socketSend.Shutdown(SocketShutdown.Both);     //禁用Socket的发送和接收功能
                socketSend.Close();                           //关闭Socket连接并释放所有相关资源           
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        Debug.Log("end OnDisable()");
    }

}

