using System;
using System.Collections.ObjectModel;
using BarcodeSannerApp.Model;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Model
{
    /// <summary>
    /// HomeMenuViewModel
    /// </summary>
    public class HomeMenuViewModel
    {
        public ObservableCollection<MenuItemModel> MenuList { get; set; }

        public HomeMenuViewModel()
        {
            MenuList = new ObservableCollection<MenuItemModel>();
            MenuList.Add(new MenuItemModel { Name = ConstantsString.Inventory });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.AccessControl });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.PreFilters });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.Setings });
            MenuList.Add(new MenuItemModel { Name = ConstantsString.About });
        }
    }
}
