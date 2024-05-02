#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace RedmineClient
{
    public partial class App : Application
    {
        const int WindowWidth = 600;
        const int WindowHeight = 800;
        const int TaskBarHeight = 50;
        public App()
        {
            InitializeComponent();

            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
                var mauiWindow = handler.VirtualView;
                var nativeWindow = handler.PlatformView;
                nativeWindow.Activate();
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                int Width = Preferences.Get("Width", WindowWidth);
                int Height = Preferences.Get("Height", WindowHeight);
                appWindow.Resize(new SizeInt32(Width, Height));

                var screen = DeviceDisplay.MainDisplayInfo;
                var screenX = screen.Width - appWindow.Size.Width;
                var screenY = screen.Height - appWindow.Size.Height - TaskBarHeight;
                int X = Preferences.Get("X", (int)screenX);
                int Y = Preferences.Get("Y", (int)screenY);
                var position = new PointInt32((int)X, (int)Y);
                appWindow.Move(position);

                nativeWindow.Closed += (sender, args) =>
                {
                    Preferences.Set("Width", appWindow.Size.Width);
                    Preferences.Set("Height", appWindow.Size.Height);
                    Preferences.Set("X", appWindow.Position.X);
                    Preferences.Set("Y", appWindow.Position.Y);
                };
#endif
            });

            MainPage = new MainPage();
        }

    }
}
