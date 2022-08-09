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
    /// Interaction logic for RequestMedicineCheckView.xaml
    /// </summary>
    public partial class RequestMedicineCheckView : UserControl
    {
        public RequestMedicineCheckView()
        {
            InitializeComponent();
            this.DataContext = new RequestMedicineCheckViewModel();
        }

        public RequestMedicineCheckView(RequestMedicineCheckViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
