using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Pages.Settings.Sigulation
{
    public partial class SingulationControlPage : ContentPage
    {

        string sessionStatusOnLoad;
        string tagPopulationOnLoad;
        string inventoryStateOnLoad;
        string slFlagStateOnLoading;

        public SingulationControlPage()
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

            SaveSigulationSettings();
        }

        /// <summary>
        /// Event handler of Session picker
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="pickerEventArg">Event Handler</param>
        private void OnSessionPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnSessionPickerChanged Click");
        }


        /// <summary>
        /// Saves the sigulation settings.
        /// </summary>
        internal void SaveSigulationSettings()
        {
            if (Globals.IsInventoryStart)
            {
                return;
            }
            try
            {
                SingulationControl singulationControl = new SingulationControl();
                //session
                switch (sessionPicker.SelectedItem)
                {
                    case ConstantsString.Session0:
                        singulationControl.Session = Session.SESSION_S0;
                        break;
                    case ConstantsString.Session1:
                        singulationControl.Session = Session.SESSION_S1;
                        break;
                    case ConstantsString.Session2:
                        singulationControl.Session = Session.SESSION_S2;
                        break;
                    case ConstantsString.Session3:
                        singulationControl.Session = Session.SESSION_S3;
                        break;
                    default:
                        break;
                }

                //tag population  

                singulationControl.TagPopulation = Int32.Parse(tagPopulationPicker.Items[tagPopulationPicker.SelectedIndex]);

                //sl flag
                switch (slFlagPicker.SelectedItem)
                {
                    case ConstantsString.All:
                        singulationControl.SelectedFlag = SLFlag.SLFLAG_ALL;
                        break;
                    case ConstantsString.Deasserted:
                        singulationControl.SelectedFlag = SLFlag.SLFLAG_DEASSERTED;
                        break;
                    case ConstantsString.Asserted:
                        singulationControl.SelectedFlag = SLFlag.SLFLAG_ASSERTED;
                        break;
                    default:
                        break;
                }

                //inventory
                switch (inventoryStatePicker.SelectedItem)
                {
                    case ConstantsString.StateA:
                        singulationControl.State = InventoryState.INVENTORYSTATE_A;
                        break;
                    case ConstantsString.StateB:
                        singulationControl.State = InventoryState.INVENTORYSTATE_B;
                        break;
                    case ConstantsString.AbFlip:
                        singulationControl.State = InventoryState.INVENTORYSTATE_AB_FLIP;
                        break;
                    default:
                        break;
                }

                SdkHandler.ConnectedReader.Configuration.Antennas.SingulationControl = singulationControl;
                Globals.IsSaveSigulationSettings = true;

            }
            catch (Exception e)
            {
                Globals.IsSaveSigulationSettings = false;
                Console.WriteLine("Exception " + e.Message);
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToSaveData, ConstantsString.MsgActionOk);
            }

        }


        /// <summary>
        /// Updates the user interface.
        /// </summary>
        internal void UpdateUi()
        {

            try
            {
                if (SdkHandler.ConnectedReader != null)
                {

                    SingulationControl singulationControl = SdkHandler.ConnectedReader.Configuration.Antennas.SingulationControl;

                    //session
                    switch (singulationControl.Session)
                    {
                        case Session.SESSION_S0:
                            sessionPicker.SelectedIndex = 0;
                            sessionStatusOnLoad = ConstantsString.Session0;
                            break;
                        case Session.SESSION_S1:
                            sessionPicker.SelectedIndex = 1;
                            sessionStatusOnLoad = ConstantsString.Session1;
                            break;
                        case Session.SESSION_S2:
                            sessionPicker.SelectedIndex = 2;
                            sessionStatusOnLoad = ConstantsString.Session2;
                            break;
                        case Session.SESSION_S3:
                            sessionPicker.SelectedIndex = 3;
                            sessionStatusOnLoad = ConstantsString.Session3;
                            break;
                        default:
                            break;
                    }

                    //Tag
                    switch (singulationControl.TagPopulation.ToString())
                    {
                        case ConstantsString.Tag0:
                            tagPopulationPicker.SelectedIndex = 0;
                            tagPopulationOnLoad = ConstantsString.Tag0;
                            break;
                        case ConstantsString.Tag30:
                            tagPopulationPicker.SelectedIndex = 1;
                            tagPopulationOnLoad = ConstantsString.Tag30;
                            break;
                        case ConstantsString.Tag100:
                            tagPopulationPicker.SelectedIndex = 2;
                            tagPopulationOnLoad = ConstantsString.Tag100;
                            break;
                        case ConstantsString.Tag200:
                            tagPopulationPicker.SelectedIndex = 3;
                            tagPopulationOnLoad = ConstantsString.Tag200;
                            break;
                        case ConstantsString.Tag300:
                            tagPopulationPicker.SelectedIndex = 4;
                            tagPopulationOnLoad = ConstantsString.Tag300;
                            break;
                        case ConstantsString.Tag400:
                            tagPopulationPicker.SelectedIndex = 5;
                            tagPopulationOnLoad = ConstantsString.Tag400;
                            break;
                        case ConstantsString.Tag500:
                            tagPopulationPicker.SelectedIndex = 6;
                            tagPopulationOnLoad = ConstantsString.Tag500;
                            break;
                        case ConstantsString.Tag600:
                            tagPopulationPicker.SelectedIndex = 7;
                            tagPopulationOnLoad = ConstantsString.Tag600;
                            break;
                        default:
                            break;

                    }

                    //Inventory state
                    switch (singulationControl.State)
                    {
                        case InventoryState.INVENTORYSTATE_A:
                            inventoryStatePicker.SelectedIndex = 0;
                            inventoryStateOnLoad = ConstantsString.StateA;
                            break;
                        case InventoryState.INVENTORYSTATE_B:
                            inventoryStatePicker.SelectedIndex = 1;
                            inventoryStateOnLoad = ConstantsString.StateB;
                            break;
                        case InventoryState.INVENTORYSTATE_AB_FLIP:
                            inventoryStatePicker.SelectedIndex = 2;
                            inventoryStateOnLoad = ConstantsString.AbFlip;
                            break;
                        default:
                            break;
                    }

                    //Sl Flag
                    switch (singulationControl.SelectedFlag)
                    {
                        case SLFlag.SLFLAG_ALL:
                            slFlagPicker.SelectedIndex = 0;
                            slFlagStateOnLoading = ConstantsString.All;
                            break;
                        case SLFlag.SLFLAG_DEASSERTED:
                            slFlagPicker.SelectedIndex = 1;
                            slFlagStateOnLoading = ConstantsString.Deasserted;
                            break;
                        case SLFlag.SLFLAG_ASSERTED:
                            slFlagPicker.SelectedIndex = 2;
                            slFlagStateOnLoading = ConstantsString.Asserted;
                            break;
                        default:
                            break;
                    }

                }



            }
            catch (Exception e)
            {
                Globals.IsSaveSigulationSettings = false;
                Console.WriteLine("Exception " + e.Message);
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
            }

        }


        internal bool IsChangeSigulationSettings(string session, string tagPopulation, string inventoryState, string slFlagState)
        {

            if (sessionStatusOnLoad != session || tagPopulationOnLoad != tagPopulation
                || inventoryStateOnLoad != inventoryState || slFlagStateOnLoading != slFlagState)
            {
                Globals.IsSaveSigulationSettings = true;
                return true;
            }
            else
            {
                Globals.IsSaveSigulationSettings = false;
                return false;
            }
        }


    }
}

