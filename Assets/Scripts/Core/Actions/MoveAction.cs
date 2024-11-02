using Core.Executors;

namespace Core.Actions
{
    public class MoveAction : IAction
    {
        public float Direction { get; }
        public bool IsSprint { get; }
        private float _elapsedTime;

        public MoveAction(float direction, bool isSprint)
        {
            Direction = direction;
            IsSprint = isSprint;
        }

        public void Accept(IActionExecutor executor)
        {
            executor.Visit(this);
        }

        public float GetElapsedTime() => _elapsedTime;

        public void SetElapsedTime(float elapsedTime)
        {
            _elapsedTime = elapsedTime;
        }
    }
}