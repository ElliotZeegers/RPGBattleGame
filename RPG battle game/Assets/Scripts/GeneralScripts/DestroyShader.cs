using System;
using UnityEngine;

public class DestroyShader : MonoBehaviour
{
    private Material _mat;
    private float _timeActive;
    //De tijd waarna het object vernietigd wordt (gehaald uit de shader zelf)
    private float _destroyTime;

    //Event dat getriggerd wordt vlak voor vernietiging
    public Action OnFinish;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        _mat = renderer.material;
        //Haalt de waarde op uit de shader
        _destroyTime = _mat.GetFloat("_LoopTime");
        
    }

    void Update()
    {
        _timeActive += Time.deltaTime;

        //Verander de waarde in de shader
        if (_mat != null)
        {
            _mat.SetFloat("_CustomTime", _timeActive);
        }
        //Als de shader zijn loop heeft voltooid + extra tijd, voer OnFinish uit en verwijder het object
        if (_timeActive >= _destroyTime + 0.5f)
        {
            OnFinish?.Invoke();
            Destroy(gameObject);
        }
    }
}
