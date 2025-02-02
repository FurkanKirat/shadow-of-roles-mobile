using UnityEngine;
using UnityEngine.UI;
using System;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public GameObject loadingScreen;
        public GameObject confirmationDialog;
        public Text confirmationText;
        private Action onConfirmAction;

        void Awake()
        {
            Instance = this;
        }

        public void ShowLoadingScreen(bool show)
        {
            loadingScreen.SetActive(show);
        }

        public void ShowConfirmationDialog(string message, Action onConfirm)
        {
            confirmationDialog.SetActive(true);
            confirmationText.text = message;
            onConfirmAction = onConfirm;
        }

        public void OnConfirm()
        {
            confirmationDialog.SetActive(false);
            onConfirmAction?.Invoke();
        }

        public void OnCancel()
        {
            confirmationDialog.SetActive(false);
        }
    }
}

