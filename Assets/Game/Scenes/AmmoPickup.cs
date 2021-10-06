using UnityEngine;
using Game.Gameplay.Units;

namespace Game.Gameplay.Pickups
{
    public class AmmoPickup : BasePickup
    {
        [SerializeField] int ammoIndex;
        [SerializeField] int amount;
        [SerializeField] GameObject[] renderersPerAmmoIndex;
        

    

        protected override void Activate(PlayerController player)
        {
            player.Weapon.AddAmmo(ammoIndex, amount);
        }

        protected override void OnEnable()
        {
            for (int i = 0; i < renderersPerAmmoIndex.Length; ++i)
                renderersPerAmmoIndex[i].SetActive(i == ammoIndex);
        }
    
    }
}
