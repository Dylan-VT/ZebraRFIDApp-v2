using System;
using Xamarin.Forms;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Model
{
    public class ReaderModel
    {
        public string ReaderName { get; set; }
        public string BluetoothAddress { get; set; }
        public bool isVisibleBluetoothAddress { get; set; }
        public string SerialNumber { get; set; }
        public bool isVisibleSerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public bool isVisibleModelNumber { get; set; }
        public int ReaderId { get; set; }
        public string ConectionStatus { get; set; }
        public Reader ReaderObject { get; set; }
        public Color TextColor { get; set; }

    }
}
