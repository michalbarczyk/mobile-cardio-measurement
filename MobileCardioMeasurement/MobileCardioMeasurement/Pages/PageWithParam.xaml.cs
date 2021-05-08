using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCardioMeasurement.Pages.Base;
using MobileCardioMeasurement.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileCardioMeasurement.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageWithParam : BasePage<PageWithParamViewModel>
    {
        public PageWithParam()
        {
            InitializeComponent();
        }
    }
}