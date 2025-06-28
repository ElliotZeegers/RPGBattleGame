using System;
using UnityEngine;

public class ControllerInteractInput : MonoBehaviour, IPlayerInteractInput
{
    private float _inputCooldown = 0.25f;
    private float _cooldownTimer = 0f;

    void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    public int SelectOption()
    {
        float horizontalInput = Input.GetAxisRaw("JoystickHorizontal");

        if (_cooldownTimer <= 0f)
        {
            if (horizontalInput > 0f)
            {
                _cooldownTimer = _inputCooldown;
                return -1;
            }
            else if (horizontalInput < 0f)
            {
                _cooldownTimer = _inputCooldown;
                return +1;
            }
        }

        return 0;
    }

    public bool Confirm()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton0);
    }

    public bool Return()
    {
        return Input.GetKeyDown(KeyCode.JoystickButton1);
    }
}
