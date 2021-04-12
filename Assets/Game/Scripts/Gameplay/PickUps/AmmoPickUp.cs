using UnityEngine;
using Game.Gameplay.Units;

namespace Game.Gameplay.Pickups
{
    public class AmmoPickup : BasePickup
    {
        [SerializeField] int ammoIndex;
        [SerializeField] int amount;

        protected override void Activate(PlayerController player)
        {
            player.Weapon.AddAmmo(ammoIndex, amount);
        }
    }
}
