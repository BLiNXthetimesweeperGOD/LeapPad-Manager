// Decompiled with JetBrains decompiler
// Type: LFConnect.Form3
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LFConnect
{
  public class Form3 : Form
  {
    private List<string> files = (List<string>) null;
    private int totalfiles = 0;
    private bool ready = false;
    private string curpath = Directory.GetCurrentDirectory();
    private string cacheLocation = "cache\\videocache";
    private string fullfileLocation = "";
    private string fullCacheLocation = "";
    private string fmt = "000";
    private string vquality = "";
    private string video = "";
    private string Download = "";
    private string onlyfile = "";
    private string size = "";
    private string width = "";
    private string height = "";
    private string package = "";
    private string q = "";
    private string json = "";
    private string meta = "";
    private string addedvid = "";
    private IContainer components = (IContainer) null;
    private Label packageID;
    private Label PackageIDText;
    private TextBox developer;
    private Label developerText;
    private Label statusText;
    private Button convertVideo;
    private Label themeNameText;
    private TextBox videoName;
    private Label themeInfo;
    private Label vidAdded;
    private Label label1;
    private Button createPackage;
    private Label statusInfo;
    private RadioButton radioButton1;
    private RadioButton radioButton2;
    private Label label2;
    private Label label4;
    private ListBox quality;
    private Label label5;
    private TextBox youtube;
    private RadioButton radioButton3;
    private Button button1;
    private TextBox ffmpeg;
    private Label label3;
    private Label label6;
    private ProgressBar progressBar1;
    private Button btn_addConvertVideo;

    public Form3()
    {
      this.InitializeComponent();
      this.fullCacheLocation = Path.Combine(this.curpath, this.cacheLocation);
      this.fullfileLocation = Path.Combine(this.curpath, nameof (files));
      this.checkCacheFolder();
      this.quality.SelectedIndex = 0;
      this.packageID.Text = LFConnect.Properties.Settings.Default.videoID.ToString();
      if (System.IO.File.Exists("files\\ffmpeg.exe"))
        return;
      if (MessageBox.Show("ffmpeg is missing and must be downloaded before you can convert videos.\nWould you like me to download it for you?", "ffmpeg.exe missing", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        this.statusInfo.Text = "Downloading FFmpeg";
        this.progressBar1.Visible = true;
        this.convertVideo.Enabled = false;
        this.createPackage.Enabled = false;
        if (System.IO.File.Exists("cache\\ffmpeg.7z"))
          this.extractFfmpeg();
        else
          this.downloadFfmpeg();
      }
      else
      {
        int num = (int) MessageBox.Show("Please download ffmpeg and put ffmpeg.exe in the files folder\nYou are unable to convert videos without it.");
        Process.Start("http://www.ffmpeg.org/download.html");
        this.convertVideo.Enabled = false;
        this.createPackage.Enabled = false;
        this.statusInfo.Text = "ffmpeg.exe is missing";
      }
    }

    private void checkCacheFolder()
    {
      try
      {
        string path = Path.Combine(this.curpath, "Videos");
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
          this.statusInfo.Text = "Created video folder";
        }
        this.ready = true;
      }
      catch
      {
        this.statusInfo.Text = "Could not create cache folder!";
      }
    }

    private void btn_ConvertVideo(object sender, EventArgs e)
    {
      if (this.ready)
      {
        this.video = this.GetSafeFilename(this.videoName.Text).Trim();
        this.Download = this.youtube.Text.Trim();
        this.package = Convert.ToInt32(this.packageID.Text).ToString(this.fmt);
        if (!(this.video != string.Empty))
          return;
        this.checkCacheFolder();
        this.ClearVideoCache();
        if (this.UnzipTemplate())
        {
          this.q = this.quality.SelectedItem.ToString();
          bool flag = this.radioButton2.Checked;
          if (this.radioButton2.Checked)
          {
            this.width = "480";
            this.height = "272";
            this.size = "-s 480x272";
          }
          else if (this.radioButton3.Checked)
          {
            this.width = "480";
            this.height = "272";
            this.size = "-vf scale=\"'if(gt(a,4/3),480,-1)':'if(gt(a,4/3),-1,272)'\"";
          }
          else
          {
            this.width = "363";
            this.height = "272";
            this.size = "-s 363x272";
          }
          if (this.q == "Low")
            this.vquality = "200";
          if (this.q == "Medium")
            this.vquality = "400";
          if (this.q == "High")
            this.vquality = "600";
          if (this.q == "Higher")
            this.vquality = "800";
          if (this.q == "Super")
            this.vquality = "1000";
          if (this.Download != string.Empty)
            this.DownloadVideo();
          this.ConvertVideo();
          this.CreateVideoFiles();
        }
      }
      else
        this.statusInfo.Text = "Can't proceed. Maybe the cache folder can't be created..";
    }

    private void btn_CreateApp(object sender, EventArgs e)
    {
      if (this.ready)
      {
        this.CreateVideoZip();
        this.ClearVideoCache();
        this.ResetForm();
      }
      else
        this.statusInfo.Text = "Can't proceed. Maybe the cache folder can't be created..";
    }

    private void frm3_dragDrop(object sender, DragEventArgs e)
    {
      if (!this.ready || !e.Data.GetDataPresent(DataFormats.FileDrop))
        return;
      this.files = new List<string>((IEnumerable<string>) (string[]) e.Data.GetData(DataFormats.FileDrop));
      if (this.files.Count > 0)
      {
        this.statusInfo.Text = "Adding Video";
        this.totalfiles = this.files.Count;
        if (this.totalfiles == 1)
        {
          this.onlyfile = Path.GetFileNameWithoutExtension(this.files[0]);
          this.vidAdded.Text = this.onlyfile + "." + Path.GetExtension(this.files[0]).ToLower();
          this.videoName.Text = this.onlyfile;
          this.addedvid = this.files[0];
          this.statusInfo.Text = "Added Video";
        }
        else
          this.statusInfo.Text = "You can only add 1 video";
      }
    }

    private void frm3_dragEnter(object sender, DragEventArgs e)
    {
      if (this.ready && this.GetFilename(out string _, e))
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }

    private void btn_addConvertVideo_Click(object sender, EventArgs e)
    {
      if (!this.ready)
        return;
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Multiselect = true;
      int num = (int) openFileDialog.ShowDialog();
      this.files = new List<string>((IEnumerable<string>) openFileDialog.FileNames);
      if (this.files.Count > 0)
      {
        this.statusInfo.Text = "Adding Video";
        this.totalfiles = this.files.Count;
        if (this.totalfiles == 1)
        {
          this.onlyfile = Path.GetFileNameWithoutExtension(this.files[0]);
          this.vidAdded.Text = this.onlyfile + "." + Path.GetExtension(this.files[0]).ToLower();
          this.videoName.Text = this.onlyfile;
          this.addedvid = this.files[0];
          this.statusInfo.Text = "Added Video";
        }
        else
          this.statusInfo.Text = "You can only add 1 video";
      }
    }

    protected bool GetFilename(out string filename, DragEventArgs e)
    {
      filename = string.Empty;
      return true;
    }

    public string GetSafeFilename(string filename) => string.Join("", filename.Split(Path.GetInvalidFileNameChars()));

    private void DownloadVideo()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = false;
      startInfo.FileName = "files\\getvideo.exe";
      startInfo.Arguments = "-i -o \"cache\\downvideo.mp4\" --no-part \"" + this.Download + "\"";
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          this.statusInfo.Text = "Downloaded video";
          this.addedvid = Path.Combine(this.curpath, "cache") + "\\downvideo.mp4";
        }
      }
      catch
      {
        this.statusInfo.Text = "Could not download video!";
        this.ClearVideoCache();
      }
    }

    private void ConvertVideo()
    {
      if (this.addedvid != string.Empty)
      {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = false;
        string str = "";
        if (this.ffmpeg.Text.Trim() != string.Empty)
          str = this.ffmpeg.Text.Trim();
        startInfo.FileName = "files\\ffmpeg.exe";
        startInfo.Arguments = "-i \"" + this.addedvid + "\" " + str + " -acodec libvorbis -b " + this.vquality + "k -ac 2 -ab 56k -ar 16000 -r 15 " + this.size + " \"" + this.fullCacheLocation + "\\Video.ogg\"";
        try
        {
          using (Process process = Process.Start(startInfo))
          {
            process.WaitForExit();
            this.statusInfo.Text = "Converted video, proceed with the next step";
          }
        }
        catch
        {
          this.statusInfo.Text = "Could not convert video!";
          this.ClearVideoCache();
        }
      }
      else
      {
        this.statusInfo.Text = "No video found!";
        this.ClearVideoCache();
      }
      if (!System.IO.File.Exists("cache\\downvideo.mp4"))
        return;
      new FileInfo("cache\\downvideo.mp4").Delete();
    }

    private void CreateVideoFiles()
    {
      string str = this.GetSafeFilename(this.developer.Text).Trim();
      if (str == string.Empty)
        str = "Deak Phreak";
      string s;
      using (Process process = new Process())
      {
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.ErrorDialog = false;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.FileName = "files\\ffmpeg.exe";
        process.StartInfo.Arguments = "-i \"" + this.fullCacheLocation + "\\Video.ogg\"";
        process.Start();
        StreamReader standardError = process.StandardError;
        process.WaitForExit(500);
        string end = standardError.ReadToEnd();
        s = end.Substring(end.IndexOf("Duration: ") + "Duration: ".Length, "00:00:00.00".Length);
        s = Math.Round(TimeSpan.Parse(s).TotalSeconds, 0).ToString();
        if (this.radioButton3.Checked)
        {
          Match match = Regex.Match(end, "([0-9]{2,3})x([0-9]{2,3})", RegexOptions.IgnoreCase);
          if (match.Success)
          {
            this.width = match.Groups[1].Value;
            this.height = match.Groups[2].Value;
          }
        }
      }
      str.Replace("\"", "");
      Form3 form3_1 = this;
      form3_1.meta = form3_1.meta + "MetaVersion=\"1.0\"" + Environment.NewLine;
      Form3 form3_2 = this;
      form3_2.meta = form3_2.meta + "Device=\"LeapPad2Explorer\"" + Environment.NewLine;
      Form3 form3_3 = this;
      form3_3.meta = form3_3.meta + "Type=\"Application\"" + Environment.NewLine;
      Form3 form3_4 = this;
      form3_4.meta = form3_4.meta + "ProductID=0x00020" + this.package + "\"" + Environment.NewLine;
      Form3 form3_5 = this;
      form3_5.meta = form3_5.meta + "PackageID=\"DEAK-0x00020" + this.package + "-000000\"" + Environment.NewLine;
      Form3 form3_6 = this;
      form3_6.meta = form3_6.meta + "Version=\"1.0.0.0\"" + Environment.NewLine;
      Form3 form3_7 = this;
      form3_7.meta = form3_7.meta + "Locale=\"en-us\"" + Environment.NewLine;
      Form3 form3_8 = this;
      form3_8.meta = form3_8.meta + "Publisher=\"" + str + "\"" + Environment.NewLine;
      Form3 form3_9 = this;
      form3_9.meta = form3_9.meta + "Developer=\"" + str + "\"" + Environment.NewLine;
      Form3 form3_10 = this;
      form3_10.meta = form3_10.meta + "Name=\"" + this.video + "\"" + Environment.NewLine;
      Form3 form3_11 = this;
      form3_11.meta = form3_11.meta + "AppSo=\"App.so\"" + Environment.NewLine;
      Form3 form3_12 = this;
      form3_12.meta = form3_12.meta + "Icon=\"iconLPAD.png\"" + Environment.NewLine;
      Form3 form3_13 = this;
      form3_13.meta = form3_13.meta + "PreviewImage=\"previewimage.png\"" + Environment.NewLine;
      Form3 form3_14 = this;
      form3_14.meta = form3_14.meta + "Category=\"Video\"" + Environment.NewLine;
      Form3 form3_15 = this;
      form3_15.meta = form3_15.meta + "DeviceAccess=1" + Environment.NewLine;
      System.IO.File.WriteAllText(this.fullCacheLocation + "\\meta.inf", this.meta);
      this.json = "";
      Form3 form3_16 = this;
      form3_16.json = form3_16.json + "{" + Environment.NewLine;
      Form3 form3_17 = this;
      form3_17.json = form3_17.json + "\t\"VideoFile\":\"Video.ogg\"," + Environment.NewLine;
      Form3 form3_18 = this;
      form3_18.json = form3_18.json + "\t\"AudioFile\":\"Video.ogg\"," + Environment.NewLine;
      Form3 form3_19 = this;
      form3_19.json = form3_19.json + "\t\"VideoTime\":" + s.Trim() + "," + Environment.NewLine;
      Form3 form3_20 = this;
      form3_20.json = form3_20.json + "\t\"FrameRate\":15," + Environment.NewLine;
      Form3 form3_21 = this;
      form3_21.json = form3_21.json + "\t\"SourceWidth\":" + this.width + "," + Environment.NewLine;
      Form3 form3_22 = this;
      form3_22.json = form3_22.json + "\t\"SourceHeight\":" + this.height + "," + Environment.NewLine;
      Form3 form3_23 = this;
      form3_23.json = form3_23.json + "\t\"TargetWidth\":" + this.width + "," + Environment.NewLine;
      Form3 form3_24 = this;
      form3_24.json = form3_24.json + "\t\"TargetHeight\":" + this.height + "," + Environment.NewLine;
      Form3 form3_25 = this;
      form3_25.json = form3_25.json + "\t\"ShowUI\":1" + Environment.NewLine;
      Form3 form3_26 = this;
      form3_26.json = form3_26.json + "}" + Environment.NewLine;
      System.IO.File.WriteAllText(this.fullCacheLocation + "\\VideoInfo.json", this.json);
    }

    private bool UnzipTemplate()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.FileName = "files\\7za.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = "x \"files\\videoTemplate.zip\" -o\"" + this.cacheLocation + "\" * -r";
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          this.statusInfo.Text = "Extracted template";
          return true;
        }
      }
      catch
      {
        this.statusInfo.Text = "Could not extract template!";
      }
      return false;
    }

    private void CreateVideoZip()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.FileName = "files\\7za.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = "a -ttar \"Videos\\" + this.video + ".tar\" \".\\" + this.cacheLocation + "\\*\"";
      try
      {
        using (Process process = Process.Start(startInfo))
        {
          process.WaitForExit();
          this.statusInfo.Text = "Created \"Videos\\" + this.video + ".tar\"";
        }
      }
      catch
      {
        this.statusInfo.Text = "Could not create video file!";
        this.ClearVideoCache();
      }
    }

    private void ClearVideoCache()
    {
      foreach (FileSystemInfo file in new DirectoryInfo(this.fullCacheLocation).GetFiles())
        file.Delete();
    }

    private void DeleteFfmpegCache(string ffmpegLocation) => Directory.Delete(ffmpegLocation, true);

    private void ResetForm()
    {
      this.videoName.Text = "";
      Decimal num = (Decimal) (Convert.ToInt32(this.packageID.Text) + 1);
      this.packageID.Text = num.ToString();
      LFConnect.Properties.Settings.Default.videoID = num;
      LFConnect.Properties.Settings.Default.Save();
      this.video = "";
      this.package = "";
      this.json = "";
      this.meta = "";
      this.addedvid = "";
      this.width = "";
      this.onlyfile = "";
      this.vidAdded.Text = "Drop Video Here";
    }

    private void downloadFfmpeg()
    {
      WebClient webClient = new WebClient();
      webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.Completed);
      webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ProgressChanged);
      webClient.DownloadFileAsync(new Uri("http://ffmpeg.zeranoe.com/builds/win32/static/ffmpeg-latest-win32-static.7z"), "cache\\\\ffmpeg.7z");
    }

    private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e) => this.progressBar1.Value = e.ProgressPercentage;

    private void Completed(object sender, AsyncCompletedEventArgs e)
    {
      this.statusInfo.Text = "Extracting FFmpeg";
      this.progressBar1.Visible = false;
      this.extractFfmpeg();
    }

    private void extractFfmpeg()
    {
      foreach (string directory in Directory.GetDirectories("cache"))
      {
        if (Path.GetFileName(directory).Substring(0, 6) == "ffmpeg")
          this.DeleteFfmpegCache(directory);
      }
      if (System.IO.File.Exists("cache\\ffmpeg.7z") && System.IO.File.Exists("files\\7za.exe"))
      {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        startInfo.FileName = "files\\7za.exe";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.Arguments = "x \"cache\\ffmpeg.7z\" -o\"cache\" * -r";
        try
        {
          using (Process process = Process.Start(startInfo))
          {
            process.WaitForExit();
            this.statusInfo.Text = "Extracted ffmpeg";
            string str = "";
            foreach (string directory in Directory.GetDirectories("cache"))
            {
              string fileName = Path.GetFileName(directory);
              if (fileName.Substring(0, 6) == "ffmpeg")
                str = fileName;
            }
            if (str != "")
            {
              if (System.IO.File.Exists("cache\\" + str + "\\bin\\ffmpeg.exe"))
              {
                this.statusInfo.Text = str + "\\bin\\ffmpeg.exe found";
                System.IO.File.Move("cache\\" + str + "\\bin\\ffmpeg.exe", "files\\ffmpeg.exe");
                this.DeleteFfmpegCache("cache\\" + str);
                System.IO.File.Delete("cache\\ffmpeg.7z");
                this.ffmpegInstalled();
              }
              else
              {
                this.statusInfo.Text = str + "\\bin\\ffmpeg.exe not found";
                this.DeleteFfmpegCache("cache\\" + str);
              }
            }
            else
              this.statusInfo.Text = "Could not find ffmpeg cache folder";
          }
        }
        catch
        {
          this.statusInfo.Text = "Could not extract ffmpeg!";
        }
      }
      else
        this.statusInfo.Text = "Could not find ffmpeg or 7zip";
    }

    private void ffmpegInstalled()
    {
      this.statusInfo.Text = "ffmpeg has been installed";
      this.progressBar1.Visible = false;
      this.convertVideo.Enabled = true;
      this.createPackage.Enabled = true;
    }

    private void ResetPackage(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to reset the package ID count?", "Reset Package ID", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.packageID.Text = "1";
      LFConnect.Properties.Settings.Default.videoID = 1M;
      LFConnect.Properties.Settings.Default.Save();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form3));
      this.packageID = new Label();
      this.PackageIDText = new Label();
      this.developer = new TextBox();
      this.developerText = new Label();
      this.statusText = new Label();
      this.convertVideo = new Button();
      this.themeNameText = new Label();
      this.videoName = new TextBox();
      this.themeInfo = new Label();
      this.vidAdded = new Label();
      this.label1 = new Label();
      this.createPackage = new Button();
      this.statusInfo = new Label();
      this.radioButton1 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.label2 = new Label();
      this.label4 = new Label();
      this.quality = new ListBox();
      this.label5 = new Label();
      this.youtube = new TextBox();
      this.radioButton3 = new RadioButton();
      this.button1 = new Button();
      this.ffmpeg = new TextBox();
      this.label3 = new Label();
      this.label6 = new Label();
      this.progressBar1 = new ProgressBar();
      this.btn_addConvertVideo = new Button();
      this.SuspendLayout();
      this.packageID.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.packageID.Location = new Point(528, 391);
      this.packageID.Name = "packageID";
      this.packageID.Size = new Size(32, 13);
      this.packageID.TabIndex = 48;
      this.packageID.Text = "1";
      this.packageID.TextAlign = ContentAlignment.TopRight;
      this.PackageIDText.AutoSize = true;
      this.PackageIDText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.PackageIDText.Location = new Point(388, 389);
      this.PackageIDText.Name = "PackageIDText";
      this.PackageIDText.Size = new Size(134, 18);
      this.PackageIDText.TabIndex = 47;
      this.PackageIDText.Text = "Unique Package ID";
      this.developer.Location = new Point(157, 121);
      this.developer.Name = "developer";
      this.developer.Size = new Size(202, 20);
      this.developer.TabIndex = 38;
      this.developer.Text = "Deak Phreak";
      this.developerText.AutoSize = true;
      this.developerText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.developerText.Location = new Point(12, 121);
      this.developerText.Name = "developerText";
      this.developerText.Size = new Size(139, 18);
      this.developerText.TabIndex = 46;
      this.developerText.Text = "3. Developer Name:";
      this.statusText.AutoSize = true;
      this.statusText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.statusText.Location = new Point(13, 452);
      this.statusText.Name = "statusText";
      this.statusText.Size = new Size(54, 18);
      this.statusText.TabIndex = 45;
      this.statusText.Text = "Status:";
      this.convertVideo.Location = new Point(15, 264);
      this.convertVideo.Name = "convertVideo";
      this.convertVideo.Size = new Size(102, 23);
      this.convertVideo.TabIndex = 40;
      this.convertVideo.Text = "6. Convert Video";
      this.convertVideo.UseVisualStyleBackColor = true;
      this.convertVideo.Click += new EventHandler(this.btn_ConvertVideo);
      this.themeNameText.AutoSize = true;
      this.themeNameText.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.themeNameText.Location = new Point(12, 97);
      this.themeNameText.Name = "themeNameText";
      this.themeNameText.Size = new Size(109, 18);
      this.themeNameText.TabIndex = 44;
      this.themeNameText.Text = "2. Video Name:";
      this.videoName.Location = new Point(157, 98);
      this.videoName.Name = "videoName";
      this.videoName.Size = new Size(202, 20);
      this.videoName.TabIndex = 37;
      this.themeInfo.AutoSize = true;
      this.themeInfo.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.themeInfo.Location = new Point(12, 18);
      this.themeInfo.Name = "themeInfo";
      this.themeInfo.Size = new Size(248, 16);
      this.themeInfo.TabIndex = 42;
      this.themeInfo.Text = "1. Drag / drop your video you wish to use";
      this.vidAdded.AutoSize = true;
      this.vidAdded.Location = new Point(31, 50);
      this.vidAdded.Name = "vidAdded";
      this.vidAdded.Size = new Size(86, 13);
      this.vidAdded.TabIndex = 49;
      this.vidAdded.Text = "Drop Video Here";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(12, 310);
      this.label1.MaximumSize = new Size(400, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(392, 40);
      this.label1.TabIndex = 50;
      this.label1.Text = "7. Open the cache/videocache folder and edit the icon files \"iconLPAD.png\" and \"previewimage.png\"";
      this.createPackage.Location = new Point(15, 384);
      this.createPackage.Name = "createPackage";
      this.createPackage.Size = new Size(102, 23);
      this.createPackage.TabIndex = 51;
      this.createPackage.Text = "8. Create App Package";
      this.createPackage.UseVisualStyleBackColor = true;
      this.createPackage.Click += new EventHandler(this.btn_CreateApp);
      this.statusInfo.AutoSize = true;
      this.statusInfo.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.statusInfo.Location = new Point(73, 452);
      this.statusInfo.Name = "statusInfo";
      this.statusInfo.Size = new Size(0, 18);
      this.statusInfo.TabIndex = 52;
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new Point(435, 187);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(59, 17);
      this.radioButton1.TabIndex = 53;
      this.radioButton1.Text = "Square";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(285, 210);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(128, 17);
      this.radioButton2.TabIndex = 54;
      this.radioButton2.Text = "Stretched (full screen)";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(282, 163);
      this.label2.Name = "label2";
      this.label2.Size = new Size(151, 18);
      this.label2.TabIndex = 55;
      this.label2.Text = "5. Choose video size:";
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(12, 163);
      this.label4.Name = "label4";
      this.label4.Size = new Size(165, 18);
      this.label4.TabIndex = 58;
      this.label4.Text = "4. Choose video quality:";
      this.quality.FormattingEnabled = true;
      this.quality.Items.AddRange(new object[5]
      {
        (object) "Low",
        (object) "Medium",
        (object) "High",
        (object) "Higher",
        (object) "Super"
      });
      this.quality.Location = new Point(15, 187);
      this.quality.Name = "quality";
      this.quality.Size = new Size(199, 43);
      this.quality.TabIndex = 59;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(282, 18);
      this.label5.Name = "label5";
      this.label5.Size = new Size(219, 16);
      this.label5.TabIndex = 60;
      this.label5.Text = "Or enter the URL to a youtube video";
      this.youtube.Location = new Point(285, 43);
      this.youtube.Name = "youtube";
      this.youtube.Size = new Size(259, 20);
      this.youtube.TabIndex = 61;
      this.radioButton3.AutoSize = true;
      this.radioButton3.Checked = true;
      this.radioButton3.Location = new Point(285, 187);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(108, 17);
      this.radioButton3.TabIndex = 63;
      this.radioButton3.TabStop = true;
      this.radioButton3.Text = "Keep aspect ratio";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.button1.Location = new Point(503, 410);
      this.button1.Name = "button1";
      this.button1.Size = new Size(57, 23);
      this.button1.TabIndex = 64;
      this.button1.Text = "Reset";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.ResetPackage);
      this.ffmpeg.Location = new Point(285, 267);
      this.ffmpeg.Name = "ffmpeg";
      this.ffmpeg.Size = new Size(259, 20);
      this.ffmpeg.TabIndex = 65;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(144, 269);
      this.label3.Name = "label3";
      this.label3.Size = new Size(138, 13);
      this.label3.TabIndex = 66;
      this.label3.Text = "Optional ffmpeg commands:";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(463, 290);
      this.label6.Name = "label6";
      this.label6.Size = new Size(77, 13);
      this.label6.TabIndex = 67;
      this.label6.Text = "-af volume=1.5";
      this.progressBar1.Location = new Point(12, 482);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(541, 13);
      this.progressBar1.TabIndex = 68;
      this.progressBar1.Visible = false;
      this.btn_addConvertVideo.Location = new Point(157, 45);
      this.btn_addConvertVideo.Name = "btn_addConvertVideo";
      this.btn_addConvertVideo.Size = new Size(102, 23);
      this.btn_addConvertVideo.TabIndex = 69;
      this.btn_addConvertVideo.Text = "1. Add Video";
      this.btn_addConvertVideo.UseVisualStyleBackColor = true;
      this.btn_addConvertVideo.Click += new EventHandler(this.btn_addConvertVideo_Click);
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(565, 504);
      this.Controls.Add((Control) this.btn_addConvertVideo);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.ffmpeg);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.radioButton3);
      this.Controls.Add((Control) this.youtube);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.quality);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.radioButton2);
      this.Controls.Add((Control) this.radioButton1);
      this.Controls.Add((Control) this.statusInfo);
      this.Controls.Add((Control) this.createPackage);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.vidAdded);
      this.Controls.Add((Control) this.packageID);
      this.Controls.Add((Control) this.PackageIDText);
      this.Controls.Add((Control) this.developer);
      this.Controls.Add((Control) this.developerText);
      this.Controls.Add((Control) this.statusText);
      this.Controls.Add((Control) this.convertVideo);
      this.Controls.Add((Control) this.themeNameText);
      this.Controls.Add((Control) this.videoName);
      this.Controls.Add((Control) this.themeInfo);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Form3);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Video Creator - By Deak Phreak";
      this.DragDrop += new DragEventHandler(this.frm3_dragDrop);
      this.DragEnter += new DragEventHandler(this.frm3_dragEnter);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
