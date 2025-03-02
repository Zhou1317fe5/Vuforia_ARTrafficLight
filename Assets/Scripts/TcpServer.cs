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


    public string recMes = "NULL";                        //���յ�����Ϣ
    private int recTimes = 0;                              //���յ�����Ϣ���� 

    private string inputIp;                //ip��ַ
    private string inputPort = "6000";                     //�˿�ֵ
    public string inputMessage;                  //���Է��͵���Ϣ   



    private Socket socketWatch;                            //���Լ������׽���
    private Socket socketSend;                             //���ԺͿͻ���ͨ�ŵ��׽���

    public bool isSendData = false;                       //�Ƿ����������ݰ�ť
    private bool ifConnect = false;                  //�Ƿ���������ť

    private bool getNew = false;    //�Ƿ���յ�����Ϣ
                                    //��ʾ����IP
    public Text displayselfAddress;
    /*
    // ·�ڱ�ŵ�����ӳ��  
    Dictionary<int, string> crossingMap = new Dictionary<int, string>{
  {0, "000"},
  {1, "001"},
  {2, "010"},
  {3, "011"},
  {4, "100"},
  {5, "101"},
  {6, "110"}
};

    // ʱ�䵽����ӳ��
    Dictionary<float, string> timeMap = new Dictionary<float, string> {
  {10f, "00"},
  {20f, "01"},
  {30f, "10"}
};*/

    void Start()
    {

        //���屾����������ip�Ͷ˿ڶ�

        //inputIp = GetAndIPv4(); //�ֻ���
        //inputIp = GetPCIP(); //���Զ�
        inputIp = GetIOSIP3();//ISO

        inputIp = inputIp.Trim();

        Debug.Log("��ȡIPΪ��" + inputIp);
        // ��ʾ����IP
        displayselfAddress.text = inputIp;
        Connect();

    }

    // Update is called once per frame
    void Update()
    {

        if (getNew == true)
        {
            getNew = false;


            string recType;  // ����ʱ�仹���޸�ʱ�䡣��0�����ã���1���޸ġ�
            int recCurrentLuKou = -1;// ·�ڱ��
            float recTime = -1; // ���̵�ʱ��


            /*
            //����������Ϣ
            recType = recMes.Substring(0, 1);
            Debug.Log("recType:"+ recType);
   
            //����·����Ϣ
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


                //����ʱ����Ϣ
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
            
            //�������ʹ���
            if (recType == "0") //�������̵�
            {
                Debug.Log("���յ�#0,��ʼ����SelectText�ű��������̵�");
                _st.Tcp_ButNameClick_test02();
            }
            else if (recType == "1"){ //����
                Debug.Log("���յ����ú��̵Ƶ���Ϣ,��ʼ���ú��̵�");
                Debug.Log("���յ���·��Ϊ: " + recCurrentLuKou + ",���յ���ʱ��Ϊ: " + recTime);
                //���÷��� ���ú�·��ʱ��
                _tsr.TcpSetTime(recCurrentLuKou, recTime);
            }

            else if (recType == "2") //�޸�
            {
                Debug.Log("���յ��л����̵Ƶ���Ϣ,��ʼ�л����̵�");
                Debug.Log("·��" + recCurrentLuKou + "���̵�ʱ���л�Ϊ" + recTime);
                //���÷��� �л���·��ʱ��
                _tsr.Tcp_QH_Btn(recCurrentLuKou, recTime);
            }

            else if (recType == "3") //����
            {
                Debug.Log("���յ�#3,��ʼ��������");
                _st.StopLKSheZhi();
            }

            else
            {
                
            }
        }

    }




    /// <summary>
    /// <span style="font-family: Arial, Helvetica, sans-serif;">��ȡ��������IP</span>
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
                        //�õ�IPv4�ĵ�ַ�� AddressFamily.InterNetworkָ����IPv4
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
    /// ��ȡ��������IP
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
                        //�õ�IPv4�ĵ�ַ�� AddressFamily.InterNetworkָ����IPv4
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
        ///��ȡ���ص�IP��ַ
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
/// ��ȡ�ֻ�IP
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
            Debug.LogError("IP ��ȡʧ��");
        }
        return ipAddress;
    }


    /// <summary>
    /// ��ȡ����IP
    /// </summary>

    public static string GetPCIP()
    {
        try
        {
            string HostName = Dns.GetHostName(); //�õ�������
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                //��IP��ַ�б���ɸѡ��IPv4���͵�IP��ַ
                //AddressFamily.InterNetwork��ʾ��IPΪIPv4,
                //AddressFamily.InterNetworkV6��ʾ�˵�ַΪIPv6����
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


    //����tcpͨ������
    private void Connect()
    {
        try
        {
            int _port = Convert.ToInt32(inputPort);         //��ȡ�˿ں�
            string _ip = inputIp;                           //��ȡip��ַ

            Debug.Log(" ip ��ַ�� ��" + _ip);
            Debug.Log(" �˿ں��� ��" + _port);

            ifConnect = true;                         //����˼�����ť

            //info = "ip��ַ�� �� " + _ip + "�˿ں��� �� " + _port;

            //�����ʼ����ʱ �ڷ���˴���һ���������IP�Ͷ˿ںŵ�Socket
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);   //��������˿�

            socketWatch.Bind(point);                        //�󶨶˿ں�

            Debug.Log("�����ɹ�!");
            //info = "�����ɹ�";

            socketWatch.Listen(10);                         //���ü��������ͬʱ����10̨

            //���������߳�
            Thread thread = new Thread(Listen);
            thread.IsBackground = true;
            thread.Start(socketWatch);
        }
        catch { }
    }

    /// <summary>
    /// �ȴ��ͻ��˵����� ���Ҵ�����֮ͨ�ŵ�Socket
    /// </summary>
    void Listen(object o)
    {
        try
        {
            Socket socketWatch = o as Socket;
            while (true)
            {
                socketSend = socketWatch.Accept();           //�ȴ����տͻ�������

                Debug.Log(socketSend.RemoteEndPoint.ToString() + ":" + "���ӳɹ�!");
                //info = socketSend.RemoteEndPoint.ToString() + "  ���ӳɹ�!";

                Thread r_thread = new Thread(Received);      //����һ�����̣߳�ִ�н�����Ϣ����
                r_thread.IsBackground = true;
                r_thread.Start(socketSend);

                Thread s_thread = new Thread(SendMessage);   //����һ�����̣߳�ִ�з�����Ϣ����
                s_thread.IsBackground = true;
                s_thread.Start(socketSend);
            }
        }
        catch { }
    }

    /// <summary>
    /// �������˲�ͣ�Ľ��տͻ��˷�������Ϣ
    /// </summary>
   
    void Received(object o)
    {
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                Thread.Sleep(1);//���ӳ٣�����#######
                //byte[] messageBytes = new byte[1024 * 6];         //�ͻ������ӷ������ɹ��󣬷��������տͻ��˷��͵���Ϣ
                byte[] messageBytes = new byte[1024];
                int len = socketSend.Receive(messageBytes);       //ʵ�ʽ��յ�����Ч�ֽ���
                if (len == 0)
                {
                    break;
                }

                string str = Encoding.UTF8.GetString(messageBytes, 0, len);
                Debug.Log("���յ�����Ϣ��" + socketSend.RemoteEndPoint + ":" + str);
                recMes = str;
                getNew = true;

                recTimes++;
                //info = "���յ�һ�����ݣ����մ���Ϊ��" + recTimes;
                Debug.Log("�������ݴ�����" + recTimes);

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
                Thread.Sleep(1);//���ӳ٣�����#######
                byte[] buffer = new byte[1024];         //�ͻ������ӷ������ɹ��󣬷��������տͻ��˷��͵���Ϣ
                int len = socketSend.Receive(buffer);       //ʵ�ʽ��յ�����Ч�ֽ���
                if (len == 0)
                {
                    break;  //�����⣿
                }

                //ȡ��һ���ֽ���Ϊ��Ϣ�ֽ�
                byte messageByte = buffer[0];


                //������Ϣ���ͱ���
                string typeBits = Convert.ToString(messageByte, 2).PadLeft(8, '0').Substring(0, 1);
                //int messageType = int.Parse(typeBits);
                recType = typeBits;



                //����·�ڱ���
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


                //����ʱ�����
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


                Debug.Log("���յ���Ϣ����ϢΪ��");
                Debug.Log("����Ϊ��"+ typeBits + ",·��Ϊ��"+ crossingId +"��ʱ��Ϊ��"+time);

            
                getNew = true;

                recTimes++;
                //info = "���յ�һ�����ݣ����մ���Ϊ��" + recTimes;
                Debug.Log("�������ݴ�����" + recTimes);


            }
        }
        catch { }
    }*/


    /// <summary>
    /// �������˲�ͣ����ͻ��˷�����Ϣ
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

                    Debug.Log("���͵�����Ϊ :" + inputMessage);
                    Debug.Log("���͵������ֽڳ��� :" + sendByte.Length);

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
                socketWatch.Shutdown(SocketShutdown.Both);    //����Socket�ķ��ͺͽ��չ���
                socketWatch.Close();                          //�ر�Socket���Ӳ��ͷ����������Դ

                socketSend.Shutdown(SocketShutdown.Both);     //����Socket�ķ��ͺͽ��չ���
                socketSend.Close();                           //�ر�Socket���Ӳ��ͷ����������Դ           
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        Debug.Log("end OnDisable()");
    }

}

