using System;
using ZebraRfidSdk;

namespace ZebraRFIDApp.API
{
    public class Globals
    {

        /// <summary>
        /// Inventory state.
        /// </summary>
        public enum InventoryState
        {
            None,
            Start,
            Stop
        }

        /// <summary>
        /// Batch mode state.
        /// </summary>
        public enum BatchModeState
        {
            Auto,
            Enable,
            Disable,
            BatchModeConnected
        }

        /// <summary>
        /// Unique tag enabled state
        /// </summary>
        private static bool uniqueTagEnabled = false;
        public static bool UniqueTagEnabled
        {
            get
            {
                return uniqueTagEnabled;
            }
            set
            {
                uniqueTagEnabled = value;
            }

        }



        /// <summary>
        /// The invetory trigger click status.
        /// </summary>
        private static bool _invetoryViewAppeared = false;
        public static bool InvetoryViewAppeared
        {
            get
            {
                return _invetoryViewAppeared;
            }
            set
            {
                _invetoryViewAppeared = value;
            }
        }

        /// <summary>
        /// The tag locate status.
        /// </summary>
        private static bool _tagLocateStatus = false;
        public static bool TagLocateStatus
        {
            get
            {
                return _tagLocateStatus;
            }
            set
            {
                _tagLocateStatus = value;
            }
        }

        /// <summary>
        /// The start press inventory.
        /// </summary>
        private static InventoryState _startPressInventory = InventoryState.None;
        public static InventoryState StartPressInventory
        {
            get
            {
                return _startPressInventory;
            }
            set
            {
                _startPressInventory = value;
            }
        }

        /// <summary>
        /// The conect status.
        /// </summary>
        private static bool _conectStatus = false;
        public static bool ConectStatus
        {
            get
            {
                return _conectStatus;
            }
            set
            {
                _conectStatus = value;
            }
        }


        /// <summary>
        /// The coonected reader object.
        /// </summary>
        private static Reader connectedReader = null;
        public static Reader ConnectedReader
        {
            get
            {
                return connectedReader;
            }
            set
            {
                connectedReader = value;
            }

        }
        /// <summary>
        /// The reader batch mode.
        /// </summary>
        private static BatchModeState readerBatchMode = BatchModeState.Auto;
        public static BatchModeState ReaderBatchMode
        {
            get
            {
                return readerBatchMode;
            }
            set
            {
                readerBatchMode = value;
            }

        }

        /// <summary>
        /// The status of inventory start.
        /// </summary>
        private static bool isInventoryStart = false;
        public static bool IsInventoryStart
        {
            get
            {
                return isInventoryStart;
            }
            set
            {
                isInventoryStart = value;
            }

        }

        /// <summary>
        /// The status of tag locating start.
        /// </summary>
        private static bool isTagLocatingStart = false;
        public static bool IsTagLocatingStart
        {
            get
            {
                return isTagLocatingStart;
            }
            set
            {
                isTagLocatingStart = value;
            }

        }


        /// <summary>
        /// The scanner registered.
        /// </summary>
        private static bool scannerRegistered = false;
        public static bool ScannerRegistered
        {
            get
            {
                return scannerRegistered;
            }
            set
            {
                scannerRegistered = value;
            }

        }

        /// <summary>
        /// The previous click.
        /// </summary>
        private static int _previousClick = -1;
        public static int PreviousClick
        {
            get
            {
                return _previousClick;
            }
            set
            {
                _previousClick = value;
            }
        }

        /// <summary>
        /// Tab bar height.
        /// </summary>
        private static int _tabBarHeight = -1;
        public static int TabBarHeight
        {
            get
            {
                return _tabBarHeight;
            }
            set
            {
                _tabBarHeight = value;
            }
        }

        /// <summary>
        /// The conceted identifier.
        /// </summary>
        private static int _concetedID = ConstantsString.Zero;
        public static int ConcetedID
        {
            get
            {
                return _concetedID;
            }
            set
            {
                _concetedID = value;
            }
        }

        /// <summary>
        /// The conceted identifier.
        /// </summary>
        private static int _tabSelection = ConstantsString.Zero;
        public static int TabSelection
        {
            get
            {
                return _tabSelection;
            }
            set
            {
                _tabSelection = value;
            }
        }

        /// <summary>
        /// The total tag count.
        /// </summary>
        private static int _totalTagCount = ConstantsString.Zero;
        public static int TotalTagCount
        {
            get
            {
                return _totalTagCount;
            }
            set
            {
                _totalTagCount = value;
            }
        }

        /// <summary>
        /// The total uniqe tag count.
        /// </summary>
        private static int _totalUniqeTagCount = ConstantsString.Zero;
        public static int TotalUniqeTagCount
        {
            get
            {
                return _totalUniqeTagCount;
            }
            set
            {
                _totalUniqeTagCount = value;
            }
        }

        /// <summary>
        /// The selected batchmode.
        /// </summary>
        private static string _selectedBatchmode = ConstantsString.Auto.ToUpper();
        public static string SelectedBatchmode
        {
            get
            {
                return _selectedBatchmode;
            }
            set
            {
                _selectedBatchmode = value;
            }
        }

        /// <summary>
        /// The selected start trigger mode.
        /// </summary>
        private static string _selectedStartTriggerMode = ConstantsString.Immediate;
        public static string SelectedStartTriggerMode
        {
            get
            {
                return _selectedStartTriggerMode;
            }
            set
            {
                _selectedStartTriggerMode = value;
            }
        }

        /// <summary>
        /// The type of the selected trigger.
        /// </summary>
        private static string _selectedTriggerType = ConstantsString.Press;
        public static string SelectedTriggerType
        {
            get
            {
                return _selectedTriggerType;
            }
            set
            {
                _selectedTriggerType = value;
            }
        }

        /// <summary>
        /// The selected stop trigger mode.
        /// </summary>
        private static string _selectedStopTriggerMode = ConstantsString.Immediate;
        public static string SelectedStopTriggerMode
        {
            get
            {
                return _selectedStopTriggerMode;
            }
            set
            {
                _selectedStopTriggerMode = value;
            }
        }

        /// <summary>
        /// The type of the selected stop trigger.
        /// </summary>
        private static string _selectedStopTriggerType = ConstantsString.Press;
        public static string SelectedStopTriggerType
        {
            get
            {
                return _selectedStopTriggerType;
            }
            set
            {
                _selectedStopTriggerType = value;
            }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns>The version.</returns>
        public static string GetVersion()
        {
            return SdkHandler.GetInstance().Version;
        }

        /// <summary>
        /// The selected tag object.
        /// </summary>
        private static TagData _selectedTagObject = null;
        public static TagData SelectedTagObject
        {
            get
            {
                return _selectedTagObject;
            }
            set
            {
                _selectedTagObject = value;
            }
        }


        /// <summary>
        /// The inventory start press in batch mode.
        /// </summary>
        private static bool _inventoryStartPressInBatchMode = false;
        public static bool InventoryStartPressInBatchMode
        {
            get
            {
                return _inventoryStartPressInBatchMode;
            }
            set
            {
                _inventoryStartPressInBatchMode = value;
            }
        }

        /// <summary>
        /// The is save tag report settings.
        /// </summary>
        private static bool _isSaveTagReportSettings = false;
        public static bool IsSaveTagReportSettings
        {
            get
            {
                return _isSaveTagReportSettings;
            }
            set
            {
                _isSaveTagReportSettings = value;
            }
        }

        /// <summary>
        /// The is save sigulation settings.
        /// </summary>
        private static bool _isSaveSigulationSettings = false;
        public static bool IsSaveSigulationSettings
        {
            get
            {
                return _isSaveSigulationSettings;
            }
            set
            {
                _isSaveTagReportSettings = value;
            }
        }

        /// <summary>
        /// The is save start stop settings.
        /// </summary>
        private static bool _isSaveStartStopSettings = false;
        public static bool IsSaveStartStopSettings
        {
            get
            {
                return _isSaveStartStopSettings;
            }
            set
            {
                _isSaveStartStopSettings = value;
            }
        }

        /// <summary>
        /// The selected link profile name.
        /// </summary>
        private static string selectedLinkProfileName = ConstantsString.LinkProfile1;
        public static string SelectedLinkProfileName
        {
            get
            {
                return selectedLinkProfileName;
            }
            set
            {
                selectedLinkProfileName = value;
            }
        }

        /// <summary>
        /// The selected link profile index.
        /// </summary>
        private static int selectedLinkProfileNameIndex = ConstantsString.defultSelectedProfileIndex;
        public static int SelectedLinkProfileNameIndex
        {
            get
            {
                return selectedLinkProfileNameIndex;
            }
            set
            {
                selectedLinkProfileNameIndex = value;
            }
        }


        /// <summary>
        /// The status of link profile page appear.
        /// </summary>
        private static bool isLinkProfilePageAppear = false;
        public static bool IsLinkProfilePageAppear
        {
            get
            {
                return isLinkProfilePageAppear;
            }
            set
            {
                isLinkProfilePageAppear = value;
            }
        }

    }
}