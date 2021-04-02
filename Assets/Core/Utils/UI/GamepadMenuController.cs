using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Utils.UI
{
    public class GamepadMenuController : MonoBehaviour
    {

        private GameObject lastSelection;

        public void ForceSelectionTo(GameObject selection)
        {
            lastSelection = selection;
            EventSystem.current.SetSelectedGameObject(selection);
        }

        private void Update()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem.currentSelectedGameObject == null)
            {
                if (lastSelection != null)
                    eventSystem.SetSelectedGameObject(lastSelection);
            }
            else
                lastSelection = eventSystem.currentSelectedGameObject;
        }
    }
}
