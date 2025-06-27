using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject _healthUIPrefab;
    [SerializeField] private Image _healthImage;
    private BattleEntity _battleEntity;
    private float _hp = 0;

    void Start()
    {
        _battleEntity = GetComponent<BattleEntity>();
        _hp = _battleEntity.Hp;
        Instantiate(_healthUIPrefab, this.transform);
        _healthImage = GetComponentsInChildren<Image>().FirstOrDefault(img => img.type == Image.Type.Filled);
    }

    public void UpdateHealthImage(float _damage)
    {
        _healthImage.fillAmount -= (_damage / _hp);
    }
}
