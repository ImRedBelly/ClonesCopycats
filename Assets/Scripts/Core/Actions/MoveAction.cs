using Core.Executors;

namespace Core.Actions
{
    public class MoveAction : IAction
    {
        public float Direction { get; }

        public MoveAction(float direction) => Direction = direction;

        public void Accept(IActionExecutor executor)
        {
            executor.Visit(this);
        }

        public float GetElapsedTime() => 0;
    }
}