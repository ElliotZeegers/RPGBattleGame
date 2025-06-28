using UnityEngine;

public class KeyboardInteractInput : MonoBehaviour, IPlayerInteractInput
{
    private KeyCode _keyRight = KeyCode.D;
    private KeyCode _keyLeft = KeyCode.A;
    private KeyCode _keyConfirm = KeyCode.Return;
    private KeyCode _keyReturn = KeyCode.Backspace;
    public int SelectOption()
    {
        if (Input.GetKeyDown(_keyRight))
        {
            return -1;
        }
        if (Input.GetKeyDown(_keyLeft))
        {
            return +1;
        }

        return 0;
    }

    public bool Confirm()
    {
        return Input.GetKeyDown(_keyConfirm);
    }

    public bool Return()
    {
        return Input.GetKeyDown(_keyReturn);
    }
}
