using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    //Transitie shaders
    [SerializeField] private GameObject _shaderEnterTransition;
    [SerializeField] private GameObject _shaderExitTransition;

    [SerializeField] private GameObject _battleSetup;
    [SerializeField] private GameObject _battleObjectPrefab;
    private GameObject _battleObject;
    private GameObject _startedBattleEnemy;

    public void EnterBattle(GameObject pPlayerGroup, GameObject pEnemyGroup, GameObject pStartedBattleEnemy)
    {
        //Instantiate de transition voor een leuk effect als je een battle in gaat
        GameObject shaderObject = Instantiate(_shaderEnterTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
        //Slaat het DestroyShader component op in destroyShader
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();
        //Slaat de enemy die de battle heeft gestart op in _startedBattleEnemy zodat we hem later kunnen vernietigen als de speler wint
        _startedBattleEnemy = pStartedBattleEnemy;

        // Als de shader is afgelopen, begin de battle
        destroyShader.OnFinish = () =>
        {
            //Instantiate een exit transition voor een leuk effect
            Instantiate(_shaderExitTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            _battleObject = Instantiate(_battleObjectPrefab, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            //Instantiate het nodige voor de battle
            Instantiate(pPlayerGroup, _battleObject.transform);
            Instantiate(pEnemyGroup, _battleObject.transform);
            Instantiate(_battleSetup, _battleObject.transform);
            //Vertelt de GameplayManager dat de battle is gestart
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
            //Vernietig de battle en de enemy die de battle heeft gestart
            Destroy(_battleObject.gameObject);
            Destroy(_startedBattleEnemy.gameObject);
            //Roept UnPause aan van de GameplayManager
            GameplayManager.Instance.UnPause();
        };
    }

    //Zelfde als ExitBattleWin() maar zonder dat de enemy vernietigd wordt
    public void ExitBattleRun()
    {
        GameObject shaderObject = Instantiate(_shaderEnterTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
        DestroyShader destroyShader = shaderObject.GetComponent<DestroyShader>();

        destroyShader.OnFinish = () =>
        {
            Instantiate(_shaderExitTransition, new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y), _mainCamera.transform.rotation);
            Destroy(_battleObject.gameObject);
            GameplayManager.Instance.UnPause();
        };
    }
}
