using System.Collections.Generic;
using Core.Utils.Pool;

namespace Core.EntitySystem
{
    public class Entity : PoolObject
    {
        public static System.Action<Entity> OnRemovedFromScene;
        public static System.Action<Entity> OnSpawnedToScene;

        protected List<EntityComponent> enabledComponents;
        protected List<EntityComponent> disabledComponents;

        private bool initialized = false;

        public T GetEntityComponent<T>() where T : EntityComponent
        {
            foreach (var component in enabledComponents)
                if (component is T result)
                    return result;

            foreach (var component in disabledComponents)
                if (component is T result)
                    return result;

            return null;
        }

        protected virtual void Awake()
        {
            enabledComponents = new List<EntityComponent>();
            disabledComponents = new List<EntityComponent>();
        }

        protected virtual void Start()
        {
            initialized = true;
            OnSpawnedToScene(this);
        }

        protected virtual void OnEnable()
        {
            if (initialized)
                OnSpawnedToScene(this);
        }

        public void Enable(EntityComponent component)
        {
            disabledComponents.Remove(component);
            enabledComponents.Add(component);
        }

        public void Disable(EntityComponent component)
        {
            enabledComponents.Remove(component);
            disabledComponents.Add(component);
        }

        public virtual void UpdateBehaviour(float deltaTime)
        {
            foreach (var component in enabledComponents)
                component.UpdateBehaviour(deltaTime);
        }

        public virtual void OnGameplayPaused()
        {
            foreach (var component in enabledComponents)
                component.OnGameplayPaused();
        }

        public virtual void OnGameplayResumed()
        {
            foreach (var component in enabledComponents)
                component.OnGameplayResumed();
        }

        protected void RemoveFromScene()
        {
            OnRemovedFromScene(this);
            PoolManager.Instance.Release(this);
        }
    }
}
