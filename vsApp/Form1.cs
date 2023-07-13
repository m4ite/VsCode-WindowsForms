using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace vsApp
{
    public partial class Form1 : Form
    {

        private string diretorio = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog.SelectedPath;
                diretorio = selectedPath;
                var files = Directory.GetFiles(selectedPath);
                foreach(var file in files)
                    treeView1.Nodes.Add(Path.GetFileName(file));
            }
        }



        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage("Untitled");
            

            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.TabIndex = 1;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
            tabPage.Controls.Add(richTextBox);

            
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var file = diretorio + '\\' + e.Node.Text;

            FileStream fs = new FileStream(file, FileMode.Open);
            StreamReader reader = new StreamReader(fs);

            TabPage tabPage = new TabPage(e.Node.Text);
            tabControl1.TabPages.Add(tabPage);

            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Text = reader.ReadToEnd();
            tabPage.Controls.Add(richTextBox);
            tabControl1.SelectedTab = tabPage;

            reader.Close();

        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.rtf";
            openFileDialog.Filter = "RTF Files|*.rtf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(fs);

                TabPage tabPage = new TabPage(Path.GetFileName(openFileDialog.FileName));
                tabControl1.TabPages.Add(tabPage);

                treeView1.Nodes.Add(Path.GetFileName(openFileDialog.FileName));

                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Dock = DockStyle.Fill;
                richTextBox.Text = reader.ReadToEnd();
                tabPage.Controls.Add(richTextBox);
                tabControl1.SelectedTab = tabPage;

                reader.Close();
            }
        }

        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            var selected = tabControl1.SelectedTab;
            tabControl1.TabPages.Remove(selected);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count != 0)
            {


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".rtf";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs);



                    writer.WriteLine(
                   tabControl1
                   .SelectedTab
                   .Controls
                   .OfType<RichTextBox>()
                   .FirstOrDefault().Text);
                    writer.Close();


                    FileStream f = new FileStream(saveFileDialog.FileName, FileMode.Open);
                    StreamReader reader = new StreamReader(f);

                    TabPage tabPage = new TabPage(Path.GetFileName(saveFileDialog.FileName));
                    tabControl1.TabPages.Add(tabPage);

                    treeView1.Nodes.Add(Path.GetFileName(saveFileDialog.FileName));


                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                    RichTextBox richTextBox = new RichTextBox();
                    richTextBox.Dock = DockStyle.Fill;
                    richTextBox.Text = reader.ReadToEnd();
                    tabPage.Controls.Add(richTextBox);
                    tabControl1.SelectedTab = tabPage;

                    reader.Close();




                }
            }

        }
    }
}
