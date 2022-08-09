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
    /// Interaction logic for RecordRenovationView.xaml
    /// </summary>
    public partial class RecordRenovationView : UserControl
    {
        public RecordRenovationView()
        {
            InitializeComponent();
            this.DataContext = new RecordRenovationViewModel();
        }

        public RecordRenovationView(RecordRenovationViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
