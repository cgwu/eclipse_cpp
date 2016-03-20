using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace RoutedEventDemo
{
    /// <summary>
    /// FrmDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class FrmDataGrid : Window
    {
        public FrmDataGrid()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Form loaded");
            DataTable dt = new DataTable();
            dt.Columns.Add("Product", Type.GetType("System.String"));
            dt.Columns.Add("数量", typeof(int));
            dt.Columns.Add("Version", Type.GetType("System.String"));
            dt.Columns.Add("Description", Type.GetType("System.String"));

            var dr = dt.NewRow();
            dr["Product"] = "苹果";
            dr["数量"] = 132;
            dr["Version"] = "1.01";
            dr["Description"] = "新鲜好吃的苹果";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Product"] = "Banana";
            dr["数量"] = 166;
            dr["Version"] = "2.01";
            dr["Description"] = "新鲜好吃的香蕉";
            dt.Rows.Add(dr);

            this.dgUser.ItemsSource = dt.AsDataView();
        }
    }
}
