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
    /// Interaction logic for ChooseFormView.xaml
    /// </summary>
    public partial class ChooseFormView : UserControl
    {
        public ChooseFormView()
        {
            InitializeComponent();
            this.DataContext = new ChooseFormViewModel();
        }

        public ChooseFormView(ChooseFormViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
