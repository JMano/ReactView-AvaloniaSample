using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Sample.Avalonia {

    internal class TabView : ContentControl {

        protected override Type StyleKeyOverride => typeof(ContentControl);

        private MainView mainView;
        private readonly Control parent;

        public TabView(Control parent) {
            this.parent = parent; 
            mainView = new MainView();
            mainView.Focusable = true;
            mainView.ContextMenuButtonClicked += OnContextMenuButtonClicked;
            mainView.WithPlugin<ViewPlugin>().NotifyViewLoaded += viewName => AppendLog(viewName + " loaded");

            Content = mainView;
        }

        private void OnContextMenuButtonClicked() {
            var contextMenu = new ContextMenu();

            var menuItems = new List<MenuItem>();
            for (var i = 0; i < 5; i++) {
                var index = i;
                var menuItem = new MenuItem() { Header = $"Menu {index}", Focusable = true };
                menuItem.Click += (_, _) => AppendLog($"Menu {index} clicked");
                menuItems.Add(menuItem);
            }

            contextMenu.ItemsSource = menuItems;
            contextMenu.Placement = PlacementMode.BottomEdgeAlignedLeft;
            contextMenu.Open(parent);
            contextMenu.Focus();
            AppendLog("Context menu opened");
        }

        public void ShowDevTools() => mainView.ShowDeveloperTools();

        public void ToggleIsEnabled() => mainView.IsEnabled = !mainView.IsEnabled;

        private void AppendLog(string log) {
            Dispatcher.UIThread.Post(() => {
                var status = this.FindControl<TextBox>("status");
                status.Text = DateTime.Now + ": " + log + Environment.NewLine + status.Text;
            });
        }
    }
}
