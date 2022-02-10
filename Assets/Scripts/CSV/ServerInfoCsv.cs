using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ServerStartInfo
{
    public int port;
}

public class ServerInfoCsv : MonoBehaviour
{
    static public ServerInfoCsv instance;

    public int port;

    public List<Dictionary<string, object>> _server_start_info;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //ReadServerInfoCsv();
    }

    public void ReadServerInfoCsv()
    {
        CsvReader.instance.ReadEx("server_info", out _server_start_info);

        for (int i = 0; i < _server_start_info.Count; i++)
        {
            ServerStartInfo info = new ServerStartInfo();

            info.port = (int)_server_start_info[i]["port"];
            //Util.LogColorYellow("MirrorServer Port : " + info.port);
            port = info.port;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*CsvReader.instance.ReadEx("server_info", out _server_start_info);

        for (int i = 0; i < _server_start_info.Count; i++)
        {
            ServerStartInfo info = new ServerStartInfo();

            info.port = (int)_server_start_info[i]["port"];
            //Util.LogColorYellow("MirrorServer Port : " + info.port);
            port = info.port;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
