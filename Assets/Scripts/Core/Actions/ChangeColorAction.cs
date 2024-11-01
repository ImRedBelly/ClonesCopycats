namespace Core.Actions
{
    public class ChangeColorAction : IAction
    {
        public void Accept(IActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}