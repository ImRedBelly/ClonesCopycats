using Core.Actions;

namespace Core
{
    public interface IActionVisitor
    {
        void Visit(MoveAction moveAction);
        void Visit(JumpAction jumpAction);
        void Visit(ChangeColorAction changeColorAction);
    }
}