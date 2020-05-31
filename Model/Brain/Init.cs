using BehaviorDesigner.Runtime.Tasks;

namespace Model.Brain
{
    public class Init:Action
    {
        public SharedAnimal Self;

        public override void OnStart()
        {
            Self.Value = GetComponent<Animal>();
            base.OnStart();
        }
    }
}