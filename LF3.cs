// Decompiled with JetBrains decompiler
// Type: LFConnect.LF3
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LFConnect
{
  public class LF3 : Form
  {
    private string fileIn = "";
    private string InputFile = "";
    private string CacheFolder = "cache";
    private string DecryptedFolder = "decrypted";
    private bool hasDecrypted = false;
    private BackgroundWorker backgroundWorkerDecrypt = new BackgroundWorker();
    private BufferedBlockCipher cipher = new BufferedBlockCipher((IBlockCipher) new SicBlockCipher((IBlockCipher) new AesEngine()));
    private IContainer components = (IContainer) null;
    private Label LF3lblStatus;
    private Button btnDecrypt;
    private Button btnBrowse;

    public LF3() => this.InitializeComponent();

    private void InitCTR(byte[] key, byte[] iv) => this.cipher.Init(false, (ICipherParameters) new ParametersWithIV((ICipherParameters) new KeyParameter(key), iv));

    private byte[] decryptCTR(byte[] data)
    {
      byte[] numArray = new byte[this.cipher.GetOutputSize(data.Length)];
      int outOff = this.cipher.ProcessBytes(data, 0, data.Length, numArray, 0);
      int num = this.cipher.DoFinal(numArray, outOff);
      int length = outOff + num;
      byte[] destinationArray = new byte[length];
      Array.Copy((Array) numArray, (Array) destinationArray, length);
      return destinationArray;
    }

    private void LF3_Load(object sender, EventArgs e)
    {
      this.LF3lblStatus.Text = "";
      this.backgroundWorkerDecrypt.WorkerReportsProgress = false;
      this.backgroundWorkerDecrypt.DoWork += new DoWorkEventHandler(this.backgroundWorkerDecrypt_DoWork);
      this.backgroundWorkerDecrypt.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerDecrypt_RunWorkerCompleted);
    }

    private void LF3_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;
      foreach (string fileInput in (string[]) e.Data.GetData(DataFormats.FileDrop))
        this.LF3Decrypt(fileInput);
    }

    private void LF3_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }

    private void LF3Decrypt(string fileInput)
    {
      this.fileIn = fileInput;
      this.LF3lblStatus.Text = "Decrypting...";
      this.LF3lblStatus.Refresh();
      this.backgroundWorkerDecrypt.RunWorkerAsync();
    }

    private void btnLF3Decrypt_Click(object sender, EventArgs e)
    {
      if (!(this.InputFile != ""))
        return;
      this.LF3Decrypt(this.InputFile);
    }

    private void btnLF3Browse_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.InputFile = openFileDialog.FileName;
    }

    private void backgroundWorkerDecrypt_DoWork(object sender, DoWorkEventArgs e)
    {
      this.hasDecrypted = false;
      string fileIn = Path.GetFileNameWithoutExtension(this.fileIn).Substring(0, 22);
      string str = this.CacheFolder + "\\" + fileIn + ".bz";
      FileStream fileStream1 = new FileStream(this.fileIn, FileMode.Open, FileAccess.Read);
      FileStream fileStream2 = new FileStream(str, FileMode.OpenOrCreate, FileAccess.Write);
      byte[] key = new byte[16]
      {
        (byte) 68,
        (byte) 238,
        (byte) 51,
        (byte) 65,
        (byte) 74,
        (byte) 86,
        (byte) 72,
        (byte) 225,
        (byte) 94,
        (byte) 28,
        (byte) 126,
        (byte) 21,
        (byte) 133,
        (byte) 177,
        (byte) 7,
        (byte) 56
      };
      byte[] numArray1 = new byte[16];
      int length = (int) new FileInfo(this.fileIn).Length;
      byte[] numArray2 = new byte[length];
      int num = fileStream1.Read(numArray1, 0, 16);
      this.InitCTR(key, numArray1);
      num = fileStream1.Read(numArray2, 0, length);
      byte[] buffer = this.decryptCTR(numArray2);
      fileStream2.Write(buffer, 0, length);
      fileStream2.Close();
      fileStream1.Close();
      GC.Collect();
      if (!File.Exists(str))
        return;
      this.UnzipFile(str);
      File.Delete(str);
      if (File.Exists(this.CacheFolder + "\\" + fileIn))
      {
        if (!Directory.Exists(this.DecryptedFolder))
          Directory.CreateDirectory(this.DecryptedFolder);
        if (this.UnzipFinalFile(fileIn))
          this.hasDecrypted = true;
        File.Delete(this.CacheFolder + "\\" + fileIn);
      }
    }

    private void backgroundWorkerDecrypt_RunWorkerCompleted(
      object sender,
      RunWorkerCompletedEventArgs e)
    {
      if (this.hasDecrypted)
        this.LF3lblStatus.Text = "Decrypted OK";
      else
        this.LF3lblStatus.Text = "Decrypt Failed";
      this.LF3lblStatus.Refresh();
    }

    private bool UnzipFile(string fileIn)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.FileName = "files\\7za.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = "x \"" + fileIn + "\" -o\"" + this.CacheFolder + "\" * -r";
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          return true;
        }
      }
      catch
      {
      }
      return false;
    }

    private bool UnzipFinalFile(string fileIn)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.FileName = "files\\7za.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = "x \"" + this.CacheFolder + "\\" + fileIn + "\" -o\"" + this.DecryptedFolder + "\\" + fileIn + "\" * -r";
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          return true;
        }
      }
      catch
      {
      }
      return false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LF3));
      this.LF3lblStatus = new Label();
      this.btnDecrypt = new Button();
      this.btnBrowse = new Button();
      this.SuspendLayout();
      this.LF3lblStatus.Location = new Point(8, 164);
      this.LF3lblStatus.Name = "LF3lblStatus";
      this.LF3lblStatus.Size = new Size(268, 47);
      this.LF3lblStatus.TabIndex = 5;
      this.LF3lblStatus.Text = "label1";
      this.LF3lblStatus.TextAlign = ContentAlignment.MiddleCenter;
      this.btnDecrypt.Location = new Point(97, 101);
      this.btnDecrypt.Name = "btnDecrypt";
      this.btnDecrypt.Size = new Size(75, 23);
      this.btnDecrypt.TabIndex = 4;
      this.btnDecrypt.Text = "Go";
      this.btnDecrypt.UseVisualStyleBackColor = true;
      this.btnDecrypt.Click += new EventHandler(this.btnLF3Decrypt_Click);
      this.btnBrowse.Location = new Point(97, 51);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new Size(75, 23);
      this.btnBrowse.TabIndex = 3;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new EventHandler(this.btnLF3Browse_Click);
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 262);
      this.Controls.Add((Control) this.LF3lblStatus);
      this.Controls.Add((Control) this.btnDecrypt);
      this.Controls.Add((Control) this.btnBrowse);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (LF3);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (LF3);
      this.Load += new EventHandler(this.LF3_Load);
      this.DragDrop += new DragEventHandler(this.LF3_DragDrop);
      this.DragEnter += new DragEventHandler(this.LF3_DragEnter);
      this.ResumeLayout(false);
    }
  }
}
