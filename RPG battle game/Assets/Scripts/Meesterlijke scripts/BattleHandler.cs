using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _shaderEnterTransition;
    [SerializeField] private GameObject _shaderExitTransition;
    [SerializeField] private GameObject _battleSetup;
    private GameObject _battleSetupObject;
    private GameObject _playerGroup;
    private GameObject _enemyGroup;
    private GameObject _startedBattleEnemy;

    public void EnterBattle(GameObject pPlayerGroup, GameObject pEnemyGroup, GameObject pStartedBattleEnemy)
    {
        GameObject shaderObject = Instantiate(_shaderEnterTransition);
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();
        _startedBattleEnemy = pStartedBattleEnemy;

        destroyShader.OnFinish = () =>
        {
            Instantiate(_shaderExitTransition);
            _playerGroup = Instantiate(pPlayerGroup);
            _enemyGroup = Instantiate(pEnemyGroup);
            _battleSetupObject = Instantiate(_battleSetup);
            Vector3 newCamPos = new Vector3(_battleSetup.transform.position.x, _battleSetup.transform.position.y, _mainCamera.transform.position.z);
            _mainCamera.transform.position = newCamPos;
        };
    }

    public void ExitBattle()
    {
        GameObject shaderObject = Instantiate(_shaderEnterTransition);
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();

        destroyShader.OnFinish = () =>
        {
            Instantiate(_shaderExitTransition);
            Destroy(_playerGroup.gameObject);
            Destroy(_enemyGroup.gameObject);
            Destroy(_battleSetupObject.gameObject);
            Destroy(_startedBattleEnemy.gameObject);
            GameplayManager.Instance.UnPause();
        };
    }
}
