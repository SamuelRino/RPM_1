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
using System.IO;
using Microsoft.Win32;
using LibMas;
using Lib_10;


// 14.09.2025 Рункин С.В. ИСП-31
// Практическая работа №1 Разработка и оформление алгоритма работы с одномерным массивом
// Разработка и использование библиотек классов для использования в программе
namespace RPM_1
{   

    // Основное окно приложения
    public partial class MainWindow : Window
    {
        private int[] _mas; // Поле для хранения исходного массива целых чисел
        private double[] _formatedMas; // Поле для хранения обработанного массива с результатами вычислений
        private bool currentMas = false; // Флаг, указывающий на наличие инициализированного массива
        public int RndMax; // Максимальное значение для генерации случайных чисел
        public int[] Mas // Свойство для доступа к основному массиву
        {
            get 
            {
                return _mas; 
            }
            set
            {
                if (_mas != value)
                {
                    _mas = value;
                    dgInput.ItemsSource = VisualArray.ToDataTable(_mas).DefaultView; // Обновление DataGrid при изменении массива
                    FormatedMasReset(); // Пересчет производного массива
                    currentMas = true; // Установка флага наличия массива
                }
            }
        }

        // Конструктор главного окна
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки создания пустого массива
        private void btnInitEmpty_Click(object sender, RoutedEventArgs e)
        {
            // Создание и отображение диалогового окна для ввода параметров массива
            AddArr window = new AddArr(this);
            window.Owner = this;
            window.ShowDialog();
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            Massiv.Fill(ref _mas, RndMax);
            dgInput.ItemsSource = VisualArray.ToDataTable(_mas).DefaultView;
            FormatedMasReset();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Massiv.Clear(ref _mas);
            dgInput.ItemsSource = VisualArray.ToDataTable(_mas).DefaultView;
            FormatedMasReset();
        }


        private void dgInput_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                int indexColumn = e.Column.DisplayIndex;
                _mas[indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                FormatedMasReset();
            }
            catch
            {
                MessageBox.Show("Исправьте значение на правильное");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (currentMas)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.DefaultExt = ".txt";
                save.Filter = "Все файлы | *.* | Текстовые файлы | *.txt";
                save.FilterIndex = 2;
                save.Title = "Сохранение массива";
                if (save.ShowDialog() == true)
                {
                    Massiv.Save(_mas, save.FileName);
                }
            } 
            else
            {
                MessageBox.Show("Массива нет");
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            RndMax = 10;
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы | *.* | Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Сохранение массива";
            if (open.ShowDialog() == true)
            {
                StreamReader check = new StreamReader(open.FileName);
                string type = check.ReadLine();
                check.Close();
                if (type == "Mas")
                {
                    try
                    {
                        Massiv.Open(ref _mas, open.FileName);
                        dgInput.ItemsSource = VisualArray.ToDataTable(_mas).DefaultView;
                        FormatedMasReset();
                        currentMas = true;
                    }
                    catch
                    {
                        MessageBox.Show("Возникла непредвиденная ошибка обработки. Вероятно, файл не содержит массива / массив битый");
                    }
                }
                else
                {
                    MessageBox.Show("Вы пытаетесь открыть файл матрицы, что  для задачи неприемлемо");
                }
            }
        }
        private void FormatedMasReset()
        {
            _formatedMas = Computing.Sqrt0Mas(_mas);
            dgOutput.ItemsSource = VisualArray.ToDataTable(_formatedMas).DefaultView;
        }
        private void NumberOnlyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Back || e.Key == Key.OemMinus)
            {
                return;
            }
            e.Handled = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Ввести n целых чисел. Вычислить для чисел > 0 функцию sqrt(x).\nРезультат обработки каждого числа вывести на экран");
        }
    }
}
