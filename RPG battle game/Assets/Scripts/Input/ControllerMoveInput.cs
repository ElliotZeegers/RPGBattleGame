using System;
using UnityEngine;

public class ControllerMoveInput : MonoBehaviour, IPlayerMoveInput
{
    public Vector2 GetMovementInput()
    {
        Vector2 input = Vector2.zero;
        //Checkt voor jostick input
        input.x = Input.GetAxisRaw("JoystickHorizontal");
        input.y = Input.GetAxisRaw("JoystickVertical");

        return input;
    }
}
