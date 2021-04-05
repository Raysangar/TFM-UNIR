using UnityEngine;

namespace Game.Gameplay.PickUps
{
    public class LifePickUp : BasePickUp
    {
        [SerializeField] int amount;

        protected override void Activate(Units.PlayerController player)
        {
            player.Life.AddLife(amount);
        }
    }
}
