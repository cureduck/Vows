using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Model.Brain
{
    public class Move:Action
    {
        public SharedAnimal Self;
        public SharedEntity Target;

        public override void OnStart()
        {
            Self.Value.SetDestination(Target.Value.transform.position);
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (Self.Value.hasReached)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Running;
            }
        }
    }
}