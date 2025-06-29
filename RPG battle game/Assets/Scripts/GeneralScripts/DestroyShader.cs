using System;
using UnityEngine;

public class DestroyShader : MonoBehaviour
{
    private Material _mat;
    private float _timeActive;
    private float _destroyTime;

    public Action OnFinish;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        _mat = renderer.material;
        _destroyTime = _mat.GetFloat("_LoopTime");
        
    }

    void Update()
    {
        _timeActive += Time.deltaTime;

        if (_mat != null)
        {
            _mat.SetFloat("_CustomTime", _timeActive);
        }
        if (_timeActive >= _destroyTime + 0.5f)
        {
            OnFinish?.Invoke();
            Destroy(gameObject);
        }
    }
}
