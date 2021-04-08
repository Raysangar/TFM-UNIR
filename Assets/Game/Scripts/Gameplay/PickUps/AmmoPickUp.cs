using UnityEngine;

namespace Game.Gameplay.PickUps
{
    public class AmmoPickup : BasePickup
    {
        [SerializeField] int ammoIndex;
        [SerializeField] int amount;

        protected override void Activate(Units.PlayerController player)
        {
            player.Weapon.AddAmmo(ammoIndex, amount);
        }
    }
}
