using System;

namespace Model.Structure
{
    public class Wall:Stru
    {
        protected override Action<Animal>[] DisiredReactions { get; }
    }
}