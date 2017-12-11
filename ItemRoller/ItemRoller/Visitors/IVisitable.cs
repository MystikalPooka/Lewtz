namespace ItemRoller.Visitors
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}