using System;

using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRFIDApp.Model;
using ZebraRFIDApp.Pages.ReaderList;
using ZebraRFIDApp.Pages.Settings.Antenna;
using ZebraRFIDApp.Pages.Settings.Sigulation;
using ZebraRFIDApp.Pages.Settings.StartStopTrigger;
using ZebraRFIDApp.Pages.Settings.TagReporting;

namespace ZebraRFIDApp.Pages.Settings
{
    /// <summary>
    /// Setting page responsible for navigate to  reader list and rfid related settings
    /// </summary>
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            Title = ConstantsString.Settings;
            BindingContext = new SettingsVewModel();

        }


        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Globals.IsLinkProfilePageAppear = false;
        }

        /// <summary>
        /// Select setting menu item in listview
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="tappedEventArg">Event argument</param>
        private void OnItemSelected(Object sender, ItemTappedEventArgs tappedEventArg)
        {

            int selectedIndex = tappedEventArg.ItemIndex;

            switch (selectedIndex)
            {
                case 0:
                    Navigation.PushAsync(new ReaderListPage());
                    break;
                case 2:
                    if (SdkHandler.ConnectedReader != null)
                    {
                        if (Globals.IsInventoryStart)
                        {
                            DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
                            return;
                        }
                        Navigation.PushAsync(new AntennaPage());
                    }
                    else
                    {
                        DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);
                    }

                    break;
                case 3:

                    if (SdkHandler.ConnectedReader != null)
                    {
                        if (Globals.IsInventoryStart)
                        {
                            DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
                            return;
                        }
                        Navigation.PushAsync(new SingulationControlPage());
                    }
                    else
                    {
                        DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);
                    }

                    break;
                case 4:

                    if (SdkHandler.ConnectedReader != null)
                    {
                        if (Globals.IsInventoryStart)
                        {
                            DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
                            return;
                        }
                        Navigation.PushAsync(new StartStopTriggerPage());
                    }
                    else
                    {
                        DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);
                    }

                    break;
                case 5:

                    if (SdkHandler.ConnectedReader != null)
                    {
                        if (Globals.IsInventoryStart)
                        {
                            DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
                            return;
                        }
                        Navigation.PushAsync(new TagReportingPage());
                    }
                    else
                    {
                        DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);
                    }

                    break;
                default:
                    DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNotSupported, ConstantsString.MsgActionOk);
                    break;
            }

        }
    }
}
