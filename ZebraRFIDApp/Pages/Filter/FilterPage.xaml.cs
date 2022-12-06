using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Pages.Filter
{
    /// <summary>
    /// Pre filter page,responsible for tag filtering.
    /// </summary>
    public partial class PreFilterPage : ContentPage
    {
     
        public PreFilterPage()
        {
            InitializeComponent();
            Title = ConstantsString.PreFilters;
            memoryBankPicker.SelectedIndex = ConstantsString.MEMORY_BANK_EPC_INDEX;
            actionPicker.SelectedIndex = ConstantsString.ACTION_INV_A_OR_ASRT_SL_INDEX;
            targetPicker.SelectedIndex = ConstantsString.TARGET_SESSION_0_INDEX;


        }

        /// <summary>
        /// Button clicked event for filter one
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event argument</param>
        void OnButtonClickedFilterOne(object sender, EventArgs args)
        {
            try
            {
                UpdateFilterOneButtonColor();
             
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }

        /// <summary>
        /// Button clicked event for filter two
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event argument</param>
        void OnButtonClickedFilterTwo(object sender, EventArgs args)
        {
            try
            {
                UpdateFilterTwoButtonColor();
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }

        
        /// <summary>
        /// Update the selection color for the filter one button.
        /// </summary>
        void UpdateFilterOneButtonColor()
        {
            buttonFilterOne.BackgroundColor = Color.CornflowerBlue;
            buttonFilterOne.TextColor = Color.White;
            buttonFilterTwo.BackgroundColor = Color.White;
            buttonFilterTwo.TextColor = Color.CornflowerBlue;
            labelFilter.Text = ConstantsString.FILTER_ONE;


        }

        /// <summary>
        /// Update the selection color for the filter two button.
        /// </summary>
        void UpdateFilterTwoButtonColor()
        {
            buttonFilterTwo.BackgroundColor = Color.CornflowerBlue;
            buttonFilterTwo.TextColor = Color.White;
            buttonFilterOne.BackgroundColor = Color.White;
            buttonFilterOne.TextColor = Color.CornflowerBlue;
            labelFilter.Text = ConstantsString.FILTER_TWO;

        }


        /// <summary>
        /// Event handler of memory bank picker
        /// </summary>
        /// <param name="sender">The object responsible for the picker selection event</param>
        /// <param name="pickerEventArg">Event handler</param>
        private void OnMemoryBankPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnMemoryBankPickerChanged ");
           

        }

        /// <summary>
        /// Event handler of target picker
        /// </summary>
        /// <param name="sender">The object responsible for the picker selection event</param>
        /// <param name="pickerEventArg">Event handler</param>
        private void OnTargetPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnTargetPickerChanged ");

        }

        /// <summary>
        /// Event handler of action picker
        /// </summary>
        /// <param name="sender">The object responsible for the picker selection event</param>
        /// <param name="pickerEventArg">Event handler</param>
        private void OnActionPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnActionPickerChanged");

        }

        /// <summary>
        /// Switch toggled for enable filter.
        /// </summary>
        /// <param name="sender">The object responsible for the switch toggled event</param>
        /// <param name="toggledArgs">Event handler</param>
        void SwitchToggledEnableFilter(object sender, ToggledEventArgs toggledArgs)
        {
            System.Diagnostics.Debug.WriteLine(" SwitchToggledEnable Click");
        }



    }


}
