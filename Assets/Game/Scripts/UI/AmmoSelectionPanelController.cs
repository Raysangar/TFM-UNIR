using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Game.Gameplay.Units;

namespace Game.UI
{
    public class AmmoSelectionPanelController : MonoBehaviour
    {
        public bool IsShowing { get; private set; }

        [SerializeField] AmmoSelectionWidget ammoSelectionWidgetPrefab;
        [SerializeField] RectTransform ammoSelectionWidgetsParent;

        private PlayerController player;
        private AmmoSelectionWidget[] ammoSelectionWidgets;
        private int lastHoveredAmmo;

        public void Show()
        {
            gameObject.SetActive(true);
            IsShowing = true;
            int ammoTypesCount = player.Weapon.AmmoTypesCount;
            for (int i = 0; i < ammoTypesCount; ++i)
                ammoSelectionWidgets[i].SetAmmoCounter(player.Weapon.GetAmmoLeft(i), player.Weapon.ClipSize);
            lastHoveredAmmo = player.Weapon.EquippedAmmoIndex;
            EventSystem.current.SetSelectedGameObject(ammoSelectionWidgets[lastHoveredAmmo].gameObject);
        }

        public void Hide(bool equipLastAmmoHovered = false)
        {
            if (equipLastAmmoHovered)
                EquipAmmo(lastHoveredAmmo);
            gameObject.SetActive(false);
            IsShowing = false;
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

        public void OnSubmitInput()
        {
            OnAmmoIndexSelected(lastHoveredAmmo);
        }

        private void SetSelectedAmmoOnAngle(float angle)
        {
            lastHoveredAmmo = (int)(angle / (360f / player.Weapon.AmmoTypesCount));
            Debug.Log(angle + " " + lastHoveredAmmo);
            EventSystem.current.SetSelectedGameObject(ammoSelectionWidgets[lastHoveredAmmo].gameObject);
        }

        public void Init(PlayerController player)
        {
            this.player = player;
            int ammoTypesCount = player.Weapon.AmmoTypesCount;
            ammoSelectionWidgets = new AmmoSelectionWidget[ammoTypesCount];
            for (int i = 0; i < ammoTypesCount; ++i)
            {
                ammoSelectionWidgets[i] = Instantiate(ammoSelectionWidgetPrefab, ammoSelectionWidgetsParent);
                ammoSelectionWidgets[i].Init(player.Weapon.GetAmmoSettings(i), ammoTypesCount, i);
            }
            Hide();
        }

        private void OnAmmoIndexSelected(int ammoIndex)
        {
            EquipAmmo(ammoIndex);
            Hide();
        }

        private void EquipAmmo(int ammoIndex)
        {
            player.Weapon.SetEquippedAmmo(ammoIndex);
        }
    }
}
