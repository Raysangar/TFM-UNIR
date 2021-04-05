using UnityEngine;

namespace Game.Gameplay
{
    public class LifePickUp : BasePickUp
    {
        [SerializeField] int amount;

        protected override void Activate(Player.PlayerController player)
        {
            player.Life.AddLife(amount);
        }
    }
}
