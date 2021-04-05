using UnityEngine;

namespace Game.Gameplay
{
    public class Life : MonoBehaviour
    {
        public System.Action OnDeath;

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
        }

        public void AddLife(int amount)
        {
            Current = Mathf.Min(Current + amount, Max);
        }
    }
}
