using UnityEngine;
using Core.Actions;
using System.Collections.Generic;

namespace Core
{
    public class CloneController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;
      
        private ActionExecutor _executor;
        private List<IAction> _actions;
        private int _currentActionIndex = 0;

        public void Initialize(List<IAction> actions)
        {
            _executor = new ActionExecutor(rigidbody,spriteRenderer, speed, jumpForce);
            _actions = new List<IAction>(actions);
        }

        private void Update()
        {
            if (_currentActionIndex < _actions.Count)
            {
                _actions[_currentActionIndex].Accept(_executor);
                _currentActionIndex++;
            }
        }
    }
}