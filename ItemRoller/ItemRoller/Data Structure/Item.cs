namespace ItemRoller.Data_Structure
{
    public abstract class Item : Component
    {
        public int Cost { get; set; }

        public override string ToString()
        {
            return base.ToString() + " | Cost: " + Cost;
        }
    }
}
