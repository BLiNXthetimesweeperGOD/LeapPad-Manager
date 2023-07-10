// Decompiled with JetBrains decompiler
// Type: LFConnect.Form2
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LFConnect
{
  public class Form2 : Form
  {
    private List<string> files = (List<string>) null;
    private int totalfiles = 0;
    private bool ready = false;
    private bool cansave = false;
    private string curpath = Directory.GetCurrentDirectory();
    private string cacheLocation = "cache\\themecache";
    private string fullfileLocation = "";
    private string fullCacheLocation = "";
    private string onlyfile = "";
    private string theme = "";
    private string fmt = "000";
    private string package = "";
    private string json = "";
    private string meta = "";
    private List<string> allImages = new List<string>();
    private IContainer components = (IContainer) null;
    private Label statusInfo;
    private ListBox themeImages;
    private Label themeInfo;
    private Label listInfo;
    private TextBox themeName;
    private Label themeNameText;
    private Button createTheme;
    private Label statusText;
    private Label toomany;
    private Button btnDeleteImg;
    private Label developerText;
    private TextBox developer;
    private Label PackageIDText;
    private Label packageID;
    private Button button1;

    public Form2()
    {
      this.InitializeComponent();
      this.fullCacheLocation = Path.Combine(this.curpath, this.cacheLocation);
      this.fullfileLocation = Path.Combine(this.curpath, nameof (files));
      this.packageID.Text = LFConnect.Properties.Settings.Default.themeID.ToString();
      this.checkCacheFolder();
      this.HideSaveInfo();
    }

    private void checkCacheFolder()
    {
      try
      {
        string path = Path.Combine(this.curpath, "Themes");
        if (!Directory.Exists(Path.Combine(this.curpath, "cache")))
          Directory.CreateDirectory(Path.Combine(this.curpath, "cache"));
        if (!Directory.Exists(this.fullCacheLocation))
        {
          Directory.CreateDirectory(this.fullCacheLocation);
          this.statusInfo.Text = "Created cache folder";
        }
        if (!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
          this.statusInfo.Text = "Created theme folder";
        }
        this.ready = true;
      }
      catch
      {
        this.statusInfo.Text = "Could not create cache folder!";
      }
    }

    private void btn_CreateTheme(object sender, EventArgs e)
    {
      if (!this.cansave)
        return;
      this.theme = this.GetSafeFilename(this.themeName.Text).Trim();
      this.package = Convert.ToInt32(this.packageID.Text).ToString(this.fmt);
      if (this.theme != string.Empty)
      {
        this.checkCacheFolder();
        Form2 form2_1 = this;
        form2_1.json = form2_1.json + "[" + Environment.NewLine;
        for (int index = 0; index < this.allImages.Count; ++index)
        {
          this.onlyfile = Path.GetFileNameWithoutExtension(this.allImages[index]);
          string flash = this.GetSafeFilename(this.onlyfile).Trim() + ".swf";
          this.CreateSWF(this.allImages[index], flash);
          if (index < this.allImages.Count - 1)
          {
            Form2 form2_2 = this;
            form2_2.json = form2_2.json + "\t\"" + flash + "\"," + Environment.NewLine;
          }
          else
          {
            Form2 form2_3 = this;
            form2_3.json = form2_3.json + "\t\"" + flash + "\"" + Environment.NewLine;
          }
        }
        this.json += "]";
        this.CreateThemeFiles();
        this.CreateThemeZip();
        this.ClearThemeCache();
        this.ResetForm();
      }
    }

    private void btnDeleteImg_Click(object sender, EventArgs e)
    {
      if (this.themeImages.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show("Please select an Item first!");
      }
      else
      {
        string str = this.themeImages.SelectedItem.ToString();
        for (int index = 0; index < this.allImages.Count; ++index)
        {
          this.onlyfile = Path.GetFileNameWithoutExtension(this.allImages[index]);
          if (this.onlyfile == str)
          {
            this.themeImages.Items.Remove((object) this.onlyfile);
            this.allImages.RemoveAt(index);
          }
        }
        if (this.allImages.Count == 0)
        {
          this.cansave = false;
          this.HideSaveInfo();
          this.btnDeleteImg.Visible = false;
        }
        if (this.allImages.Count < 11)
        {
          this.cansave = true;
          this.ShowSaveInfo();
          this.toomany.Visible = false;
        }
      }
    }

    private void frm2_dragDrop(object sender, DragEventArgs e)
    {
      if (!this.ready || !e.Data.GetDataPresent(DataFormats.FileDrop))
        return;
      this.files = new List<string>((IEnumerable<string>) (string[]) e.Data.GetData(DataFormats.FileDrop));
      if (this.files.Count > 0)
      {
        this.cansave = true;
        this.ShowSaveInfo();
        this.statusInfo.Text = "Adding Images";
        this.btnDeleteImg.Visible = true;
        this.totalfiles = this.files.Count;
        foreach (string file in this.files)
        {
          this.onlyfile = Path.GetFileNameWithoutExtension(file);
          if (!this.imageExists(this.onlyfile))
          {
            this.allImages.Add(file);
            this.themeImages.Items.Add((object) this.onlyfile);
          }
        }
        this.statusInfo.Text = "Added Images";
        if (this.allImages.Count > 10)
        {
          this.cansave = false;
          this.toomany.Visible = true;
          this.HideSaveInfo();
        }
      }
    }

    private void frm2_dragEnter(object sender, DragEventArgs e)
    {
      if (this.ready && this.GetFilename(out string _, e))
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }

    protected bool GetFilename(out string filename, DragEventArgs e)
    {
      bool filename1 = false;
      filename = string.Empty;
      if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy && e.Data.GetData("FileName") is Array data && data.Length == 1 && data.GetValue(0) is string)
      {
        filename = ((string[]) data)[0];
        string lower = Path.GetExtension(filename).ToLower();
        if (lower == ".jpg" || lower == ".jpeg" || lower == ".png" || lower == ".gif")
          filename1 = true;
      }
      return filename1;
    }

    protected bool imageExists(string file) => file != string.Empty && this.themeImages.FindStringExact(file) != -1;

    public string GetSafeFilename(string filename) => string.Join("", filename.Split(Path.GetInvalidFileNameChars()));

    private void CreateSWF(string img, string flash)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      string extension = Path.GetExtension(img);
      if (extension == ".png")
      {
        startInfo.FileName = "files\\png2swf.exe";
        startInfo.Arguments = "-j 100 -X 272 -Y 480 -o \"" + this.cacheLocation + "\\" + flash + "\" \"" + img + "\"";
      }
      else if (extension == ".gif")
      {
        startInfo.FileName = "files\\gif2swf.exe";
        startInfo.Arguments = "-X 272 -Y 480 -o \"" + this.cacheLocation + "\\" + flash + "\" \"" + img + "\"";
      }
      else
      {
        startInfo.FileName = "files\\jpeg2swf.exe";
        startInfo.Arguments = "-f -q 100 -X 272 -Y 480 -o \"" + this.cacheLocation + "\\" + flash + "\" \"" + img + "\"";
      }
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          this.statusInfo.Text = "created " + flash;
        }
      }
      catch
      {
        this.statusInfo.Text = "Could not create swf file!";
      }
    }

    private void CreateThemeFiles()
    {
      string str = this.GetSafeFilename(this.developer.Text).Trim();
      if (str == string.Empty)
        str = "Deak Phreak";
      str.Replace("\"", "");
      File.WriteAllText(this.fullCacheLocation + "\\ThemesOrder.json", this.json);
      Form2 form2_1 = this;
      form2_1.meta = form2_1.meta + "MetaVersion=\"1.0\"" + Environment.NewLine;
      Form2 form2_2 = this;
      form2_2.meta = form2_2.meta + "Device=\"LeapPad2Explorer\"" + Environment.NewLine;
      Form2 form2_3 = this;
      form2_3.meta = form2_3.meta + "Type=\"MicroDownload\"" + Environment.NewLine;
      Form2 form2_4 = this;
      form2_4.meta = form2_4.meta + "ProductID=0x1F1E0002" + Environment.NewLine;
      Form2 form2_5 = this;
      form2_5.meta = form2_5.meta + "PackageID=\"PADS-0x1F1E0002-200" + this.package + "\"" + Environment.NewLine;
      Form2 form2_6 = this;
      form2_6.meta = form2_6.meta + "Version=\"1.0.0.0\"" + Environment.NewLine;
      Form2 form2_7 = this;
      form2_7.meta = form2_7.meta + "Locale=\"en-us\"" + Environment.NewLine;
      Form2 form2_8 = this;
      form2_8.meta = form2_8.meta + "Publisher=\"" + str + "\"" + Environment.NewLine;
      Form2 form2_9 = this;
      form2_9.meta = form2_9.meta + "Developer=\"" + str + "\"" + Environment.NewLine;
      Form2 form2_10 = this;
      form2_10.meta = form2_10.meta + "Hidden=1" + Environment.NewLine;
      Form2 form2_11 = this;
      form2_11.meta = form2_11.meta + "DeviceAccess=1" + Environment.NewLine;
      Form2 form2_12 = this;
      form2_12.meta = form2_12.meta + "Name=\"Theme Set " + this.theme + "\"" + Environment.NewLine;
      Form2 form2_13 = this;
      form2_13.meta = form2_13.meta + "Description=\"UI_ThemeSet_" + this.theme.Replace(" ", "") + "\"" + Environment.NewLine;
      this.meta += "MDLType=\"UITheme\"";
      File.WriteAllText(this.fullCacheLocation + "\\meta.inf", this.meta);
    }

    private void CreateThemeZip()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.FileName = "files\\7za.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = "a -ttar \"Themes\\" + this.theme + ".tar\" \".\\" + this.cacheLocation + "\\*\"";
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          this.statusInfo.Text = "Created \"Themes\\" + this.theme + ".tar\"";
        }
      }
      catch
      {
        this.statusInfo.Text = "Could not create swf file!";
      }
    }

    private void ClearThemeCache()
    {
      foreach (FileSystemInfo file in new DirectoryInfo(this.fullCacheLocation).GetFiles())
        file.Delete();
    }

    private void ResetForm()
    {
      this.HideSaveInfo();
      this.cansave = false;
      this.themeName.Text = "";
      this.developer.Text = "";
      Decimal num = (Decimal) (Convert.ToInt32(this.packageID.Text) + 1);
      this.packageID.Text = num.ToString();
      LFConnect.Properties.Settings.Default.themeID = num;
      LFConnect.Properties.Settings.Default.Save();
      this.onlyfile = "";
      this.theme = "";
      this.package = "";
      this.json = "";
      this.meta = "";
      this.allImages.Clear();
      this.themeImages.Items.Clear();
    }

    private void ResetPackage(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to reset the package ID count?", "Reset Package ID", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.packageID.Text = "1";
      LFConnect.Properties.Settings.Default.themeID = 1M;
      LFConnect.Properties.Settings.Default.Save();
    }

    private void HideSaveInfo()
    {
      this.themeName.Enabled = false;
      this.themeNameText.Enabled = false;
      this.createTheme.Enabled = false;
      this.developerText.Enabled = false;
      this.developer.Enabled = false;
      this.PackageIDText.Enabled = false;
      this.packageID.Enabled = false;
      this.button1.Enabled = false;
    }

    private void ShowSaveInfo()
    {
      this.themeName.Enabled = true;
      this.themeNameText.Enabled = true;
      this.createTheme.Enabled = true;
      this.developerText.Enabled = true;
      this.developer.Enabled = true;
      this.PackageIDText.Enabled = true;
      this.packageID.Enabled = true;
      this.button1.Enabled = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form2));
      this.statusInfo = new Label();
      this.themeImages = new ListBox();
      this.themeInfo = new Label();
      this.listInfo = new Label();
      this.themeName = new TextBox();
      this.themeNameText = new Label();
      this.createTheme = new Button();
      this.statusText = new Label();
      this.toomany = new Label();
      this.btnDeleteImg = new Button();
      this.developerText = new Label();
      this.developer = new TextBox();
      this.PackageIDText = new Label();
      this.packageID = new Label();
      this.button1 = new Button();
      this.SuspendLayout();
      this.statusInfo.AutoSize = true;
      this.statusInfo.Location = new Point(58, 287);
      this.statusInfo.Name = "statusInfo";
      this.statusInfo.Size = new Size(0, 13);
      this.statusInfo.TabIndex = 10;
      this.themeImages.FormattingEnabled = true;
      this.themeImages.Location = new Point(223, 70);
      this.themeImages.Name = "themeImages";
      this.themeImages.Size = new Size(345, 186);
      this.themeImages.TabIndex = 10;
      this.themeInfo.AutoSize = true;
      this.themeInfo.Location = new Point(12, 9);
      this.themeInfo.Name = "themeInfo";
      this.themeInfo.Size = new Size(436, 13);
      this.themeInfo.TabIndex = 24;
      this.themeInfo.Text = "Drag / drop your images to add them to the theme. you may add up to 10 images per theme";
      this.listInfo.AutoSize = true;
      this.listInfo.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.listInfo.Location = new Point(399, 42);
      this.listInfo.Name = "listInfo";
      this.listInfo.Size = new Size(169, 25);
      this.listInfo.TabIndex = 25;
      this.listInfo.Text = "Images in theme";
      this.listInfo.TextAlign = ContentAlignment.TopRight;
      this.themeName.Location = new Point(15, 70);
      this.themeName.Name = "themeName";
      this.themeName.Size = new Size(202, 20);
      this.themeName.TabIndex = 1;
      this.themeNameText.AutoSize = true;
      this.themeNameText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.themeNameText.Location = new Point(12, 49);
      this.themeNameText.Name = "themeNameText";
      this.themeNameText.Size = new Size(98, 18);
      this.themeNameText.TabIndex = 27;
      this.themeNameText.Text = "Theme Name";
      this.createTheme.Location = new Point(61, 217);
      this.createTheme.Name = "createTheme";
      this.createTheme.Size = new Size(92, 23);
      this.createTheme.TabIndex = 4;
      this.createTheme.Text = "Create Theme";
      this.createTheme.UseVisualStyleBackColor = true;
      this.createTheme.Click += new EventHandler(this.btn_CreateTheme);
      this.statusText.AutoSize = true;
      this.statusText.Location = new Point(12, 287);
      this.statusText.Name = "statusText";
      this.statusText.Size = new Size(40, 13);
      this.statusText.TabIndex = 29;
      this.statusText.Text = "Status:";
      this.toomany.AutoSize = true;
      this.toomany.ForeColor = Color.Red;
      this.toomany.Location = new Point(220, 259);
      this.toomany.Name = "toomany";
      this.toomany.Size = new Size(232, 13);
      this.toomany.TabIndex = 30;
      this.toomany.Text = "You have too many images, please delete some";
      this.toomany.Visible = false;
      this.btnDeleteImg.Cursor = Cursors.Hand;
      this.btnDeleteImg.Location = new Point(493, 262);
      this.btnDeleteImg.Name = "btnDeleteImg";
      this.btnDeleteImg.Size = new Size(75, 23);
      this.btnDeleteImg.TabIndex = 15;
      this.btnDeleteImg.Text = "Delete";
      this.btnDeleteImg.UseVisualStyleBackColor = true;
      this.btnDeleteImg.Visible = false;
      this.btnDeleteImg.Click += new EventHandler(this.btnDeleteImg_Click);
      this.developerText.AutoSize = true;
      this.developerText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.developerText.Location = new Point(12, 111);
      this.developerText.Name = "developerText";
      this.developerText.Size = new Size(75, 18);
      this.developerText.TabIndex = 32;
      this.developerText.Text = "Developer";
      this.developer.Location = new Point(15, 133);
      this.developer.Name = "developer";
      this.developer.Size = new Size(202, 20);
      this.developer.TabIndex = 2;
      this.PackageIDText.AutoSize = true;
      this.PackageIDText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.PackageIDText.Location = new Point(12, 173);
      this.PackageIDText.Name = "PackageIDText";
      this.PackageIDText.Size = new Size(88, 18);
      this.PackageIDText.TabIndex = 34;
      this.PackageIDText.Text = "Package ID:";
      this.packageID.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.packageID.Location = new Point(106, 172);
      this.packageID.Name = "packageID";
      this.packageID.Size = new Size(37, 17);
      this.packageID.TabIndex = 36;
      this.packageID.Text = "1";
      this.packageID.TextAlign = ContentAlignment.TopRight;
      this.button1.Location = new Point(160, 168);
      this.button1.Name = "button1";
      this.button1.Size = new Size(57, 23);
      this.button1.TabIndex = 65;
      this.button1.Text = "Reset";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.ResetPackage);
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(580, 319);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.packageID);
      this.Controls.Add((Control) this.PackageIDText);
      this.Controls.Add((Control) this.developer);
      this.Controls.Add((Control) this.developerText);
      this.Controls.Add((Control) this.btnDeleteImg);
      this.Controls.Add((Control) this.toomany);
      this.Controls.Add((Control) this.statusText);
      this.Controls.Add((Control) this.createTheme);
      this.Controls.Add((Control) this.themeNameText);
      this.Controls.Add((Control) this.themeName);
      this.Controls.Add((Control) this.listInfo);
      this.Controls.Add((Control) this.themeInfo);
      this.Controls.Add((Control) this.themeImages);
      this.Controls.Add((Control) this.statusInfo);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Form2);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Theme Maker - By Deak Phreak";
      this.DragDrop += new DragEventHandler(this.frm2_dragDrop);
      this.DragEnter += new DragEventHandler(this.frm2_dragEnter);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
