using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Model.Plants;
using UnityEngine;

namespace Model.Brain
{
    public class Harvest : Action
    {
        public SharedAnimal Self;
        public SharedEntity Target;
        private Plant _plant;
        
        public override void OnStart()
        {
            _plant = Target.Value as Plant;
            Self.Value.React(_plant);
        }
        
        
        public override TaskStatus OnUpdate()
        {
            return Self.Value.status==Entity.Status.Idle ? TaskStatus.Success : TaskStatus.Running;
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