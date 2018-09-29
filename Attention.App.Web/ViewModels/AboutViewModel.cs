using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace Attention.App.Web.ViewModels
{
    public class AboutViewModel : ShellViewModel
    {
        public string Author { get; set; } = "hippieZhou";
        public string Career { get; set; } = "Developer";
        public AboutViewModel()
        {
            Title = "About";
        }
    }
}

