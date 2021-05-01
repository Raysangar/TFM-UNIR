using System.Collections.Generic;
using Core.Utils.Pool;

namespace Core.EntitySystem
{
    public class Entity : PoolObject
    {
        public static System.Action<Entity> OnRemovedFromScene;
        public static System.Action<Entity> OnSpawnedToScene;

        protected List<EntityComponent> components;

        protected virtual void Awake()
        {
            components = new List<EntityComponent>();
        }

        protected virtual void Start()
        {
            OnSpawnedToScene(this);
            foreach (var component in components)
                component.Init(this);
        }

        public virtual void UpdateBehaviour(float deltaTime)
        {
            foreach (var component in components)
                component.UpdateBehaviour(deltaTime);
        }

        public virtual void OnGameplayPaused()
        {
            foreach (var component in components)
                component.OnGameplayPaused();
        }

        public virtual void OnGameplayResumed()
        {
            foreach (var component in components)
                component.OnGameplayResumed();
        }

        protected void RemoveFromScene()
        {
            OnRemovedFromScene(this);
            PoolManager.Instance.Release(this);
        }
    }
}
