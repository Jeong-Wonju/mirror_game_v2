using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;

public partial struct CharacterSelectMsg : NetworkMessage
{
    public int index;
}

public enum NetworkState { Offline, Handshake, Lobby, World }

public class NetworkManagerWorld : NetworkManager
{
    public NetworkState state = NetworkState.Offline;

    public Dictionary<NetworkConnection, string> users = new Dictionary<NetworkConnection, string>();

    

    [Serializable]
    public class ServerInfo
    {
        public string name;
        public string ip;
    }
    public List<ServerInfo> serverList = new List<ServerInfo>()
    {
        new ServerInfo{ name = "Local", ip = "localhost"}
    };

    //[Header("Events")]
    //public UnityEvent onStartServer;
    //public UnityEvent onStopServer;

    //public UnityEventNetworkConnection onServerConnect;
    //public UnityEventNetworkConnection onServerDisconnect;

    public override void Awake()
    {
        base.Awake();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("OnStartServer");
        //onStartServer.Invoke();

        //Database.singleton.Connect();


        NetworkServer.RegisterHandler<CharacterSelectMsg>(OnServerCharacterSelect);

    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        Debug.Log("OnStopServer");
        //onStopServer.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        //string account = lobby[conn];

        //conn.identity.GetInstanceID();
        Debug.Log("OnServerConnect " + conn.connectionId);
        //Debug.Log("OnServerConnect " + conn.identity.netId);
        //onServerConnect.Invoke(conn);
        //conn->Send

        Debug.Log("conn.isAuthenticated : " + conn.isAuthenticated);
        Debug.Log("conn.isReady : " + conn.isReady);
        //conn.isReady = true;
        //

        /*if(users.ContainsKey(conn) == false)
        {
            users.Add(conn, ""+conn.connectionId);
            test_player t_player = Resources.Load<test_player>("Prefabs/player");
            
            //t_player.Invoke()
        }*/

        //onServerConnect.Invoke(conn);
        
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        Debug.Log("OnServerReady ");
    }

    void OnServerCharacterSelect(NetworkConnection conn, CharacterSelectMsg message)
    {
        Util.LogColorYellow("OnServerCharacterSelect msg idx : " + message.index);


        // load character data
        voice_module prefab = spawnPrefabs[2].GetComponent<voice_module>();
        if (prefab != null)
        {
            GameObject go = Instantiate(prefab.gameObject);

            // add to client
            NetworkServer.AddPlayerForConnection(conn, go);
        }
    }


    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        Debug.Log("OnServerDisconnect");
        
        
        //onServerDisconnect.Invoke(conn);
    }

}
