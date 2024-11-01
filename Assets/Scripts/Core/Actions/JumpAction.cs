namespace Core.Actions
{
    public class JumpAction : IAction
    {
        public void Accept(IActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}