using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace CustomControls.Popup.Runtime
{
    public class Popup : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Popup>
        {
        }

        public event Action OnConfirmed;
        public event Action OnCanceled;

        private Button _confirmButton;
        private Button _cancelButton;
        private Label _titleLabel;
        private Label _messageLabel;

        private const string StyleSheetPath = "Styles/PopupControlStyleSheet";
        private const string ContainerClassName = "popup_container";
        private const string ControlClassName = "popup_control";
        private const string HorizontalContainerClassName = "horizontal_container";
        private const string TitleClassName = "popup_title";
        private const string MessageClassName = "popup_message";
        private const string ButtonClassName = "popup_button";
        private const string ButtonCancelClassName = "popup_button_cancel";
        private const string ButtonConfirmClassName = "popup_button_confirm";

        private const string DefaultPopupTitle = "Title";
        private const string DefaultPopupMassage = "Massage";
        private const string DefaultConfirmText = "Yes";
        private const string DefaultCancelText = "No";

        public Popup()
        {
            styleSheets.Add(Resources.Load<StyleSheet>(StyleSheetPath));

            AddToClassList(ContainerClassName);

            var popupControl = new VisualElement();
            popupControl.AddToClassList(ControlClassName);
            hierarchy.Add(popupControl);

            AddTitleParts(popupControl);
            AddMessageParts(popupControl);
            AddButtonParts(popupControl);
        }


        public void Show(
            string title, string message, string confirmText, string cancelText, Action onConfirm,
            Action onCancel = null)
        {
            OnConfirmed = onConfirm;
            OnCanceled = onCancel;

            _titleLabel.text = title;
            _messageLabel.text = message;

            _confirmButton.text = confirmText;
            _cancelButton.text = cancelText;

            _confirmButton.style.display = onConfirm == null ? DisplayStyle.None : DisplayStyle.Flex;
            _cancelButton.style.display = onCancel == null ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public void Hide()
        {
            OnConfirmed = null;
            OnCanceled = null;
        }

        private void AddButtonParts(VisualElement popupControl)
        {
            var buttonsHorizontalContainer = new VisualElement();
            buttonsHorizontalContainer.AddToClassList(HorizontalContainerClassName);
            popupControl.Add(buttonsHorizontalContainer);

            _cancelButton = new Button
            {
                text = DefaultCancelText
            };

            _cancelButton.AddToClassList(ButtonClassName);
            _cancelButton.AddToClassList(ButtonCancelClassName);

            _confirmButton = new Button
            {
                text = DefaultConfirmText
            };

            _confirmButton.AddToClassList(ButtonClassName);
            _confirmButton.AddToClassList(ButtonConfirmClassName);

            buttonsHorizontalContainer.Add(_cancelButton);
            buttonsHorizontalContainer.Add(_confirmButton);

            _cancelButton.clicked += () => OnCanceled?.Invoke();
            _confirmButton.clicked += () => OnConfirmed?.Invoke();
        }

        private void AddMessageParts(VisualElement popupControl)
        {
            var messageHorizontalContainer = new VisualElement();
            messageHorizontalContainer.AddToClassList(HorizontalContainerClassName);
            popupControl.Add(messageHorizontalContainer);

            _messageLabel = new Label
            {
                text = DefaultPopupMassage
            };

            _messageLabel.AddToClassList(MessageClassName);
            messageHorizontalContainer.Add(_messageLabel);
        }

        private void AddTitleParts(VisualElement popupControl)
        {
            var titleHorizontalContainer = new VisualElement();
            titleHorizontalContainer.AddToClassList(HorizontalContainerClassName);
            popupControl.Add(titleHorizontalContainer);

            _titleLabel = new Label
            {
                text = DefaultPopupTitle
            };

            _titleLabel.AddToClassList(TitleClassName);
            titleHorizontalContainer.Add(_titleLabel);
        }
    }
}