namespace Core.EntitySystem
{
    public class EntityComponent
    {
        protected Entity entity;

        public virtual void Init(Entity entity)
        {
            this.entity = entity;
        }

        public virtual void UpdateBehaviour(float deltaTime) { }
        public virtual void OnGameplayPaused() { }
        public virtual void OnGameplayResumed() { }
    }
}
