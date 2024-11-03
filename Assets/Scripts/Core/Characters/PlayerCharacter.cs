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

        private DelayEmptyAction _cachedDelayEmptyAction;
        private float _cachedDirection;
        private bool _cachedIsSprint;

        private float _lastActionTime;

        public void Initialize(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _executor = new ActionExecutor(rigidbody, spriteRenderer, speed, jumpForce);

            _cachedDelayEmptyAction = new DelayEmptyAction(0);
            _lastActionTime = Time.time;

            _inputHandler.Move += OnMove;
            _inputHandler.ClickedJump += OnClickedJump;
            _inputHandler.ClickedSprint += OnClickedSprint;
            _inputHandler.ClickedChangeColor += OnClickedChangeColor;
        }

        public void ResetPlayer(Transform startPoint)
        {
            Actions = new List<IAction>();
            transform.position = startPoint.position;
        }

        private void OnDisable()
        {
            if (_inputHandler != null)
            {
                _inputHandler.Move -= OnMove;
                _inputHandler.ClickedJump -= OnClickedJump;
                _inputHandler.ClickedSprint -= OnClickedSprint;
                _inputHandler.ClickedChangeColor -= OnClickedChangeColor;
            }
        }

        private void Update() => _executor.Visit(_cachedDelayEmptyAction);

        private void OnMove(float direction)
        {
            if (!Mathf.Approximately(_cachedDirection, direction))
            {
                AddDelayEmptyAction();
                var moveAction = new MoveAction(direction);

                _cachedDirection = direction;
                _executor.Visit(moveAction);
                Actions.Add(moveAction);
            }
        }

        private void OnClickedJump()
        {
            if (Mathf.Abs(rigidbody.velocity.y) < 0.001f)
            {
                AddDelayEmptyAction();

                var jumpAction = new JumpAction();
                _executor.Visit(jumpAction);
                Actions.Add(jumpAction);
            }
        }

        private void OnClickedSprint(bool isClickedSprint)
        {
            if (_cachedIsSprint != isClickedSprint)
            {
                AddDelayEmptyAction();

                var sprintAction = new SprintAction(isClickedSprint);

                _cachedIsSprint = isClickedSprint;
                _executor.Visit(sprintAction);
                Actions.Add(sprintAction);
            }
        }

        private void OnClickedChangeColor()
        {
            AddDelayEmptyAction();

            var changeColorAction = new ChangeColorAction();
            _executor.Visit(changeColorAction);
            Actions.Add(changeColorAction);
        }

        private void AddDelayEmptyAction()
        {
            var delayTime = Time.time - _lastActionTime;
            Actions.Add(new DelayEmptyAction(delayTime));

            _lastActionTime = Time.time;
        }
    }
}