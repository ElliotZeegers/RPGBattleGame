using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChoice : MonoBehaviour
{
    private IPlayerInteractInput _playerInput;

    List<OptionBlock> _optionBlocks = new List<OptionBlock>();
    List<Transform> _blockPositions = new List<Transform>();

    private float _lerpProgress = 0f;
    private List<Vector3> _startPositions;
    private List<Vector3> _targetPositions;

    private bool _isLerping = false;
    private int _nextSelectedBlock = 1;
    private int _selectedBlock;

    public int SelectedBlock { get { return _selectedBlock; } }

    private void Awake()
    {
        _optionBlocks = GetComponentsInChildren<OptionBlock>().OrderBy(ob => ob.Priority).ToList();
        _playerInput = GetComponentInParent<IPlayerInteractInput>();
        _selectedBlock = _nextSelectedBlock;
        foreach (OptionBlock optionBlock in _optionBlocks)
        {
            _blockPositions.Add(optionBlock.transform);
        }
    }
    void Start()
    {
        //GameplayManager.Instance.OnSwapInput += ChangeInput;
    }


    private void Update()
    {
        DoLerp();
    }

    private void StartLerp(List<Vector3> targets)
    {
        _startPositions = _optionBlocks.Select(ob => ob.transform.position).ToList();
        _targetPositions = targets;
        _lerpProgress = 0f;
        _isLerping = true;
    }

    private void DoLerp()
    {
        if (_isLerping)
        {
            _lerpProgress += Time.deltaTime / 0.3f;
            if (_lerpProgress >= 1f)
            {
                _lerpProgress = 1f;
                _isLerping = false;
            }

            for (int i = 0; i < _optionBlocks.Count; i++)
            {
                _optionBlocks[i].transform.position = Vector3.Lerp(_startPositions[i], _targetPositions[i], _lerpProgress);
            }
        }
        else
        {
            _selectedBlock = _nextSelectedBlock;
            SelectBlock();
        }
    }

    public void SelectBlock()
    {
        int blockNumber = _playerInput.SelectOption();
        List<Vector3> newBlockPos = new List<Vector3>();

        if (blockNumber != 0)
        {
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
            if (_nextSelectedBlock >= _optionBlocks.Count())
            {
                _nextSelectedBlock = 0;
            }
            else if (_nextSelectedBlock < 0)
            {
                _nextSelectedBlock = _optionBlocks.Count() - 1;
            }

            StartLerp(newBlockPos);
        }
    }

    public void ActivateBlock()
    {
        _optionBlocks[_selectedBlock].Activate();
    }

    //public void ChangeInput()
    //{
    //    _playerInput = GetComponentInParent<IPlayerInteractInput>();
    //}

    //public void UnSubscribe()
    //{
    //    GameplayManager.Instance.OnSwapInput -= ChangeInput;
    //}
}
