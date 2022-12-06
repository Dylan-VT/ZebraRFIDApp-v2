using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Pages.Settings.Antenna
{
    /// <summary>
    /// Antenna page which responsible for get/set tari,power level and linked profile 
    /// </summary>
    public partial class AntennaPage : ContentPage
    {

        private string[] linkProfileArray = { ConstantsString.LinkProfile1, ConstantsString.LinkProfile2, ConstantsString.LinkProfile3, ConstantsString.LinkProfile4, ConstantsString.LinkProfile5, ConstantsString.LinkProfile6
        ,ConstantsString.LinkProfile7, ConstantsString.LinkProfile8 ,ConstantsString.LinkProfile9, ConstantsString.LinkProfile10
        ,ConstantsString.LinkProfile11, ConstantsString.LinkProfile12 ,ConstantsString.LinkProfile13, ConstantsString.LinkProfile14,
        ConstantsString.LinkProfile15,ConstantsString.LinkProfile16, ConstantsString.LinkProfile17 ,ConstantsString.LinkProfile18, ConstantsString.LinkProfile19,
        ConstantsString.LinkProfile20, ConstantsString.LinkProfile21 ,ConstantsString.LinkProfile22, ConstantsString.LinkProfile23
        ,ConstantsString.LinkProfile24, ConstantsString.LinkProfile25 ,ConstantsString.LinkProfile26, ConstantsString.LinkProfile27,
        ConstantsString.LinkProfile28, ConstantsString.LinkProfile29 ,ConstantsString.LinkProfile30, ConstantsString.LinkProfile31,
        ConstantsString.LinkProfile32, ConstantsString.LinkProfile33 ,ConstantsString.LinkProfile34, ConstantsString.LinkProfile35,
        ConstantsString.LinkProfile36
        };

        /// <summary>
        /// Antenna page class constructor 
        /// </summary>
        public AntennaPage()
        {
            InitializeComponent();
            Title = ConstantsString.TitleAntenna;
           
        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetAntennnaConfiguration();
            SetKeyboardType();
        }

        /// <summary>
        ///  Page on disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SetAntennnaConfiguration();
        }

        /// <summary>
        /// On item click listner for link profile 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Event Argument</param>
        private void OnItemClickListnerLinkedProfile(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LinkedProfilePage());
        }

        /// <summary>
        /// Get antenna configuration
        /// </summary>
        private void GetAntennnaConfiguration()
        {
            if (Globals.IsLinkProfilePageAppear)
            {
                lbLinkProfile.Text = linkProfileArray[Globals.SelectedLinkProfileNameIndex];
            }
            else
            {
                if (SdkHandler.ConnectedReader != null)
                {
                    AntennaConfiguration antennnaConfig = SdkHandler.ConnectedReader.Configuration.Antennas.AntennaConfiguration;
                    txtPowerLevel.Text = antennnaConfig.AntennaPower.ToString();
                    txtTari.Text = antennnaConfig.Tari.ToString();
                    lbLinkProfile.Text = linkProfileArray[antennnaConfig.LinkProfile];
                    Globals.SelectedLinkProfileNameIndex = antennnaConfig.LinkProfile;

                }
                else
                {
                    DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToSaveData, ConstantsString.MsgActionOk);
                }
            }
            

        }

        /// <summary>
        /// Set antenna configuration
        /// </summary>
        private void SetAntennnaConfiguration()
        {
            try
            {
                if (SdkHandler.ConnectedReader != null)
                {
                    AntennaConfiguration antennnaConfig = new AntennaConfiguration();
                    antennnaConfig.AntennaPower = Int16.Parse(txtPowerLevel.Text);
                    antennnaConfig.LinkProfile = Globals.SelectedLinkProfileNameIndex;
                    antennnaConfig.Tari = Int16.Parse(txtTari.Text);
                    antennnaConfig.DoSelect = true;

                    SdkHandler.ConnectedReader.Configuration.Antennas.AntennaConfiguration = antennnaConfig;

                }
                else
                {
                    DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToCommunicate, ConstantsString.MsgActionOk);
                }

            }
            catch(Exception exception)
            {
                Console.WriteLine("Exception ", exception.Message);
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToSaveData, ConstantsString.MsgActionOk);
            }
          
        }

        /// <summary>
        /// Set keyboard type for textfiled
        /// </summary>
        private void SetKeyboardType()
        {
            txtTari.Keyboard = Keyboard.Numeric;
            txtPowerLevel.Keyboard = Keyboard.Numeric;
        }
    }
}
