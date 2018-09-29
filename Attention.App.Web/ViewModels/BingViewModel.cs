using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Attention.BLL.Services;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Attention.App.Web.ViewModels
{
    public class BingViewModel : ShellViewModel
    {
        public BingViewModel()
        {
            Title = "Bing";
        }

        public void Submit()
        {
            Trace.WriteLine(DateTime.Now);
        }
    }
}

