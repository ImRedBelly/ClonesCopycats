using UnityEngine;
using Core.Characters;

namespace Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private PlayerCharacter playerCharacter;
        [SerializeField] private CloneCharacter cloneCharacter;

        private PlayerCharacter _playerCharacter;

        private void Awake()
        {
            _playerCharacter = Instantiate(playerCharacter, startPosition.position, Quaternion.identity);
            _playerCharacter.Initialize(inputHandler);
        }

        private void OnEnable()
        {
            inputHandler.SpawnClone += OnSpawnClone;
        }

        private void OnDisable()
        {
            inputHandler.SpawnClone -= OnSpawnClone;
        }

        private void OnSpawnClone()
        {
            CloneCharacter clone = Instantiate(cloneCharacter, startPosition.position, Quaternion.identity);
            clone.Initialize(_playerCharacter.Actions);
            _playerCharacter.ResetPlayer(startPosition);
        }
    }
}