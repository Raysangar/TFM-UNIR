using System.Collections.Generic;
using Core.Utils.Pool;

namespace Core.EntitySystem
{
    public class EntitiesManager
    {
        private readonly List<Entity> entities;
        private readonly List<Entity> entitiesToRemove;

        public bool Enabled { get; private set; }

        public void Pause()
        {
            Enabled = false;
            foreach (var entity in entities)
                entity.OnGameplayPaused();
        }

        public void Resume()
        {
            Enabled = true;
            foreach (var entity in entities)
                entity.OnGameplayResumed();
        }

        public void ReleaseAllEntities()
        {
            foreach (var entity in entities)
                PoolManager.Instance.Release(entity);
        }

        public EntitiesManager()
        {
            entities = new List<Entity>();
            entitiesToRemove = new List<Entity>();

            Entity.OnSpawnedToScene += OnEntitySpawnedInScene;
            Entity.OnRemovedFromScene += OnEntityRemovedFromScene;
        }

        public void Update(float deltaTime)
        {
            foreach (var entity in entitiesToRemove)
                entities.Remove(entity);
            entitiesToRemove.Clear();

            if (Enabled)
            {
                foreach (var entities in entities)
                    entities.UpdateBehaviour(deltaTime);
            }
        }

        private void OnEntitySpawnedInScene(Entity entity)
        {
            entities.Add(entity);
        }

        private void OnEntityRemovedFromScene(Entity entity)
        {
            entitiesToRemove.Add(entity);
        }
    }
}
