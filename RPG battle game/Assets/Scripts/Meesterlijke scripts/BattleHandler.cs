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
        GameObject shaderObject = Instantiate(_shaderEnterTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();
        _startedBattleEnemy = pStartedBattleEnemy;

        destroyShader.OnFinish = () =>
        {
            Instantiate(_shaderExitTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            _playerGroup = Instantiate(pPlayerGroup, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            _enemyGroup = Instantiate(pEnemyGroup, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            _battleSetupObject = Instantiate(_battleSetup, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            GameplayManager.Instance.StartBattle();
        };
    }

    public void ExitBattleWin()
    {
        GameObject shaderObject = Instantiate(_shaderEnterTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();

        destroyShader.OnFinish = () =>
        {
            Instantiate(_shaderExitTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            Destroy(_playerGroup.gameObject);
            Destroy(_enemyGroup.gameObject);
            Destroy(_battleSetupObject.gameObject);
            Destroy(_startedBattleEnemy.gameObject);
            GameplayManager.Instance.UnPause();
        };
    }

    public void ExitBattleRun()
    {
        GameObject shaderObject = Instantiate(_shaderEnterTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();

        destroyShader.OnFinish = () =>
        {
            Instantiate(_shaderExitTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            Destroy(_playerGroup.gameObject);
            Destroy(_enemyGroup.gameObject);
            Destroy(_battleSetupObject.gameObject);
            GameplayManager.Instance.UnPause();
        };
    }
}
