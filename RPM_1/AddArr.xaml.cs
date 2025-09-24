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
using LibMas;

namespace RPM_1
{
    public partial class AddArr : Window
    {       
        private MainWindow _mainWindow;
        public AddArr(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void btnInitialize_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbMasLength.Text) && !string.IsNullOrEmpty(tbRndMax.Text))
            {
                int len = Convert.ToInt32(tbMasLength.Text);
                int rndMax = Convert.ToInt32(tbRndMax.Text);
                int[] newMas = null;
                Massiv.InitEmpty(ref newMas, len);
                _mainWindow.Mas = newMas;
                _mainWindow.RndMax = rndMax;
                this.Close();
                MessageBox.Show("Массив успешно создан!");
            }
            else MessageBox.Show("Заполните пустые поля");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberOnlyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                return;
            }
            if (e.Key == Key.Back)
            {
                return;
            }
            e.Handled = true;
        }
    }
}
