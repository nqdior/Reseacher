﻿using Reseacher.Properties;
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

namespace Reseacher
{
    /// <summary>
    /// MainControl.xaml の相互作用ロジック
    /// </summary>
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        private void GridSplitter_MouseLeave(object sender, MouseEventArgs e)
        {
            Settings.Default.StripWidth = stripArea.Width.Value;
            Settings.Default.Save();
        }
    }
}
