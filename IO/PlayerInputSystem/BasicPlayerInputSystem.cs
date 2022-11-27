using UnityEngine;

namespace TwinSoulS
{
    public class PlayerInputSystem : MonoBehaviour
    {
        [SerializeField] public KeyCode[] upKeys = new KeyCode[2] { KeyCode.W, KeyCode.UpArrow };
        [SerializeField] public KeyCode[] downKeys = new KeyCode[2] { KeyCode.S, KeyCode.DownArrow };
        [SerializeField] public KeyCode[] leftKeys = new KeyCode[2] { KeyCode.A, KeyCode.LeftArrow };
        [SerializeField] public KeyCode[] rightKeys = new KeyCode[2] { KeyCode.D, KeyCode.RightArrow };
        [SerializeField] public KeyCode[] jumpKeys = new KeyCode[1] { KeyCode.Space };
        [SerializeField] public KeyCode[] interactiveKeys = new KeyCode[1] { KeyCode.E };
        [SerializeField] public KeyCode[] pauseKeys = new KeyCode[1] { KeyCode.Escape };
        [SerializeField] public KeyCode[] attackKeys = new KeyCode[1] { KeyCode.Mouse0 };
        [SerializeField] public KeyCode[] strongAttackKeys = new KeyCode[1] { KeyCode.Mouse1 };
        [SerializeField] public KeyCode[] crouchKeys = new KeyCode[1] { KeyCode.C };
        [SerializeField] public KeyCode[] runKeys = new KeyCode[2] { KeyCode.LeftShift, KeyCode.RightShift };

        public Vector3 movementVector = Vector3.zero;
        public bool jumpNow = false;
        public bool run = false;
        public bool pause = false;
        public bool attack = false;
        public bool sAttack = false;
        public bool crouch = false;
        public bool interactive = false;

        private void zero()
        {
            movementVector = Vector3.zero;
            jumpNow = false;
            run = false;
            pause = false;
            attack = false;
            sAttack = false;
            crouch = false;
            interactive = false;
        }

        public void Update()
        {
            zero();

            foreach (var key in upKeys)
            {
                if (Input.GetKey(key))
                {
                    movementVector.y += 1;
                    break;
                }
            }
            foreach (var key in downKeys)
            {
                if (Input.GetKey(key))
                {
                    movementVector.y -= 1;
                    break;
                }
            }
            foreach (var key in rightKeys)
            {
                if (Input.GetKey(key))
                {
                    movementVector.x += 1;
                    break;
                }
            }
            foreach (var key in leftKeys)
            {
                if (Input.GetKey(key))
                {
                    movementVector.x -= 1;
                    break;
                }
            }
            foreach (var key in jumpKeys)
            {
                if (Input.GetKey(key))
                {
                    jumpNow = true;
                    break;
                }
            }
            foreach (var key in runKeys)
            {
                if (Input.GetKey(key))
                {
                    run = true;
                    break;
                }
            }
            foreach (var key in crouchKeys)
            {
                if (Input.GetKey(key))
                {
                    crouch = true;
                    break;
                }
            }
            foreach (var key in attackKeys)
            {
                if (Input.GetKey(key))
                {
                    attack = true;
                    break;
                }
            }
            foreach (var key in strongAttackKeys)
            {
                if (Input.GetKey(key))
                {
                    sAttack = true;
                    break;
                }
            }
            foreach (var key in interactiveKeys)
            {
                if (Input.GetKey(key))
                {
                    interactive = true;
                    break;
                }
            }
            foreach (var key in pauseKeys)
            {
                if (Input.GetKey(key))
                {
                    pause = true;
                    break;
                }
            }

            movementVector = movementVector.normalized;
        }

    }
}
