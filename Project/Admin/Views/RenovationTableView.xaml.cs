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
    /// Interaction logic for RenovationTableView.xaml
    /// </summary>
    public partial class RenovationTableView : UserControl
    {
        public RenovationTableView()
        {
            InitializeComponent();
            this.DataContext = new RenovationTableViewModel();
        }

        public RenovationTableView(RenovationTableViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
