using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    //Begin kleur van de sprite
    [SerializeField] private Color _startColor = Color.cyan;
    //Eind kleur van de sprite
    [SerializeField] private Color _endColor = Color.red;
    [SerializeField] private float _duration = 2f;

    private float _time;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Tel tijd op gebaseerd op de ingestelde duration
        _time += Time.deltaTime / _duration;
        //PingPong zorgt ervoor dat de waarde tussen 0 en 1 op en neer gaat
        float t = Mathf.PingPong(_time, 1f);
        //Veranderd de kleur van de sprite tussen de start kleur en eind kleur
        _spriteRenderer.color = Color.Lerp(_startColor, _endColor, t);
    }
}
