using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections;
using NetworkDefine;
using System.Timers;

//using PacketParser;

// 네트웍크 관리
public class NetworkManager_Talk : MonoSingletonFactory<NetworkManager_Talk>
{
    //-------------------------------------------------------------------------
    // 디파인할것들



    //-------------------------------------------------------------------------

    private TcpClient m_tcpClient;
    private NetworkStream m_netStream;
    private Thread m_networkThread = null;
    //private String	 m_ipAddressString;	// 
    //private int		 m_port;
    //private int		 m_uid;

    private bool m_bIsStop = true;
    //private bool m_bHeaderMode  = true;		// 현재 읽어들이는 모드는 무엇인가.

    byte[] m_recvBuffer = new byte[(int)NET_DEFINE.MAX_PACKET_LENGTH];  // 받은 데이터버퍼
    public byte[] m_recvHeader = new byte[(int)NET_DEFINE.MAX_HEADER_LENGTH];   // 받은 헤더버퍼

    public byte[] m_sendHeder = new byte[(int)NET_DEFINE.MAX_HEADER_LENGTH];
    public byte[] m_sendBuffer = null;                              // 보내는 바이트 배열 버퍼

    //private int m_currentOffset = 0;	// 현재 오프셋 위치
    //private int m_readSize = (int)NET_DEFINE.MAX_HEADER_LENGTH;	// 읽어들이는 사이즈

    private Queue<byte[]> m_packetQueue = new Queue<byte[]>();
    public GameObject m_connectNetwork = null;

    private int m_serverType = 0;
    private float m_currentTick = 0;
    //private int m_pingCount = 1;
    public bool m_bLogIn = false;


    public float m_pingSpeed = 0;
    public float m_sendPingSpeed = 0;
    public bool m_bCheckPing = false;

    public bool m_bStart = false;
    public float m_startTick = 0;

    private int ReceiveCount = 0;
    private int PacketError = 0;
    private uint cmd = 0;

    public ServerTest m_ServerTest = null;


    // for Debug
    private int enqueueCount;

    private int g_packetSize = 0;
    private int g_headerSize = 0;
    private int g_mainBufferLen = 0;
    private int g_readDataProcInCount = 0;
    private int g_readDataProcDoneCount = 0;
    private int g_netErrorContinue = 0;

    public int GetPacketSize() { return g_packetSize; }
    public int GetHeaderSize() { return g_headerSize; }
    public int GetMainBufferLen() { return g_mainBufferLen; }
    public int GetReadDataProcInCount() { return g_readDataProcInCount; }
    public int GetReadDataProcDoneCount() { return g_readDataProcDoneCount; }
    public int GetNetErrorContinue() { return g_netErrorContinue; }

    public void InitPacketSize() { g_packetSize = 0; }
    public void InitHeaderSize() { g_headerSize = 0; }
    public void InitMainBufferLen() { g_mainBufferLen = 0; }


    //-------------------------------------------------------------------------
    // 생성자
    //-------------------------------------------------------------------------
    public NetworkManager_Talk()
    {
        m_tcpClient = null;
        m_netStream = null;
        m_networkThread = null;
        //m_ipAddressString = "";
        //m_port = -1;
        //m_uid = -1;

        m_bIsStop = true;
        //m_bHeaderMode	= true;		// 현재 읽어들이는 모드는 무엇인가.

        m_recvBuffer = new byte[(int)NET_DEFINE.MAX_PACKET_LENGTH]; // 받은 데이터버퍼
        m_recvHeader = new byte[(int)NET_DEFINE.MAX_HEADER_LENGTH]; // 받은 헤더버퍼
        m_sendHeder = new byte[(int)NET_DEFINE.MAX_HEADER_LENGTH];

        //m_currentOffset = 0;	// 현재 오프셋 위치
        //m_readSize = (int)NET_DEFINE.MAX_HEADER_LENGTH;	// 읽어들이는 사이즈

        m_packetQueue = new Queue<byte[]>();
        //m_pingCount = 1;

    }

    //-------------------------------------------------------------------------
    // 소멸자
    //-------------------------------------------------------------------------
    ~NetworkManager_Talk()
    {
        shutdown();
        m_tcpClient = null;
        m_netStream = null;
        m_networkThread = null;
        m_recvBuffer = null;    // 받은 데이터버퍼
        m_recvHeader = null;    // 받은 헤더버퍼

        m_packetQueue = null;
        //m_connectNetwork = null;

    }

    protected override void Awake()
    {
        m_connectNetwork = GameObject.Find("_Network");
    }

    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    public int GetReceivePacketCount()
    {
        return ReceiveCount;
    }

    public int GetEnqueCount()
    {
        return enqueueCount;
    }

    public int GetPacketError()
    {
        return PacketError;
    }

    public uint GetCmd()
    {
        return cmd;
    }
    //-------------------------------------------------------------------------
    // NAME : updateNetwork()
    // DESC : 업데이트 네트웍
    //-------------------------------------------------------------------------	
    public void updateNetwork()
    {
        if (m_tcpClient == null)
        {
            return;
        }
        if (m_tcpClient.Connected && !m_bIsStop)
        {
            while (true)
            {
                int rt;

                // Header Read. --- -----------------------------------------
                rt = 0;
                //m_currentOffset = 0;	

                rt = m_netStream.Read(m_recvHeader, 0, 4);

                if (rt != 4)
                {
                    g_netErrorContinue += 1;
                    //Debug.Log("[packet error] rt != 4");
                    continue;
                }

                ReceiveCount++;

                int packetSize;

                packetSize = System.BitConverter.ToInt16(m_recvHeader, 0);



                //cmd 		= reverseByte2ToInt (m_recvHeader, 2);
                // Header Read Done. ----------------------------------------

                if (packetSize < 4 || packetSize > (int)NET_DEFINE.MAX_PACKET_LENGTH)
                {
                    g_netErrorContinue += 100;
                    //Debug.Log("[packet error] packetSize < 4 || packetSize > (int)NET_DEFINE.MAX_PACKET_LENGTH");
                    continue;
                }
                if (g_packetSize < packetSize)
                    g_packetSize = packetSize;

                // Data Read. ------------------------------------------------ 
                if (packetSize > 4)
                {
                    int readSize = 0;

                    g_readDataProcInCount++;

                    int while_count = 0;

                    while (true)
                    {
                        if (m_netStream.CanRead)
                        {
                            readSize += m_netStream.Read(m_recvBuffer, readSize, packetSize - 4 - readSize);
                            while_count++;
                            //Debug.Log("while_count " + while_count);
                        }

                        if (readSize >= packetSize - 4)
                            break;
                    }
                    //Debug.Log("recvbuff 0 " + m_recvBuffer[0]);
                    //Debug.Log("recvbuff 1 " + m_recvBuffer[1]);
                    //Debug.Log("recvbuff 2 " + m_recvBuffer[2]);
                    //Debug.Log("recvbuff 3 " + m_recvBuffer[3]);

                    //Debug.Log("packet read size : " + readSize);

                    g_readDataProcDoneCount++;
                }
                else
                {

                }

                byte[] mainBuffer;
                mainBuffer = new byte[packetSize];

                if (g_packetSize < packetSize)
                    g_packetSize = packetSize;

                if (mainBuffer.Length <= 0)
                {
                    g_netErrorContinue += 10000;
                    //Debug.Log("[packet error] mainBuffer.Length <= 0");
                    continue;
                }

                //Debug.Log("main buffer length : " + mainBuffer.Length);

                // Copy to mainBuffer
                Buffer.BlockCopy(m_recvHeader, 0, mainBuffer, 0, 4);
                Buffer.BlockCopy(m_recvBuffer, 0, mainBuffer, 4, packetSize - 4);

                //Debug.Log("packet enque packetSize : " + packetSize);

                bool ret;

                /*Debug.Log(mainBuffer[0]);
                Debug.Log(mainBuffer[1]);
                Debug.Log(mainBuffer[2]);
                Debug.Log(mainBuffer[3]);
                if(mainBuffer.Length > 4)
                Debug.Log(mainBuffer[4]);
                if (mainBuffer.Length > 5)
                    Debug.Log(mainBuffer[5]);
                if (mainBuffer.Length > 6)
                    Debug.Log(mainBuffer[6]);
                if (mainBuffer.Length > 7)
                    Debug.Log(mainBuffer[7]);
                if (mainBuffer.Length > 8)
                    Debug.Log(mainBuffer[8]);
                if (mainBuffer.Length > 9)
                    Debug.Log(mainBuffer[9]);*/

                ret = enqueue(mainBuffer);  // Insert to dataQueue
                if (ret == true)
                    enqueueCount++;
            }
        }
        else if (!m_tcpClient.Connected)
        {





            //shutdown();
        }



    }


    //-------------------------------------------------------------------------
    // NAME : enqueue(byte[] buffer)
    // DESC : 큐에 내용을 담는다.
    //-------------------------------------------------------------------------	
    public bool enqueue(byte[] buffer)
    {
        if (buffer.Length > 0)
        {
            m_packetQueue.Enqueue(buffer);

            m_packetQueue.Count();

            return true;
        }
        return false;
    }


    //-------------------------------------------------------------------------
    // NAME : bool deQueueOnce(out byte[] buffer)
    // DESC : 큐에 있는 내용을 꺼내온다.
    //-------------------------------------------------------------------------	
    public bool deQueueOnce(out byte[] buffer)
    {

        if (m_packetQueue.Count > 0)
        {
            int length = ((byte[])m_packetQueue.Peek()).Length;
            buffer = new byte[length];
            buffer = (byte[])m_packetQueue.Dequeue();

            Debug.Log("deQueue packet length" + length);

            return true;
        }



        buffer = new byte[10240];
        return false;
    }

    public void iCmdToStr(_PTCODE code)
    {
        Debug.Log("(S -->)" + code);
    }

    public void PacketSend(_PTCODE code, byte[] buffer)
    {
        iCmdToStr(code);

        int DataByteSize = buffer.GetLength(0);

        // send
        byte[] header;
        header = new byte[4];

        NetworkManager_Talk.reverseIntToByte2(header, 4 + DataByteSize, 0); // 총 사이즈 기록
        NetworkManager_Talk.reverseIntToByte2(header, (int)code, 2);         // 명령어

        //header[4] = 0;                                         // 패킷의 종류
        //header[5] = 0xfe;                                      // 패킷 체크   

        byte[] data;
        data = new byte[DataByteSize];

        //m_NetworkManager_Talk.reverseIntToByte4(data, 4 + DataByteSize,0);    // data 사이즈 기록 ( 자신포함 )

        System.Buffer.BlockCopy(buffer, 0, data, 0, DataByteSize);     // date 내용 기록

        sendPacket(header, 4, data, DataByteSize);
    }

    //-------------------------------------------------------------------------
    // NAME : sendPacket(byte[] sendheader, int length, byte[] senddata, int sendlength)
    // DESC : 서버로 데이터 보낸다
    //-------------------------------------------------------------------------	
    public bool sendPacket(byte[] sendheader, int length, byte[] senddata, int sendlength)
    {
        // m_tcpClient가 살아있는지 확인한다.
        if (null == m_tcpClient)
        {
            // 디스커넥트 시켜준다.
            //m_bLogIn = false;
            //m_connectNetwork.SendMessage("ShowDisconnectServerPopup");
            //m_connectNetwork.SendMessage("DestroyNetwork", m_serverType);

            return false;
        }
        // 커넥트 되어있는지.
        if (!m_tcpClient.Connected)
        {
            // 디스커넥트 시켜준다.
            //m_bLogIn = false;
            //m_connectNetwork.SendMessage("ShowDisconnectServerPopup");
            //m_connectNetwork.SendMessage("DestroyNetwork", m_serverType);

            return false;
        }
        m_sendBuffer = new byte[length + sendlength];
        // 해당 데이터 카피
        Buffer.BlockCopy(sendheader, 0, m_sendBuffer, 0, length);
        // 데이터에 값이 있으면 데이터도 카피해준다.
        if (sendlength > 0)
        {
            Buffer.BlockCopy(senddata, 0, m_sendBuffer, length, sendlength);
        }
        length += sendlength;
        // 패킷을 보낸다.
        try
        {
            //reverseIntToByte4(m_sendBuffer,length,2);

            m_netStream.BeginWrite(m_sendBuffer, 0, m_sendBuffer.Length, new AsyncCallback(sendComplete), null);
            m_netStream.Flush();
            return true;
        }
        catch (System.Exception ex)
        {
            string exString = ex.Message;


            return false;
        }
    }


    //-------------------------------------------------------------------------
    // NAME : sendComplete(IAsyncResult ar)
    // DESC : 송신 완료
    //-------------------------------------------------------------------------	
    public void sendComplete(IAsyncResult ar)
    {
        if (null == m_tcpClient)
        {
            return;
        }
        m_netStream.EndWrite(ar);

    }


    //-------------------------------------------------------------------------
    // NAME : shutdown()
    // DESC : 접속 종료
    //-------------------------------------------------------------------------	
    public void shutdown()
    {
        if (m_tcpClient == null)
        {

            return;
        }
        try
        {
            m_bIsStop = true;
            m_netStream.Close();
            m_tcpClient.Close();
            m_networkThread.Abort();

        }
        catch (System.Exception ex)
        {
            string exString = ex.Message;
            ;
        }
        finally
        {
            m_netStream = null;
            m_tcpClient = null;
        }
    }

    void OnDestroy()
    {
        shutdown();

    }

    //-------------------------------------------------------------------------
    // 데이터 관련 정보를 바이트 배열로 
    //-------------------------------------------------------------------------
    static public void reverseIntToByte4(byte[] data, int value, int offset)
    {
        data[offset++] = (byte)(value & 0xFF);
        data[offset++] = (byte)((value & 0xFF00) >> 8);
        data[offset++] = (byte)((value & 0xFF0000) >> 16);
        data[offset] = (byte)((value & 0xFF000000) >> 24);
    }


    //-------------------------------------------------------------------------
    static public void reverseIntToByte2(byte[] data, int value, int offset)
    {
        data[offset++] = (byte)(value & 0xFF);
        data[offset] = (byte)((value & 0xFF00) >> 8);
    }





    ////-------------------------------------------------------------------------
    //// NAME : EnterServer(string path, string uidString)
    //// DESC : 서버입장.
    ////-------------------------------------------------------------------------	
    //public void EnterServer(string ipPath, string uidString)
    //{
    //    ipPath = Application.dataPath + "/Resources/IpInfo.txt";
    //    readIpPort(ipPath);	// 아이피 정보 알아온다.
    //    Invoke("startNetwork", 1);
    //}



    //-------------------------------------------------------------------------
    // NAME : UpdateIpInfo(string ipPath, int port, int uid)
    // DESC : 
    //-------------------------------------------------------------------------	
    public void UpdateIpInfo(string ipPath, int port, int uid)
    {
        //m_ipAddressString = ipPath;	// 
        //m_port			  = port;
        //m_uid			  = uid;
    }


    //-------------------------------------------------------------------------
    // NAME : UpdateIpInfo(string ipPath, int port, int uid)
    // DESC : 서버에 커넥션 시킨다.
    //-------------------------------------------------------------------------
    public void ConnectionServer(string ipString, int port, int serverType)
    {
        try
        {
            m_tcpClient = new TcpClient(ipString, port);
            int i = 0;
            if (m_tcpClient.Connected)
            {
                m_netStream = m_tcpClient.GetStream();
                m_bIsStop = false;
                m_serverType = serverType;

                m_networkThread = new Thread(new ThreadStart(updateNetwork));
                m_networkThread.IsBackground = true;
                m_networkThread.Start();

                if (m_serverType == 0)
                {
                    //m_connectNetwork.SendMessage("LogInMatchServer");
                }
                else
                {
                    m_connectNetwork.SendMessage("LogInGameServer");
                }


            }
            else
            {
                m_ServerTest.m_Text.text = "m_tcpClient.Connected : " + m_tcpClient.Connected.ToString();
            }
        }
        catch (Exception ex)
        {
            string exString = ex.Message;
            //DebugString debugstring = (DebugString)GameObject.Find("SceneManager").GetComponent("DebugString");
            m_ServerTest.m_Text.text = exString;
            Debug.Log(exString);
        }
    }


    //-------------------------------------------------------------------------
    // NAME : Update()
    // DESC : Update is called once per frame
    //-------------------------------------------------------------------------	
    void Update()
    {
        //byte[] buffer;

        PacketParse();


        //if (deQueueOnce(out buffer))
        //{
        //	m_connectNetwork.SendMessage("UserMessage : ", buffer);//, 1);


        //}
        if (m_tcpClient != null)
        {
            if (m_tcpClient.Connected == false)
            {
                //Debug.LogError("서버랑 접속이 끊어졌다");
                //SharedObject.g_ServerDisconnect = true;
                // Application.LoadLevel("Login");
            }

            //SceneManager SceneMgr = GameObject.Find("SceneManager").GetComponent<SceneManager>();

            if (m_tcpClient.Connected)
            {
                //SceneMgr.Connect = true;
            }
            else
            {
                //SceneMgr.Connect = false;
            }
        }

        if (m_bLogIn)
        {
            float deltaTime = Time.deltaTime;
            // 시간이 변함에따라 결과틱을 변화 시켜준다. ========================
            if (m_currentTick > (int)NET_DEFINE.REFRESH_TICK && !m_bCheckPing)
            {
                m_currentTick = 0;
                //RequestPing(0, m_pingCount, 0);
                m_bCheckPing = true;
            }
            else
            {
                m_currentTick += deltaTime * 1000;
            }
            // 핑체크
            if (m_bCheckPing)
            {
                m_pingSpeed += deltaTime * 1000;
            }
        }

        // 시작해주는 시간을 체크한다.
        if (m_bStart)
        {
            float deltaTime = Time.deltaTime;

            if (m_startTick > (int)NET_DEFINE.START_TICK)
            {
                m_startTick = 0;
                m_bStart = false;
                // 여기로 들어오면 바로 씬이동을 시켜준다.

            }
            else
            {
                m_startTick += deltaTime * 1000;
            }
        }


    }

    public void PacketParse()
    {

        byte[] buffer;

        if (deQueueOnce(out buffer) == true)
        {

            uint cmd = (uint)System.BitConverter.ToInt16(buffer, 2);

            GetComponent<PacketReceiver>().PacketParse((_PTCODE)cmd, buffer);
        }
    }
}