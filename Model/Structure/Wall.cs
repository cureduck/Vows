using System;

namespace Model.Structure
{
    public class Wall:Stru
    {
        protected override Action<Entity>[] DisiredReactions { get; }
    }
}