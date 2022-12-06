using System;
using System.Collections.ObjectModel;
using BarcodeSannerApp.Model;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Model
{
    /// <summary>
    /// TagDataViewModel
    /// </summary>
    public class TagDataViewModel
    {
        public ObservableCollection<MenuItemModel> MenuList { get; set; }

        public TagDataViewModel()
        {
            MenuList = new ObservableCollection<MenuItemModel>();
            MenuList.Add(new MenuItemModel { Name = ConstantsString.Tag1 });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.Tag2 });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.Tag3 });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.Tag4 });
       
        }
    }
}
