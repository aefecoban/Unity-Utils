using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        InputSystem2D.instance.UIFuncsDown.Add(() => Debug.Log("Down = " + InputSystem2D.instance.pressedKey.ToString()));
        InputSystem2D.instance.UIFuncsUp.Add(() => Debug.Log("Up = " + InputSystem2D.instance.pressedKey.ToString()));
        InputSystem2D.instance.UIFuncs.Add(() => Debug.Log("Pressing = " + InputSystem2D.instance.pressedKey.ToString()));
    }
    
    void Update()
    {
        InputSystem2D.instance.Movement();
        Debug.Log(InputSystem2D.instance.MovementVector);
    }

}
