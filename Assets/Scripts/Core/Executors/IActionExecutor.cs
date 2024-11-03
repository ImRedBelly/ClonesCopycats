using Core.Actions;

namespace Core.Executors
{
    public interface IActionExecutor
    {
        void Visit(DelayEmptyAction delayEmptyAction);
        void Visit(MoveAction moveAction);
        void Visit(JumpAction jumpAction);
        void Visit(SprintAction sprintAction);
        void Visit(ChangeColorAction changeColorAction);
    }
}