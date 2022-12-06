using System;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Model
{
    /// <summary>
    /// TagDataModel class
    /// </summary>
    public class TagDataModel
    {
        public string tagID { get; set; }
        public string tagSeenCount { get; set; }
        public string tagTitle { get; set; }
        public string tagPc { get; set; }
        public string tagRssi { get; set; }
        public string tagPhase { get; set; }
        public string tagChannel { get; set; }
        public TagData tagData { get; set; }
        public TagSeenCount tagSeenCountObj{ get; set; }


        public bool isVisibleDetail { get; set; }
        public bool isVisibleTagPc { get; set; }
        public bool isVisibleTagRssi { get; set; }
        public bool isVisibleTagPhase { get; set; }
        public bool isVisibleTagChannel { get; set; }



    }
}
