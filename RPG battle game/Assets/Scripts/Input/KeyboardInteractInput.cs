using UnityEngine;

public class KeyboardInteractInput : MonoBehaviour, IPlayerInteractInput
{
    private KeyCode _keyRight = KeyCode.D;
    private KeyCode _keyLeft = KeyCode.A;
    private KeyCode _keyConfirm = KeyCode.Return;
    private KeyCode _keyReturn = KeyCode.Backspace;
    public int SelectOption()
    {
        //Als er op _keyRight wordt gedrukt dan returnt hij -1 als er op _keyLeft wordt gedrukt dan returnt hij +1, en als er niks wordt gedrukt dan returnt hij 0
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
        //Geeft true terug als er op de juiste knop is geklikt
        return Input.GetKeyDown(_keyConfirm);
    }

    public bool Return()
    {
        //Geeft true terug als er op de juiste knop is geklikt
        return Input.GetKeyDown(_keyReturn);
    }
}
