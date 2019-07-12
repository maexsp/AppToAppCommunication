using System;
using System.Diagnostics;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var packageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName; //To get the package family name of this app which is needed in second app.
            Debug.WriteLine(packageFamilyName);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var protocol = "app2://";   //protocol name of second app that is defined in package.appmanifest of second app.
            var packageFamilyName = "7df22c17-5ec3-4de0-b903-c295473af4a0_syk1cnben41w6";  //This is derived from second app.
            var status = await Launcher.QueryUriSupportAsync(new Uri(protocol), LaunchQuerySupportType.UriForResults, packageFamilyName);
            if (status == LaunchQuerySupportStatus.Available)
            {
                var options = new LauncherOptions
                {
                    TargetApplicationPackageFamilyName = packageFamilyName
                };
                var values = new ValueSet();
                values.Add("UserName", "AppUser ABC");
                values.Add("Message", "This is message from App 1");
                var result = await Launcher.LaunchUriForResultsAsync(new Uri(protocol), options, values);
                if ((result.Status == LaunchUriStatus.Success) && (result.Result != null))
                {
                    var isUser = result.Result["IsUser"] as string;
                    var msg = result.Result["Message"] as string;
                    if (isUser == "true")
                    {
                        var dialog = new MessageDialog(msg, "Success");
                        await dialog.ShowAsync();
                    }
                }
                if ((result.Status == LaunchUriStatus.Success) && (result.Result == null))
                {
                    var dialog = new MessageDialog("Failed to retrieve return value. Result object is null!", "Error");
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
