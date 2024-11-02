using Core.Actions;

namespace Core.Executors
{
    public interface IActionExecutor
    {
        void Visit(MoveAction moveAction);
        void Visit(JumpAction jumpAction);
        void Visit(ChangeColorAction changeColorAction);
    }
}