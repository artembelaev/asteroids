using AsteroidGame.Settings;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace AsteroidGame
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerControllerPrefab;
        [SerializeField] private AsteroidsFactory _asteroidsFactoryPrefab;
        [SerializeField] private BalanceSettings _balanceSettings;

        private PlayerController _playerController;
        private AsteroidsFactory _asteroidsFactory;
        private GameStateMachine _fsm;

        private void Awake()
        {
            Install();
        }

        private void Install()
        {
            InstallPlayer();
            InstallFactory();
            InstallGameStates();
        }

        private void InstallPlayer()
        {
            _playerController = Instantiate(_playerControllerPrefab, transform);
        }

        private void InstallFactory()
        {
            _asteroidsFactory = Instantiate(_asteroidsFactoryPrefab, transform);
        }

        private void InstallGameStates()
        {
            _fsm = new GameStateMachine();
            _fsm.AddState(new GameStatePlay(_asteroidsFactory, _balanceSettings));
            _fsm.Enter(GameStateEnum.Play);
        }
    }
}