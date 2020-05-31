using BehaviorDesigner.Runtime.Tasks;

namespace Model.Brain
{
    public class ChoosePlant:Action
    {
        public SharedPlant target;
        public SharedAnimal Self;
        

        public override TaskStatus OnUpdate()
        {
            target.Value= Self.Value.properties.Plants.Find(plant => plant.Ripe);
            return target.Value != null ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}