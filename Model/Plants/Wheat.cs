using Model.Items;

namespace Model.Plants
{
    public class Wheat:Plant
    {
        private static readonly Item[] _drop = {new hp(), new hp()};
        public override string DisplayName => "Wheat";
        public override Item[] Drop => _drop;
        
    }
}