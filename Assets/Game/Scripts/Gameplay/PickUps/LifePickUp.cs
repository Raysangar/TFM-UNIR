using UnityEngine;
using Game.Gameplay.Units;

namespace Game.Gameplay.Pickups
{
    public class LifePickup : BasePickup
    {
        [SerializeField] int amount;

        protected override void Activate(PlayerController player)
        {
            player.Life.AddLife(amount);
        }
    }
}
