using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Polybius_square
{
    public partial class MainForm : Form
    {
        char[,] alfrus = {
                             {'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё'},
                             {'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М'}, 
                             {'Н', 'О', 'П', 'Р', 'С', 'Т', 'У'},
                             {'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ'},
                             {'Ы', 'Ь', 'Э', 'Ю', 'Я', '0', '1'},
                             {'2','3', '4', '5', '6', '7', '8'}, 
                             {'9', ' ', ',', '.', '!', '?', '\n'}
                         };

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files (*.txt)|*.txt";
            openDialog.FilterIndex = 1;
            openDialog.RestoreDirectory = true;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openDialog.FileName;
                textBoxDecrypt.Text = File.ReadAllText(fileName, Encoding.UTF8);
            }
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            List<int> encryptInt = new List<int>();
            List<int> encryptVer = new List<int>();
            string decryptText = textBoxDecrypt.Text;
            string encryptText = "";
            for (int i = 0; i < decryptText.Length; i++)
            {
                for (int j = 0; j < alfrus.GetLength(0); j++)
                {
                    for (int k = 0; k < alfrus.GetLength(1); k++)
                    {
                        if (Char.ToLower(alfrus[j, k]) == decryptText[i] || Char.ToUpper(alfrus[j, k]) == decryptText[i])
                        {
                            encryptInt.Add(j);
                            encryptVer.Add(k);
                            break;
                        }
                    }
                }
            }
            encryptInt.AddRange(encryptVer);
            for (int i = 0; i < encryptInt.Count; i += 2)
            {
                encryptText += alfrus[encryptInt[i], encryptInt[i + 1]];
            }
            textBoxEncrypt.Text = encryptText;
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text files (*.txt)|*.txt";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK && saveDialog.FileName.Length > 0)
            {
                File.WriteAllText(saveDialog.FileName, textBoxEncrypt.Text, Encoding.UTF8);
            }
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            List<int> encryptInt = new List<int>();
            List<int> encryptVer = new List<int>();
            string decryptText = textBoxDecrypt.Text;
            string encryptText = "";
            for (int i = 0; i < decryptText.Length; i++)
            {
                for (int j = 0; j < alfrus.GetLength(0); j++)
                {
                    for (int k = 0; k < alfrus.GetLength(1); k++)
                    {
                        if (Char.ToLower(alfrus[j, k]) == decryptText[i] || Char.ToUpper(alfrus[j, k]) == decryptText[i])
                        {
                            encryptInt.Add(j);
                            encryptInt.Add(k);
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < (encryptInt.Count / 2); i++)
            {
                encryptText += alfrus[encryptInt[i], encryptInt[i + (encryptInt.Count / 2)]];
            }
            encryptText = encryptText.Replace("\n", "\r\n");
            textBoxEncrypt.Text = encryptText;
        }
    }
}