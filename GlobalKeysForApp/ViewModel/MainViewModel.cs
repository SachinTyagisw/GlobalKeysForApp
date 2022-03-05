using GlobalKeysForApp.Annotations;
using GlobalKeysForApp.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GlobalKeysForApp.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public delegate void ApplicationFocusEvent();
        public event ApplicationFocusEvent GetFocusedEvent;

        private HotKey _globalHandler;
        private bool _isGlobalStrokeRegistered;

        private bool _enableGlobalShortcut;
        public bool EnableGlobalShortCut
        {
            get { return _enableGlobalShortcut; }

            set
            {
                _enableGlobalShortcut = value;
                if (value)
                    RegisterGlobalHook();
                else
                    UnRegisterGlobalHook();
                OnPropertyChanged();
            }
        }

        private bool isApplicationFocused;
        public bool IsApplicationFocused
        {
            get
            {
                return isApplicationFocused;
            }
            set
            {
                isApplicationFocused = value;
                OnPropertyChanged();
            }
        }


        private void RegisterGlobalHook()
        {
            //This code is initlizing the hot key and associted the applicaiton with those key stroke: Unmanaged Code
            if (!_isGlobalStrokeRegistered)
            {
                _globalHandler = new HotKey(Key.Space, KeyModifier.Ctrl | KeyModifier.Shift, OnHotKeyHandler);
                _isGlobalStrokeRegistered = true;
            }
        }

        private void OnHotKeyHandler(HotKey obj)
        {
            IsApplicationFocused = false;
            IsApplicationFocused = true;
            if (GetFocusedEvent != null)
            {
                GetFocusedEvent.Invoke();
            }
        }

        public void UnRegisterGlobalHook()
        {
            if (_globalHandler != null)
            {
                _globalHandler.Unregister();
                _globalHandler.Dispose();
                _isGlobalStrokeRegistered = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
