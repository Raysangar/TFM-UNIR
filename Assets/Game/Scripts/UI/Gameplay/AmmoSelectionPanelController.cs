using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Game.Gameplay.Units;

namespace Game.UI.Gameplay
{
    public class AmmoSelectionPanelController : BasePanelController
    {
        [SerializeField] AmmoSelectionWidget ammoSelectionWidgetPrefab;
        [SerializeField] RectTransform ammoSelectionWidgetsParent;

        private PlayerController player;
        private AmmoSelectionWidget[] ammoSelectionWidgets;
        private int lastHoveredAmmo;
        private bool equipLastAmmoHoveredBeforeHiding;
        private bool canEquipOnNextHiding;

        public override void Show()
        {
            int ammoTypesCount = player.Weapon.AmmoTypesCount;
            for (int i = 0; i < ammoTypesCount; ++i)
                ammoSelectionWidgets[i].SetAmmoCounter(player.Weapon.GetAmmoLeft(i), player.Weapon.ClipSize);
            lastHoveredAmmo = player.Weapon.EquippedAmmoIndex;
            EventSystem.current.SetSelectedGameObject(ammoSelectionWidgets[lastHoveredAmmo].gameObject);
            gameObject.SetActive(true);
            IsActive = true;
            canEquipOnNextHiding = true;
        }

        public override void Hide()
        {
            if (canEquipOnNextHiding && equipLastAmmoHoveredBeforeHiding)
                EquipAmmo(lastHoveredAmmo);
            gameObject.SetActive(false);
            IsActive = false;
        }

        public void OnSubmitInput()
        {
            EquipAmmo(lastHoveredAmmo);
            canEquipOnNextHiding = false;
            Hide();
        }

        public void OnJoystickSelectionInput(InputAction.CallbackContext context)
        {
            Quaternion input = context.ReadValue<Quaternion>();
            SetSelectedAmmoOnAngle(input.eulerAngles.z);
        }

        public void OnMouseSelectionInput(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            input.x -= Screen.width * .5f;
            input.y -= Screen.height * .5f;
            float angle = Vector2.Angle(input, new Vector2(0, input.x < 0 ? 1 : -1));
            if (input.x < 0)
                angle += 180;
            SetSelectedAmmoOnAngle(angle);
        }

        private void SetSelectedAmmoOnAngle(float angle)
        {
            lastHoveredAmmo = (int)(angle / (360f / player.Weapon.AmmoTypesCount));
            EventSystem.current.SetSelectedGameObject(ammoSelectionWidgets[lastHoveredAmmo].gameObject);
        }

        public void Init(PlayerController player, bool equipLastAmmoHoveredBeforeHiding)
        {
            this.player = player;
            this.equipLastAmmoHoveredBeforeHiding = equipLastAmmoHoveredBeforeHiding;

            int ammoTypesCount = player.Weapon.AmmoTypesCount;
            ammoSelectionWidgets = new AmmoSelectionWidget[ammoTypesCount];
            for (int i = 0; i < ammoTypesCount; ++i)
            {
                ammoSelectionWidgets[i] = Instantiate(ammoSelectionWidgetPrefab, ammoSelectionWidgetsParent);
                ammoSelectionWidgets[i].Init(player.Weapon.GetAmmoSettings(i), ammoTypesCount, i);
            }
        }

        private void EquipAmmo(int ammoIndex)
        {
            player.Weapon.SetEquippedAmmo(ammoIndex);
        }
    }
}
