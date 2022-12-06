using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Pages.LocateTag
{
    public partial class LocateTagPage : ContentPage
    {
        Readers readerManager;

        public LocateTagPage()
        {
            InitializeComponent();
            readerManager = SdkHandler.GetInstance().ReaderManager;
            if (SdkHandler.ConnectedReader != null)
            {
                SdkHandler.ConnectedReader.ProximityPercent += ReaderManager_ProximityPercent;
                
                
            }
        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateUI();



        }

        /// <summary>
        /// update ui
        /// </summary>
        void UpdateUI()
        {
            if (Globals.SelectedTagObject != null)
            {
                txtTagPattern.Text = Globals.SelectedTagObject.Id;
                

                if (btnLocateTagStart.Text.Equals("STOP"))
                {
                    precentageBoxView.HeightRequest = 0;
                    lbPrecentage.Text = "0%";
                }

              
                btnLocateTagStart.Text = (Globals.IsTagLocatingStart) ? ConstantsString.Stop : ConstantsString.Start;
            }
        }

        /// <summary>
        /// OnButtonClickedResetDefault
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Event Argument</param>
        async void OnButtonClickedStartLocateTag(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickedStartLocateTag Click");

            try
            {
                if (SdkHandler.ConnectedReader != null)
                {
                    
                    if (!string.IsNullOrEmpty(txtTagPattern.Text))
                    {
                        if (btnLocateTagStart.Text.Equals("START"))
                        {
                            SdkHandler.ConnectedReader.Actions.TagLocate.Start(txtTagPattern.Text);
                            btnLocateTagStart.Text = ConstantsString.Stop;
                            Globals.IsTagLocatingStart = true;

                        }
                        else
                        {
                            SdkHandler.ConnectedReader.Actions.TagLocate.Stop();
                            btnLocateTagStart.Text = ConstantsString.Start;
                            Globals.IsTagLocatingStart = false;
                        }
                    }
                    else
                    {
                        await DisplayAlert(ConstantsString.Msg, ConstantsString.MsgPleaseTag, ConstantsString.MsgActionOk);

                    }

                }
                else
                {
                    await DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);


                }


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
                await DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToPerformStartStopTagLocating, ConstantsString.MsgActionOk);

            }



        }



        /// <summary>
        /// Readers the manager proximity percent.
        /// </summary>
        /// <param name="proximityPrecent">Proximity precent.</param>
        void ReaderManager_ProximityPercent(int proximityPrecent)
        {

            System.Diagnostics.Debug.WriteLine(" ReaderManager_ProximityPercent " + proximityPrecent);
            UpdateTagIndicatorUI(proximityPrecent);

        }

        /// <summary>
        /// Update indicator bar color
        /// </summary>
        /// <param name="precentage">Precentage value</param>
        void UpdateTagIndicatorUI(int precentage)
        {
            int tabBarHeight = Convert.ToInt32(precentageBackgroundBoxView.Height);

            Device.BeginInvokeOnMainThread(() =>
            {
                precentageBoxView.VerticalOptions = LayoutOptions.End;
                precentageBoxView.BackgroundColor = Color.Blue;

                int precetageHeight = (int)(((precentage * tabBarHeight) / 100));
                precentageBoxView.HeightRequest = precetageHeight;

                lbPrecentage.Text = string.Format("{0} %", precentage);
            });
        }

    }

}

