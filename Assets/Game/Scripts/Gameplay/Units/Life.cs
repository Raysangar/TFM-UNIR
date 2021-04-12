using UnityEngine;

namespace Game.Gameplay.Units
{
    public class Life : MonoBehaviour
    {
        public System.Action OnDeath;
        public System.Action OnChanged;

        public int Max { get; private set; }
        public int Current { get; private set; }

        public void Init(int maxLife)
        {
            Max = Current = maxLife;
        }

        public void AddDamage(int damage)
        {
            Current -= damage;
            if (Current <= 0)
            {
                Current = 0;
                OnDeath();
            }
            else
            {
                OnChanged?.Invoke();
            }
        }

        public void AddLife(int amount)
        {
            Current = Mathf.Min(Current + amount, Max);
        }
    }
}
