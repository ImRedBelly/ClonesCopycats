using System;
using UnityEngine;
using Core.Actions;
using Core.Executors;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Core.Characters
{
    public class CloneCharacter : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;

        private ActionExecutor _executor;
        private List<IAction> _actions;

        public async void Initialize(List<IAction> actions)
        {
            _executor = new ActionExecutor(rigidbody, spriteRenderer, speed, jumpForce);
            _actions = new List<IAction>(actions);

            await ProcessActions();
        }

        private async UniTask ProcessActions()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();

            try
            {
                foreach (var action in _actions)
                {
                    float elapsedTime = 0f;
                    float actionDuration = action.GetElapsedTime();

                    while (elapsedTime < actionDuration)
                    {
                        action.Accept(_executor);
                        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                        elapsedTime += Time.deltaTime;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}