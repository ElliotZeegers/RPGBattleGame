using UnityEngine;

public class DisableBattleBox : MonoBehaviour, IProtectable
{
    Collider2D _collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrantSpawnProtection()
    {

    }
}
