using System;
using UnityEngine;

public class ControllerInteractInput : MonoBehaviour, IPlayerInteractInput
{
    //Heb een cooldown ingebouwd voor in de menu's zodat het niet te snel gaat
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
        //Pakt de input van de Joystick, heb custom in de input manager deze gemaakt zodat hij alleen naar controller input kijkt en niet die van een toetsenbord
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
        //Geeft true terug als er op de juiste knop is geklikt
        return Input.GetKeyDown(KeyCode.JoystickButton0);
    }

    public bool Return()
    {
        //Geeft true terug als er op de juiste knop is geklikt
        return Input.GetKeyDown(KeyCode.JoystickButton1);
    }
}
