using Avalonia.Controls;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Tabs;

namespace NeoBoxLauncher.Tabs;

public partial class SitesTab : UserControl, ITab {
    private readonly Config Config;

    public SitesTabViewModel ViewModel { get; }

    public SitesTab(SitesTabViewModel viewModel, Config config) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
        Config = config;

        RefreshSites();
    }

    public void SetContext(Tab tab) {
    }

    private void RefreshSites() {
        ViewModel.Sites.Clear();

        foreach (var site in Config.Sites) {
            AddSite(site.Title, site.Url);
        }
    }

    private void AddSite(string title, string url) {
        ViewModel.Sites.Add(new Site {
            Title = title,
            Url = url,
        });
    }
}