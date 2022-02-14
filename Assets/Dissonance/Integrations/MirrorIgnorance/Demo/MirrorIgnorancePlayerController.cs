using Mirror;
using UnityEngine;

namespace Dissonance.Integrations.MirrorIgnorance.Demo
{
    public class MirrorIgnorancePlayerController : NetworkBehaviour
    {
        private JoystickSetterExample joystic;

        private void Start()
        {
            joystic = GameObjectManager.instance.my_joystick;
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            //var controller = GetComponent<CharacterController>();
            
            var rotation = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var speed = 5;
            
            transform.Rotate(0, rotation, 0);
            
            if(joystic.variableJoystick.Direction.x != 0 || joystic.variableJoystick.Direction.y != 0)
            {
                Debug.Log("joystick pos : " + joystic.variableJoystick.Direction);
            }

            Vector3 forward = new Vector3(joystic.variableJoystick.Direction.x, 0, joystic.variableJoystick.Direction.y);

            if (joystic.variableJoystick.Direction.x != 0 || joystic.variableJoystick.Direction.y != 0)
            {
                Debug.Log("forward  : " + forward);
                Debug.Log("speed  : " + speed);
            }

            Vector3 pos = transform.position;

            pos = pos + forward * speed;

            transform.position = pos;

            //controller.SimpleMove(forward * speed);

            /*if (transform.position.y < -3)
            {
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }*/
        }
    }
}