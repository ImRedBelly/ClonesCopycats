using Core.Executors;

namespace Core.Actions
{
    public class SprintAction : IAction
    {
        public bool IsSprint { get; }

        public SprintAction(bool isSprint) => IsSprint = isSprint;

        public void Accept(IActionExecutor executor)
        {
            executor.Visit(this);
        }

        public float GetElapsedTime() => 0;
    }
}