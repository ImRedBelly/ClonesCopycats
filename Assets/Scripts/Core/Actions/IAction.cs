namespace Core.Actions
{
    public interface IAction
    {
        void Accept(IActionVisitor visitor);
    }
}