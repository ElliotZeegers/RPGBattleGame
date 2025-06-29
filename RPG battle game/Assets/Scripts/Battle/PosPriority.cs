using UnityEngine;

public class PosPriority : MonoBehaviour
{
    //Enum om aan te geven of deze positie voor een speler of vijand is
    public enum PosTypes
    {
        Player,
        Enemy
    }

    [SerializeField] private int _priority;
    [SerializeField] private PosTypes _posType;

    public int Priority { get { return _priority; } }
    public PosTypes PosType { get { return _posType; } }
}
