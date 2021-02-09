﻿using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;

namespace ReactViewControl {

    partial class ReactViewRender : Control {

        private static WindowBase hiddenWindow;

        private static WindowBase GetHiddenWindow() {
            if (hiddenWindow == null) {
                hiddenWindow = new Window() {
                    IsVisible = false,
                    Focusable = false,
                    Title = "Hidden React View Window"
                };
            }
            return hiddenWindow;
        }

        partial void ExtraInitialize() {
            VisualChildren.Add(WebView);
        }

        partial void PreloadWebView() {
            var window = Host?.FindLogicalAncestorOfType<WindowBase>() ?? GetHiddenWindow();
            // initialize browser with full screen size to avoid html measure issues on initial render
            var initialBrowserSizeWidth = (int)window.Screens.All.Max(s => s.WorkingArea.Width * (ExtendedWebView.Settings.OsrEnabled ? 1 : s.PixelDensity));
            var initialBrowserSizeHeight = (int)window.Screens.All.Max(s => s.WorkingArea.Height * (ExtendedWebView.Settings.OsrEnabled ? 1 : s.PixelDensity));
            WebView.InitializeBrowser(window, initialBrowserSizeWidth, initialBrowserSizeHeight);
        }

        protected override void OnGotFocus(GotFocusEventArgs e) {
            e.Handled = true;
            WebView.Focus();
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
            base.OnPropertyChanged(change);

            if (!ExtendedWebView.Settings.OsrEnabled && change.Property == IsEffectivelyEnabledProperty) {
                 DisableInputInteractions(!IsEffectivelyEnabled);
            }
        }

        private static bool IsFrameworkAssemblyName(string name) {
            return name.StartsWith("Avalonia") || name == "mscorlib";
        }
    }
}
