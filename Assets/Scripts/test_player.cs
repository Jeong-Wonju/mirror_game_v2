using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class test_player : NetworkBehaviour
{
    //Vector2 m_pos;

    private JoystickSetterExample joystic;

    public float speed = 5;

    Vector2 m_netDir;
    Vector2 m_netTruePos;
    
    NavMeshAgent2D agent;

    public Animator animator;
    
    [Command]
    public void CmdCharMove(Vector2 Dir, Vector2 TruePos) 
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
        writer.WriteVector2((Vector2)m_netTruePos);
        

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

    [Server]
    public override void OnStartServer()
    {
        transform.GetChild(0).gameObject.SetActive(false);


        Debug.Log("[Server]OnStartServer netID" + netId);
    }

    /*[ClientRpc]
    public void RpcWarp(Vector2 position)
    {
        agent.Warp(position);
    }*/

    [Client]
    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log("[Client] new create netID : " + netId);

        if(GameObjectManager.instance)
        {
            if (GameObjectManager.instance.my_player)
                Debug.Log("[Client ] my netID : (NetID)" + GameObjectManager.instance.my_player.netId);
        }

        //Debug.Log("my netID : (NetID)" + GameObjectManager.instance.my_player.netId);

        //Debug.Log("player create : (NetID)" + netIdentity.netId);
        if (GameObjectManager.instance.my_player == null)
        {
            if (isLocalPlayer)
            {
                GameObjectManager.instance.my_player = this;
                joystic = GameObjectManager.instance.my_joystick;
                Debug.Log("[Cleint] my player setting ok!!!(netId)" + netId);
            }
        }

        if (isLocalPlayer)
        {
            Camera.main.transform.SetParent(transform);

            gameObject.AddComponent<NavMeshAgent2D>();
            agent = GetComponent<NavMeshAgent2D>();
            agent.Warp(new Vector2(0, 0));
        }
        else
        {

            //GetComponent<NavMeshAgent2D>().enabled = false;
        }

        
    }

    Vector2 prev_Dir;
    bool bStop = false;

    void SetCharacterDir(Vector2 Dir)
    {
        if (Dir.x != 0 || Dir.y != 0)
        {
            animator.SetFloat("LookX", Dir.x);
            animator.SetFloat("LookY", Dir.y);
            bStop = false;
            prev_Dir = Dir;
        }
        else
        {
            //if (bStop == false)
            {
                animator.SetFloat("LookX", prev_Dir.x);
                animator.SetFloat("LookY", prev_Dir.y);

                bStop = true;
            }
        }
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
                Debug.DrawLine(transform.position, transform.position + (Vector3)Dir, Color.green, 0, false);

                agent.velocity = Dir * speed;
            }
            
            Vector2 true_pos = agent.truePosition;

            GetComponent<test_player>().CmdCharMove(Dir, true_pos);

            SetCharacterDir(Dir);

            
            /*else
            {
                // ��ġ �̵� ���� ���� ���� ó�� ������ �߰� �߰� ��ġ�� ������ �ʿ䰡 �־ ����

                if (IsMove)
                {
                    //Sync �϶� �ѹ��� ������
                    Vector2 true_pos = agent.truePosition;
                    GetComponent<test_player>().CmdCharMove(new Vector2(0, 0), true_pos, true);
                    
                    IsMove = false;
                }
            }*/

            
            
        }
        else
        {
            Vector3 pos = transform.position;
            Vector2 pos_vec2;
            pos_vec2.x = pos.x;
            pos_vec2.y = pos.y;

            Vector2 Dir = m_netTruePos - pos_vec2;
            //Dir.Normalize();

            pos.x = pos.x + Dir.x * Time.deltaTime* speed;
            pos.y = pos.y + Dir.y * Time.deltaTime * speed;

            float short_dis = Vector2.Distance(pos_vec2, m_netTruePos);
            //Debug.Log("short_dis : " + short_dis);
            if (short_dis >= 0.038f)
                transform.position = pos;
            else
                transform.position = m_netTruePos;


            SetCharacterDir(Dir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
    }

}
