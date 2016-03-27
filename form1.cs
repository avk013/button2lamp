using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

using System.Net;
//using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        SerialPort MyPort = new SerialPort("COM3");

        private void button1_Click(object sender, EventArgs e)
        {
            int i,j;
            int[,] z = new int[,] { { 1, 2, 3, 4 }, { 0, 9, 8, 7 }, { 5, 6, 4, 2 } };
            for (i = 0; i < 4; i++)
                for (j = 0; j < 3; j++) dataGridView1.Rows[i].Cells[j].Value = Convert.ToString( z[i, j]);

        }


        private void dg(DataGridView datagridname, int max_column, int max_row)
        {
            int min = 0;//0;
            for (int i = min; i < max_column + min; i++) datagridname.Columns.Add("Column", i.ToString());
            for (int i = min; i < max_row ; i++) datagridname.Rows.Add();
           for (int i = min; i <= max_row ; i++) datagridname.Rows[i - min].HeaderCell.Value = i.ToString();
            datagridname.AutoResizeColumns();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dg(dataGridView1, 3, 4);
            dg(dataGridView2, 3, 4);
        }

        private void button2_Click(object sender, EventArgs e)
        {

       
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string html0 = "";
                WebRequest request = WebRequest.Create(new Uri(@"http://edis.pp.ua/project+/button/index.php?date_req=" + DateTime.Now.ToString("dd.MM.yyyy")));
                // Получить ответ с сервера
                WebResponse response = request.GetResponse();

                // Получаем поток данных из ответа
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    // Выводим исходный код страницы
                    string line;
                    while ((line = stream.ReadLine()) != null)
                        html0 += line + "\n";
                }

                //выбираем значение            
                string match = Regex.Match(html0, @"значение.*?лампочки").ToString();
                //вынимаем чсло
                match = Regex.Match(match, @"[0-9]").ToString();

                textBox1.Text = match;
                if (match == "1") try
                    {
                        MyPort.Open();
                        MyPort.RtsEnable = true;

                    }
                    catch (Exception ex)
                    {
                        textBox1.Text = "Error opening my port: {0}" + ex.Message;
                    }
                //if (match == "0")
                else
                    try
                    {
                        MyPort.RtsEnable = false;
                       MyPort.Close();
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text = "Error opening my port: {0}" + ex.Message;
                    }
            }
            catch { }
        }    
        //if (MyPort.RtsEnable()) textBox1.Text="++";
        }}
