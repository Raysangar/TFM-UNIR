using Core.EntitySystem;
using Action = System.Action;
using Update = System.Action<float>;
using Check = System.Func<bool>;

namespace Core.AI
{
    public class FiniteState
    {
        private readonly Action enterStateBehaviour;
        private readonly Action exitStateBehaviour;
        private readonly Update updateBehaviour;
        private readonly Check[] shouldMoveToState;

        public FiniteState(Action onEnterState, Action onExitState, Update onUpdate, Check[] moveToStateConditions)
        {
            enterStateBehaviour = onEnterState;
            exitStateBehaviour = onExitState;
            updateBehaviour = onUpdate;
            shouldMoveToState = moveToStateConditions;
        }

        public void Start()
        {
            enterStateBehaviour?.Invoke();
        }

        

        public void Update(float deltaTime)
        {
            updateBehaviour?.Invoke(deltaTime);
        }

        public void Finish()
        {
            exitStateBehaviour?.Invoke();
        }

        public (bool, int) ShouldMoveToAnotherState()
        {
            int i = 0;
            while (i < shouldMoveToState.Length && (shouldMoveToState[i] == null || !shouldMoveToState[i]()))
                ++i;
            return (i < shouldMoveToState.Length, i);
        }
    }

    public class FiniteStateMachineComponent : EntityComponent
    {
        private int currentState;
        private readonly FiniteState[] states;

        public FiniteStateMachineComponent(Entity entity, bool startAutomatically, FiniteState[] states) : base(entity)
        {
            this.states = states;
            currentState = 0;

            if (startAutomatically)
                states[currentState].Start();
        }

        public void Reset()
        {
            currentState = 0;
            states[currentState].Start();
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            states[currentState].Update(deltaTime);

            (bool, int) shouldChangeState = states[currentState].ShouldMoveToAnotherState();
            if (shouldChangeState.Item1)
            {
                states[currentState].Finish();
                currentState = shouldChangeState.Item2;
                states[currentState].Start();
            }
        }
    }
}
