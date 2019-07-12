# UWP App-To-App Communication

This reproduces a bug/limitation by UWP App-To-App communication when starting a client app and no result is returned by using:
```
Windows.System.Launcher.LaunchUriForResultsAsync(...)
```
API: https://docs.microsoft.com/de-de/windows/uwp/launch-resume/how-to-launch-an-app-for-results 

Note: This also does not work using Xamarin.Forms UWP

The Master-App opens a Client App. When closing the Client App manually by ALT+F4 or Window Close Button the returned Result (ValueSet) from the Client App is NULL even if the Client App returns explicitly the ValueSet result in the Suspending event successfully using ProtocolForResultsOperation.ReportCompleted(values): 

Suspending UWP API:
https://docs.microsoft.com/en-us/windows/uwp/launch-resume/suspend-an-app

See also General API overview:
https://slideplayer.com/slide/7501828/

```
Application.Current.Suspending += CurrentOnSuspending;

private void CurrentOnSuspending(object sender, SuspendingEventArgs e)
{
    var deferral = e.SuspendingOperation.GetDeferral();
    SuccessCallBack();
    deferral.Complete();
}

private void SuccessCallBack()
{
    if (protocolForResultsActivatedEventArgs != null)
    {
        var values = new ValueSet();
        values.Add("Message", "This is message from app 2");
        values.Add("IsUser", "true");
        protocolForResultsActivatedEventArgs.ProtocolForResultsOperation.ReportCompleted(values);
    }
}
```

![Alt text](AppToAppInUWP.gif?raw=true "UWP Demo App to App Communication")

Postet to https://wpdev.uservoice.com/forums/110705-universal-windows-platform/suggestions/38129332-uwp-app-to-app-communication-via-launcher-launchur  
