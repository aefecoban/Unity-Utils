using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInputManager
{
    [SerializeField] public KeyCode[] forwardKeys = new KeyCode[2] { KeyCode.W, KeyCode.UpArrow };
    [SerializeField] public KeyCode[] backwardKeys = new KeyCode[2] { KeyCode.S, KeyCode.DownArrow };
    [SerializeField] public KeyCode[] leftKeys = new KeyCode[2] { KeyCode.A, KeyCode.LeftArrow};
    [SerializeField] public KeyCode[] rightKeys = new KeyCode[2] { KeyCode.D, KeyCode.RightArrow };
    [SerializeField] public KeyCode[] runKeys = new KeyCode[2] { KeyCode.LeftShift, KeyCode.RightShift };
    [SerializeField] public KeyCode[] jumpKeys = new KeyCode[1] { KeyCode.Space };
    [SerializeField] public KeyCode[] crouchKeys = new KeyCode[1] { KeyCode.C };

    public Vector2 MovementVector = Vector2.zero;
    public bool IsRun = false;
    public bool IsCrouch = false;
    public bool IsJump = false;

    public List<Action> callbackForActionKeys = new List<Action>();

    Tuple<KeyCode[], Action>[] MovementKeys = null;

    public void Prepare()
    {
        MovementKeys = new Tuple<KeyCode[], Action>[7]
        {
            new Tuple<KeyCode[], Action>(forwardKeys, () => { MovementVector.x++; }),
            new Tuple<KeyCode[], Action>(backwardKeys, () => { MovementVector.x--; }),
            new Tuple<KeyCode[], Action>(leftKeys, () => { MovementVector.y++; }),
            new Tuple<KeyCode[], Action>(rightKeys, () => { MovementVector.y--; }),
            new Tuple<KeyCode[], Action>(runKeys, () => { IsRun = true; }),
            new Tuple<KeyCode[], Action>(crouchKeys, () => { IsCrouch = true; }),
            new Tuple<KeyCode[], Action>(jumpKeys, () => { IsJump = true; })
        };
    }

    private void ResetAll()
    {
         MovementVector = Vector2.zero;
         IsRun = false;
         IsCrouch = false;
         IsJump = false;
    }

    public void Update()
    {
        ResetAll();

        foreach (Tuple<KeyCode[], Action> T in MovementKeys)
            foreach (var key in T.Item1)
                if (Input.GetKey(key))
                {
                    T.Item2();
                    break;
                }

        MovementVector = MovementVector.normalized;
    }

}
