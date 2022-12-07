using System;

using Xamarin.Forms;

namespace ZebraRFIDApp.Pages.Database
{
    public class DatabasePage : ContentPage
    {
        //empty page
        StackLayout page = null;
        public DatabasePage()
        {
            page = new StackLayout();

            //connect to sql
            //MySQL Connection established below.
            string connStr = "server=webdb.uvm.edu;user=jjung2_admin;database=JJUNG2_RFID_TEST;port=3306;password=UzAn4dsM6VIZigk1";
            //MySqlConnection conn = new MySqlConnection(connStr);

            Console.WriteLine("Connecting to MySQL...");
            //conn.Open();

            //select assetItemID from table, use this to add buttons to the Children of Parent where the text is the asset it
            string sql = "SELECT assetItemID, tagID from tblTestTagInfo;";
            //MySqlCommand cmd = new MySqlCommand(sql, conn);
            //MySqlDataReader rdr = cmd.ExecuteReader();
            //while (rdr.Read())
            //{   //for each item retrieved, create a new button with it's ID.
            //	var databaseButton = new Button()
            //	{
            //		Text = (string)rdr[0],
            //		VerticalOptions = LayoutOptions.CenterAndExpand,
            //		HorizontalOptions = LayoutOptions.CenterAndExpand,
            //		TextColor = Color.Black,
            //		BorderColor = Color.Blue,
            //		BackgroundColor = Color.Gray,
            //		FontSize = 20,
            //	};
            //	//event handler for click
            //	databaseButton.Clicked += (sender, args) =>
            //	{   //new page with rdr 1
            //		Navigation.PushAsync(new DatabaseItemPage((string) rdr[1]));
            //	};
            //	//add to page
            //	page.Children.Add(databaseButton);

            //}


            //copy page to content
            Content = page;
        }
    }
}