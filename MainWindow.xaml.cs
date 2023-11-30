using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApphomework27.Models;
using WpfApphomework27.Servises;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApphomework27
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<TodoModel>_todoDataList;
        private FileIOServise _fileIOServise;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        public MainWindow()
        {
            InitializeComponent();
            
        }

        
                private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType==ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted|| e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOServise.SaveDate(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            _fileIOServise = new FileIOServise(PATH);

            /* _todoDataList = new BindingList<TodoModel>()*/
            try
            {
                _todoDataList = _fileIOServise.LoadData();
            }
            catch (Exception ex) 
            {
                MessageBox.Show (ex.Message);
                Close();
            }
            dgTodolist.ItemsSource = _todoDataList;
            _todoDataList.ListChanged += _todoDataList_ListChanged;
        }
       
    }

}
