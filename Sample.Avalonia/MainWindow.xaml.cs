using System.Collections;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace Sample.Avalonia {

    internal class MainWindow : Window {

        private int counter = 1;

        private TabControl tabs;

        public MainWindow() {
            AvaloniaXamlLoader.Load(this);

            tabs = this.FindControl<TabControl>("tabs");
            CreateTab();
            
#if DEBUG
            this.AttachDevTools(new KeyGesture(Key.F5));
#endif
        }

        public void CreateTab() {
            var tabItem = new TabItem() { Header = "View " + counter };
            tabItem.Content = new TabView(tabItem);
            ((IList)tabs.Items).Add(tabItem);
            counter++;
        }

        private TabView SelectedView => (TabView) tabs.SelectedContent;

        private void OnNewTabClick(object sender, RoutedEventArgs e) => CreateTab();

        private void OnToggleThemeStyleSheetMenuItemClick(object sender, RoutedEventArgs e) => Settings.IsLightTheme = !Settings.IsLightTheme;

        private void OnShowDevToolsMenuItemClick(object sender, RoutedEventArgs e) => SelectedView.ShowDevTools();

        private void OnToggleIsEnabledMenuItemClick(object sender, RoutedEventArgs e) => SelectedView.ToggleIsEnabled();
    }
}