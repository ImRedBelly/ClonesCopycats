using System;
using UnityEngine;

namespace Core
{
    public class InputHandler : MonoBehaviour
    {
        public event Action<float, bool> Move;
        public event Action ClickedJump;
        public event Action ClickedChangeColor;
        public event Action SpawnClone;

        private void Update()
        {
            float rawInput = Input.GetAxis("Horizontal");
            float direction = rawInput < 0 ? -1 : rawInput > 0 ? 1 : 0;
            Move?.Invoke(direction, Input.GetKey(KeyCode.LeftShift));

            if (Input.GetButtonDown("Jump"))
                ClickedJump?.Invoke();

            if (Input.GetKeyDown(KeyCode.Q))
                ClickedChangeColor?.Invoke();
            if (Input.GetKeyDown(KeyCode.R))
                SpawnClone?.Invoke();
        }
    }
}