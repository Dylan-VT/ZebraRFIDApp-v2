using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using ZebraRFIDApp.API;
using ZebraRFIDApp.Model;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Pages.RapidRead
{

    /// <summary>
    /// Rapid read page
    /// Responsible analyze tag read speed
    /// </summary>
    public partial class RapidReadPage : ContentPage
    {
        Stopwatch stopWatch;
        Readers readerManager;
        int tagReadTimeInSecond = 0;

        public RapidReadPage()
        {
            InitializeComponent();
            rapidReadStartButton.Source = ConstantsString.ImgRapidReadStart;
            try
            {
                readerManager = SdkHandler.GetInstance().ReaderManager;
                readerManager.TagDataEvent += ReaderNotifyDataEvent;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }

        }

        /// <summary>
        /// Button click event for start rapid read
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickedStartRapidRead(object sender, EventArgs args)
        {

            if (SdkHandler.ConnectedReader != null)
            {
                System.Diagnostics.Debug.WriteLine("OnButtonClickedStartRapidRead Click");
                var imageSource = (ImageButton)sender;
                var selectedImage = imageSource.Source as FileImageSource;
                try
                {


                    if (selectedImage.File == ConstantsString.ImgRapidReadStart)
                    {
                        ClearAllValuesBeforeStartRapidRead();
                        rapidReadStartButton.Source = ConstantsString.ImgRapidReadStop;
                        StartTagReadTimer();
                        SdkHandler.ConnectedReader.Actions.Inventory.Start();
                        Globals.IsInventoryStart = true;
                    }
                    else
                    {
                        rapidReadStartButton.Source = ConstantsString.ImgRapidReadStart;
                        StopTagReadTimer();
                        SdkHandler.ConnectedReader.Actions.Inventory.Stop();
                        Globals.IsInventoryStart = false;
                        SdkHandler.ConnectedReader.Actions.Inventory.PurgeData();
                        Globals.StartPressInventory = Globals.InventoryState.Stop;
                        tagReadTimeInSecond = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                DisplayAlert(ConstantsString.Msg, ConstantsString.MsgNoActiveReader, ConstantsString.MsgActionOk);

            }

        }

        /// <summary>
        /// Clear all values before start rapid read
        /// </summary>
        void ClearAllValuesBeforeStartRapidRead()
        {

            SdkHandler.ClearTagataList();
            SdkHandler.ClearTagSeenList();

            SdkHandler.ClearGroupTagsData();
            lableTotalUniqueTag.Text = ConstantsString.ZeroValue;
            lableTotalReadTag.Text = ConstantsString.ZeroValue;
            lableReadRate.Text = ConstantsString.ZeroValue;

        }

        /// <summary>
        /// Start tag read timer
        /// </summary>
        void StartTagReadTimer()
        {

            stopWatch = new Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
            Device.StartTimer(TimeSpan.FromSeconds(ConstantsString.RapidReadTimeSpanSecond), () =>
            {
                TimeSpan timeStamp = stopWatch.Elapsed;
                string elapsedTime = String.Format(ConstantsString.RapidReadTimeFormat, timeStamp.Minutes, timeStamp.Seconds);
                string elapsedTimeInSeconds = timeStamp.TotalSeconds.ToString(ConstantsString.TotalSecondTagReadFormat);
                tagReadTimeInSecond = Int32.Parse(elapsedTimeInSeconds);
                Device.BeginInvokeOnMainThread(() =>
                {
                    lableReadTime.Text = elapsedTime;
                    lableReadRate.Text = ReadRate(tagReadTimeInSecond);

                });
                return true;
            });

        }

        /// <summary>
        /// Stop tag read timer
        /// </summary>
        void StopTagReadTimer()
        {
            stopWatch.Stop();
        }

        string ReadRate(int totalSecondsForTagRead)
        {
            if (totalSecondsForTagRead >= 1)
            {
                int tagReadRate = 0;
                int totalTagRead = Int32.Parse(lableTotalReadTag.Text);
                tagReadRate = (totalTagRead / totalSecondsForTagRead);
                return tagReadRate.ToString(ConstantsString.TotalSecondTagReadFormat);
            }

            return ConstantsString.ZeroValue;

        }

        /// <summary>
        /// Update the lables when rapid read start
        /// </summary>
        /// <param name="tagListReadByReader">The tag list read by the reader</param>
        void UpdateRapidReadUiLables(List<TagSeenCount> tagListReadByReader)
        {

            try
            {

                Console.WriteLine("tag seen counts list " + tagListReadByReader);
                if (tagListReadByReader != null && tagListReadByReader.Count > 0)
                {
                    foreach (TagSeenCount tagSeenCount in tagListReadByReader)
                    {
                        SdkHandler.UpdateTagDetail(tagSeenCount.TagData, tagSeenCount);
                    }

                    int totalTagCount = 0;

                    totalTagCount = SdkHandler.TagSeenCountList.Sum(a => a.SeenCount);

                    if (Globals.UniqueTagEnabled && Globals.ReaderBatchMode != Globals.BatchModeState.Enable)
                    {
                        totalTagCount = SdkHandler.GroupTagsData.Count;
                    }
                   
                    lableTotalReadTag.Text = totalTagCount.ToString();
                    lableTotalUniqueTag.Text = SdkHandler.GroupTagsData.Count.ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }



        }

        /// <summary>
        /// Reader notify data event.
        /// </summary>
        /// <param name="tagData">Tag data.</param>
        private void ReaderNotifyDataEvent(TagData tagData)
        {

            System.Diagnostics.Debug.WriteLine("Inventory ReaderNotifyDataEvent " + tagData.Id);

            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateRapidReadUiLables(SdkHandler.GetTagSeenList());

            });


        }



    }
}
