using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] Text _turnText;
    [SerializeField] Text _battleDoneText;

    public void UpdateTurnUI()
    {
        _turnText.text = "Beurt: " + BattleManager.Instance.TurnCounter;
    }

    public void UpdateWinnerUI(string pDisplayText)
    {
        _battleDoneText.text = pDisplayText;
    }
}
