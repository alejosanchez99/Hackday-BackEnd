namespace Atom.Entities
{
    public class Metric
    {
        public string title { get; set; }
        public int count { get; set; }
        public int sum { get; set; }
        public int? min { get; set; }
        public int? max { get; set; }
    }
}
