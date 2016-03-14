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
     
        
        //Для указания кодировки страницы
        public static Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        SerialPort MyPort = new SerialPort("COM19");

        /*
         // страшный кусок который перебирает все порты
         var portNames = SerialPort.GetPortNames();

foreach(var port in portNames) {
    //Try for every portName and break on the first working
}
          
         * */


        private void button1_Click(object sender, EventArgs e)
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
                if (match=="1") try
                    {
                        MyPort.Open();
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text="Error opening my port: {0}"+ex.Message;
                    }
                else try
                    {
                        MyPort.Close();
                    }
                    catch (Exception ex)
                    {
                        textBox1.Text = "Error opening my port: {0}" + ex.Message;
                    }
            }
            catch { }
            
            
        }
    }
}
