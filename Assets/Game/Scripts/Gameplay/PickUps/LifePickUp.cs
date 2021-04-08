using UnityEngine;

namespace Game.Gameplay.PickUps
{
    public class LifePickup : BasePickup
    {
        [SerializeField] int amount;

        protected override void Activate(Units.PlayerController player)
        {
            player.Life.AddLife(amount);
        }
    }
}
