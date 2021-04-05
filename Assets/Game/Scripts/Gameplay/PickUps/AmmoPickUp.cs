using UnityEngine;

namespace Game.Gameplay.PickUps
{
    public class AmmoPickUp : BasePickUp
    {
        [SerializeField] int ammoIndex;
        [SerializeField] int amount;

        protected override void Activate(Units.PlayerController player)
        {
            player.Weapon.AddAmmo(ammoIndex, amount);
        }
    }
}
