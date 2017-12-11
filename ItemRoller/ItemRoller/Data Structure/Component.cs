using ItemRoller.Visitors;
using Newtonsoft.Json;

namespace ItemRoller.Data_Structure
{
    public abstract class Component : IVisitable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("probability")]
        public int Probability { get; set; }

        [JsonProperty("book")]
        public string Book { get; set; }

        [JsonProperty("item types")]
        public ItemTypes Types { get; set; }

        public Table ParentTable { get; set; }

        public abstract void Accept(IVisitor visitor);

        public abstract object Clone();

        public override string ToString()
        {
            return GetType().Name + ": " + Name + " | Prob: " + Probability + " | Book: " + Book;
        }
    }
}
