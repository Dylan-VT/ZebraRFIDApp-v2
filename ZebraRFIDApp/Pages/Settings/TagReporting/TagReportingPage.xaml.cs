using System;
using System.Collections.Generic;
using ZebraRFIDApp.API;
using ZebraRfidSdk;
using Xamarin.Forms;

namespace ZebraRFIDApp.Pages.Settings.TagReporting
{
    public partial class TagReportingPage : ContentPage
    {
        TagReportConfiguration tagReportConfigurationOnLoading;
        TagReportConfiguration tagReportConfigurationOnSaving;

        bool uniqeTagStausOnLoading;
        string batchModeOnLoading;


        public TagReportingPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateUi();

        }

      

        /// <summary>
        /// Page on Disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Globals.IsInventoryStart)
            {
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToSaveData, ConstantsString.MsgActionOk);
                return;
            }
            SaveConfiguration();

        }

        /// <summary>
        /// Updates the user interface.
        /// </summary>
        public void UpdateUi()
        {
            try
            {
                TagReportConfiguration tagReportConfiguration = SdkHandler.ConnectedReader.Configuration.TagReportConfiguration;
                switchPC.IsToggled = tagReportConfiguration.Pc;
                switchRSSI.IsToggled = tagReportConfiguration.Rssi;
                swichPhase.IsToggled = tagReportConfiguration.Phase;
                swichChannelIndex.IsToggled = tagReportConfiguration.ChannelIdx;
                swichTagSeenCount.IsToggled = tagReportConfiguration.TagSeenCount;


                bool uniqueTagReport = SdkHandler.ConnectedReader.Configuration.UniqueTagReport;
                switchUniqueTag.IsToggled = uniqueTagReport;
                Globals.UniqueTagEnabled = uniqueTagReport;
                uniqeTagStausOnLoading = uniqueTagReport;

                BatchMode batchMode = SdkHandler.ConnectedReader.Configuration.BatchModeConfiguration;
                string batchmodeconfig = "";
                switch (batchMode)
                {
                    case BatchMode.ENABLE:
                        batchmodeconfig = ConstantsString.Enable;
                        batchModePicker.SelectedIndex = 2;
                        break;
                    case BatchMode.DISABLE:
                        batchmodeconfig = ConstantsString.Disable;
                        batchModePicker.SelectedIndex = 0;
                        break;
                    default:
                        batchmodeconfig = ConstantsString.Auto;
                        batchModePicker.SelectedIndex = 1;
                        break;
                }
                batchModeOnLoading = batchmodeconfig.ToUpper();

                tagReportConfigurationOnLoading = tagReportConfiguration;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
            }

        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public void SaveConfiguration()
        {
            try
            {

                if (!IsChangeSettings(tagReportConfigurationOnLoading, uniqeTagStausOnLoading, batchModeOnLoading))
                {
                    TagReportConfiguration tagReportConfiguratio = new TagReportConfiguration();
                    tagReportConfiguratio.Pc = switchPC.IsToggled;
                    tagReportConfiguratio.Rssi = switchRSSI.IsToggled;
                    tagReportConfiguratio.Phase = swichPhase.IsToggled;
                    tagReportConfiguratio.ChannelIdx = swichChannelIndex.IsToggled;
                    tagReportConfiguratio.TagSeenCount = swichTagSeenCount.IsToggled;
                    SdkHandler.ConnectedReader.Configuration.TagReportConfiguration = tagReportConfiguratio;
                    tagReportConfigurationOnSaving = tagReportConfiguratio;

                    bool reportUniqueTag = switchUniqueTag.IsToggled;
                    SdkHandler.ConnectedReader.Configuration.UniqueTagReport = reportUniqueTag;
                    Globals.UniqueTagEnabled = reportUniqueTag;

                    string batchMode = (string)batchModePicker.SelectedItem;

                    if (batchMode.Equals(ConstantsString.Enable, StringComparison.InvariantCultureIgnoreCase))
                    {
                        SdkHandler.ConnectedReader.Configuration.BatchModeConfiguration = BatchMode.ENABLE;
                        Globals.ReaderBatchMode = Globals.BatchModeState.Enable;
                        Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeEnable;

                    }
                    else if (batchMode.Equals(ConstantsString.Disable, StringComparison.InvariantCultureIgnoreCase))
                    {
                        SdkHandler.ConnectedReader.Configuration.BatchModeConfiguration = BatchMode.DISABLE;
                        Globals.ReaderBatchMode = Globals.BatchModeState.Disable;
                        Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeDisable;

                    }
                    else
                    {
                        SdkHandler.ConnectedReader.Configuration.BatchModeConfiguration = BatchMode.AUTO;
                        Globals.ReaderBatchMode = Globals.BatchModeState.Auto;
                        Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeAuto;

                    }
                    Globals.IsSaveTagReportSettings = true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToSaveData, ConstantsString.MsgActionOk);
            }

        }

        internal bool IsChangeSettings(TagReportConfiguration tagReportConfiguration, bool uniqeTagStaus, string batchMode)
        {

            if (Globals.IsInventoryStart)
            {
                return true;
            }
            bool chanelIndex = swichChannelIndex.IsToggled;
            bool pc = switchPC.IsToggled;
            bool TagSeenCount = swichTagSeenCount.IsToggled;
            bool Rssi = switchRSSI.IsToggled;
            bool uniqueTag = switchUniqueTag.IsToggled;
            bool phase = swichPhase.IsToggled;
            string batchModeState = (string)batchModePicker.SelectedItem;


            if (tagReportConfigurationOnLoading.ChannelIdx != chanelIndex
                   || tagReportConfigurationOnLoading.Pc != pc
                   || tagReportConfigurationOnLoading.TagSeenCount != TagSeenCount
                   || tagReportConfigurationOnLoading.Rssi != Rssi
                   || tagReportConfigurationOnLoading.Phase != phase
                   || uniqeTagStausOnLoading != uniqueTag
                   || batchModeOnLoading != batchModeState)
            {
                Globals.IsSaveTagReportSettings = true;
                return false;
            }
            else
            {
                Globals.IsSaveTagReportSettings = false;
                return true;
            }

        }


        /// <summary>
        /// Switch Toggled PC Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchToggledPC(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledPC Click");
        }

        /// <summary>
        /// Switch Toggled RSSI Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchToggledRSSI(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledRSSI Click");
        }

        /// <summary>
        /// Switch Toggled Phase Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchToggledPhase(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledPhase Click");
        }

        /// <summary>
        /// Switch Toggled Chanel index Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchToggledChannelIndex(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledChannelIndex Click");
        }

        /// <summary>
        /// Switch Toggled Tag seen count Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchToggledTagSeenCount(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledTagSeenCount Click");
        }

        /// <summary>
        /// BatchMode Picker Changed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="pickerEventArg">Event Argument</param>
        void OnBatchModePickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnBatchModePickerChanged Click");
        }


        /// <summary>
        /// Switch Toggled Unique Tag Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchToggledUniqueTag(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledUniqueTag Click");
        }

    }
}
