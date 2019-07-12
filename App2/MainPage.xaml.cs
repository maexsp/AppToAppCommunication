using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App2
{
    public sealed partial class MainPage : Page
    {
        private ProtocolForResultsActivatedEventArgs pfrArgs;
        private string userName;
        private string msg;
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var packageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName; //To get the package family name of this app which is needed in first app.
            Debug.WriteLine(packageFamilyName);
            Application.Current.Suspending += CurrentOnSuspending;
        }

        private void CurrentOnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            SuccessCallBack();
            deferral.Complete();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pfrArgs = e.Parameter as ProtocolForResultsActivatedEventArgs;
            if (pfrArgs != null)
            {
                userName = pfrArgs.Data["UserName"] as string;
                msg = pfrArgs.Data["Message"] as string;
                Name.Text = userName;
                Message.Text = msg;
            }
        }

        private void ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            SuccessCallBack();
        }

        private void SuccessCallBack()
        {
            if (pfrArgs != null)
            {
                var values = new ValueSet();
                values.Add("Message", "This is message from app 2");
                values.Add("IsUser", "true");
                pfrArgs.ProtocolForResultsOperation.ReportCompleted(values);
            }
        }
    }
}
