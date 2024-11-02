using UnityEngine;
using Core.Actions;
using Core.Executors;
using System.Collections.Generic;

namespace Core.Characters
{
    public class PlayerCharacter : MonoBehaviour
    {
        public List<IAction> Actions { get; private set; } = new();

        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;

        private InputHandler _inputHandler;
        private ActionExecutor _executor;

        private MoveAction _cachedMoveAction;
        private float _lastActionTime;

        public void Initialize(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _executor = new ActionExecutor(rigidbody, spriteRenderer, speed, jumpForce);

            _inputHandler.Move += OnMove;
            _inputHandler.ClickedJump += OnClickedJump;
            _inputHandler.ClickedChangeColor += OnClickedChangeColor;
        }

        private void OnDisable()
        {
            if (_inputHandler != null)
            {
                _inputHandler.Move -= OnMove;
                _inputHandler.ClickedJump -= OnClickedJump;
                _inputHandler.ClickedChangeColor -= OnClickedChangeColor;
            }
        }

        public void ResetPlayer(Transform startPoint)
        {
            Actions = new List<IAction>();
            transform.position = startPoint.position;
        }

        private void OnMove(float direction, bool isSprint)
        {
            float elapsedTime = Time.time - _lastActionTime;
            if (_cachedMoveAction == null || !Mathf.Approximately(_cachedMoveAction.Direction, direction) || _cachedMoveAction.IsSprint != isSprint)
            {
                if (_cachedMoveAction != null)
                {
                    _cachedMoveAction.SetElapsedTime(elapsedTime);
                    Actions.Add(_cachedMoveAction);
                }

                _cachedMoveAction = new MoveAction(direction, isSprint);
                _lastActionTime = Time.time;
            }

            _executor.Visit(_cachedMoveAction);
        }

        private void OnClickedJump()
        {
            if (Mathf.Abs(rigidbody.velocity.y) < 0.001f)
            {
                AddCachedMoveAction();
                var jumpAction = new JumpAction();
                _executor.Visit(jumpAction);
                Actions.Add(jumpAction);

                _cachedMoveAction = new MoveAction(0, false);
                _lastActionTime = Time.time;
            }
        }

        private void OnClickedChangeColor()
        {
            AddCachedMoveAction();
            var changeColorAction = new ChangeColorAction();
            _executor.Visit(changeColorAction);
            Actions.Add(new ChangeColorAction());
            _cachedMoveAction = new MoveAction(0, false);
            _lastActionTime = Time.time;
        }

        private void AddCachedMoveAction()
        {
            if (_cachedMoveAction != null)
            {
                _cachedMoveAction.SetElapsedTime(Time.time - _lastActionTime);
                Actions.Add(_cachedMoveAction);
            }
        }
    }
}