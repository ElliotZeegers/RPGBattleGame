using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _startColor = Color.cyan;
    [SerializeField] private Color _endColor = Color.red;
    [SerializeField] private float _duration = 2f;

    private float _time;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _time += Time.deltaTime / _duration;
        float t = Mathf.PingPong(_time, 1f);

        _spriteRenderer.color = Color.Lerp(_startColor, _endColor, t);
    }
}
