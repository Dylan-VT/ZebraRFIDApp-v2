using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Pages.About
{
    /// <summary>
    /// AboutPage
    /// </summary>
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Title = ConstantsString.About;
#if __ANDROID__
            if (SdkHandler.ConnectedReader != null)
            {
                lbSdkVersion.IsVisible = true;
                lbSdkVersion.Text = ConstantsString.SDKVersion + SdkHandler.GetInstance().Version;

            }
            else
            {
                lbSdkVersion.IsVisible = false;
            }
#endif



#if __IOS__
            lbSdkVersion.Text = ConstantsString.SDKVersion + SdkHandler.GetInstance().Version;
#endif
            lbAppVersion.Text = ConstantsString.ApplicationVersion + AppInfo.VersionString;
            lbCopyright.Text = ConstantsString.CopyrightMsg;
        }
    }
}
