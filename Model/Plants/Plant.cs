using System;

namespace Model.Plants
{
    public class Plant:Property
    {
        public float Progress { get; private set; }
        public bool Ripe => Progress >= 1;
        public float GrowUpTime { get; }
        public Item[] DropOutTemplate { get; }
        
        public override Action<Entity>[] GetReactions(Entity sponser)
        {
            return !Ripe ? null : new Action<Entity>[1]{PickUp};
        }

        public virtual void PickUp(Entity sponser)
        {
            if (sponser is Animal a)
            {
                foreach (var item in DropOutTemplate)
                {
                    
                }
                
            }
        }

        public ISubject owner { get; }
    }
}