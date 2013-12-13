using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using ClassLibraryLFSRCipher;
using System.Threading;

namespace CipherPlugins
{
    public partial class MainForm : Form
    {
        private List<string> allTypes = new List<string>();
        private List<string> algorithmNames = new List<string>();
        private List<string> assemblyFileNames = new List<string>();
        private DllConteiner dllContainer;
        private Object lockNumberRequest = new Object();
        private Object lockAddingInControl = new Object();
        private Object lockLoadDll = new Object();
        private int countRequst;
        private bool isAppearNewDll;


        public struct DataThread
        {
            public string algorithm;
            public string key;
            public string fileFromName;
            public string fileToName;
            public DllConteiner dllContainer;
            public bool isEncrypt;
            public Panel panel;
            public int number;
            public DataThread(string algorithm, string key, string fileFromName, string fileToName, DllConteiner dllContainer, bool isEncrypt,Panel panel,int number)
            {
                this.algorithm = algorithm;
                this.key = key;
                this.fileFromName = fileFromName;
                this.fileToName = fileToName;
                this.dllContainer = dllContainer;
                this.isEncrypt = isEncrypt;
                this.panel = panel;
                this.number = number;
            }
        };

        public MainForm()
        {
            InitializeComponent();
            countRequst = 0;
            isAppearNewDll = false;
        }

        delegate void AddProgressBar(ProgressBar pb);
        delegate void AddLabel(Label lb);
        delegate void ChangeComboBox(ComboBox cb,List<string> names);
        ChangeComboBox delegateChangeCb = new ChangeComboBox(ChangeCB);

        void CipherFile(object obj)
        {
            DataThread data = (DataThread)obj;
            try
            {
                FileStream sr = new FileStream(data.fileFromName, FileMode.Open);
                FileStream sw = new FileStream(data.fileToName, FileMode.Create);
                ICiphers algorithm;
                lock(lockLoadDll)
                    algorithm = data.dllContainer.CreateInstance(data.dllContainer.GetAlgorithmIndex(data.algorithm), data.key);
                byte[] result = { };
                byte[] incomingBytes = new byte[sr.Length];
                sr.Read(incomingBytes, 0, (int)sr.Length);

                Label label = CreateLabel("Algorithm " + data.algorithm + ((data.isEncrypt) ? " encrypt " : " decrypt ") + "from " + data.fileFromName + " to " + data.fileToName, data.number);
                lock (lockAddingInControl)
                    data.panel.Invoke(new AddLabel((lb) => data.panel.Controls.Add(lb)), label);

                ProgressBar pb = CreateProgressBar(incomingBytes.Length, label.Location.X, label.Location.Y + label.Height + 10);
                lock (lockAddingInControl)
                    data.panel.Invoke(new AddProgressBar((p) => data.panel.Controls.Add(p)), pb);

                if (data.isEncrypt)
                    result = algorithm.Encrypt(incomingBytes, pb);
                else
                    result = algorithm.Decrypt(incomingBytes, pb);
                sw.Write(result, 0, result.Length);
                sr.Close();
                sw.Close();
            }
            catch
            {
                Label label = CreateLabel("Algorithm " + data.algorithm + ((data.isEncrypt) ? " encrypt " : " decrypt ") + "can't open the file either " + data.fileFromName + " or " + data.fileToName, data.number);
                lock (lockAddingInControl)
                    data.panel.Invoke(new AddLabel((lb) => data.panel.Controls.Add(lb)), label);
            }
        }

        private ProgressBar CreateProgressBar(int length,int xPosition,int yPosition)
        {
            ProgressBar pb = new ProgressBar();
            pb.Step = 1;
            pb.Size = new Size(200, 10);
            pb.Location = new Point(xPosition,yPosition);
            pb.Maximum = length;
            return pb;
        }

        private Label CreateLabel(string text, int number)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Location = new Point(5, (number - 1) * (label.Height + 15));
            label.Text = text;
            return label;
        }

        private void CheckAppearNewDll()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(100);
                dllContainer.algorithmNames.Clear();
                dllContainer.allTypes.Clear();
                dllContainer.assemblyFileNames.Clear();
                IEnumerable<ICiphers> algorithms = dllContainer.EnumerateAlgorithm(dllContainer.CreateDomain(Directory.GetCurrentDirectory()));
                count = 0;
                foreach (ICiphers algorithm in algorithms)
                    ++count;
                if (count != algorithmNames.Count)
                {
                    //isAppearNewDll = true;
                    lock (lockLoadDll)
                    {
                        FillAlgorithmsData();
                    }
                }
            }
        }

        public static void ChangeCB(ComboBox cb,List<string> names)
        {
            cb.Items.Clear();
            for (int i = 0; i < names.Count; ++i)
                cb.Items.Add(names[i]);
        }
         
        private void FillAlgorithmsData()
        {
            /*if (cb_chooseAlgorithm.InvokeRequired)
                cb_chooseAlgorithm.Invoke(new ChangeComboBox((cb) => cb.Items.Clear()), cb_chooseAlgorithm);
            else*/
            allTypes.Clear();
            algorithmNames.Clear();
            assemblyFileNames.Clear();
            for (int i = 0; i < dllContainer.allTypes.Count; ++i)
                allTypes.Add(dllContainer.allTypes[i]);
            for (int i = 0; i < dllContainer.algorithmNames.Count; ++i)
                algorithmNames.Add(dllContainer.algorithmNames[i]);
            for (int i = 0; i < dllContainer.assemblyFileNames.Count; ++i)
                assemblyFileNames.Add(dllContainer.assemblyFileNames[i]);

            if (cb_chooseAlgorithm.InvokeRequired)
                cb_chooseAlgorithm.Invoke(delegateChangeCb,cb_chooseAlgorithm,algorithmNames);
                //cb_chooseAlgorithm.Invoke(new ChangeComboBox((cb) => cb.Items.Clear()), cb_chooseAlgorithm);
        }

        private void bt_do_Click(object sender, EventArgs e)
        {
            lock (lockNumberRequest)
                ++countRequst;
            DataThread dt = new DataThread(cb_chooseAlgorithm.Text, tb_key.Text, tb_fromFile.Text, tb_toFile.Text, dllContainer, rb_encrypt.Checked,pn_Main,countRequst);
            Thread thread = new Thread(CipherFile);
            thread.Start(dt);
        }
        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dllContainer.UnloadDomain();
            dllContainer.domain = null;
            GC.Collect(2);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cb_chooseAlgorithm.Items.Clear();
            dllContainer = new DllConteiner(cb_chooseAlgorithm);
            dllContainer.LoadDll();
            FillAlgorithmsData();
            Thread loadingThread = new Thread(CheckAppearNewDll);
            loadingThread.Start();
        }

        private void bt_openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
                tb_fromFile.Text = openFileDialog1.FileName;
        }

        private void bt_saveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
                tb_toFile.Text = saveFileDialog1.FileName;
        }
    }
}
