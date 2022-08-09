﻿using System;
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

using Admin.ViewModel;

namespace Admin.Views
{
    /// <summary>
    /// Interaction logic for RatingsView.xaml
    /// </summary>
    public partial class RatingsView : UserControl
    {
        public RatingsView()
        {
            InitializeComponent();
            this.DataContext = new RatingsViewModel();
        }

        public RatingsView(RatingsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
