using Core.Executors;

namespace Core.Actions
{
    public class DelayEmptyAction : IAction
    {
        private float _elapsedTime;

        public DelayEmptyAction(float elapsedTime)
        {
            _elapsedTime = elapsedTime;
        }

        public void Accept(IActionExecutor executor)
        {
            executor.Visit(this);
        }
 
        public float GetElapsedTime() => _elapsedTime;

    }
}