using UnityEngine;

public class PosPriority : MonoBehaviour
{
    public enum PosTypes
    {
        Player,
        Enemy
    }

    [SerializeField] int _priority;
    [SerializeField] PosTypes _posType;

    public int Priority { get { return _priority; } }
    public PosTypes PosType { get { return _posType; } }
}
