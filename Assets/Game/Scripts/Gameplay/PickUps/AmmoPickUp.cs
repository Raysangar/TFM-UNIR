using UnityEngine;

namespace Game.Gameplay
{
    public class AmmoPickUp : BasePickUp
    {
        [SerializeField] int ammoIndex;
        [SerializeField] int amount;

        protected override void Activate(Player.PlayerController player)
        {
            player.Weapon.AddAmmo(ammoIndex, amount);
        }
    }
}
