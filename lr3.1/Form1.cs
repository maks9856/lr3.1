using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr3._1
{
    public partial class Form1 : Form
    {
        private int[] array;
        ThreadManager threadManager;
        public Form1()
        {
            InitializeComponent();

            InitializeComboBoxes();
            threadManager = new ThreadManager();
            threadManager.OnMessage += DisplayArrayProcessorMessage;
        }

        private void InitializeComboBoxes()
        {
            comboBox1.Items.AddRange(new object[] { 2, 4, 8 });
            ThreadPriorityHelper threadPriorityHelper = new ThreadPriorityHelper();
            foreach (var item in threadPriorityHelper.GetAllThreadPriorities())
            {
                comboBox2.Items.Add(item);
            }
        }

        private void DisplayArrayProcessorMessage(string message)
        {
            Invoke((Action)(() => richTextBox2.AppendText(message + Environment.NewLine)));
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (int.TryParse(textBox1.Text, out int arraySize) && arraySize > 0)
            {
                array = new int[arraySize];
                await array.GenerateRandomArrayAsync(-1000, 1000);
                await Task.Run(() =>
                {
                    Invoke((Action)(() => richTextBox1.Clear()));
                    for (int i = 0; i < array.Length; i++)
                    {
                        Invoke((Action)(() => richTextBox1.AppendText(array[i] + " ")));
                        
                    }
                });
            }
            else
            { 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is int numberOfThreads)
            {
                threadManager.CreateThreads(array, numberOfThreads);
                DisplayThreadsInComboBox();
            }
            else
            {
            }
        }
        private void DisplayThreadsInComboBox()
        {
            comboBox3.Items.Clear();
            foreach (var thread in threadManager.GetThreads())
            {
                comboBox3.Items.Add(thread.ManagedThreadId.ToString());
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            threadManager.FindMinInParallel();
            comboBox3.Items.Clear();
            threadManager.ClearThreads();
            comboBox3.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox3.SelectedItem != null && int.TryParse(comboBox3.SelectedItem.ToString(), out int id) &&
    Enum.TryParse<ThreadPriority>(comboBox2.SelectedItem?.ToString(), out ThreadPriority priority))
            {
                threadManager.ChangeThreadPriority(id, priority);
            }

        }


    }
}
