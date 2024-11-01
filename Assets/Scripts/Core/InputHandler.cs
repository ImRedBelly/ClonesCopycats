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
            Move?.Invoke(Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.LeftShift));

            if (Input.GetButtonDown("Jump"))
                ClickedJump?.Invoke();
            if (Input.GetKeyDown(KeyCode.Q))
                ClickedChangeColor?.Invoke();
            if (Input.GetKeyDown(KeyCode.R))
                SpawnClone?.Invoke();
        }
    }
}