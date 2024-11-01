using Core.Actions;
using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class PlayerController : MonoBehaviour
    {
        public List<IAction> Actions { get; private set; } = new();
     
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;

        private InputHandler _inputHandler;
        private ActionExecutor _executor;

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

        private void OnMove(float direction, bool isSprint)
        {
            var moveAction = new MoveAction(direction, isSprint);
            _executor.Visit(moveAction);
            Actions.Add(moveAction);
        }

        private void OnClickedJump()
        {
            if (Mathf.Abs(rigidbody.velocity.y) < 0.001f)
            {
                var jumpAction = new JumpAction();
                _executor.Visit(jumpAction);
                Actions.Add(jumpAction);
            }
        }

        private void OnClickedChangeColor()
        {
            var changeColorAction = new ChangeColorAction();
            _executor.Visit(changeColorAction);
            Actions.Add(changeColorAction);
        }

        public void ResetPlayer(Transform startPoint)
        {
            Actions = new List<IAction>();
            transform.position = startPoint.position;
        }
    }
}