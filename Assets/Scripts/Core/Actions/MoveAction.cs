namespace Core.Actions
{
    public class MoveAction : IAction
    {
        public float Direction { get; }
        public bool IsSprint { get; }

        public MoveAction(float direction, bool isSprint)
        {
            Direction = direction;
            IsSprint = isSprint;
        }

        public void Accept(IActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}