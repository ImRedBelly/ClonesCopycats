using Core.Executors;

namespace Core.Actions
{
    public class ChangeColorAction : IAction
    {
        public void Accept(IActionExecutor executor)
        {
            executor.Visit(this);
        }

        public float GetElapsedTime() => 0;
    }
}