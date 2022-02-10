using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleServer : MonoBehaviour
{
    private void Awake()
    {
        ServerInfoCsv.instance.ReadServerInfoCsv();
        NetworkManagerWorld.singleton.GetComponent<kcp2k.KcpTransport>().Port = (ushort)ServerInfoCsv.instance.port;
        
        Util.LogColorYellow("awake --> csv port : " + ServerInfoCsv.instance.port);
    }

    // Start is called before the first frame update
    void Start()
    {
        NetworkManagerWorld.singleton.StartServer();

    }

    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
