using CustomControls.Popup.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace Samples
{
    [RequireComponent(typeof(UIDocument))]
    public class ControlSample : MonoBehaviour
    {
        private const string Title = "HELLO";
        private const string Message = "Are you here?";
        private const string ConfirmText = "Yeah!";
        private const string CancelText = "Nope";

        private VisualElement _root;
        private Popup _popupControl;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;

            _popupControl = AddPopupControl(_root);
        }

        private Popup AddPopupControl(VisualElement root)
        {
            var popupControl = new Popup();

            popupControl.Show(Title, Message, ConfirmText, CancelText, OnConfirm, OnCancel);

            root.Add(popupControl);

            return popupControl;
        }

        private void OnConfirm()
        {
            _popupControl.Hide();
            _root.Remove(_popupControl);
            Debug.Log("Confirmed");
        }

        private void OnCancel()
        {
            _popupControl.Hide();
            _root.Remove(_popupControl);
            Debug.Log("Canceled");
        }
    }
}