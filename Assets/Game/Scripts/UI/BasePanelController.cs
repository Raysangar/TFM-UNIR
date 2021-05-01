using UnityEngine;

namespace Game.UI
{
    public abstract class BasePanelController : MonoBehaviour
    {
        public bool IsActive 
        {
            get => isActive; 
            protected set
            {
                isActive = value;
                if (isActive)
                    gainUiFocusCallback();
                else
                    looseUiFocusCallback();
            }
        }

        private bool isActive;
        private System.Action gainUiFocusCallback;
        private System.Action looseUiFocusCallback;

        public void Init(System.Action gainUiFocusCallback, System.Action looseUiFocusCallback)
        {
            this.gainUiFocusCallback = gainUiFocusCallback;
            this.looseUiFocusCallback = looseUiFocusCallback;
            Hide();
        }

        public abstract void Show();
        public abstract void Hide();
    }
}
