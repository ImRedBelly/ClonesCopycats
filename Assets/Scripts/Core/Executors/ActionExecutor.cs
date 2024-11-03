using UnityEngine;
using Core.Actions;

namespace Core.Executors
{
    public class ActionExecutor : IActionExecutor
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly float _speed;
        private readonly float _jumpForce;

        private float _moveDirection;
        private float _sprintAppendSpeed;

        private const float AdditionalSprintSpeed = 15;

        public ActionExecutor(Rigidbody2D rigidbody, SpriteRenderer spriteRenderer, float speed, float jumpForce)
        {
            _rigidbody = rigidbody;
            _spriteRenderer = spriteRenderer;
            _speed = speed;
            _jumpForce = jumpForce;
        }


        public void Visit(DelayEmptyAction delayEmptyAction)
        {
            _rigidbody.velocity = new Vector2(_moveDirection * (_speed + _sprintAppendSpeed), _rigidbody.velocity.y);
        }

        public void Visit(MoveAction moveAction)
        {
            _rigidbody.velocity = Vector2.zero;
            _moveDirection = moveAction.Direction;
        }

        public void Visit(JumpAction jumpAction)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Visit(SprintAction sprintAction)
        {
            _sprintAppendSpeed = sprintAction.IsSprint ? AdditionalSprintSpeed : 0;
        }

        public void Visit(ChangeColorAction changeColorAction)
        {
            var r = Random.Range(0f, 1f);
            var g = Random.Range(0f, 1f);
            var b = Random.Range(0f, 1f);

            Color newColor = new Color(r, g, b);
            _spriteRenderer.color = newColor;
        }
    }
}