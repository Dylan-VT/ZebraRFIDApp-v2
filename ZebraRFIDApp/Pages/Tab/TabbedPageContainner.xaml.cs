using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZebraRFIDApp.API;

namespace ZebraRFIDApp.Pages.Tab
{
    /// <summary>
    /// TabbedPageContainner
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageContainner : TabbedPage
    {
        public TabbedPageContainner(string page)
        {
            InitializeComponent();
            var pages = Children.GetEnumerator();
           
            if (page == ConstantsString.RapidRadLoad)
            {
                pages.MoveNext(); // First page
                CurrentPage = pages.Current;
                Title = ConstantsString.RapidRead;
            }
            else if(page == ConstantsString.InventoryLoad)
            {
                pages.MoveNext(); // First page
                pages.MoveNext(); // Second page
                CurrentPage = pages.Current;
                Title = ConstantsString.Inventory;
            }
            else if(page == ConstantsString.LocateTagLoad)
            {
                pages.MoveNext(); // First page
                pages.MoveNext(); // Second page
                pages.MoveNext(); // Third page
                CurrentPage = pages.Current;
                Title = ConstantsString.LocateTag;

            }
            else
            {
                pages.MoveNext(); // First page
                pages.MoveNext(); // Second page
                pages.MoveNext(); // Third page
                pages.MoveNext(); // Fourth page
                CurrentPage = pages.Current;
                Title = ConstantsString.AccessControl;
            }

        }
    }
}
