using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem2D : MonoBehaviour
{

    public static InputSystem2D instance;

    [Header("Movement")]
    public bool updateYourself = false;
    public KeyCode[] UpKeys = new KeyCode[] { KeyCode.W, KeyCode.UpArrow };
    public KeyCode[] DownKeys = new KeyCode[] { KeyCode.S, KeyCode.DownArrow };
    public KeyCode[] LeftKeys = new KeyCode[] { KeyCode.A, KeyCode.LeftArrow };
    public KeyCode[] RightKeys = new KeyCode[] { KeyCode.D, KeyCode.RightArrow };
    public KeyCode[] JumpKeys = new KeyCode[] { KeyCode.Space };

    [Space][Space]
    [Header("UI")]
    public KeyCode[] MainMenuKeys = new KeyCode[] { KeyCode.Escape };
    public List<Action> UIFuncs = new List<Action>() { };
    public List<Action> UIFuncsDown = new List<Action>() { };
    public List<Action> UIFuncsUp = new List<Action>() { };
    
    public Vector2 MovementVector = Vector2.zero;
    public KeyCode pressedKey = KeyCode.Escape;

    private void Awake()
    {
        if(InputSystem2D.instance != null)
        {
            Destroy(this);
        }
        else
        {
            InputSystem2D.instance = this;
        }
    }

    public void Movement()
    {
        MovementVector = Vector2.zero;

        #region loopsForMovement
        for (int i = 0; i < UpKeys.Length; i++)
        {
            if (Input.GetKey(UpKeys[i]))
            {
                MovementVector.y += 1f;
                break;
            }
        }
        for (int i = 0; i < DownKeys.Length; i++)
        {
            if (Input.GetKey(DownKeys[i]))
            {
                MovementVector.y -= 1f;
                break;
            }
        }
        for (int i = 0; i < LeftKeys.Length; i++)
        {
            if (Input.GetKey(LeftKeys[i]))
            {
                MovementVector.x -= 1f;
                break;
            }
        }
        for (int i = 0; i < RightKeys.Length; i++)
        {
            if (Input.GetKey(RightKeys[i]))
            {
                MovementVector.x += 1f;
                break;
            }
        }
        #endregion

        Vector2 x = new Vector2(MovementVector.x, 0).normalized;
        Vector2 y = new Vector2(0, MovementVector.y).normalized;
        MovementVector = new Vector2(x.x, y.y);

    }

    void Update()
    {
        #region Movement
        if (updateYourself)
        {
            Movement();
        }
        #endregion

        #region UI
        if (UIFuncs.Count > 0 || UIFuncsDown.Count > 0 || UIFuncsUp.Count > 0)
        {
            for(int i = 0; i < MainMenuKeys.Length; i++)
            {
                pressedKey = MainMenuKeys[i];
                bool breakMe = false;
                if (Input.GetKey(MainMenuKeys[i]))
                {
                    breakMe = true;
                    for (int j = 0; j < UIFuncs.Count; j++)
                    {
                        UIFuncs[i]();
                    }

                }
                if (Input.GetKeyDown(MainMenuKeys[i]))
                {
                    breakMe = true;
                    for (int j = 0; j < UIFuncsDown.Count; j++)
                    {
                        UIFuncsDown[i]();
                    }
                }
                if (Input.GetKeyUp(MainMenuKeys[i]))
                {
                    breakMe = true;
                    for (int j = 0; j < UIFuncsUp.Count; j++)
                    {
                        UIFuncsUp[i]();
                    }
                }
            }
        }
        #endregion
    }
}
