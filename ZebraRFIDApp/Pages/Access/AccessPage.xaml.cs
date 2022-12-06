using System;
using System.Collections.Generic;
using ZebraRFIDApp.API;
using Xamarin.Forms;
using ZebraRfidSdk;
using ZebraRFIDApp.Model;

namespace ZebraRFIDApp.Pages.Access
{
    /// <summary>
    /// AccessPage
    /// </summary>
    public partial class AccessPage : ContentPage
    {

        private short ByteSize;
        private short Offset;
        MemoryBank selectedMemoryBankValue;
        MemoryBank selectedMemoryBankValueLock;
        LockPrivilege selectedLockPriviledgeValue;
        public AccessPage()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Page on appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateReadAndWriteButtonColor();
            UpdateReadWriteScreenUI();
            memoryBankPicker.SelectedIndex = 0;
            offSetPicker.SelectedIndex = 0;
            memoryBankLockPicker.SelectedIndex = 0;
            selectedMemoryBankValue = MemoryBank.MEMORYBANK_EPC;
            selectedMemoryBankValueLock = MemoryBank.MEMORYBANK_EPC;
            selectedLockPriviledgeValue = LockPrivilege.READ_WRITE;
            UpdateOffsetAndByteSizeAccordingToMemoryBank(selectedMemoryBankValue);
            entryData.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
            if (Globals.SelectedTagObject != null)
            {
                txtTagID.Text = Globals.SelectedTagObject.Id;
            }
        }

        /// <summary>
        ///  Page on disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            entryData.Text = "";
        }

        /// <summary>
        /// Button clicked event for tag read write
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickedReadWrite(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickedReadWrite Click");

            try
            {
                UpdateReadAndWriteButtonColor();
                UpdateReadWriteScreenUI();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }

        /// <summary>
        /// Update the selection color for the read and write button.
        /// </summary>
        void UpdateReadAndWriteButtonColor()
        {
            buttonReadAndWrite.BackgroundColor = Color.CornflowerBlue;
            buttonReadAndWrite.TextColor = Color.White;
            buttonLock.BackgroundColor = Color.White;
            buttonLock.TextColor = Color.CornflowerBlue;
            buttonKill.BackgroundColor = Color.White;
            buttonKill.TextColor = Color.CornflowerBlue;
        }

        /// <summary>
        /// Button clicked event for tag lock
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickedLock(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickedLock Click");

            try
            {
                UpdateLockButtonColor();
                UpdateLockScreenUI();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }


        /// <summary>
        /// Update the selection color for the lock button.
        /// </summary>
        void UpdateLockButtonColor()
        {
            buttonReadAndWrite.BackgroundColor = Color.White;
            buttonReadAndWrite.TextColor = Color.CornflowerBlue;
            buttonLock.BackgroundColor = Color.CornflowerBlue;
            buttonLock.TextColor = Color.White;
            buttonKill.BackgroundColor = Color.White;
            buttonKill.TextColor = Color.CornflowerBlue;
        }

        /// <summary>
        /// Button clicked event for tag kill
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickedKill(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickedKill Click");

            try
            {
                UpdateKillButtonColor();
                UpdateKillScreenUI();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }

        /// <summary>
        /// Update the selection color for the kill button.
        /// </summary>
        void UpdateKillButtonColor()
        {
            buttonReadAndWrite.BackgroundColor = Color.White;
            buttonReadAndWrite.TextColor = Color.CornflowerBlue;
            buttonLock.BackgroundColor = Color.White;
            buttonLock.TextColor = Color.CornflowerBlue;
            buttonKill.BackgroundColor = Color.CornflowerBlue;
            buttonKill.TextColor = Color.White;
        }

        /// <summary>
        /// Update the ui for the read and write tab.
        /// </summary>
        void UpdateReadWriteScreenUI()
        {
            labelOffset.Text = ConstantsString.TitleOffSet;
            labelPassword.Text = ConstantsString.TitlePassword;
            offSetPicker.IsVisible = false;
            entryOffset.IsVisible = true;
            labelLength.IsVisible = true;
            entryLength.IsVisible = true;
            labelData.IsVisible = true;
            entryData.IsVisible = true;
            entryLength.IsVisible = true;
            labelMemoryBank.IsVisible = true;
            memoryBankPicker.IsVisible = true;
            labelOffset.IsVisible = true;
            gridViewLockAndKill.IsVisible = false;
            gridViewReadAndWrite.IsVisible = true;
            memoryBankPicker.IsVisible = true;
            memoryBankLockPicker.IsVisible = false;

        }

        /// <summary>
        /// Update the ui for the lock tab.
        /// </summary>
        void UpdateLockScreenUI()
        {
            labelOffset.Text = ConstantsString.TitleLockPriviledge;
            labelPassword.Text = ConstantsString.TitlePassword;
            offSetPicker.IsVisible = true;
            entryOffset.IsVisible = false;
            labelLength.IsVisible = false;
            entryLength.IsVisible = false;
            labelData.IsVisible = false;
            entryData.IsVisible = false;
            entryLength.IsVisible = false;
            labelMemoryBank.IsVisible = true;
            memoryBankPicker.IsVisible = true;
            labelOffset.IsVisible = true;
            entryOffset.IsVisible = false;
            buttonLockAndKill.Text = ConstantsString.TitleLock;
            gridViewReadAndWrite.IsVisible = false;
            gridViewLockAndKill.IsVisible = true;
            memoryBankPicker.IsVisible = false;
            memoryBankLockPicker.IsVisible = true;
        }

        /// <summary>
        /// Update the ui for the kill tab.
        /// </summary>
        void UpdateKillScreenUI()
        {
            UpdateLockScreenUI();
            labelMemoryBank.IsVisible = false;
            memoryBankPicker.IsVisible = false;
            labelOffset.IsVisible = false;
            entryOffset.IsVisible = false;
            offSetPicker.IsVisible = false;
            labelPassword.Text = ConstantsString.TitleKillPassword;
            buttonLockAndKill.Text = ConstantsString.TitleKill;
            gridViewReadAndWrite.IsVisible = false;
            gridViewLockAndKill.IsVisible = true;
            memoryBankLockPicker.IsVisible = false;
        }

        /// <summary>
        /// Event handler of memory bank picker
        /// </summary>
        /// <param name="sender">The object responsible for the picker selection event</param>
        /// <param name="pickerEventArg">Event Handler</param>
        private void OnMemoryBankPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnMemoryBankPickerChanged Click");
            try
            {
                switch (memoryBankPicker.SelectedItem)
                {
                    case ConstantsString.MEMORYBANK_EPC:
                        selectedMemoryBankValue = MemoryBank.MEMORYBANK_EPC;
                        break;
                    case ConstantsString.MEMORYBANK_RESERVED:
                        selectedMemoryBankValue = MemoryBank.MEMORYBANK_RESV;
                        break;
                    case ConstantsString.MEMORYBANK_TID:
                        selectedMemoryBankValue = MemoryBank.MEMORYBANK_TID;
                        break;
                    case ConstantsString.MEMORYBANK_USER:
                        selectedMemoryBankValue = MemoryBank.MEMORYBANK_USER;
                        break;
                    default:
                        break;
                }
                UpdateOffsetAndByteSizeAccordingToMemoryBank(selectedMemoryBankValue);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
           
        }

        /// <summary>
        /// Event handler of memory bank lock picker
        /// </summary>
        /// <param name="sender">The object responsible for the picker selection event</param>
        /// <param name="pickerEventArg">Event Handler</param>
        private void OnMemoryBankLockPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnMemoryBankLockPickerChanged Click");

            try
            {
                switch (memoryBankLockPicker.SelectedItem)
                {
                    case ConstantsString.MEMORYBANK_EPC:
                        selectedMemoryBankValueLock = MemoryBank.MEMORYBANK_EPC;
                        break;
                    case ConstantsString.MEMORYBANK_ACCESS_PASSWORD:
                        selectedMemoryBankValueLock = MemoryBank.MEMORYBANK_ACCESS;
                        break;
                    case ConstantsString.MEMORYBANK_KILL_PASSWORD:
                        selectedMemoryBankValueLock = MemoryBank.MEMORYBANK_KILL;
                        break;
                    case ConstantsString.MEMORYBANK_TID:
                        selectedMemoryBankValueLock = MemoryBank.MEMORYBANK_TID;
                        break;
                    case ConstantsString.MEMORYBANK_USER:
                        selectedMemoryBankValueLock = MemoryBank.MEMORYBANK_USER;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }

        /// <summary>
        /// Event handler of offset picker
        /// </summary>
        /// <param name="sender">The object responsible for the picker selection event</param>
        /// <param name="pickerEventArg">Event Handler</param>
        private void OnOffSetPickerChanged(object sender, EventArgs pickerEventArg)
        {
            System.Diagnostics.Debug.WriteLine(" OnOffSetPickerChanged Click");

            try
            {
                switch (offSetPicker.SelectedItem)
                {
                    case ConstantsString.LOCK_PRIVILEDGE_READ_WRITE:
                        selectedLockPriviledgeValue = LockPrivilege.READ_WRITE;
                        break;
                    case ConstantsString.LOCK_PRIVILEDGE_PERMANENT_LOCK:
                        selectedLockPriviledgeValue = LockPrivilege.PERMANENT_LOCK;
                        break;
                    case ConstantsString.LOCK_PRIVILEDGE_PERMANENT_UNLOCK:
                        selectedLockPriviledgeValue = LockPrivilege.PERMANENT_UNLOCK;
                        break;
                    case ConstantsString.LOCK_PRIVILEDGE_LOCK:
                        selectedLockPriviledgeValue = LockPrivilege.UNLOCK;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
        }

        /// <summary>
        /// Button clicked event for tag read
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickTagRead(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickRead");
            try
            {
                if (txtTagID.Text.Length > 0)
                {
                    TagData tagdataObject = Globals.ConnectedReader.AccessOperationsReadTag(txtTagID.Text, txtPassword.Text, ByteSize, Offset, selectedMemoryBankValue);
                    entryData.Text = tagdataObject.MemoryBankData;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
            
        }

        /// <summary>
        /// Button clicked event for tag write
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickTagWrite(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickWrite");
            try
            {
                bool tagWriteResult = Globals.ConnectedReader.AccessOperationsWriteTag(txtTagID.Text, txtPassword.Text, entryData.Text, Offset, selectedMemoryBankValue, false);
                Console.WriteLine("SuccessWrite" + tagWriteResult);
                
                if (tagWriteResult)
                {
                    DisplayAlert(ConstantsString.MsgConnectTitle, ConstantsString.MessageWriteSuccess, ConstantsString.MsgActionOk);
                }else
                {
                    DisplayAlert(ConstantsString.MsgConnectTitle, ConstantsString.MessageWriteFailed, ConstantsString.MsgActionOk);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
            
        }

        /// <summary>
        /// Button clicked event for tag lock and tag kill
        /// </summary>
        /// <param name="sender">The object responsible for click event</param>
        /// <param name="args">Event Argument</param>
        void OnButtonClickTagLockAndKill(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnButtonClickTagLockAndKill");
            try
            {
                if (buttonLockAndKill.Text == ConstantsString.TitleLock)
                {
                    bool tagLockResult = Globals.ConnectedReader.AccessOperationsLockTag(txtTagID.Text, txtPassword.Text, selectedMemoryBankValueLock, selectedLockPriviledgeValue);
                    Console.WriteLine("SuccessLock" + tagLockResult);

                    if (tagLockResult)
                    {
                        DisplayAlert(ConstantsString.MsgConnectTitle, ConstantsString.MessageLockSuccess, ConstantsString.MsgActionOk);
                    }
                    else
                    {
                        DisplayAlert(ConstantsString.MsgConnectTitle, ConstantsString.MessageLockFailed, ConstantsString.MsgActionOk);
                    }
                }else
                {
                    bool tagKillResult = Globals.ConnectedReader.AccessOperationsKillTag(txtTagID.Text, txtPassword.Text);
                    Console.WriteLine("SuccessKill" + tagKillResult);

                    if (tagKillResult)
                    {
                        DisplayAlert(ConstantsString.MsgConnectTitle, ConstantsString.MessageKillSuccess, ConstantsString.MsgActionOk);
                    }
                    else
                    {
                        DisplayAlert(ConstantsString.MsgConnectTitle, ConstantsString.MessageKillFailed, ConstantsString.MsgActionOk);
                    }
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }

        }


        /// <summary>
        /// Update the memory bank data.
        /// </summary>
        /// <param name="memoryBank">Selected memory bank data</param>
        private void UpdateOffsetAndByteSizeAccordingToMemoryBank(MemoryBank memoryBank)
        {
            Console.WriteLine(memoryBank);
            try
            {
                switch (memoryBank)
                {
                    case MemoryBank.MEMORYBANK_EPC:
                        ByteSize = 0;
                        Offset = 2;
                        entryOffset.Text = Offset.ToString();
                        entryLength.Text = ByteSize.ToString();
                        break;
                    case MemoryBank.MEMORYBANK_TID:
                        ByteSize = 0;
                        Offset = 0;
                        entryOffset.Text = Offset.ToString();
                        entryLength.Text = ByteSize.ToString();
                        break;
                    case MemoryBank.MEMORYBANK_USER:
                        ByteSize = 0;
                        Offset = 0;
                        entryOffset.Text = Offset.ToString();
                        entryLength.Text = ByteSize.ToString();
                        break;
                    case MemoryBank.MEMORYBANK_ACCESS:
                        ByteSize = 2;
                        Offset = 2;
                        entryOffset.Text = Offset.ToString();
                        entryLength.Text = ByteSize.ToString();
                        break;
                    case MemoryBank.MEMORYBANK_KILL:
                        ByteSize = 0;
                        Offset = 2;
                        entryOffset.Text = Offset.ToString();
                        entryLength.Text = ByteSize.ToString();
                        break;
                }
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error " + e.Message);
            }
            
        }
    }
}
