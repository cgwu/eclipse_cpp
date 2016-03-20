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
using System.Windows.Shapes;

namespace RoutedEventDemo
{
    /// <summary>
    /// FrmThemeChange.xaml 的交互逻辑
    /// </summary>
    public partial class FrmThemeChange : Window
    {
        public FrmThemeChange()
        {
            InitializeComponent();
        }

        private void btnClassic_Click(object sender, RoutedEventArgs e)
        {
            ThemeSwitcher.SwitchTheme(ThemeEnum.CLASSIC);
        }

        private void btnRoyale_Click(object sender, RoutedEventArgs e)
        {
            ThemeSwitcher.SwitchTheme(ThemeEnum.ROYALE);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /* 
            CLASSIC = 1,
            ROYALE = 2,
            LUNA = 4,
            LUNA_HOMESTEAD = 8,
            LUNA_METALLIC = 16,
            /// <summary>
            /// Vista默认主题
            /// </summary>
            AERO = 32,
            /// <summary>
            /// Office2007主题
            /// </summary>
            OFFICE2007 = 64
            */
            this.cbTheme.Items.Add("CLASSIC");
            this.cbTheme.Items.Add("ROYALE");
            this.cbTheme.Items.Add("LUNA");
            this.cbTheme.Items.Add("LUNA_HOMESTEAD");
            this.cbTheme.Items.Add("LUNA_METALLIC");
            this.cbTheme.Items.Add("AERO");
            this.cbTheme.Items.Add("OFFICE2007");
        }

        private void cbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theme = (ThemeEnum)Enum.Parse(typeof(ThemeEnum), cbTheme.SelectedValue.ToString());
            ThemeSwitcher.SwitchTheme(theme);
        }

        private void btnShowMsg_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello中国");
            var frm = new FrmDataGrid();
            frm.Show();
        }

        private void btnRemoveTheme_Click(object sender, RoutedEventArgs e)
        {
            var theme = (ThemeEnum)Enum.Parse(typeof(ThemeEnum), cbTheme.SelectedValue.ToString());
            ThemeSwitcher.UnloadTheme(theme);
        }
    }
}
