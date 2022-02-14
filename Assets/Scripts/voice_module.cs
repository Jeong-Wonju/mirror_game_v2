using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class voice_module : NetworkBehaviour
{
    private JoystickSetterExample joystic;

    public float speed = 5;

    Vector2 m_netDir;
    Vector3 m_netTruePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Command]
    public void CmdCharMove(Vector2 Dir, Vector3 TruePos)
    {
        //Debug.Log("CmdCharMove : (NetID)" + netIdentity.netId);

        m_netDir = Dir;
        m_netTruePos = TruePos;

        transform.position = m_netTruePos;


        //Debug.Log("CmdCharMove Dir :" + m_netDir);
        //Debug.Log("CmdCharMove true_pos :" + m_netTruePos);


        SetDirtyBit(1);
    }

    [Server]
    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        //Debug.Log("[server] OnSerialize : " + name);

        writer.WriteVector2((Vector2)m_netDir);
        writer.WriteVector3((Vector2)m_netTruePos);


        //Debug.Log("OnSerialize Dir :" + m_netDir);
        //Debug.Log("OnSerialize true_pos :" + m_netTruePos);


        return true;
    }

    [Client]
    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        //if (!isLocalPlayer)
        {
            //Debug.Log("[client] OnDeserialize : " + netId);

            m_netDir = reader.ReadVector2();
            m_netTruePos = reader.ReadVector2();


            //Debug.Log("OnDeserialize Dir :" + m_netDir);
            //Debug.Log("OnDeserialize true_pos :" + m_netTruePos);
            //Debug.Log("OnDeserialize sync :" + m_bSync);


        }
    }

    [Client]
    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log("[Client] new create netID : " + netId);

        joystic = GameObjectManager.instance.my_joystick;
    }

    void Update()
    {
        if (isServer) return;

        if (isLocalPlayer)
        {
            if (joystic == null) return;

            Vector2 Dir;
            Dir.x = joystic.variableJoystick.Direction.x;
            Dir.y = joystic.variableJoystick.Direction.y;
            //Dir.Normalize();
            if (Dir.x != 0 || Dir.y != 0)
            {
                //Debug.DrawLine(transform.position, transform.position + (Vector3)Dir, Color.green, 0, false);
                Vector3 pos = transform.position;
                pos.x = pos.x + Dir.x * Time.deltaTime * speed;
                pos.z = pos.z + Dir.y * Time.deltaTime * speed;

                transform.position = pos;

            }

            Vector3 net_pos = transform.position;

            CmdCharMove(Dir, net_pos);

        }
        else
        {
            Vector3 pos = transform.position;
            

            Vector3 Dir = m_netTruePos - pos;
            //Dir.Normalize();

            pos.x = pos.x + Dir.x * Time.deltaTime * speed;
            pos.z = pos.z + Dir.z * Time.deltaTime * speed;

            float short_dis = Vector3.Distance(pos, m_netTruePos);
            //Debug.Log("short_dis : " + short_dis);
            if (short_dis >= 0.038f)
                transform.position = pos;
            else
                transform.position = m_netTruePos;
        }
    }
}
