﻿using Patient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Drawing;
using System;
using Microsoft.Win32;
using System.IO;

namespace Patient.Views
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        
        public ReportPage()
        {
            InitializeComponent();
            this.DataContext = new ReportPageViewModel();
        }

        
    }
}
