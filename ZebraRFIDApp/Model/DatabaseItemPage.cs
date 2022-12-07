using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BarcodeSannerApp.Model;

using Xamarin.Forms;
using ZebraRFIDApp.API;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using static CoreFoundation.DispatchSource;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Security.Policy;
/// <summary>
/// This page is called when you click on an item in the Database menu. It will be used to display the item about that specific asset, as well as feature the ability to modify it
/// </summary>
/// 



namespace ZebraRFIDApp.Model
{

    public class DataFromDataBase
    {
        public List<Truck> data { get; set; }
    }
    public class Truck
    {
        public string truckID { get; set; }
        public string license_plate { get; set; }
        public string make_model { get; set; }
        public string manufactureYear { get; set; }
        public string driverID { get; set; }
        public string acquisition_date { get; set; }
        public string deployment_date { get; set; }
        public string tagID { get; set; }
        public string manufactureDate { get; set; }
        public string dateEntered { get; set; }
        public string installationDate { get; set; }
        public string message { get; set; }

    }    
    public class DatabaseItemPage : ContentPage
    {
        //use this to build the table section, then copy it to content
        public string Name { get; set; }
        TableSection cells = null;
		//page
		TableView page = null;
        //list that has labels for entry cells
        public ObservableCollection<MenuItem> MenuList { get; set; }


        List<string> truckCellLabels = new List<string>() {
            "Truck ID: ",
            "License Plate: ",
            "Make & Model: ",
            "Manufactured (Year): ",
            "Driver ID: ",
            "Acquisition Date: ",
            "Deployment Date: ",
            "Tag ID: ",
            "Manufacture Date: ",
            "Date Entered: ",
            "Installation Date: ",
            "Message: "
		};

        List<string> cellLabelsInspection = new List<string>() {
           "tag ID: ",
           "Inspector Name: ",
           "Result: "
        };


        public DatabaseItemPage(string tagID, List<List<string>> downloaded_list)
        {
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            // index of truck tag ID is stored at
            int tagIdIndex = 8;

            // index is set to the index of the current item if found, otherwise it remains at -1
            int index = -1;
            var text = File.ReadAllText(fileName);

            for (int i = 0; i < downloaded_list.Count; i++)
            {
                for (int j = 0; j < downloaded_list[i].Count; j++)
                {
                    Console.Write(downloaded_list[i][j]);
                    Console.Write(" | ");
                }
                Console.WriteLine();
            }
            //create new TableSection
            TableSection cells = new TableSection();

            page = new TableView()
			{
				Intent = TableIntent.Form,
				Root = new TableRoot
				{
					cells
				}
			};
            for (int i = 0; i < downloaded_list.Count; i++)
            {
                Console.WriteLine(downloaded_list[i][tagIdIndex]);
                if (tagID == downloaded_list[i][tagIdIndex])
                {
                    index = i;
                    break;
                }
            }


            if (index != -1) //item does exist
            {
                for (int i = 0; i < truckCellLabels.Count; i++)
                {   //create new buttons for Table
                    var itemField = new EntryCell()
                    {
                        Label = truckCellLabels[i],
                        Text = downloaded_list[index][i + 1],

                    };
                    Console.WriteLine(downloaded_list[index][i + 1]);
                    cells.Add(itemField);

                }

                //create Inspection title cell
                var inspectionHeader = new EntryCell()
                {
                    Label = "Inspection: "
                };
                cells.Add(inspectionHeader);
                //create inspection cells
                for (int i = 0; i < cellLabelsInspection.Count; ++i)
                {   //create new buttons for Table
                        var itemField = new EntryCell()
                        {
                            Label = cellLabelsInspection[i],
                        };
                        cells.Add(itemField);
                }
            }
            else //item not downloaded
            {
                for (int i = 0; i < truckCellLabels.Count; ++i)
                {   //create new buttons for Table
                    if (i == 0)
                    {
                        var itemField = new EntryCell()
                        {
                            Label = truckCellLabels[i],
                            Text = tagID,

                        };
                        cells.Add(itemField);
                    }
                    else
                    {
                        var itemField = new EntryCell()
                        {
                            Label = truckCellLabels[i],
                        };
                        cells.Add(itemField);
                    }
                }
                for (int i = 0; i < cellLabelsInspection.Count; ++i)
                {   //create new buttons for Table
                    if (i == 0)
                    {
                        var itemField = new EntryCell()
                        {
                            Label = cellLabelsInspection[i],
                            Text = (string)tagID,
                        };
                        cells.Add(itemField);
                    }
                    else
                    {
                        var itemField = new EntryCell()
                        {
                            Label = cellLabelsInspection[i],
                        };
                        cells.Add(itemField);
                    }
                }
            } 



            

            var submitTruck = new Button()
			{
				Text = "Submit Truck",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Black,
				BorderColor = Color.Blue,
				BackgroundColor = Color.Gray,
				FontSize = 20,
				Margin = new Thickness(5, 0)
            };

            var submit2 = new Button()
            {
                Text = "Submit Inspection",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Black,
                BorderColor = Color.Blue,
                BackgroundColor = Color.Gray,
                FontSize = 20,
                Margin = new Thickness(5, 0)
            };

            //event handler for click
            //TODO: 
            submitTruck.Clicked += async (sender, args) =>
			{
                List<string> temp_list = new List<string>();
                foreach(EntryCell i in cells)
                {
                    temp_list.Add(i.Text);
                }
                string lat = "";
                string lon = "";
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        lat = location.Latitude.ToString();
                        lon = location.Longitude.ToString();
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
                using (StreamWriter sw = System.IO.File.AppendText(fileName))
                {
                    sw.WriteLine("MessageBoard^" + temp_list[0] + '|' + temp_list[1] + '|' + temp_list[2] + '|'
                    + temp_list[3] + '|' + temp_list[4] + '|' + temp_list[5] + '|' + temp_list[6] + '|' + temp_list[7] + '|' + temp_list[8] + '|' + temp_list[9] + '|'
                    + temp_list[10] + '|' + temp_list[11]);
                    sw.WriteLine("Usage^" + temp_list[0] + '|' + "Jay Hwasung Jung" + '|' + lat + "," + lon);
                }
                DisplayAlert(ConstantsString.Msg, "MessageBoard saved", ConstantsString.MsgActionOk);
            };
            submit2.Clicked += (sender, args) =>
            {   //new page with rdr 0
                List<string> temp_list = new List<string>();
                foreach (EntryCell i in cells)
                {
                    temp_list.Add(i.Text);
                }
                using (StreamWriter sw = System.IO.File.AppendText(fileName))
                {
                    sw.WriteLine("Inspection^" + temp_list[0] + '|' + temp_list[13] + "|" + temp_list[14]);
                }
                DisplayAlert(ConstantsString.Msg, "Inspection saved", ConstantsString.MsgActionOk);
            };

            //create content page
            Content = new StackLayout
			{
				Children =
				{
                    page,
					submitTruck,
                    submit2
				}
			};
			
		}
        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
        CancellationTokenSource cts;

        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }
    }
}