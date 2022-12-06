using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRFIDApp.Model;
using ZebraRfidSdk;
using static System.Net.WebRequestMethods;

namespace ZebraRFIDApp.Pages.Inventory
{

    /// <summary>
    /// InventoryPage
    /// </summary>
    public partial class InventoryPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        public List<List<string>> downloaded_data = new List<List<string>>();
        bool disable = false;
        List<TagData> groupTagsData = new List<TagData>();
        List<TagSeenCount> tagSeenCounts = new List<TagSeenCount>();
        List<TagData> GroupTagsData { get => groupTagsData; set => groupTagsData = value; }
        List<TagSeenCount> TagSeenCounts { get => tagSeenCounts; set => tagSeenCounts = value; }

        List<TagDataModel> reverseTagDataList = new List<TagDataModel>();
        IList<TagDataModel> tagDataList = SdkHandler.GetTagDataList();

        string currentCachedRequest;

        Readers readerManager;
        public class DataFromDataBase
        {
            public List<MessageBoard> data { get; set; }
        }
        public class MessageBoard
        {
            public string message { get; set; }
            public string assetItemID { get; set; }
            public string assetItemName { get; set; }
            public string manufactureDate { get; set; }
            public string manufacturer { get; set; }
            public string vendor { get; set; }
            public string purchaseDate { get; set; }
            public string PurchasePrice { get; set; }

            public string Comment { get; set; }
            public string tagID { get; set; }
            public string rfidCode { get; set; }
            public string dateEntered { get; set; }
            public string installationDate { get; set; }
        }
        public InventoryPage()
        {
            InitializeComponent();

            try
            {
                readerManager = SdkHandler.GetInstance().ReaderManager;
                readerManager.TagDataEvent += ReaderNotifyDataEvent;
                readerManager.OperationBatchmode += ReaderManager_OperationBatchmode;
                //readerManager.TriggerNotifyEvent += ReaderManagerTriggerEvent;



            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }
        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            lbHeaderDetail.HeightRequest = ConstantsString.HeaderDefaultHeight;

            Globals.InvetoryViewAppeared = true;
            if (Globals.ReaderBatchMode == Globals.BatchModeState.BatchModeConnected)
            {

                btnInventory.Text = ConstantsString.Stop;
                ListViewHederHeightSet();
                Globals.IsInventoryStart = true;
            }
            else if (Globals.StartPressInventory == Globals.InventoryState.Start)
            {
                btnInventory.Text = ConstantsString.Stop;
                Globals.IsInventoryStart = true;
            }
            else if (Globals.IsInventoryStart)
            {
                btnInventory.Text = ConstantsString.Stop;
                Globals.IsInventoryStart = true;

            }
            else
            {
                btnInventory.Text = ConstantsString.Start;
                Globals.IsInventoryStart = false;
                UpdateUI();
            }
        }
        /// <summary>
        /// update the ui
        /// </summary>
        void UpdateUI()
        {
            TagDataModel testTag = new TagDataModel();

            reverseTagDataList = new List<TagDataModel>(tagDataList);
            lstTagData.ItemsSource = reverseTagDataList;


            lbTotalTags.Text = string.Format(ConstantsString.TotalTagStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetTotalTag());
            lbUniqueTags.Text = string.Format(ConstantsString.UniqueStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetUniqueTotalTag());

        }


        /// <summary>
        /// Selecte a Tag in Listview
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="tappedEventArg">Event Argument</param>
        private void OnItemSelected(Object sender, ItemTappedEventArgs tappedEventArg)
        {
            //open DatabaseItemPage for that tag
            Navigation.PushAsync(new DatabaseItemPage(tagDataList[tappedEventArg.ItemIndex].tagID, downloaded_data));
            /* Original Code
            if (!this.disable)
            {
                this.disable = true;
                System.Diagnostics.Debug.WriteLine("OnItemSelected");

                if (((ListView)sender).SelectedItem == null)
                {
                    return;
                }

                if (Globals.SelectedTagObject == null && reverseTagDataList[tappedEventArg.ItemIndex].tagData != null)
                {
                    SdkHandler.UpdateTagDataList(reverseTagDataList[tappedEventArg.ItemIndex].tagData, true);
                    Globals.SelectedTagObject = reverseTagDataList[tappedEventArg.ItemIndex].tagData;
                }
                else
                {

                    if ((Globals.SelectedTagObject == reverseTagDataList[tappedEventArg.ItemIndex].tagData))
                    {
                        SdkHandler.UpdateTagDataList(reverseTagDataList[tappedEventArg.ItemIndex].tagData, false);
                        Globals.SelectedTagObject = null;
                    }
                    else
                    {
                        SdkHandler.UpdateTagDataList(Globals.SelectedTagObject, false);
                        SdkHandler.UpdateTagDataList(reverseTagDataList[tappedEventArg.ItemIndex].tagData, true);
                        Globals.SelectedTagObject = reverseTagDataList[tappedEventArg.ItemIndex].tagData;
                    }

                }

            

                
                reverseTagDataList = new List<TagDataModel>(tagDataList);
                reverseTagDataList.Reverse();
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.lstTagData.ItemsSource = null;
                    lstTagData.ItemsSource = reverseTagDataList;
                });
             

                this.disable = false;

            }
            */

        }

        /// <summary>
        /// clear the list view
        /// </summary>
        void ClearList()
        {
            lstTagData.ItemsSource = null;
            SdkHandler.ClearTagataList();
            SdkHandler.ClearTagSeenList();

            SdkHandler.ClearGroupTagsData();
            SdkHandler.totalTag = ConstantsString.ZeroValue;
            SdkHandler.totalUniqueTag = ConstantsString.ZeroValue;
            lbUniqueTags.Text = string.Format(ConstantsString.UniqueStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetTotalTag());
            lbTotalTags.Text = string.Format(ConstantsString.TotalTagStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetUniqueTotalTag());
        }

        /// <summary>
        /// Set height for listview header  
        /// </summary>
        void ListViewHederHeightSet()
        {
            if (Globals.ReaderBatchMode == Globals.BatchModeState.BatchModeConnected || Globals.ReaderBatchMode == Globals.BatchModeState.Enable)
            {
                lbHeaderDetail.Text = ConstantsString.InventoryInBatchMode;
                lbHeaderDetail.HeightRequest = ConstantsString.HeaderExpandHeight;
            }
            else
            {
                lbHeaderDetail.Text = "";
                lbHeaderDetail.HeightRequest = ConstantsString.HeaderDefaultHeight;
            }
        }

        /// <summary>
        /// Button click event Start/Stop inventory
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event Argument</param>
        void OnButtonClickedStartStop(object sender, EventArgs eventArgs)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickedResetDefault Click");
            try
            {
                if (SdkHandler.ConnectedReader != null)
                {

                    if (btnInventory.Text == ConstantsString.Start)
                    {
                        ClearList();
                        ListViewHederHeightSet();


                        SdkHandler.ConnectedReader.Actions.Inventory.Start();
                        btnInventory.Text = ConstantsString.Stop;
                        Globals.IsInventoryStart = true;
                    }
                    else
                    {
                        SdkHandler.ConnectedReader.Actions.Inventory.Stop();

                        btnInventory.Text = ConstantsString.Start;
                        Globals.IsInventoryStart = false;

                        if (SdkHandler.ConnectedReader.Configuration.BatchModeConfiguration == BatchMode.ENABLE)
                        {
                            Globals.ReaderBatchMode = Globals.BatchModeState.Enable;
                        }

                        lbHeaderDetail.Text = "";
                        if (Globals.ReaderBatchMode == Globals.BatchModeState.BatchModeConnected)
                        {

                            lbHeaderDetail.HeightRequest = ConstantsString.HeaderDefaultHeight;
                            Globals.ReaderBatchMode = Globals.BatchModeState.Auto;
                            SdkHandler.ConnectedReader.Actions.Inventory.GetBatchData();
                            Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeAuto;
                        }
                        else if (Globals.ReaderBatchMode == Globals.BatchModeState.Enable)
                        {
                            SdkHandler.ConnectedReader.Actions.Inventory.GetBatchData();
                            Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeEnable;
                        }


                        SdkHandler.ConnectedReader.Actions.Inventory.PurgeData();
                        Globals.StartPressInventory = Globals.InventoryState.Stop;

                    }
                }
                else
                {

                    DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToPerformStartStopInventory, ConstantsString.MsgActionOk);

            }



        }
        /// <summary>
        /// Button click event Start/Stop inventory
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event Argument</param>
        void OnButtonClickedSync(object sender, EventArgs eventArgs)
        {
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            string database_type = "";
            if (CheckForInternetConnection())
            {
                // Open the file to read from.
                using (StreamReader sr = System.IO.File.OpenText(fileName))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] currentCachedRequest = s.Split('^');
                        string[] currentCachedRequestParameters = currentCachedRequest[1].Split('|');
                        database_type = currentCachedRequest[0];



                        if (database_type == "MessageBoard")
                        {
                            // Need help to understand what is going on here
                            Console.WriteLine(
                                " " + currentCachedRequestParameters[0] +
                                " " + currentCachedRequestParameters[1] +
                                " " + currentCachedRequestParameters[2] +
                                " " + currentCachedRequestParameters[3] +
                                " " + currentCachedRequestParameters[4] +
                                " " + currentCachedRequestParameters[5] +
                                " " + currentCachedRequestParameters[6] +
                                " " + currentCachedRequestParameters[7] +
                                " " + currentCachedRequestParameters[8] +
                                " " + currentCachedRequestParameters[9]);
                            var values = new Dictionary<string, string>
                            {
                            /* 
                            data.Add("MessageBoard");
                            data.Add(item.tagID);
                            data.Add(item.truckID);
                            data.Add(item.license_plate);
                            data.Add(item.make_model);
                            data.Add(item.manufactureYear);
                            data.Add(item.driverID);
                            data.Add(item.acquisition_date);
                            data.Add(item.deployment_date);
                            data.Add(item.manufactureDate);
                            data.Add(item.dateEntered);
                            data.Add(item.installationDate);
                            */
                                { "request", "message"},

                                { "assetItemID", currentCachedRequestParameters[0]},
                                { "truckID", currentCachedRequestParameters[1]},
                                { "license_plate", currentCachedRequestParameters[2]},
                                { "make_model", currentCachedRequestParameters[3]},
                                { "manufactureYear", currentCachedRequestParameters[4]},
                                { "driverID", currentCachedRequestParameters[5]},
                                { "acquisition_date", currentCachedRequestParameters[6]},
                                { "manufactureDate", currentCachedRequestParameters[7]},
                                { "dateEntered", currentCachedRequestParameters[8]},
                                { "installationDate", currentCachedRequestParameters[9]}
                            };
                            var content = new FormUrlEncodedContent(values);

                            var response = client.PostAsync("https://jjung2.w3.uvm.edu/RFIDproject/REST_API/api/write.php", content);
                            Console.WriteLine(response.ToString());
                        }
                        else if (database_type == "Inspection")
                        {
                            Console.WriteLine(s);
                            var values = new Dictionary<string, string>
                            {
                                { "request", "inspection"},
                                { "tagID", s.Split('^')[1].Split('|')[0]},
                                { "inspectorName", s.Split('^')[1].Split('|')[1]},
                                { "inspectionResult", s.Split('^')[1].Split('|')[2]}
                            };
                            var content = new FormUrlEncodedContent(values);

                            var response = client.PostAsync("https://jjung2.w3.uvm.edu/RFIDproject/REST_API/api/write.php", content);
                            Console.WriteLine(response.ToString());
                        }
                        else if (database_type == "Usage")
                        {
                            var values = new Dictionary<string, string>
                            {
                                { "request", "usage"},
                                { "tagID", s.Split('^')[1].Split('|')[0]},
                                { "user", s.Split('^')[1].Split('|')[1]},
                                { "gpsAddress", s.Split('^')[1].Split('|')[2]}
                            };
                            var content = new FormUrlEncodedContent(values);

                            var response = client.PostAsync("https://jjung2.w3.uvm.edu/RFIDproject/REST_API/api/write.php", content);
                            Console.WriteLine(response.ToString());
                        }

                    }
                }
                DisplayAlert(ConstantsString.Msg, "Successfully Sync", ConstantsString.MsgActionOk);
            }
            else
            {
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoInternet, ConstantsString.MsgActionOk);
            }

        }
        /// <summary>
        /// Button click event Start/Stop inventory
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event Argument</param>
        ///
        public bool CheckForInternetConnection()
        {
            var current = Connectivity.NetworkAccess;
            bool status = false;
            switch (current)
            {
                case Xamarin.Essentials.NetworkAccess.Internet:
                    status = true;
                    // Connected to internet
                    break;
                case Xamarin.Essentials.NetworkAccess.Unknown:
                    // Internet access is unknown
                    status = false;
                    break;
            }
            return status;
        }

        void OnButtonClickedDownload(object sender, EventArgs eventArgs)
        {
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");

            if (CheckForInternetConnection())
            {
                var json = new WebClient().DownloadString("https://jjung2.w3.uvm.edu/RFIDproject/REST_API/api/read.php?request=tag&id=all");
                DataFromDataBase dataList = JsonConvert.DeserializeObject<DataFromDataBase>(json);
                List<string> data = new List<string>();
                System.IO.File.WriteAllText(fileName, "");
                // Create a file to write to.
                downloaded_data = new List<List<string>>();
                foreach (var item in dataList.data)
                {
                    Console.WriteLine(item.tagID);
                    data = new List<string>();

                    data.Add("MessageBoard");

                    data.Add(item.message);
                    data.Add(item.assetItemID);
                    data.Add(item.assetItemName);
                    data.Add(item.manufactureDate);
                    data.Add(item.manufacturer);
                    data.Add(item.vendor);
                    data.Add(item.purchaseDate);
                    data.Add(item.PurchasePrice);
                    data.Add(item.Comment);
                    data.Add(item.tagID);
                    data.Add(item.rfidCode);
                    data.Add(item.dateEntered);
                    data.Add(item.installationDate);

                    using (StreamWriter sw = System.IO.File.AppendText(fileName))
                    {
                        sw.WriteLine("MessageBoard^" + item.message + '|' + item.assetItemID + '|' + item.assetItemName + '|'
                        + item.manufactureDate + '|' + item.manufacturer + '|' + item.vendor + '|' + item.purchaseDate + '|'
                        + item.PurchasePrice + '|' + item.Comment + '|' + item.tagID + '|' + item.rfidCode + '|' + item.dateEntered
                        + '|' + item.installationDate);
                    }

                    downloaded_data.Add(data);
                }
                var text = System.IO.File.ReadAllText(fileName);
                Console.WriteLine(text);
                DisplayAlert(ConstantsString.Msg, "successfully downloaded from database", ConstantsString.MsgActionOk);
            }
            else
            {
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoInternet, ConstantsString.MsgActionOk);
            }

        }

        /// <summary>
        /// Update the list view
        /// </summary>
        /// <param name="tagSeenCountsList">TagSeenCount List</param>
        void UpdateListView(List<TagSeenCount> tagSeenCountsList)
        {

            try
            {

                Console.WriteLine("tag seen counts list " + tagSeenCountsList);
                if (tagSeenCountsList != null && tagSeenCountsList.Count > 0)
                {
                    foreach (TagSeenCount tagSeenCount in tagSeenCountsList)
                    {
                        SdkHandler.UpdateTagDetail(tagSeenCount.TagData, tagSeenCount);
                    }

                    int tagSum = 0;

                    tagSum = SdkHandler.TagSeenCountList.Sum(a => a.SeenCount);

                    if (Globals.UniqueTagEnabled && Globals.ReaderBatchMode != Globals.BatchModeState.Enable)
                    {
                        SdkHandler.totalTag = SdkHandler.GroupTagsData.Count.ToString();
                    }
                    else
                    {
                        SdkHandler.totalTag = tagSum.ToString();
                    }

                    SdkHandler.totalUniqueTag = SdkHandler.GroupTagsData.Count.ToString();

                    tagDataList = SdkHandler.GetTagDataList();
                    reverseTagDataList = new List<TagDataModel>(tagDataList);
                    lstTagData.ItemsSource = reverseTagDataList;
                    if (Globals.UniqueTagEnabled && Globals.ReaderBatchMode != Globals.BatchModeState.Enable && Globals.ReaderBatchMode != Globals.BatchModeState.BatchModeConnected)
                    {
                        lbTotalTags.Text = string.Format(ConstantsString.TotalTagStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetUniqueTotalTag());
                    }
                    else
                    {
                        lbTotalTags.Text = string.Format(ConstantsString.TotalTagStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetTotalTag());
                    }

                    lbUniqueTags.Text = string.Format(ConstantsString.UniqueStringFormat, System.Environment.NewLine, System.Environment.NewLine, SdkHandler.GetUniqueTotalTag());


                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }
        }



        /// <summary>
        /// Handle the search button click event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event Argument</param>
        void Handle_SearchButtonPressed(object sender, System.EventArgs eventArgs)
        {
            System.Diagnostics.Debug.WriteLine("Handle_SearchButtonPressed Click");
        }

        /// <summary>
        /// Is the os reader notify data event.
        /// </summary>
        /// <param name="tagData">Tag data.</param>
        private void ReaderNotifyDataEvent(TagData tagData)
        {

            System.Diagnostics.Debug.WriteLine("Inventory ReaderNotifyDataEvent " + tagData.Id);


            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateListView(SdkHandler.GetTagSeenList());

            });


        }


        /// <summary>
        /// Event handler of Reader in batchmode.
        /// </summary>
        /// <param name="eventStatus">Event status.</param>
        private void ReaderManager_OperationBatchmode(EventStatus eventStatus)
        {
            if (Globals.ReaderBatchMode == Globals.BatchModeState.BatchModeConnected
                    || Globals.ReaderBatchMode == Globals.BatchModeState.Enable)
            {
                lbHeaderDetail.Text = ConstantsString.InventoryInBatchMode;
                lbHeaderDetail.HeightRequest = ConstantsString.HeaderExpandHeight;
            }
            else
            {
                lbHeaderDetail.Text = "";
                lbHeaderDetail.HeightRequest = ConstantsString.HeaderDefaultHeight;
            }
        }


        /// <summary>
        /// The event trigger notify.
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        /// <param name="triggerEvent">Trigger event.</param>
        void ReaderManagerTriggerEvent(int readerID, TriggerType triggerEvent)
        {
            Console.WriteLine("TriggerType  " + triggerEvent);
            try
            {
                if (SdkHandler.ConnectedReader != null)
                {
                    if (triggerEvent == TriggerType.TRIGGERTYPE_PRESS && btnInventory.Text == ConstantsString.Start)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            ClearList();
                            ListViewHederHeightSet();
                            btnInventory.Text = ConstantsString.Stop;
                        });

                        SdkHandler.ConnectedReader.Actions.Inventory.Start();
                        Globals.IsInventoryStart = true;
                    }
                    else
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            btnInventory.Text = ConstantsString.Start;
                            lbHeaderDetail.Text = "";
                        });
                        SdkHandler.ConnectedReader.Actions.Inventory.Stop();

                        Globals.IsInventoryStart = false;

                        if (SdkHandler.ConnectedReader.Configuration.BatchModeConfiguration == BatchMode.ENABLE)
                        {
                            Globals.ReaderBatchMode = Globals.BatchModeState.Enable;
                        }


                        if (Globals.ReaderBatchMode == Globals.BatchModeState.BatchModeConnected)
                        {

                            lbHeaderDetail.HeightRequest = ConstantsString.HeaderDefaultHeight;
                            Globals.ReaderBatchMode = Globals.BatchModeState.Auto;
                            SdkHandler.ConnectedReader.Actions.Inventory.GetBatchData();
                            Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeAuto;
                        }
                        else if (Globals.ReaderBatchMode == Globals.BatchModeState.Enable)
                        {
                            SdkHandler.ConnectedReader.Actions.Inventory.GetBatchData();
                            Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeEnable;
                        }


                        SdkHandler.ConnectedReader.Actions.Inventory.PurgeData();
                        Globals.StartPressInventory = Globals.InventoryState.Stop;

                    }

                }
                else
                {
                    DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
                Device.BeginInvokeOnMainThread(() =>
                {
                    btnInventory.Text = ConstantsString.Start;
                    lbHeaderDetail.Text = "";
                });
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgUnableToPerformStartStopInventory, ConstantsString.MsgActionOk);

            }
        }
    }
}
