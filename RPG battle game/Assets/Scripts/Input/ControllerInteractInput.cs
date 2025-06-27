using System;
using UnityEngine;

public class ControllerInteractInput : MonoBehaviour, IPlayerInteractInput
{
    private string _horizontalAxis = "Horizontal";
    private string _confirmButton = "Submit";
    private string _returnButton = "Cancel";

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
        float horizontalInput = Input.GetAxisRaw(_horizontalAxis);

        if (_cooldownTimer <= 0f)
        {
            if (horizontalInput > 0.5f)
            {
                _cooldownTimer = _inputCooldown;
                return -1;
            }
            else if (horizontalInput < -0.5f)
            {
                _cooldownTimer = _inputCooldown;
                return +1;
            }
        }

        return 0;
    }

    public bool Confirm()
    {
        return Input.GetButtonDown(_confirmButton);
    }

    public bool Return()
    {
        return Input.GetButtonDown(_returnButton);
    }
}
