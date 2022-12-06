using System;
using System.Collections.ObjectModel;
using BarcodeSannerApp.Model;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Model
{
    /// <summary>
    /// ReaderListViewModel
    /// </summary>
    public class ReaderListViewModel
    {

        public ObservableCollection<MenuItemModel> ReaderList { get; set; }

        public ReaderListViewModel()
        {

            ReaderList = new ObservableCollection<MenuItemModel>();
            ReaderList.Add(new MenuItemModel { Name = ConstantsString.RfidReader1 });
            ReaderList.Add(new MenuItemModel { Name = ConstantsString.RfidReader2 });
         
        }
    }
}
