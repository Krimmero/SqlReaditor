using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SqlReaditor.Helpers
{
    static class SelectedItem
    {
        private static DataGridCellInfo _cellInfo;
        private static FrameworkElement _frameworkElement;

        public static FrameworkElement FrameworkElement
        {
            get { return _frameworkElement; }
            set { _frameworkElement = value; }
        }

        public static DataGridCellInfo CellInfo
        {
            get { return _cellInfo; }
            set { _cellInfo = value; }
        }
    }
}
