﻿using Model;
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

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for ReferralPage.xaml
    /// </summary>
    public partial class ReferralPage : Page
    {
        public ReferralPage(Examination exam)
        {
            InitializeComponent();
            this.DataContext = new ViewModel.ReferralViewModel(exam);
        }

    }
}
