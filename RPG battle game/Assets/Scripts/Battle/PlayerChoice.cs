using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChoice : MonoBehaviour
{
    private IPlayerInteractInput _playerInput;

    //Lijsten met de option blocks en de posities van de blocks
    private List<OptionBlock> _optionBlocks = new List<OptionBlock>();
    private List<Transform> _blockPositions = new List<Transform>();

    //Float voor bijhouden hoever de lerp is
    private float _lerpProgress = 0f;
    private List<Vector3> _startPositions;
    private List<Vector3> _targetPositions;

    //Bool om te kijke of het aan het lerpen is
    private bool _isLerping = false;
    private int _nextSelectedBlock = 1;
    private int _selectedBlock;

    public int SelectedBlock { get { return _selectedBlock; } }

    private void Awake()
    {
        //Haalt alle OptionBlocks op en sorteert ze op priority
        _optionBlocks = GetComponentsInChildren<OptionBlock>().OrderBy(ob => ob.Priority).ToList();
        _playerInput = GetComponentInParent<IPlayerInteractInput>();
        _selectedBlock = _nextSelectedBlock;
        // Sla de transforms van de blocks op
        foreach (OptionBlock optionBlock in _optionBlocks)
        {
            _blockPositions.Add(optionBlock.transform);
        }
    }

    private void Update()
    {
        //Voert de lerp uit als dat moet
        DoLerp();
    }

    //Start het lerpen naar
    private void StartLerp(List<Vector3> targets)
    {
        //Maakt een lijst van de huidige posities van alle OptionBlocks
        _startPositions = _optionBlocks.Select(ob => ob.transform.position).ToList();
        _targetPositions = targets;
        _lerpProgress = 0f;
        _isLerping = true;
    }

    //Animeert de blokken naar hun nieuwe posities
    private void DoLerp()
    {
        //Checkt of er gelerpt wordt
        if (_isLerping)
        {
            // Verhoogt de voortgang van de lerp op basis van tijd (0.3 seconden animatieduur)
            _lerpProgress += Time.deltaTime / 0.3f;
            //Checkt of de lerp klaar is
            if (_lerpProgress >= 1f)
            {
                _lerpProgress = 1f;
                _isLerping = false;
            }

            //Verplaatst elk OptionBlock op basis van de voortgang tussen start en target positie
            for (int i = 0; i < _optionBlocks.Count; i++)
            {
                _optionBlocks[i].transform.position = Vector3.Lerp(_startPositions[i], _targetPositions[i], _lerpProgress);
            }
        }
        else
        {
            //Als er geen lerp bezig is zet zet dan _selectedBlock op de waarde van _nextSelectedBlock
            _selectedBlock = _nextSelectedBlock;
            SelectBlock();
        }
    }

    public void SelectBlock()
    {
        //Slaat input van de speler op
        int blockNumber = _playerInput.SelectOption();
        List<Vector3> newBlockPos = new List<Vector3>();

        //Als de input anders is dan 0 voer de code hieronder uit
        if (blockNumber != 0)
        {
            //zorgt ervoor dat de blokken hun nieuwe positie krijgen
            for (int i = 0; i < _optionBlocks.Count(); i++)
            {
                int targetIndex = blockNumber + i;

                if (targetIndex >= _optionBlocks.Count())
                {
                    targetIndex = 0;
                }
                else if (targetIndex < 0)
                {
                    targetIndex = _optionBlocks.Count() - 1;
                }

                newBlockPos.Add(_blockPositions[targetIndex].transform.position);
            }
            
            _nextSelectedBlock -= blockNumber;
            //Zorgt ervoor dat _nextSelectedBlock binnen de grenzen van _optionBlocks blijft. Als _nextSelectedBlock te hoog is, wordt hij op 0 gezet, als hij te laag is wordt hij op het laatste element gezet.
            if (_nextSelectedBlock >= _optionBlocks.Count())
            {
                _nextSelectedBlock = 0;
            }
            else if (_nextSelectedBlock < 0)
            {
                _nextSelectedBlock = _optionBlocks.Count() - 1;
            }

            //Start de lerp zodat de blokken naar de nieuwe posities gaan
            StartLerp(newBlockPos);
        }
    }

    //Activeert de geselecteerde optie
    public void ActivateBlock()
    {
        _optionBlocks[_selectedBlock].Activate();
    }
    //Zorgt ervoor dat de input opnieuw opgehaald wordt
    public void ChangeInput()
    {
        _playerInput = GetComponentInParent<IPlayerInteractInput>();
    }
}
