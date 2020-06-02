using BehaviorDesigner.Runtime.Tasks;

namespace Model.Brain
{
    public class ChoosePlant:Conditional
    {
        public SharedPlant target;
        public SharedAnimal Self;
        

        public override TaskStatus OnUpdate()
        {
            var t= Self.Value.properties.Plants.Find(plant => plant.Ripe);
            target.Value = t;
            return target.Value != null ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}