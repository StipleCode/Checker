using CsvHelper;
using SharpCompress.Archives.Rar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checker
{
    public partial class Form1 : Form
    {
        public Data data;
        public Form1()
        {
            InitializeComponent();
            this.data = new Data("URL:", "Username:", "Password:");
        }

        string FolderName;

        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                FolderName = folderBrowserDialog1.SelectedPath;

                foreach (string file in System.IO.Directory.EnumerateFiles(FolderName, "*.rar", System.IO.SearchOption.AllDirectories))
                {
                    checkedListBox1.Items.Add(file);
                    checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, true);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            checkedListBox2.Items.Clear();
            string folderName;
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                foreach (var item in checkedListBox1.Items)
                {
                    var archive = RarArchive.Open(item.ToString());
                    foreach (RarArchiveEntry entry in archive.Entries)
                    {
                        if (!entry.IsDirectory && entry.Key.Contains("Passwords.txt") )
                        {
                            checkedListBox2.Items.Add(entry.Key);
                            checkedListBox2.SetItemChecked(checkedListBox2.Items.Count - 1, true);
                            Stream stream = entry.OpenEntryStream();
                            SearchInFile(stream);
                        }
                    }
                    SaveIntoCsv(item.ToString());
                    data.listData.Clear();
                }
            }
        }
        private void SearchInFile(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                // Read line by line  
                while ((line = reader.ReadLine()) != null)
                {
                    if (!(line == "") && line.Contains(data.URL))
                    {
                        string temp = line;
                        string URL = line.Substring(line.IndexOf(":") + 2);
                        temp = reader.ReadLine();
                        string Username = temp.Substring(temp.IndexOf(":") + 2);
                        temp = reader.ReadLine();
                        string Pass = temp.Substring(temp.IndexOf(":") + 2);

                        data.listData.Add(new listDataStruct(URL, Username, Pass));
                    }
                }
            }
        }
        public void SaveIntoCsv(string name)
        {
            string csvFile = FolderName + name.Substring(name.LastIndexOf("\\"), name.Length - name.LastIndexOf("\\") - 4) + ".csv";
            int k = 1;
            while (File.Exists(csvFile))
            {
                csvFile = FolderName + name.Substring(name.LastIndexOf("\\"), name.Length - name.LastIndexOf("\\") - 4) + k + ".csv";
                k++;
            }
            using (StreamWriter streamWriter = new StreamWriter(csvFile))
            {
                using (CsvWriter csvWriter = new CsvWriter((TextWriter)streamWriter, CultureInfo.CurrentCulture))
                    csvWriter.WriteRecords<listDataStruct>((IEnumerable<listDataStruct>)data.listData);
            }
        }



      

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this.data);
            if (form2.ShowDialog() != DialogResult.OK)
                return;
            this.data.URL = form2.textBox1.Text;
            this.data.Username = form2.textBox2.Text;
            this.data.Pass = form2.textBox3.Text;
        }
    }
    }
