using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Model.Plants;
using UnityEngine;

namespace Model.Brain
{
    public class Harvest : Action
    {
        public SharedAnimal Self;
        public SharedPlant Target;
        
        public override void OnStart()
        {
            Self.Value.Move2React(Target.Value);
        }

        public override TaskStatus OnUpdate()
        {
            return Self.Value.hasReached ? TaskStatus.Success : TaskStatus.Running;
        }
    }

    public class React : Action
    {
        public SharedAnimal Self;
        public SharedEntity Target;
        
        public override void OnStart()
        {
            Self.Value.Move2React(Target.Value);
        }
        
        
        public override TaskStatus OnUpdate()
        {
            return Self.Value.hasReached ? TaskStatus.Success : TaskStatus.Running;
        }
    }
    
    
    [System.Serializable]
    public class SharedAnimal : SharedVariable <Animal>
    {
        public static implicit operator SharedAnimal(Animal value) { return new SharedAnimal { Value = value }; }
    }

    [System.Serializable]
    public class SharedPlant : SharedVariable<Plant>
    {
        public static implicit operator SharedPlant(Plant value) { return new SharedPlant { Value = value }; }
    }

    [System.Serializable]
    public class SharedEntity : SharedVariable<Entity>
    {
        public static implicit operator SharedEntity(Entity value) { return new SharedEntity { Value = value }; }
    }
}