using Core.Executors;

namespace Core.Actions
{
    public interface IAction
    {
        void Accept(IActionExecutor executor);
        float GetElapsedTime();
    }
}