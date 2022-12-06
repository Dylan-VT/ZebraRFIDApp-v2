using System;
namespace ZebraRFIDApp.API
{
    public static class ConstantsString
    {
            public const string MsgNoInternet = "No Internet Connection";

            public const int HeaderExpandHeight = 30;
            public const int HeaderDefaultHeight = 0;

            public const string TotalTagStringFormat = "TOTAL TAGS{0} {1} {2}";
            public const string UniqueStringFormat = "UNIQUE TAGS{0} {1} {2}";
            public const string PcFormat = "PC{0} {1}";
            public const string RssiFormat = "RSSI{0} {1}";
            public const string PhaseFormat = "PHASE{0} {1}";
            public const string ChannerFormat = "CHANNEL{0} {1}";
            public const string DefaultTagsSeenCount = "1";

            public const string BatchModeType = "BatchModeType";
            public const string BatchModeTypeAuto = "Auto";
            public const string BatchModeTypeEnable = "Enable";
            public const string BatchModeTypeDisable = "Disable";
            public const string BatchModeTypeConnected= "BatchModeConnected";


            public const string CheckMark = "\u2713";
            public const string UnCheckMark = "      ";
            public const string Empty = "";
            public const string Connected = "Connected";

            public const string ApplicationVersion = "Application version v";
            public const string SDKVersion = "SDK version v";

            public const string CopyrightMsg = "©2022 Zebra Technologies Corp. and/or its affiliates.  All rights reserved";

            //Main menu
            public const string RapidRead = "Rapid Read";
            public const string Inventory = "Inventory";
            public const string LocateTag = "Locate Tag";
            public const string Setings = "Settings";
            public const string AccessControl = "Access Control";
            public const string PreFilters = "Pre Filters";
            public const string About = "About";

            //Setting menu
            public const string ReaderList = "Reader List";
            public const string Application = "Application";
            public const string Antenna = "Antenna";
            public const string SingulationControl = "Singulation Control";
            public const string StartStopTriggers = "Start/Stop Triggers";
            public const string TagReporting = "Tag Reporting";
            public const string Regulatory = "Regulatory";
            public const string Battery = "Battery";
            public const string Beeper = "Beeper";
            public const string PowerOptimization = "Power Optimization";
            public const string SaveConfiguration = "Save Configuration";

            //Image
            public const string ImgReaderList = "title_rdl";
            public const string ImgApplication = "title_sett";
            public const string ImgAntenna = "title_antn";
            public const string ImgSingulationControl = "title_singl";
            public const string ImgStartStopTriggers = "title_strstp";
            public const string ImgTagReporting = "title_tags";
            public const string ImgRegulatory = "title_reg";
            public const string ImgBattery = "title_batt";
            public const string ImgBeeper = "title_beep";
            public const string ImgPowerOptimization = "title_pwr_on";
            public const string ImgSaveConfiguration = "title_save";
            public const string ImgRapidReadStart = "startImage";
            public const string ImgRapidReadStop = "stopImage";

            //TabContaner
            public const string InventoryLoad = "Inventory";
            public const string LocateTagLoad = "LocateTag";
            public const string RapidRadLoad = "RapidRead";
            public const string AccessLoad = "Access";
            public const string RapidReadTimeFormat = "{0:00}:{1:00}";
            public const double RapidReadTimeSpanSecond = 1;
            public const string TotalSecondTagReadFormat = "N0";




        //Color Codes
        public const string ColorNavigationBarBlue = "#389cff";

            //Sample readers
            public const string RfidReader1 = "RFID-1279900076545454";
            public const string RfidReader2 = "RFID-97900064456656453";

            //Samples Tag data
            public const string Tag1 = "1279900076545454";
            public const string Tag2 = "97900064456656453";
            public const string Tag3 = "00024442323232467";
            public const string Tag4 = "66622223323323233";

            //Inventory
            public const string Start = "START";
            public const string Stop = "STOP";


            //picker sigulation tag
            public const string Tag0 = "0";
            public const string Tag30 = "30";
            public const string Tag100 = "100";
            public const string Tag200 = "200";
            public const string Tag300 = "300";
            public const string Tag400 = "400";
            public const string Tag500 = "500";
            public const string Tag600 = "600";

            //picker sigulation sl Flag
            public const string All = "ALL";
            public const string Deasserted = "DEASSERTED";
            public const string Asserted = "ASSERTED";

            //picker sigulation session
            public const string Session0 = "S0";
            public const string Session1 = "S1";
            public const string Session2 = "S2";
            public const string Session3 = "S3";

            //picker sigulation inventory state
            public const string StateA = "STATE A";
            public const string StateB = "STATE B";
            public const string AbFlip = "AB FLIP";

            public const int SingulationNoOfRow = 8;

            //Singulation cell id
            public const string SessionCellId = "session_cell";
            public const string SessionPickerCellId = "session_pkr_cell";
            public const string TagPopulationCellId = "tag_polulation_cell";
            public const string TagPopulationPickerCellId = "tag_polulation_pkr_cell";
            public const string InventoryCellId = "inventory_state_cell";
            public const string InventoryPickerCellId = "inventory_state_pkr_cell";
            public const string SlFlagCellId = "sl_flag_cell";
            public const string SlFlagPickerCellId = "sl_flag_pkr_cell";










            public const string Immediate = "Immediate";
            public const string Handheld = "Handheld";
            public const string Periodic = "Periodic";
            public const string Duration = "Duration";
            public const string TagObservation = "Tag Observation";
            public const string NAttempts = "N Attempts";

            public const string Press = "PRESS";
            public const string Release = "RELEASE";

            public const string Disable = "Disable";
            public const string Auto = "Auto";
            public const string Enable = "Enable";
            public const string TriggerTypeRelease = "TRIGGERTYPE_RELEASE";

            public const string InventoryInBatchMode = "  Inventory running in Batch Mode";



            //Start Stop cell
            public const string StartSectionTitle = "start_section_title_cell";
            public const string StartTriggerCell = "start_trigger_cell";
            public const string StartTriggerTypePickerCell = "start_trigger_pkr_one_cell";
            public const string StartTriggerPressReleasePickerCell = "trigger_typ_cell";
            public const string StartTriggerPeriodicCell = "periodic_cell";
            public const string StartTriggerPickerTypeCell = "start_trigger_pkr_two_cell";
            public const string StopSectionTitleCell = "stop_section_title_cell";
            public const string StopTriggerCell = "stop_trigger_cell";
            public const string StopTriggerTypeCell = "stop_trigger_pkr_one_cell";

            public const string StopTriggerPressReleaseCell = "stop_trigger_typ_cell";
            public const string StopTriggerPickerPressReleaseCell = "stop_trigger_pkr_two_cell";
            public const string StopTriggerDurationCell = "stop_triger_duration_cell";
            public const string StopTriggerTagObservationCell = "stop_triger_tag_obs_cell";

            public const string StopTriggerNoAtmptsCell = "stop_triger_no_atmpts_obs_cell";
            public const string StopTriggerTimeoutCell = "stop_triger_time_out_cell";

            public const int PickerExpandCelHeight = 150;
            public const int DetailCelHeight = 40;
            public const int HideCelHeight = 0;
            public const int StatrStopReportNoOfRow = 15;
            public const int TagReportingNoOfRow = 11;

            public const string TagReportFieldCell = "tag_rprt_dt_fld_cell";
            public const string TagReportPcCell = "pc_cell";
            public const string TagReportRssiCell = "rssi_cell";
            public const string TagReportPhaseCell = "phase_cell";
            public const string TagReportChanelIndexCell = "channel_index_cell";
            public const string TagReportTagSeenCell = "tag_seen_count";
            public const string TagReportBatchModeSettingCell = "batch_mode_settings_cell";
            public const string TagReportBatchModeCell = "batch_mode_cell";
            public const string TagReportBatchModePickerCell = "batch_mode_pkr_cell";
            public const string TagReportUniqeTagSettingsCell = "unq_tag_settings_cell";
            public const string TagReportUniqeTagCell = "report_unq_tag_cell";





            public const string MessageTitle = "Exception";
            public const string MessageOk = "Ok";


            public const string ReadersList = "Readers List";

            public const string Trigger = "Start/Stop Triggers";
            public const string TagReportingSetting = "Tag Reporting";





            public const string Settings = "Settings";

            public const string ZeroValue = "0";



            public const string ImgRapidRead = "btn_rr.png";
            public const string ImgInventory = "btn_inv.png";
            public const string ImgLocateTag = "btn_locate.png";
            public const string ImgSettings = "btn_sett.png";
            public const string ImgAcessControl = "btn_access.png";
            public const string ImgPreFilters = "btn_filter.png";

            public const string ImgSettingsReaderList = "dl_rdl.png";
            public const string ImgSettingsApplication = "dl_sett.png";
            public const string ImgSettingsAntenna = "title_antn.png";
            public const string ImgSettingsSigulationControl = "title_singl.png";
            public const string ImgSettingsStartStopTrigger = "title_strstp.png";
            public const string ImgSettingsTagReporting = "title_tags.png";
            public const string ImgSettingsRegulatory = "title_reg.png";
            public const string ImgSettingsBattery = "title_batt.png";
            public const string ImgSettingsBeeper = "dl_beep.png";
            public const string ImgSettingsPowerOptimization = "title_pwr_on.png";
            public const string ImgSettingsSaveConfiguration = "title_save.png";

            public const string StoryboardIDSetting = "SettingsViewController";
            public const string StoryboardIDVersion = "VersionViewController";
            public const string StoryboardIDInventory = "InventoryViewController";
            public const string StoryboardIDReaderList = "ReaderListViewController";
            public const string StoryboardIDTrigger = "TriggerViewController";
            public const string StoryboardIDLocateTag = "LocateTagViewController";
            public const string StoryboardIDTagReporting = "TagReportingViewController";
            public const string StoryboardIDStartStopTrigger = "StartStopViewController";
            public const string StoryboardIDAboutViewController = "AboutViewController";
            public const string StoryboardIDInventoryTabViewController = "InventoryTabViewController";
            public const string StoryboardIDSingulationabViewController = "SingulationViewController";




            public const string InventoryTagCell = "InventoryTagCustomCell";
            public const string ReaderListCell = "ReaderListCustomCell";

            public const string TitleHome = "Home";
            public const string TitleSetting = "Settings";
            public const string TitleLocateTag = "Locate Tag";
            public const string TitleInventory = "Inventory";
            public const string TitleReaderList = "Readers List";
            public const string TitleTagReporting = "Tag Reporting";
            public const string TitleBatchMode = "Batch mode";
            public const string TitleAbout = "About";
            public const string TitleStartStop = "Start\\Stop Triggers";
            public const string TitleSingulation = "Singulation Control";





            public const string ZeroPrecentage = "0%";


            public const float InventoryExpandRowHeight = 100;
            public const float InventoryRowHeight = 40;

            public const float ReaderListExpandRowHeight = 135;
            public const float ReaderListRowHeight = 45;

            public const float SettingCellImageWidth = 24;
            public const float SettingCellImageHeight = 24;


            public const float StartStopTriggerTitleCellHeight = 40;
            public const float StartStopTriggerHeightZero = 0;
            public const float StartStopTriggerPickerHeight = 150;

            public const int Zero = 0;

            public const string ReaderOnBatchMode = "Reader in BatchMode";
            public const string MsgFailedApplySettings= "Failed to apply settings";


           public const int NoOfColoumsInPicker = 1;

            public const int LengthOfBluetoothString = 2;
            public const String BluetoothAddressSeprator = ":";
            public const String NoAvialableReader = "No readers available";


            public const int SettingReaderRow = 0;
            public const int SettingSingulationRow = 3;
            public const int SettingStartStopTriggerRow = 4;
            public const int SettingTagReportingRow = 5;
            public const String SettingsAreSaved = "Settings are being saved";

            public const string RegionCodeNA = "NA";
            public const string RegionCodeUSA = "USA";
            public const string RegionCodeGBR = "GBR";
            public const string SupportedChannelGBR = "866900";

            //Alert Message
            public const string MsgDisconnect = "This will disconnect the application from the scanner, however the device will still be paired to the system.";
            public const string MsgAppsettingDisconnect = " will disconnect the application from the scanner";
            public const string MsgDisconnectTitle = "Disconnect?";
            public const string MsgDisconnectAction = "Disconnect?";
            public const string MsgCancelAction = "Cancel";
            public const string MsgContinueAction = "Continue";
            public const string Msg = "Message";
            public const string MsgUnableToCommunicate = "Unable to communicate with scanner.";
            public const string MsgPleaseTag = "Please select a tag";
            public const string MsgNoActiveReader = "No Active Reader";
            public const string MsgUnableToLoadData = "Unable to load data";
            public const string MsgUnableToSaveData = "Unable to save data";
            public const string MsgUnableToPerformStartStopInventory = "Unable to perform start/stop inventory";
            public const string MsgUnableToPerformStartStopTagLocating = "Unable to perform start/stop tag locating";


            public const string MsgConnectTitle = "Zebra Scanner Control";
            public const string MsgConnected = "\n has connected";
            public const string MsgActionOk = "OK";
            public const string MsgNotSupported = "Feature not supported.";


            public const int StopTriggerPressIndex = 0;
            public const int StopTriggerReleaseIndex = 1;
            public const int StopTriggerImmediateIndex = 0;
            public const int StopTriggerHandheldIndex = 1;
            public const int StopTriggerDurationIndex = 2;
            public const int StopTriggerTagObservationIndex = 3;
            public const int StopTriggerNAttemptsIndex = 4;

            public const int StartTriggerPressIndex = 0;
            public const int StartTriggerReleaseIndex = 1;
            public const int StartTriggerImmediateIndex = 0;
            public const int StartTriggerHandheldIndex = 1;
            public const int StartTriggerPeriodicIndex = 2;

            public const string Model = "Model   :  ";
            public const string Serial = "Serial    :  ";


            public const string LinkProfile1 = "60000 MV 4 1500 25000 25000 0";

            public const string LinkProfile2 = "640000 MV FMO 1500 6250 6250 0";
            public const string LinkProfile3 = "640000 MV FMO 2000 6250 6250 0";

            public const string LinkProfile4 = "120000 MV 2 1500 25000 25000 0";
            public const string LinkProfile5 = "120000 MV 2 1500 12500 23000 2100";
            public const string LinkProfile6 = "120000 MV 2 2000 25000 25000 0";
            public const string LinkProfile7 = "120000 MV 2 2000 12500 23000 2100";

            public const string LinkProfile8 = "128000 MV 2 1500 25000 25000 0";
            public const string LinkProfile9 = "128000 MV 2 1500 12500 23000 2100";
            public const string LinkProfile10 = "128000 MV 2 2000 25000 25000 0";
            public const string LinkProfile11 = "128000 MV 2 2000 12500 23000 2100";

            public const string LinkProfile12 = "160000 MV 2 1500 12500 18800 2100";
            public const string LinkProfile13 = "160000 MV 2 2000 12500 18800 2100";

            public const string LinkProfile14 = "60000 MV 4 1500 25000 25000 0";
            public const string LinkProfile15 = "60000 MV 4 1500 12500 23000 2100";
            public const string LinkProfile16 = "60000 MV 4 2000 25000 25000 0";
            public const string LinkProfile17 = "60000 MV 4 2000 12500 23000 2100";

            public const string LinkProfile18 = "64000 MV 4 1500 25000 25000 0";
            public const string LinkProfile19 = "64000 MV 4 1500 12500 23000 2100";
            public const string LinkProfile20 = "64000 MV 4 2000 25000 25000 0";
            public const string LinkProfile21 = "64000 MV 4 2000 12500 23000 2100";

            public const string LinkProfile22 = "80000 MV 4 1500 12500 18800 2100";
            public const string LinkProfile23 = "80000 MV 4 2000 12500 18800 2100";

            public const string LinkProfile24 = "668 MV FMO 668 668 668 668";

            public const string LinkProfile25 = "320000 MV FMO 1500 12500 18800 2100";
            public const string LinkProfile26 = "320000 MV FMO 2000 12500 18800 2100";


            public const string LinkProfile27 = "30000 MV 8 1500 25000 25000 0";
            public const string LinkProfile28 = "30000 MV 8 1500 12500 23000 2100";
            public const string LinkProfile29 = "30000 MV 8 2000 25000 25000 0";
            public const string LinkProfile30 = "30000 MV 8 2000 12500 23000 2100";

            public const string LinkProfile31 = "32000 MV 8 1500 25000 25000 0";
            public const string LinkProfile32 = "32000 MV 8 1500 12500 23000 2100";
            public const string LinkProfile33 = "32000 MV 8 2000 25000 25000 0";
            public const string LinkProfile34 = "32000 MV 8 2000 12500 23000 2100";


            public const string LinkProfile35 = "40000 MV 8 1500 12500 18800 2100";
            public const string LinkProfile36 = "40000 MV 8 2000 12500 18800 2100";
         
            public const string TitleLinkProfile = "Link Profile";
            public const string TitleAntenna = "Antenna";
            public const string UiSpaceFoTheCheckMark = " ";
            public const string UiSpaceFoTheUnCheckMark = "    ";

            public const int defultSelectedProfileIndex = 0;

            // Access page constants
            public const string TitleOffSet = "Offset";
            public const string TitlePassword = "Password";
            public const string TitleLock = "LOCK";
            public const string TitleLockPriviledge = "Lock Priviledge";
            public const string TitleKill = "KILL";
            public const string TitleKillPassword = "Kill Password";

            public const string MessageWriteFailed = "Write Failed";
            public const string MessageWriteSuccess = "Write Succeed";
            public const string MessageLockFailed = "Lock Failed";
            public const string MessageLockSuccess = "Lock Succeed";
            public const string MessageKillFailed = "Kill Failed";
            public const string MessageKillSuccess = "Kill Succeed";


            //picker access control
            public const string MEMORYBANK_NONE = "NONE";
            public const string MEMORYBANK_EPC = "EPC";
            public const string MEMORYBANK_RESERVED = "RESERVED";
            public const string MEMORYBANK_TID = "TID";
            public const string MEMORYBANK_USER = "USER";
            public const string MEMORYBANK_ACCESS_PASSWORD = "ACCESS PASSWORD";
            public const string MEMORYBANK_KILL_PASSWORD = "KILL PASSWORD";

            //picker access control lock priviledge
            public const string LOCK_PRIVILEDGE_READ_WRITE = "Read and Write";
            public const string LOCK_PRIVILEDGE_PERMANENT_LOCK = "Permanent Lock";
            public const string LOCK_PRIVILEDGE_PERMANENT_UNLOCK = "Permanent Unlock";
            public const string LOCK_PRIVILEDGE_LOCK = "Unlock";



            public const string FILTER_ONE = "Filter 1";
            public const string FILTER_TWO = "Filter 2";

            public const int MEMORY_BANK_EPC_INDEX = 0;
            public const int ACTION_INV_A_OR_ASRT_SL_INDEX = 0;
            public const int TARGET_SESSION_0_INDEX = 1;



    }
}