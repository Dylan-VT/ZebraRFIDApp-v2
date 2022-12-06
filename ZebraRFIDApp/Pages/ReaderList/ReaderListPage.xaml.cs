using System;
using System.Collections.Generic;
using ZebraRfidSdk;
using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRFIDApp.Model;
using System.Threading.Tasks;

namespace ZebraRFIDApp.Pages.ReaderList
{
    /// <summary>
    /// ReaderListPage
    /// </summary>
    public partial class ReaderListPage : ContentPage
    {
        Readers readerManager;
        ReaderModel selectedReader;
        List<ReaderModel> customReaderList = new List<ReaderModel>();

        public ReaderListPage()
        {
            InitializeComponent();
            Title = ConstantsString.ReaderList;
            readerManager = SdkHandler.GetInstance().ReaderManager;
            readerManager.Connected += ReaderConnectedEvent;
            readerManager.Disconnected += ReaderManager_Disconnected;
            readerManager.Appeared += ReaderManager_Appeared;
            readerManager.Disappeared += ReaderManager_Disappeared;

            this.lstReaders.ItemsSource = null;
            customReaderList = SdkHandler.GetActiveReaderList();
            lstReaders.ItemsSource = customReaderList;
            lbConnectingStatus.IsVisible = false;



        }

        /// <summary>
        /// Select a rfid reader from listview
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="tappedEventArg">Event argument</param>
        private async void OnItemSelected(Object sender, ItemTappedEventArgs tappedEventArg)
        {

            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }

            selectedReader = (ReaderModel)tappedEventArg.Item;

            if (Globals.ConnectedReader == null && selectedReader != null)
            {
                try
                {


                    await Task.Run(() => ShowActivityIndicator())
                                           .ContinueWith((connectingToScanner) =>
                                           {
                                               customReaderList[tappedEventArg.ItemIndex].ReaderObject.Connect();

                                           });


                }

                catch (Exception e)
                {
                    Console.WriteLine("Exception " + e.Message);
                    HideLoadingIndicator();
                    await DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToCommunicate, ConstantsString.MsgActionOk);


                }
            }
            else
            {
                try
                {

                        customReaderList[tappedEventArg.ItemIndex].ReaderObject.Disconnect();

                }

                catch (Exception e)
                {
                    Console.WriteLine("Exception " + e.Message);
                    HideLoadingIndicator();
                    await DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToCommunicate, ConstantsString.MsgActionOk);



                }
            }

        }


        /// <summary>
        /// Hide activity indicator
        /// </summary>
        protected void HideLoadingIndicator()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                lbConnectingStatus.IsVisible = false;
                listIndicator.IsRunning = false;
                listIndicator.IsVisible = false;

            });
        }

        /// <summary>
        /// Show activity indicator
        /// </summary>
        protected void ShowActivityIndicator()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                listIndicator.IsRunning = true;
                listIndicator.IsVisible = true;
                lbConnectingStatus.IsVisible = true;

            });

        }

        /// <summary>
        /// Change the color of lable  in list view
        /// </summary>
        /// <param name="readerModel">Reader Model</param>
        /// <param name="color">Prefer color</param>
        void changeTextColorInListView(ReaderModel readerModel, Color color)
        {
            if (readerModel != null)
            {
                foreach (var item in customReaderList)
                {
                    if (item.ReaderName == readerModel.ReaderName)
                    {
                        item.TextColor = color;
                    }
                    else
                    {
                        item.TextColor = Color.Black;
                    }


                }
            }

        }

        /// <summary>
        /// Adds to global reader list.
        /// adding new readers to global reader list
        /// </summary>
        /// <param name="availableReader">Available reader object.</param>
        private void addToGlobalReaderList(Reader availableReader)
        {
            Reader alreadyAvailableReader = SdkHandler.ReaderList.Find(reader => reader.Id == availableReader.Id);
            if (alreadyAvailableReader == null)
            {
                SdkHandler.ReaderList.Add(availableReader);
            }
        }

        /// <summary>
        /// Event handler of  appeared event.
        /// </summary>
        /// <param name="reader">Available reader.</param>
        private void ReaderAppearedEvent(Reader reader)         {
            Console.WriteLine("Reader appear " + reader.Name);

        }

        /// <summary>
        /// Event handler of reader connected event.
        /// </summary>
        /// <param name="reader">Available reader.</param>
        private void ReaderConnectedEvent(Reader reader)
        {

            Device.BeginInvokeOnMainThread(() =>
            {

                Console.WriteLine("ReaderConnectedEvent Reader " + reader.Name);
                Globals.ConectStatus = true;
                Globals.ConcetedID = reader.Id;
                Globals.ConnectedReader = reader;
                if (Globals.ConnectedReader != null)
                {

                    SdkHandler.UpdateReaderConnectionStatus(Globals.ConnectedReader.Name, ConstantsString.CheckMark, true, ConstantsString.Connected);

                }
                this.lstReaders.ItemsSource = null;
                customReaderList = SdkHandler.GetReaderList();
                lstReaders.ItemsSource = customReaderList;
                SdkHandler.ConnectedReader = reader;

                changeTextColorInListView(selectedReader, Color.Green);
                HideLoadingIndicator();
                SetRegulatoryConfiguration(reader);
            });



        }

        /// <summary>
        /// Event handler of Reader Disconnected event
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        private void ReaderManager_Disconnected(int readerID)
        {
            Console.WriteLine("ReaderManager_Disconnected Reader List " + readerID);
            Globals.ConcetedID = 0;
            Globals.ConectStatus = false;
            if (Globals.ConnectedReader != null)
            {
                SdkHandler.UpdateReaderConnectionStatus(Globals.ConnectedReader.Name, ConstantsString.UnCheckMark, false, ConstantsString.Empty);

            }
            Globals.ConnectedReader = null;
            SdkHandler.ConnectedReader = null;
            this.lstReaders.ItemsSource = null;
            customReaderList = SdkHandler.GetReaderList();
            lstReaders.ItemsSource = customReaderList;
            changeTextColorInListView(selectedReader, Color.Black);
            SdkHandler.ClearTagataList();
            Globals.IsInventoryStart = false;
            HideLoadingIndicator();

        }

        /// <summary>
        /// Event handler of reader disconnected event.
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        private void ReaderDisconnectedEvent(int readerID)
        {
            Console.WriteLine("ReaderDisconnectedEvent Reader " + readerID);
            Globals.ConcetedID = 0;
            Globals.ConectStatus = false;

        }

        /// <summary>
        /// Event handler of reader disappeared event.
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        private void ReaderDisappearedEvent(int readerID)
        {
            Console.WriteLine("ReaderDisappearedEvent Reader " + readerID);


            try
            {
                this.lstReaders.ItemsSource = null;
                customReaderList = SdkHandler.GetReaderList();
                lstReaders.ItemsSource = customReaderList;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }

        }

        /// <summary>
        /// Event handler of Reader disappear event
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        private void ReaderManager_Disappeared(int readerID)
        {

            try
            {
                this.lstReaders.ItemsSource = null;
                customReaderList = SdkHandler.GetReaderList();
                lstReaders.ItemsSource = customReaderList;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }



        }
        /// <summary>
        ///  Event handler of Reader appeared event
        /// </summary>
        /// <param name="readerInfo">Reader info.</param>
        private void ReaderManager_Appeared(Reader readerInfo)
        {
            try
            {

                this.lstReaders.ItemsSource = null;
                customReaderList = SdkHandler.GetReaderList();
                lstReaders.ItemsSource = customReaderList;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }


        }

        /// <summary>
        /// Set the regulatory configuration.
        /// </summary>
        /// <param name="availableReader">Available reader.</param>
        internal void SetRegulatoryConfiguration(Reader availableReader)
        {
            try
            {
                if (availableReader != null)
                {
                    if (Globals.ReaderBatchMode != Globals.BatchModeState.BatchModeConnected
                                  || Globals.ReaderBatchMode != Globals.BatchModeState.Enable)
                    {
                        RegulatoryConfig config = availableReader.Configuration.RegulatoryConfig;

                        if (config.RegionCode == ConstantsString.RegionCodeNA)
                        {
                            List<RegionInformation> supportedRegions = availableReader.SupportedRegions;

                            foreach (RegionInformation supportRegion in supportedRegions)
                            {
                                if (ConstantsString.RegionCodeUSA == supportRegion.RegionCode)
                                {
                                    config.RegionCode = ConstantsString.RegionCodeUSA;
                                    config.EnableChannels = new object[0];
                                    config.HoppingConfig = HoppingConfig.HOPPINGCONFIG_DEFAULT;
                                    break;
                                }
                                else if (ConstantsString.RegionCodeGBR == supportRegion.RegionCode)
                                {
                                    config.RegionCode = ConstantsString.RegionCodeGBR;
                                    config.EnableChannels = new object[] { "865700", "866300" };
                                    config.HoppingConfig = HoppingConfig.HOPPINGCONFIG_DEFAULT;
                                    break;
                                }
                            }

                            availableReader.Configuration.RegulatoryConfig = config;
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }


        }

    }
}
