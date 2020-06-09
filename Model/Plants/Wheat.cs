using Model.Items;

namespace Model.Plants
{
    public class Wheat:Plant
    {
        private static readonly Item[] _drop = {new hp {Qty = 3}};
        public override string DisplayName => "Wheat";
        public override Item[] Drop => _drop;
        
    }
}