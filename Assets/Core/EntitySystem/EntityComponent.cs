namespace Core.EntitySystem
{
    public class EntityComponent
    {
        protected Entity entity;
        private bool enabled;

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                if (enabled)
                {
                    entity.Enable(this);
                    OnEnabled();
                }
                else
                {
                    entity.Disable(this);
                    OnDisabled();
                } 
            }
        }

        public EntityComponent(Entity entity)
        {
            this.entity = entity;
            entity.Enable(this);
        }

        public virtual void UpdateBehaviour(float deltaTime) { }
        public virtual void OnGameplayPaused() { }
        public virtual void OnGameplayResumed() { }

        protected virtual void OnEnabled() {}
        protected virtual void OnDisabled() {}
    }
}
