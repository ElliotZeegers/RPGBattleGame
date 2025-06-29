using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] Text _turnText;
    [SerializeField] Text _battleDoneText;

    public void UpdateTurnUI()
    {
        //Update de text met de huidige TurnCounter
        _turnText.text = "Beurt: " + BattleManager.Instance.TurnCounter;
    }

    public void UpdateWinnerUI(string pDisplayText)
    {
        //Zorgt ervoor dat er zien is wie er heeft gewonnen na een battle
        _battleDoneText.text = pDisplayText;
    }
}
