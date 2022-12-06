using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZebraRFIDApp.Model;
using ZebraRfidSdk;

namespace ZebraRFIDApp.API
{
    public class SdkHandler
    {
       
        public static List<Reader> ReaderList
        {
            set { readerList = value; }
            get { return readerList; }
        }
        public static List<ReaderModel> activeReaderList = new List<ReaderModel>();
        public static Reader ConnectedReader { get => connectedReader; set => connectedReader = value; }
        public static List<TagDataModel> tagDataList = new List<TagDataModel>();
        public static string totalTag;
        public static string totalUniqueTag;
       

        static List<TagData> groupTagsData = new List<TagData>();
        static List<TagSeenCount> tagSeenCounts = new List<TagSeenCount>();
        public static List<TagData> GroupTagsData { get => groupTagsData; set => groupTagsData = value; }
        public static List<TagSeenCount> TagSeenCountList { get => tagSeenCounts; set => tagSeenCounts = value; }
        static List<Reader> readerList = new List<Reader>();

        static RfidSdk sdkInstance = null;
        static Readers readerManager = null;
        static Reader connectedReader = null;


        public SdkHandler()
        {
        }

        /// <summary>
        /// Gets the SDK instance
        /// </summary>
        /// <returns>SDK instance</returns>
        public static RfidSdk GetInstance()
        {

            if (sdkInstance == null)
            {
                sdkInstance = new RfidSdk();
                readerManager = sdkInstance.ReaderManager;
                readerManager.SetOperationMode(OpMode.OPMODE_MFI);
                readerManager.Disconnected += ReaderManager_Disconnected;
                readerManager.Connected += ReaderManager_Connected;
                readerManager.Appeared += ReaderManager_Appeared;
                readerManager.Disappeared += ReaderManager_Disappeared;
                readerManager.TagDataEvent += ReaderNotifyDataEvent;
                readerManager.OperationBatchmode += ReaderManager_OperationBatchmode;


            }
            return sdkInstance;
        }

        /// <summary>
        /// Update scanner connection status in scannerList
        /// </summary>
        /// <param name="readerName">reader name</param>
        /// <param name="status">Status</param>
        /// <param name="isReaderConnected">Status of reader connection</param>
        /// <param name="testingString">Testing string</param>
        public static void UpdateReaderConnectionStatus(string readerName, string status, bool isReaderConnected, string testingString)
        {
            try
            {
                var indexOf = activeReaderList.IndexOf(activeReaderList.Find(reader => reader.ReaderName == readerName));
                activeReaderList[indexOf].ConectionStatus = status;
                if (isReaderConnected)
                {
                    if (Globals.ReaderBatchMode == Globals.BatchModeState.Enable || Globals.ReaderBatchMode == Globals.BatchModeState.BatchModeConnected)
                    {
                        activeReaderList[indexOf].isVisibleBluetoothAddress = isReaderConnected;
                        activeReaderList[indexOf].BluetoothAddress = ConstantsString.ReaderOnBatchMode;

                    }
                    else
                    {
                        string modeType = Application.Current.Properties[ConstantsString.BatchModeType].ToString();

                        if (modeType == ConstantsString.BatchModeTypeEnable || modeType == ConstantsString.BatchModeTypeConnected)
                        {

                            activeReaderList[indexOf].isVisibleBluetoothAddress = isReaderConnected;
                            activeReaderList[indexOf].BluetoothAddress = ConstantsString.ReaderOnBatchMode;


                        }
                        else
                        {
                            activeReaderList[indexOf].isVisibleBluetoothAddress = isReaderConnected;
                            activeReaderList[indexOf].BluetoothAddress = FormatBluetoothAddress(ConnectedReader.ReaderCapabilities.BluetoothAddress); ;
                            activeReaderList[indexOf].isVisibleModelNumber = isReaderConnected;
                            activeReaderList[indexOf].ModelNumber = ConstantsString.Model + ConnectedReader.ReaderCapabilities.Model;
                            activeReaderList[indexOf].isVisibleSerialNumber = isReaderConnected;
                            activeReaderList[indexOf].SerialNumber = ConstantsString.Serial + ConnectedReader.ReaderCapabilities.SerialNumber;



                        }
                    }



                }



                else
                {
                    activeReaderList[indexOf].isVisibleBluetoothAddress = isReaderConnected;
                    activeReaderList[indexOf].BluetoothAddress = testingString;
                    activeReaderList[indexOf].isVisibleModelNumber = isReaderConnected;
                    activeReaderList[indexOf].ModelNumber = testingString;
                    activeReaderList[indexOf].isVisibleSerialNumber = isReaderConnected;
                    activeReaderList[indexOf].SerialNumber = testingString;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception" + e.Message);
            }

           
        }

        /// <summary>
        /// Format the bluetooth address.
        /// </summary>
        /// <returns>The bluetooth address.</returns>
        /// <param name="address">Address.</param>
        private static string FormatBluetoothAddress(string address)
        {
            System.Text.StringBuilder bluetoothAddress = new System.Text.StringBuilder(address);
            for (int index = bluetoothAddress.Length - ConstantsString.LengthOfBluetoothString; index >= ConstantsString.LengthOfBluetoothString; index -= ConstantsString.LengthOfBluetoothString)
            {
                bluetoothAddress.Insert(index, ConstantsString.BluetoothAddressSeprator);
            }
            return bluetoothAddress.ToString();
        }

        /// <summary>
        /// Disconnects the previous connected device..
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        public static void DisconnectPreviousConnectedDevice(int readerID)
        {
            try
            {
                Reader availableReader = readerList.Find((obj) => obj.Id == readerID);
                if (availableReader != null)
                {
                    availableReader.Disconnect();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }


        }

        /// <summary>
        /// Clear TagDataList
        /// </summary>
        public static void ClearTagataList()
        {
            if (tagDataList != null)
            {
                tagDataList.Clear();
            }
        }

        /// <summary>
        /// Clear TagSeenList
        /// </summary>
        public static void ClearTagSeenList()
        {
            if (TagSeenCountList != null)
            {
                TagSeenCountList.Clear(); 
            }
            
        }

        /// <summary>
        /// Get tagdata list
        /// </summary>
        /// <returns>TagData List</returns>
        public static List<TagDataModel> GetTagDataList()
        {
            return tagDataList;
        }

        /// <summary>
        /// Get tag seen list
        /// </summary>
        /// <returns>TagData List</returns>
        public static List<TagSeenCount> GetTagSeenList()
        {
            return TagSeenCountList;
        }

        /// <summary>
        /// Clear tag data list
        /// </summary>
        public static void ClearTagDataList()
        {
            tagDataList.Clear();
        }

        /// <summary>
        /// Clear group data
        /// </summary>
        public static void ClearGroupTagsData()
        {
            GroupTagsData.Clear();
        }

        /// <summary>
        /// Update TagDataList
        /// </summary>
        /// <param name="tagData"></param>
        /// <param name="visible"></param>
        public static void UpdateTagDataList(TagData tagData,bool visible)
        {
            try
            {
                var indexOf = tagDataList.IndexOf(tagDataList.Find(scanner => scanner.tagID == tagData.Id));
                tagDataList[indexOf].isVisibleDetail = visible;
                tagDataList[indexOf].isVisibleTagPc = visible;
                tagDataList[indexOf].isVisibleTagPhase = visible;
                tagDataList[indexOf].isVisibleTagRssi = visible;
                tagDataList[indexOf].isVisibleTagChannel = visible;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }
            
        }

        /// <summary>
        /// Event handler of Reader in batchmode.
        /// </summary>
        /// <param name="eventStatus">Event status.</param>
        static void ReaderManager_OperationBatchmode(EventStatus eventStatus)
        {
            Globals.ReaderBatchMode = Globals.BatchModeState.BatchModeConnected;
            Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeConnected;


        }

        /// <summary>
        /// Event handler of Reader disappear event
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        static void ReaderManager_Disappeared(int readerID)
        {

            try
            {
                Console.WriteLine("ReaderManager_Disappeared " + readerID);
                ReaderModel availableReader = activeReaderList.Find((obj) => obj.ReaderId == readerID);
                if (availableReader != null)
                {
                    activeReaderList.Remove(availableReader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }



        }

        /// <summary>
        /// Set connected rfidReader object to ConnectedRfidReader.
        /// </summary>
        /// <param name="reader">Reader info.</param>
        static void ReaderManager_Connected(Reader reader)
        {
            Console.WriteLine("ReaderManager_Connected " + reader.Name);
            try
            {
                ConnectedReader = reader;
                
               
                if (ConnectedReader != null)
                {
                   
                    if (Globals.ReaderBatchMode != Globals.BatchModeState.BatchModeConnected)
                    {
                        BatchMode batchMode = ConnectedReader.Configuration.BatchModeConfiguration;
                        switch (batchMode)
                        {
                            case BatchMode.AUTO:
                                Globals.ReaderBatchMode = Globals.BatchModeState.Auto;
                                Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeAuto;
                                break;
                            case BatchMode.ENABLE:
                                Globals.ReaderBatchMode = Globals.BatchModeState.Enable;
                                Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeEnable;
                                break;
                            case BatchMode.DISABLE:
                                Globals.ReaderBatchMode = Globals.BatchModeState.Disable;
                                Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeDisable;
                                break;
                            default:
                                Globals.ReaderBatchMode = Globals.BatchModeState.Auto;
                                Application.Current.Properties[ConstantsString.BatchModeType] = ConstantsString.BatchModeTypeAuto;
                                break;

                        }
                    }
                    Globals.UniqueTagEnabled = ConnectedReader.Configuration.UniqueTagReport;


                }

               

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }

        }

        /// <summary>
        /// Event handler of Reader Disconnected event
        /// </summary>
        /// <param name="readerID">Reader identifier.</param>
        static void ReaderManager_Disconnected(int readerID)
        {
            Console.WriteLine("ReaderManager_Disconnected " + readerID);
            ConnectedReader = null;
            Globals.ReaderBatchMode = Globals.BatchModeState.Auto;

        }

        /// <summary>
        ///  Event handler of Reader appeared event
        /// </summary>
        /// <param name="readerInfo">Reader info.</param>
        static void ReaderManager_Appeared(Reader readerInfo)
        {
            try
            {
                Console.WriteLine("ReaderManager_Appeared " + readerInfo.Name);
                ReaderModel availableReader = activeReaderList.Find((obj) => obj.ReaderId == readerInfo.Id);
                if (availableReader == null)
                {
                    AddActiveReaders(readerInfo);
                  

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }


        }

        /// <summary>
        /// Is the os reader notify data event.
        /// </summary>
        /// <param name="tagData">Tag data.</param>
        static void ReaderNotifyDataEvent(TagData tagData)
        {

            try
            {
                Console.WriteLine("+++ReaderNotifyDataEvent " + tagData.Id);

                TagData availableReader = GroupTagsData.Find((obj) => obj.Id == tagData.Id);
                TagSeenCount tagSeen = TagSeenCountList.Find((obj) => obj.TagID == tagData.Id);
                if (availableReader == null)
                {
                    TagSeenCount seenCount = new TagSeenCount(tagData);
                    TagSeenCountList.Add(seenCount);
                    GroupTagsData.Add(tagData);

                }
                else
                {
                    if (tagSeen != null)
                    {
                        tagSeen.SeenCount++;
                    }

                }

                totalUniqueTag = GroupTagsData.Count.ToString();

            }
            catch(Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }
            

        }



        /// <summary>
        /// Update tagDataList List
        /// </summary>
        /// <param name="tagData">TagData</param>
        /// <param name="tagSeenCount">TagSeenCount</param>
        public static void UpdateTagDetail(TagData tagData , TagSeenCount tagSeenCount)
        {
         

            TagDataModel avilablityOfTagDataId = tagDataList.Find((obj) => obj.tagData.Id == tagData.Id);
            if (avilablityOfTagDataId == null)
            {
  
                string pc = string.Format(ConstantsString.PcFormat, System.Environment.NewLine, FormatingTagDataProperty(tagData.Pc));
                string rssi = string.Format(ConstantsString.RssiFormat, System.Environment.NewLine, FormatingTagDataProperty(tagData.PeakRSSI.ToString()));
                string phase = string.Format(ConstantsString.PhaseFormat, System.Environment.NewLine, FormatingTagDataProperty(tagData.PhaseInfo.ToString()));
                string channel = string.Format(ConstantsString.ChannerFormat, System.Environment.NewLine, FormatingTagDataProperty(tagData.ChannelIndex.ToString()));
             
                tagDataList.Add(new TagDataModel
                {
                    tagID = tagData.Id,
                    tagData = tagData,
                    isVisibleDetail = false,
                    tagSeenCount = tagSeenCount.SeenCount.ToString(),
                    tagPc = pc,
                    tagRssi = rssi,
                    tagPhase = phase,
                    tagChannel = channel
                });

            }
            else
            {
                var indexOf = tagDataList.IndexOf(tagDataList.Find(scanner => scanner.tagID == tagData.Id));
                if (tagSeenCount != null)
                {
                    if (Globals.UniqueTagEnabled && Globals.ReaderBatchMode != Globals.BatchModeState.Enable &&  Globals.ReaderBatchMode != Globals.BatchModeState.BatchModeConnected)
                    {
                        tagDataList[indexOf].tagSeenCount = "1";
                    }
                    else
                    {
                        tagDataList[indexOf].tagSeenCount = tagSeenCount.SeenCount.ToString();
                    }
                    
                    
                }
                else
                {
                    tagDataList[indexOf].tagSeenCount = "1";
                }
                
            }
        }

  

        /// <summary>
        /// Formatings the tag data property.
        /// </summary>
        /// <returns>The tag data property.</returns>
        /// <param name="tagDataProperty">Tag data property.</param>
        private static string FormatingTagDataProperty(string tagDataProperty)
        {
           return (tagDataProperty == "0" || tagDataProperty == "" || tagDataProperty == null) ? "_" : tagDataProperty;
        }

        /// <summary>
        /// Get Total tag
        /// </summary>
        /// <returns></returns>
        public static string GetTotalTag()
        {
            if (totalTag == null)
            {
                return "0";
            }
            return totalTag;
        }

        /// <summary>
        /// Get Unique Total tag
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueTotalTag()
        {
            if (totalUniqueTag == null)
            {
                return "0";
            }
            return totalUniqueTag;
        }


        /// <summary>
        /// Adding rfid readers into ReaderModel list
        /// </summary>
        /// <param name="reader"></param>
        private static void AddActiveReaders(Reader reader)
        {
            ReaderModel activeReader = activeReaderList.Find((obj) => obj.ReaderName == reader.Name);

            if (activeReader == null)
            {
                activeReaderList.Add(new ReaderModel { ReaderObject = reader, ReaderId = reader.Id, ReaderName = reader.Name, BluetoothAddress = "", ModelNumber = "", SerialNumber = "", ConectionStatus = "" });

            }


        }

        /// <summary>
        /// To get currently avilable readers
        /// </summary>
        /// <returns></returns>
        public static List<ReaderModel> GetReaderList()
        {
            return activeReaderList;
        }


       

        /// <summary>
        /// Get paired readers
        /// </summary>
        /// <returns></returns>
        public static List<ReaderModel> GetActiveReaderList()
        {


            List<Reader> readers = GetInstance().ReaderManager.GetReaders();

            if (readers != null && readers.Count > 0)
            {
                foreach (Reader reader in readers)
                {
                    AddActiveReaders(reader);
                    Console.WriteLine("Reader ID: " + reader.Name);

                }
            }
            return activeReaderList;
        }

   

    }
}
