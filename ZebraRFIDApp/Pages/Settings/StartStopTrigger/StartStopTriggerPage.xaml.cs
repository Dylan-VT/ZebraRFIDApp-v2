using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Pages.Settings.StartStopTrigger
{
    public partial class StartStopTriggerPage : ContentPage
    {
        bool isUnableToSaveStartTriggerSettings = false;
        bool isUnableToSaveStopTriggerSettings = false;
        bool isUnableToLoadStartTriggerSettings = false;
        bool isUnableToLoadStopTriggerSettings = false;

        public StartStopTriggerPage()
        {
            InitializeComponent();
            HideStartStopUiElements();


        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateStartTriggerUi();
            UpdateStopTriggerUi();
            if (isUnableToLoadStartTriggerSettings || isUnableToLoadStopTriggerSettings)
            {
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToLoadData, ConstantsString.MsgActionOk);
            }
         
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
            SaveConfigStartTriggerUi();
            SaveConfigStopTrigger();
            if (isUnableToSaveStartTriggerSettings || isUnableToSaveStopTriggerSettings)
            {
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToSaveData, ConstantsString.MsgActionOk);
            }
           
        }

        /// <summary>
        /// Hide all ui elements
        /// </summary>
        void HideStartStopUiElements()
        {
            StartTriggerVisible(false);
            StartPeriodicVisible(false);
            StopTriggerVisble(false);
            StopDurationVisible(false);
            StopTagObservationVisible(false);
            StopNAttemptsVisible(false);
        }

        /// <summary>
        /// Stop picker changed event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="pickerChangedEventArg">Event Argument</param>
        void OnStopTriggerPickerChanged(object sender, EventArgs pickerChangedEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnStopTriggerPickerChanged Click");
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            switch (selectedIndex)
            {
                case 0:
                    ShowStopImmediate();
                    break;
                case 1:
                    ShowStopHandheld();
                    break;
                case 2:
                    ShowStopDuration();
                    break;
                case 3:
                    ShowStopTagObservation();
                    break;
                default:
                    ShowStopNAttempts();
                    break;
            }
     

        }

        /// <summary>
        /// Show ui element for Immediate
        /// </summary>
        void ShowStopImmediate()
        {
            StopTriggerVisble(false);
            StopDurationVisible(false);
            StopTagObservationVisible(false);
            StopNAttemptsVisible(false);
        }

        /// <summary>
        /// Show ui element for Handheld
        /// </summary>
        void ShowStopHandheld()
        {
            StopTriggerVisble(true);
            StopDurationVisible(false);
            StopTagObservationVisible(false);
            StopNAttemptsVisible(false);
        }

        /// <summary>
        /// Show ui element for Duration
        /// </summary>
        void ShowStopDuration()
        {
            StopTriggerVisble(false);
            StopDurationVisible(true);
            StopTagObservationVisible(false);
            StopNAttemptsVisible(false);
        }

        /// <summary>
        /// Show ui element for TagObservation
        /// </summary>
        void ShowStopTagObservation()
        {
            StopTriggerVisble(false);
            StopDurationVisible(false);
            StopTagObservationVisible(true);
            StopNAttemptsVisible(false);
        }

        /// <summary>
        /// Show ui element for NAttempts
        /// </summary>
        void ShowStopNAttempts()
        {
            StopTriggerVisble(false);
            StopDurationVisible(false);
            StopTagObservationVisible(false);
            StopNAttemptsVisible(true);
        }

        /// <summary>
        /// Start pressed/released picker changed event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="pickerChangedEventArg">Event Argument</param>
        void OnStartPressedReleasedPickerChanged(object sender, EventArgs pickerChangedEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnStartPressedReleasedPickerChanged Click");
        }

        /// <summary>
        /// Stop pressed/released picker changed event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="pickerChangedEventArg">Event Argument</param>
        void OnStopPressedReleasedPickerChanged(object sender, EventArgs pickerChangedEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnStopPressedReleasedPickerChanged Click");
        }

        /// <summary>
        /// Stop picker changed event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="pickerChangedEventArg">Event Argument</param>
        void OnStartTriggerPickerChanged(object sender, EventArgs pickerChangedEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnStartTriggerPickerChanged Click");
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            switch (selectedIndex)
            {
                case 0:
                    ShowStartImmediate();
                    break;
                case 1:
                    ShowStartHandheld();
                    break;
                default:
                    ShowStartPeriodic();
                    break;
            }

        }

        /// <summary>
        /// Show Start Immediate UI elements
        /// </summary>
        void ShowStartImmediate()
        {
            StartTriggerVisible(false);
            StartPeriodicVisible(false);
        }

        /// <summary>
        /// Show Start Handheld UI elements
        /// </summary>
        void ShowStartHandheld()
        {
            StartTriggerVisible(true);
            StartPeriodicVisible(false);
        }

        /// <summary>
        /// Show Start Periodic UI elements
        /// </summary>
        void ShowStartPeriodic()
        {
            StartTriggerVisible(false);
            StartPeriodicVisible(true);
        }


        /// <summary>
        /// Visible handling - Start triger ui elements
        /// </summary>
        /// <param name="visible">Visible</param>
        void StartTriggerVisible(bool visible)
        {
            lbTriggerReleased.IsVisible = visible;
            startPressedReleasedPicker.IsVisible = visible;

        }

        /// <summary>
        /// Visible handling - Start periodic ui elements
        /// </summary>
        /// <param name="visible">Visible</param>
        void StartPeriodicVisible(bool visible)
        {
            txtPerodic.IsVisible = visible;
            lbPerodic.IsVisible = visible;

        }

        /// <summary>
        /// Visible handling - Stop trigger ui elements
        /// </summary>
        /// <param name="visible">Visible</param>
        void StopTriggerVisble(bool visible)
        {
            lbStopTriggerReleased.IsVisible = visible;
            stopPressedReleasedPicker.IsVisible = visible;
            lbTimeOut.IsVisible = visible;
            txtTimeout.IsVisible = visible;
        }

        /// <summary>
        /// Visible handling - Stop duration ui elements
        /// </summary>
        /// <param name="visible">Visible</param>
        void StopDurationVisible(bool visible)
        {
            lbStopDuration.IsVisible = visible;
            txtStopDuration.IsVisible = visible;
        }

        /// <summary>
        /// Visible handling - Stop tag observation ui elements
        /// </summary>
        /// <param name="visible">Visible</param>
        void StopTagObservationVisible(bool visible)
        {
            lbStopTagObservation.IsVisible = visible;
            txtStopTagObservation.IsVisible = visible;
            lbObservationTimeOut.IsVisible = visible;
            txtObservationTimeout.IsVisible = visible;
        }

        /// <summary>
        /// Visible handling - Stop N Attempts ui elements
        /// </summary>
        /// <param name="visible">Visible</param>
        void StopNAttemptsVisible(bool visible)
        {
            lbStopNAttemptsTimeout.IsVisible = visible;
            txtStopNAttemptsTimeout.IsVisible = visible;
            lbStopNAttempts.IsVisible = visible;
            txtStopNAttempts.IsVisible = visible;
        }

        /// <summary>
        /// switch Trigger Pressed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchTriggerPressed(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchTriggerPressed Click");
        }

        /// <summary>
        /// switch Trigger Released Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchTriggerReleased(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchTriggerReleased Click");
        }

        /// <summary>
        /// switch Stop Trigger Pressed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchStopTriggerPressed(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchStopTriggerPressed Click");

        }

        /// <summary>
        /// switch Stop Trigger Released Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="toggledArgs">Event Argument</param>
        void SwitchStopTriggerReleased(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchStopTriggerReleased Click");

        }


        /// <summary>
        /// Updates the start trigger user interface.
        /// </summary>
        public void UpdateStartTriggerUi()
        {
            try
            {

                StartTriggerConfiguration startTriggerConfiguration = SdkHandler.ConnectedReader.Configuration.StartTriggerConfiguration;
                if (!startTriggerConfiguration.RepeatMonitoring && !startTriggerConfiguration.StartOnHandheldTrigger)
                {

                    ShowStartImmediate();
                    startPicker.SelectedIndex = ConstantsString.StartTriggerImmediateIndex;


                }
                else if (startTriggerConfiguration.RepeatMonitoring && startTriggerConfiguration.StartOnHandheldTrigger)
                {

                    ShowStartHandheld();
                    startPicker.SelectedIndex = ConstantsString.StartTriggerHandheldIndex;


                    if (startTriggerConfiguration.TriggerType.ToString() == ConstantsString.TriggerTypeRelease)
                    {
                        startPressedReleasedPicker.SelectedIndex = ConstantsString.StartTriggerReleaseIndex;

                    }
                    else
                    {
                        startPressedReleasedPicker.SelectedIndex = ConstantsString.StartTriggerPressIndex;
                    }
                }
                else if (startTriggerConfiguration.RepeatMonitoring && !startTriggerConfiguration.StartOnHandheldTrigger)
                {
                    ShowStartPeriodic();
                    startPicker.SelectedIndex = ConstantsString.StartTriggerPeriodicIndex;
                    txtPerodic.Text = startTriggerConfiguration.StartDelay.ToString();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                isUnableToLoadStartTriggerSettings = true;
            }

        }


        /// <summary>
        /// Updates the stop trigger user interface.
        /// </summary>
        public void UpdateStopTriggerUi()
        {

           try
            {
             
                StopTriggerConfiguration stopTriggerConfiguration = SdkHandler.ConnectedReader.Configuration.StopTriggerConfiguration;
                if (!stopTriggerConfiguration.StopOnTimeout)
                {
                    ShowStopImmediate();
                    stopPicker.SelectedIndex = ConstantsString.StopTriggerImmediateIndex;

                }
                else if (stopTriggerConfiguration.StopOnHandheldTrigger)
                {
                    ShowStopHandheld();
                    stopPicker.SelectedIndex = ConstantsString.StopTriggerReleaseIndex;

                    if (stopTriggerConfiguration.TriggerType.ToString() == ConstantsString.TriggerTypeRelease)
                    {
                        stopPressedReleasedPicker.SelectedIndex = ConstantsString.StopTriggerReleaseIndex;
                    }
                    else
                    {
                        stopPressedReleasedPicker.SelectedIndex = ConstantsString.StopTriggerPressIndex;
                    }
                    txtTimeout.Text = stopTriggerConfiguration.StopTimeout.ToString();

                }
                else if (stopTriggerConfiguration.StopOnTagCount)
                {

                    ShowStopTagObservation();
                    stopPicker.SelectedIndex = ConstantsString.StopTriggerTagObservationIndex;
                    txtStopTagObservation.Text = stopTriggerConfiguration.StopTagCount.ToString();
                    txtObservationTimeout.Text = stopTriggerConfiguration.StopTimeout.ToString();


                }
                else if (stopTriggerConfiguration.StopOnInventoryCount)
                {

                    ShowStopNAttempts();
                    stopPicker.SelectedIndex = ConstantsString.StopTriggerNAttemptsIndex;
                    txtStopNAttemptsTimeout.Text = stopTriggerConfiguration.StopTimeout.ToString(); 
                    txtStopNAttempts.Text = stopTriggerConfiguration.StopInventoryCount.ToString();

                }
                else
                {
                    ShowStopDuration();
                    stopPicker.SelectedIndex = ConstantsString.StopTriggerDurationIndex;
                    txtStopDuration.Text = stopTriggerConfiguration.StopTimeout.ToString();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                isUnableToLoadStopTriggerSettings = true;

            }

        }


        /// <summary>
        /// Saves the config start trigger user interface.
        /// </summary>
        public void SaveConfigStartTriggerUi()
        {
            if (Globals.IsInventoryStart)
            {
                return;
            }

            try
            {
                StartTriggerConfiguration startTriggerConfiguration = new StartTriggerConfiguration();
                startTriggerConfiguration.StartDelay = Int32.Parse(LoadingEntryTextValue(txtPerodic.Text));

                if (startPressedReleasedPicker.SelectedIndex == ConstantsString.StartTriggerPressIndex)
                {
                    startTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;
                }
                else
                {
                    startTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_RELEASE;
                }

                if (startPicker.SelectedIndex == ConstantsString.StartTriggerHandheldIndex)
                {
                    startTriggerConfiguration.StartOnHandheldTrigger = true;

                }
                else
                {
                    startTriggerConfiguration.StartOnHandheldTrigger = false;
                }

                if (startPicker.SelectedIndex == ConstantsString.StartTriggerPeriodicIndex)
                {
                    startTriggerConfiguration.RepeatMonitoring = true;
                }
                else
                {
                    startTriggerConfiguration.RepeatMonitoring = false;
                    if (startPicker.SelectedIndex == ConstantsString.StartTriggerHandheldIndex)
                    {
                        startTriggerConfiguration.RepeatMonitoring = true;
                    }
                }

                SdkHandler.ConnectedReader.Configuration.StartTriggerConfiguration = startTriggerConfiguration;
                Globals.IsSaveTagReportSettings = true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                Globals.IsSaveTagReportSettings = false;
                isUnableToSaveStartTriggerSettings = true;
            }

        }

        /// <summary>
        /// Saves the config stop trigger.
        /// </summary>
        public void SaveConfigStopTrigger()
        {
            if (Globals.IsInventoryStart)
            {
                return;
            }
            try
            {
                StopTriggerConfiguration stopTriggerConfiguration = new StopTriggerConfiguration();

                stopTriggerConfiguration.StopAccessCount = 0;
                stopTriggerConfiguration.StopInventoryCount = 0;
                stopTriggerConfiguration.StopOnAccessCount = false;
                stopTriggerConfiguration.StopOnHandheldTrigger = false;
                stopTriggerConfiguration.StopOnInventoryCount = false;


                stopTriggerConfiguration.StopOnTagCount = false;
                stopTriggerConfiguration.StopOnTimeout = false;
                stopTriggerConfiguration.StopTagCount = 0;
                stopTriggerConfiguration.StopTimeout = 0;
                stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;

                if (stopPicker.SelectedIndex == ConstantsString.StopTriggerImmediateIndex)
                {
                    stopTriggerConfiguration.StopAccessCount = 0;
                    stopTriggerConfiguration.StopInventoryCount = 0;
                    stopTriggerConfiguration.StopOnAccessCount = false;
                    stopTriggerConfiguration.StopOnHandheldTrigger = false;
                    stopTriggerConfiguration.StopOnInventoryCount = false;

                    stopTriggerConfiguration.StopOnTagCount = false;
                    stopTriggerConfiguration.StopOnTimeout = false;
                    stopTriggerConfiguration.StopTagCount = 0;
                    stopTriggerConfiguration.StopTimeout = 0;
                    stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;
                }
                else if (stopPicker.SelectedIndex == ConstantsString.StopTriggerHandheldIndex)
                {
                    stopTriggerConfiguration.StopAccessCount = 0;
                    stopTriggerConfiguration.StopInventoryCount = 0;
                    stopTriggerConfiguration.StopOnAccessCount = false;
                    stopTriggerConfiguration.StopOnHandheldTrigger = true;
                    stopTriggerConfiguration.StopOnInventoryCount = false;

                    stopTriggerConfiguration.StopOnTagCount = false;
                    stopTriggerConfiguration.StopOnTimeout = true;
                    stopTriggerConfiguration.StopTagCount = 0;


                    stopTriggerConfiguration.StopTimeout = Int32.Parse(LoadingEntryTextValue(txtTimeout.Text));

                    if (stopPressedReleasedPicker.SelectedIndex == ConstantsString.StopTriggerPressIndex)
                    {
                        stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;
                    }
                    else
                    {
                        stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_RELEASE;
                    }



                }
                else if (stopPicker.SelectedIndex == ConstantsString.StopTriggerTagObservationIndex)
                {
                    stopTriggerConfiguration.StopAccessCount = 0;
                    stopTriggerConfiguration.StopInventoryCount = 0;
                    stopTriggerConfiguration.StopOnAccessCount = false;
                    stopTriggerConfiguration.StopOnHandheldTrigger = false;
                    stopTriggerConfiguration.StopOnInventoryCount = false;

                    stopTriggerConfiguration.StopOnTagCount = true;
                    stopTriggerConfiguration.StopOnTimeout = true;
                    stopTriggerConfiguration.StopTagCount = Int32.Parse(LoadingEntryTextValue(txtStopTagObservation.Text));
                    stopTriggerConfiguration.StopTimeout = Int32.Parse(LoadingEntryTextValue(txtObservationTimeout.Text));
                    stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;
                }

                else if (stopPicker.SelectedIndex == ConstantsString.StopTriggerNAttemptsIndex)
                {

                    stopTriggerConfiguration.StopAccessCount = 0;
                    stopTriggerConfiguration.StopInventoryCount = Int32.Parse(LoadingEntryTextValue(txtStopNAttempts.Text));
                    stopTriggerConfiguration.StopOnAccessCount = false;
                    stopTriggerConfiguration.StopOnHandheldTrigger = false;
                    stopTriggerConfiguration.StopOnInventoryCount = true;

                    stopTriggerConfiguration.StopOnTagCount = false;
                    stopTriggerConfiguration.StopOnTimeout = true;
                    stopTriggerConfiguration.StopTagCount = 0;
                    stopTriggerConfiguration.StopTimeout = Int32.Parse(LoadingEntryTextValue(txtStopNAttemptsTimeout.Text));
                    stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;
                }

                else
                {
                    stopTriggerConfiguration.StopAccessCount = 0;
                    stopTriggerConfiguration.StopInventoryCount = 0;
                    stopTriggerConfiguration.StopOnAccessCount = false;
                    stopTriggerConfiguration.StopOnHandheldTrigger = false;
                    stopTriggerConfiguration.StopOnInventoryCount = false;

                    stopTriggerConfiguration.StopOnTagCount = false;
                    stopTriggerConfiguration.StopOnTimeout = true;
                    stopTriggerConfiguration.StopTagCount = 0;
                    stopTriggerConfiguration.StopTimeout = Int32.Parse(LoadingEntryTextValue(txtStopDuration.Text));
                    stopTriggerConfiguration.TriggerType = ZebraRfidSdk.TriggerType.TRIGGERTYPE_PRESS;
                }

                SdkHandler.ConnectedReader.Configuration.StopTriggerConfiguration = stopTriggerConfiguration;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                isUnableToSaveStopTriggerSettings = true;
            }


        }

        /// <summary>
        /// Entry text validation
        /// </summary>
        /// <param name="entryValue">Text value</param>
        /// <returns>Valid text</returns>
        private string LoadingEntryTextValue(string entryValue)
        {
            return (entryValue == null) ? "0" : entryValue;
        }
    }
}