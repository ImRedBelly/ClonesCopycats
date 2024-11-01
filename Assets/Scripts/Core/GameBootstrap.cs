using UnityEngine;

namespace Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CloneController cloneController;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = Instantiate(playerController, startPosition.position, Quaternion.identity);
            _playerController.Initialize(inputHandler);
        }

        private void OnEnable()
        {
            inputHandler.SpawnClone += OnSpawnClone;
        }

        private void OnSpawnClone()
        {
            CloneController clone = Instantiate(cloneController, startPosition.position, Quaternion.identity);
            clone.Initialize(_playerController.Actions);
            _playerController.ResetPlayer(startPosition);
        }
    }
}