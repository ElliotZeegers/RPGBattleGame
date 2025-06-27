using System;
using UnityEngine;

public class KeyboardMoveInput : MonoBehaviour, IPlayerMoveInput
{
    private KeyCode _keyForward = KeyCode.W;
    private KeyCode _keyBackward = KeyCode.S;
    private KeyCode _keyRight = KeyCode.D;
    private KeyCode _keyLeft = KeyCode.A;

    public Vector2 GetMovementInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(_keyForward))
        {
            input.y += 1;
        }
        if (Input.GetKey(_keyBackward))
        {
            input.y -= 1;
        }
        if (Input.GetKey(_keyRight))
        {
            input.x += 1;
        }
        if (Input.GetKey(_keyLeft))
        {
            input.x -= 1;
        }

        if (input != Vector2.zero)
        {
            input.Normalize();
        }

        return input;
    }
}
