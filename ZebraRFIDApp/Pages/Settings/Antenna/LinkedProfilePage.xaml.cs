using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ZebraRFIDApp.API;
using ZebraRFIDApp.Model;

namespace ZebraRFIDApp.Pages.Settings.Antenna
{
    /// <summary>
    /// Linked profile page responsible for select link profile
    /// </summary>
    public partial class LinkedProfilePage : ContentPage
    {
        private List<LinkedProfileModel> linkedProfileList = new List<LinkedProfileModel>();
        private ObservableCollection<LinkedProfileModel> linkProfileList { get; set; }
        private string[] linkProfileArray = { ConstantsString.LinkProfile1, ConstantsString.LinkProfile2, ConstantsString.LinkProfile3, ConstantsString.LinkProfile4, ConstantsString.LinkProfile5, ConstantsString.LinkProfile6
        ,ConstantsString.LinkProfile7, ConstantsString.LinkProfile8 ,ConstantsString.LinkProfile9, ConstantsString.LinkProfile10
        ,ConstantsString.LinkProfile11, ConstantsString.LinkProfile12 ,ConstantsString.LinkProfile13, ConstantsString.LinkProfile14,
        ConstantsString.LinkProfile15,ConstantsString.LinkProfile16, ConstantsString.LinkProfile17 ,ConstantsString.LinkProfile18, ConstantsString.LinkProfile19,
        ConstantsString.LinkProfile20, ConstantsString.LinkProfile21 ,ConstantsString.LinkProfile22, ConstantsString.LinkProfile23
        ,ConstantsString.LinkProfile24, ConstantsString.LinkProfile25 ,ConstantsString.LinkProfile26, ConstantsString.LinkProfile27,
        ConstantsString.LinkProfile28, ConstantsString.LinkProfile29 ,ConstantsString.LinkProfile30, ConstantsString.LinkProfile31,
        ConstantsString.LinkProfile32, ConstantsString.LinkProfile33 ,ConstantsString.LinkProfile34, ConstantsString.LinkProfile35,
        ConstantsString.LinkProfile36
        };


        /// <summary>
        ///  Linked profile page class constructor
        /// </summary>
        public LinkedProfilePage()
        {
            InitializeComponent();
            Title = ConstantsString.TitleLinkProfile;
            PrepareTheLinkProfileList();
            Globals.IsLinkProfilePageAppear = true;


        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

         
        }

        /// <summary>
        /// Prepare the link profile list
        /// </summary>
        private void PrepareTheLinkProfileList()
        {
            linkedProfileList = new List<LinkedProfileModel>();
            linkProfileList = new ObservableCollection<LinkedProfileModel>();

            foreach (string profileLink in linkProfileArray)
            {
                if  (profileLink == Globals.SelectedLinkProfileName) {
                    linkProfileList.Add(new LinkedProfileModel { LinkProfileName = ConstantsString.CheckMark  + ConstantsString.UiSpaceFoTheCheckMark + profileLink });
                }
                else
                {
                    linkProfileList.Add(new LinkedProfileModel { LinkProfileName = ConstantsString.UiSpaceFoTheUnCheckMark + profileLink });
                }              

            }
            listLinkProfile.ItemsSource = linkProfileList;

        }

        /// <summary>
        /// Select link profile item in listview
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="tappedEventArg">Event argument</param>
        private void OnItemSelected(Object sender, ItemTappedEventArgs tappedEventArg)
        {
            var selectedLinkProfilename = (LinkedProfileModel)(tappedEventArg.Item);
            Globals.SelectedLinkProfileName = linkProfileArray[tappedEventArg.ItemIndex];           
            Globals.SelectedLinkProfileNameIndex = tappedEventArg.ItemIndex;
            refreshListView();

        }

        /// <summary>
        /// Refresh listView to update the check mark
        /// </summary>
        private void refreshListView()
        {
            this.listLinkProfile.ItemsSource = null;
            PrepareTheLinkProfileList();
           
        }


    }
}


