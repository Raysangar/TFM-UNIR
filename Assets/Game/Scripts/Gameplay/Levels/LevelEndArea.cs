using Game.Gameplay.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class LevelEndArea : Pickups.BasePickup
    {
        public System.Action OnPlayerReachedEndOfLevel;

        protected override void Activate(PlayerController player)
        {
            OnPlayerReachedEndOfLevel();
        }
    }
}
