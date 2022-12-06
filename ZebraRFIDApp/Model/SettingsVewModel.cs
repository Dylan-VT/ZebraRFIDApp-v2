using System;
using System.Collections.ObjectModel;
using BarcodeSannerApp.Model;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Model
{
    /// <summary>
    /// SettingsVewModel
    /// </summary>
    public class SettingsVewModel
    {
        public ObservableCollection<ImageMenuModel> SettingList { get; set; }

        public SettingsVewModel()
        {
            SettingList = new ObservableCollection<ImageMenuModel>();
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.ReaderList, ImageName = ConstantsString.ImgReaderList});
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.Application, ImageName = ConstantsString.ImgApplication});
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.Antenna, ImageName = ConstantsString.ImgAntenna });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.SingulationControl, ImageName = ConstantsString.ImgSingulationControl });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.StartStopTriggers, ImageName = ConstantsString.ImgStartStopTriggers });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.TagReporting, ImageName = ConstantsString.ImgTagReporting });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.Regulatory, ImageName = ConstantsString.ImgRegulatory });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.Battery, ImageName = ConstantsString.ImgBattery });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.Beeper, ImageName = ConstantsString.ImgBeeper });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.PowerOptimization, ImageName = ConstantsString.ImgPowerOptimization });
            SettingList.Add(new ImageMenuModel { Name = ConstantsString.SaveConfiguration, ImageName = ConstantsString.ImgSaveConfiguration });

        }
    }
}
