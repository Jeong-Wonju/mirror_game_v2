using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class voice_module : NetworkBehaviour
{
    private JoystickSetterExample joystic;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
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

            GetComponent<test_player>().CmdCharMove(Dir, net_pos);

        }
    }
}
