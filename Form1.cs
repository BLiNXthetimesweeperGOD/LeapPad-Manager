// Decompiled with JetBrains decompiler
// Type: LFConnect.Form1
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using LFConnect.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace LFConnect
{
  public class Form1 : Form
  {
    private string lpmVersion;
    private string Device;
    private string folderInstall;
    private string parentPin;
    private bool cancelAppInstall;
    private bool Connected;
    private bool DownloadPopup;
    private bool hasVideoPlayer;
    private bool isInstalling;
    private dftp my_dftp;
    private string m_filename;
    private string onlyfile;
    private string runMfgCommand;
    private string curpath;
    private string flagFile;
    private int totalfiles;
    private int currentfile;
    private int totalProgressValue;
    private long totalFileSize;
    private long totalFileSent;
    private long curFileSize;
    private bool limitSerial;
    private bool onlyService;
    private bool debug;
    private bool deakmenu;
    private bool ispublic;
    private List<string> allowedSerials;
    private List<string> allAppPaths;
    private List<string> allAppNames;
    private List<string> allAppTypes;
    private List<string> allDownloadPaths;
    private List<string> allDownloadNames;
    private List<string> allDownloadTypes;
    private BackgroundWorker backgroundWorker1;
    private List<string> files;
    private List<long> fileSizeArray;
    private IContainer components;
    private ProgressBar pbBatt;
    private Label label2;
    private ProgressBar pbSpace;
    private Label label1;
    private ProgressBar pbFileProgress;
    private Label label3;
    private Label lblFilename;
    private Label progPercent;
    private ProgressBar totalProgress;
    private Label label5;
    private Label totalPercent;
    private ListBox deleteApps;
    private ListBox deleteDownloads;
    private Button btnDeleteApp;
    private Button btnDeleteDownload;
    private Button btnDownloadApp;
    private Button btnDownloadDownload;
    private Button exportListButton;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem leapPadToolStripMenuItem;
    private ToolStripMenuItem connectToolStripMenuItem;
    private ToolStripMenuItem disconnectToolStripMenuItem;
    private ToolStripMenuItem rebootToolStripMenuItem;
    private ToolStripMenuItem developToolStripMenuItem;
    private ToolStripMenuItem calibrateScreenToolStripMenuItem1;
    private ToolStripMenuItem loadDevModeToolStripMenuItem;
    private ToolStripMenuItem resetUnitToolStripMenuItem1;
    private ToolStripMenuItem installTestToolsToolStripMenuItem;
    private ToolStripMenuItem themeMakerToolStripMenuItem1;
    private GroupBox installProgressGroup;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private Label fileSize;
    private Label fileSent;
    private Label curentSize;
    private Label totalFileSizeProgress;
    private ToolStripMenuItem deaksToolsToolStripMenuItem;
    private ToolStripMenuItem installDefaultGreenAppsToolStripMenuItem;
    private ToolStripMenuItem installDefaultPurpleAppsToolStripMenuItem;
    private ToolStripMenuItem installPrincessAppsToolStripMenuItem;
    private ToolStripMenuItem installLP1AppsToolStripMenuItem;
    private Button exportListIdButton;
    private ColorDialog colorDialog1;
    private TextBox sendCommand;
    private Button button1;
    private Button button2;
    private TextBox downFile;
    private ToolStripMenuItem videoMakerToolStripMenuItem;
    private LinkLabel linkLabel1;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem checkForUpdateToolStripMenuItem;
    private Button cancel;
    private ToolStripMenuItem dumpCartToolStripMenuItem;
    private Label totalApps;
    private ToolStripMenuItem setCalibrationDataToolStripMenuItem;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem installAppsFrominstallFolderToolStripMenuItem;
    private Label spaceLeft;
    private Label spaceTotal;
    private ToolStripMenuItem setCalibrationData3ToolStripMenuItem;
    private ToolStripMenuItem ServiceToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
    private ToolStripMenuItem setCalibrationData2ToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem changeParentPinToolStripMenuItem;
    private RichTextBox statusLog;
    private ToolStripMenuItem lF3Menu;
    private ToolStripMenuItem generateSerialHashToolStripMenuItem;
    private ToolStripMenuItem installLP3AppsToolStripMenuItem;
    private ToolStripMenuItem toggleMainAppToolStripMenuItem;
    private ToolStripMenuItem installAppsToolStripMenuItem;
    private ToolStripMenuItem changeHostFileToolStripMenuItem;

    public Form1()
    {
      this.lpmVersion = "7.5.0";
      this.Device = "Unknown";
      this.folderInstall = "";
      this.parentPin = "";
      this.cancelAppInstall = false;
      this.Connected = false;
      this.DownloadPopup = false;
      this.hasVideoPlayer = false;
      this.isInstalling = false;
      this.my_dftp = (dftp) null;
      this.m_filename = "";
      this.onlyfile = "";
      this.runMfgCommand = "";
      this.curpath = Directory.GetCurrentDirectory();
      this.flagFile = "flags.txt";
      this.totalfiles = 0;
      this.currentfile = 0;
      this.totalProgressValue = 0;
      this.totalFileSize = 0L;
      this.totalFileSent = 0L;
      this.curFileSize = 0L;
      this.limitSerial = false;
      this.onlyService = false;
      this.debug = false;
      this.deakmenu = true;
      this.ispublic = false;
      this.allowedSerials = new List<string>();
      this.allAppPaths = new List<string>();
      this.allAppNames = new List<string>();
      this.allAppTypes = new List<string>();
      this.allDownloadPaths = new List<string>();
      this.allDownloadNames = new List<string>();
      this.allDownloadTypes = new List<string>();
      this.backgroundWorker1 = new BackgroundWorker();
      this.files = new List<string>();
      this.fileSizeArray = new List<long>();
      this.components = (IContainer) null;
      this.InitializeComponent();
      this.backgroundWorker1.WorkerSupportsCancellation = true;
      this.Text += this.lpmVersion;
      this.setFlagOptions();
      if (this.ispublic)
      {
        this.deakmenu = false;
        this.limitSerial = true;
        this.Text += " - Public Release?";
      }
      if (this.debug)
        this.Text += " (Debug Mode)";
      if (this.deakmenu)
        this.lF3Menu.Visible = true;
      if (this.onlyService)
      {
        this.deakmenu = false;
        this.ispublic = true;
        this.Text += " (Service Mode)";
        this.helpToolStripMenuItem.Visible = false;
        this.checkForUpdateToolStripMenuItem.Visible = false;
        this.settingsToolStripMenuItem.Visible = false;
        this.linkLabel1.Visible = false;
        this.themeMakerToolStripMenuItem1.Visible = false;
        this.videoMakerToolStripMenuItem.Visible = false;
        this.installAppsFrominstallFolderToolStripMenuItem.Visible = false;
        this.toolStripSeparator1.Visible = false;
      }
      else
      {
        if (Directory.Exists(Path.Combine(this.curpath, "install")))
          return;
        Directory.CreateDirectory(Path.Combine(this.curpath, "install"));
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.backgroundWorker1.WorkerReportsProgress = true;
      this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
    }

    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;
      if (this.Connected)
        this.setInstallDetails((string[]) e.Data.GetData(DataFormats.FileDrop));
      else
        this.UpdateLog("You must connect first");
    }

    private void Form1_DragEnter(object sender, DragEventArgs e)
    {
      if (!this.onlyService)
      {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
          e.Effect = DragDropEffects.Copy;
        else
          e.Effect = DragDropEffects.None;
      }
      else
        e.Effect = DragDropEffects.None;
    }

    public void setParentPin()
    {
      this.parentPin = "";
      if (!(this.Device == "LeapPad Ultra") && !(this.Device == "LeapPad 3"))
        return;
      try
      {
        string str1 = Encoding.ASCII.GetString(this.my_dftp.cat("/LF/Bulk/settings.cfg"));
        char[] chArray = new char[1]{ '\n' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.StartsWith("ParentPIN="))
          {
            this.parentPin = str2.Replace("ParentPIN=", "");
            this.UpdateLog("Parent Pin: " + this.parentPin, "done");
          }
        }
      }
      catch
      {
        this.UpdateLog("Parent Pin: Can't find settings.cfg", "warning", true);
      }
    }

    public void saveParentPin(object sender, EventArgs e)
    {
      string input = Prompt.ShowPinDialog("Enter a new 4 digit pin number", "Change Parent Pin");
      if (Regex.IsMatch(input, "^\\d\\d\\d\\d$"))
      {
        this.my_dftp.run_buffer("#!/bin/sh\necho \"" + Encoding.ASCII.GetString(this.my_dftp.cat("/LF/Bulk/settings.cfg")).Replace("ParentPIN=" + this.parentPin, "ParentPIN=" + input) + "\" > /LF/Bulk/settings.cfg\n");
        this.UpdateLog("New pin '" + input + "' has been saved.", "done", true);
        this.parentPin = input;
      }
      else
        this.UpdateLog("You entered an invalid pin, please enter a 4 digit value", "error", true);
    }

    public void SetDevice()
    {
      if (this.my_dftp.is_dir("/LF/Base/EmeraldMfgTest"))
      {
        this.Device = "Leapster Explorer";
      }
      else
      {
        if (this.my_dftp.is_dir("/LF/Base/MadridMfgTest"))
          this.Device = "LeapPad 1";
        if (this.my_dftp.is_dir("/LF/Bulk/ProgramFiles/RioConnmanApp"))
          this.Device = "LeapPad Ultra";
        if (this.my_dftp.is_dir("/LF/Bulk/ProgramFiles/PADS-0x002B001E-000000"))
          this.Device = "LeapPad 3";
        if (this.my_dftp.is_dir("/LF/Base/MfgTest"))
          this.Device = "LeapPad 2";
        string message = Encoding.ASCII.GetString(this.my_dftp.cat("/Firmware/meta.inf"));
        if (this.hasDebug())
        {
          this.UpdateLog("Firmware Info:");
          this.UpdateLog("LINEBREAK");
          this.UpdateLog(message);
          this.UpdateLog("LINEBREAK");
        }
        string str1 = message;
        char[] chArray = new char[1]{ '\n' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.Contains("Device=\"LeapsterExplorer\""))
          {
            this.Device = "Leapster Explorer";
            break;
          }
          if (str2.Contains("Device=\"LeapPadExplorer\""))
          {
            this.Device = "LeapPad 1";
            break;
          }
          if (str2.Contains("Device=\"LeapPad2Explorer\""))
          {
            this.Device = "LeapPad 2";
            break;
          }
          if (str2.Contains("Device=\"LeapPadUltra\""))
          {
            this.Device = "LeapPad Ultra";
            break;
          }
          if (str2.Contains("Device=\"LeapPad3\""))
          {
            this.Device = "LeapPad 3";
            break;
          }
          if (str2.Contains("Device=\"LeapTV\""))
          {
            this.Device = "LeapTV";
            break;
          }
          if (str2.Contains("Device=\"LeapPadPlatinum\""))
          {
            this.Device = "LeapPad Platinum";
            break;
          }
        }
      }
    }

    public void getInstalledApps()
    {
      int num = 0;
      this.clearApps();
      try
      {
        foreach (string dir in this.my_dftp.dir_list("/LF/Bulk/ProgramFiles"))
        {
          char[] chArray = new char[1]{ ' ' };
          string[] strArray = dir.Split(chArray);
          string input = strArray[strArray.Length - 1].Replace("/", "").Trim();
          string str1 = "";
          str1 = !this.ispublic ? "([A-Z0-9]+)" : "DEAK";
          string str2 = "([A-Z0-9]+)";
          if (Regex.Match(input, str2 + "-0x([A-Za-z0-9]+)-([A-Za-z0-9]+)$", RegexOptions.IgnoreCase).Success)
          {
            string[] appInfo = this.getAppInfo("/LF/Bulk/ProgramFiles/" + input);
            if (appInfo[0] == "MoviePlayerWidget")
              this.hasVideoPlayer = true;
            if (LFConnect.Properties.Settings.Default.LimitApps)
            {
              if (appInfo[1] != "NA" && appInfo[1] != "More" && appInfo[1] != "Other")
              {
                this.allAppPaths.Add(input);
                this.allAppNames.Add(appInfo[0]);
                this.allAppTypes.Add(appInfo[1]);
                ++num;
              }
            }
            else
            {
              this.allAppPaths.Add(input);
              this.allAppNames.Add(appInfo[0]);
              this.allAppTypes.Add(appInfo[1]);
              ++num;
            }
          }
        }
        this.totalApps.Text = this.allAppNames.Count.ToString() + " Apps Installed";
      }
      catch
      {
        this.UpdateLog("Error getting package list", "error", true);
      }
      try
      {
        foreach (string dir in this.my_dftp.dir_list("/LF/Bulk/Downloads"))
        {
          char[] chArray = new char[1]{ ' ' };
          string[] strArray = dir.Split(chArray);
          string input = strArray[strArray.Length - 1].Replace("/", "").Trim();
          if (Regex.Match(input, "([A-Z0-9]+)-0x([A-Za-z0-9]+)-([A-Za-z0-9]+)$", RegexOptions.IgnoreCase).Success)
          {
            string[] appInfo = this.getAppInfo("/LF/Bulk/Downloads/" + input);
            if (!this.ispublic || this.ispublic && appInfo[4] == "UITheme")
            {
              this.allDownloadPaths.Add(input);
              this.allDownloadNames.Add(appInfo[0]);
              this.allDownloadTypes.Add(appInfo[2]);
              ++num;
            }
          }
        }
      }
      catch
      {
        this.UpdateLog("Error getting download package list", "error", true);
      }
      for (int index = 0; index < this.allAppNames.Count; ++index)
        this.deleteApps.Items.Add((object) (this.allAppTypes[index] + " - " + this.allAppNames[index]));
      for (int index = 0; index < this.allDownloadNames.Count; ++index)
        this.deleteDownloads.Items.Add((object) (this.allDownloadTypes[index] + " - " + this.allDownloadNames[index]));
    }

    private void UpdateConnected()
    {
      this.disconnectToolStripMenuItem.Visible = true;
      this.installAppsFrominstallFolderToolStripMenuItem.Visible = true;
      this.installAppsToolStripMenuItem.Visible = true;
      this.connectToolStripMenuItem.Visible = false;
      this.rebootToolStripMenuItem.Visible = true;
      this.exportListButton.Visible = true;
      this.exportListIdButton.Visible = true;
      this.btnDeleteApp.Visible = true;
      this.developToolStripMenuItem.Visible = true;
      if ((this.Device == "LeapPad Ultra" || this.Device == "LeapPad 3") && this.parentPin != "")
        this.changeParentPinToolStripMenuItem.Visible = true;
      if (!this.limitSerial)
      {
        this.btnDownloadApp.Visible = true;
        this.btnDeleteDownload.Visible = true;
        this.btnDownloadDownload.Visible = true;
      }
      if (this.deakmenu)
      {
        this.deaksToolsToolStripMenuItem.Visible = true;
        this.button1.Enabled = true;
        this.button2.Enabled = true;
        this.installTestToolsToolStripMenuItem.Visible = true;
        this.dumpCartToolStripMenuItem.Visible = true;
      }
      if (this.ispublic)
      {
        this.exportListIdButton.Visible = false;
        this.exportListButton.Visible = false;
        this.btnDeleteDownload.Visible = true;
      }
      if (!this.onlyService)
        return;
      this.ServiceToolStripMenuItem.Visible = true;
      this.exportListIdButton.Visible = false;
      this.exportListButton.Visible = false;
      this.btnDeleteDownload.Visible = false;
      this.developToolStripMenuItem.Visible = false;
      this.btnDownloadApp.Visible = false;
      this.btnDeleteApp.Visible = false;
      this.btnDeleteDownload.Visible = false;
      this.btnDownloadDownload.Visible = false;
      this.developToolStripMenuItem.Visible = false;
      this.installAppsFrominstallFolderToolStripMenuItem.Visible = false;
    }

    private void UpdateDisconnected()
    {
      this.disconnectToolStripMenuItem.Visible = false;
      this.installAppsFrominstallFolderToolStripMenuItem.Visible = false;
      this.installAppsToolStripMenuItem.Visible = false;
      this.connectToolStripMenuItem.Visible = true;
      this.rebootToolStripMenuItem.Visible = false;
      this.developToolStripMenuItem.Visible = false;
      this.deaksToolsToolStripMenuItem.Visible = false;
      this.exportListButton.Visible = false;
      this.exportListIdButton.Visible = false;
      this.btnDeleteApp.Visible = false;
      this.btnDownloadApp.Visible = false;
      this.btnDeleteDownload.Visible = false;
      this.btnDownloadDownload.Visible = false;
      this.changeParentPinToolStripMenuItem.Visible = false;
      this.clearApps();
    }

    private void enable_disable_mainapp(object sender, EventArgs e)
    {
      if (!this.FlagExists("main_app"))
      {
        this.my_dftp.run_buffer("#!/bin/sh\ntouch /flags/main_app\n");
        this.UpdateLog("After you reboot the device, the dashboard will no longer load.", "done");
      }
      else
      {
        this.my_dftp.run_buffer("#!/bin/sh\nrm -f /flags/main_app\n");
        this.UpdateLog("The dashboard will now load as normal after rebooting", "done");
      }
    }

    private void btnReset_Click(object sender, EventArgs e) => this.ResetUnit();

    private void btnNewCalib_Click(object sender, EventArgs e) => this.RunNewCalib();

    private void btnMfgTest_Click(object sender, EventArgs e) => this.RunMfgTest();

    private void btnSendCommand_Click(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        if (this.deakmenu)
        {
          if (string.IsNullOrEmpty(this.sendCommand.Text))
            return;
          string text = this.sendCommand.Text;
          this.UpdateLog("Sending Command [" + text + "]");
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\n" + text + "\n"));
          this.UpdateLog("Command Ran", "done");
        }
        else
        {
          int num1 = (int) MessageBox.Show("Sorry this feature is only for Deak");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("You must connect first");
      }
    }

    private void btnToggleDev_Click(object sender, EventArgs e)
    {
      if (!this.FlagExists("developer"))
      {
        this.my_dftp.run_buffer("#!/bin/sh\ntouch /flags/developer\n");
        this.UpdateLog("Set Flag: developer");
        this.UpdateLog("You can now reboot, then assign an IP to the LP adapter of 192.168.0.xxx", "done");
        this.UpdateLog("You can FTP to the LP using the IP: 192.168.0.111, user: root and blank pass", "done");
      }
      else
      {
        this.my_dftp.run_buffer("#!/bin/sh\nrm -f /flags/developer\n");
        this.UpdateLog("Removed Flag: developer", "done");
      }
    }

    private void changeHostFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        if (this.FileExists("/etc/hosts.bak"))
        {
          this.my_dftp.run_buffer("#!/bin/sh\nrm /etc/hosts && cp -avr /etc/hosts.bak /etc/hosts && rm /etc/hosts.bak\n");
          this.UpdateLog("Reverted host file back to original. Reboot required to take effect.");
        }
        else if (this.FileExists("/etc/hosts"))
        {
          switch (MessageBox.Show("Are you sure you want to change your hosts file?\nThis will disable your connection to leapfrogs content servers.", "Are you sure?", MessageBoxButtons.YesNo))
          {
            case DialogResult.Yes:
              this.my_dftp.run_buffer("#!/bin/sh\ncp -avr /etc/hosts /etc/hosts.bak\n");
              string str = Encoding.ASCII.GetString(this.my_dftp.cat("/etc/hosts"));
              this.my_dftp.run_buffer("#!/bin/sh\necho -e \"" + "127.0.0.1       http://devicelog.leapfrog.com/upca/device_log_upload\n127.0.0.1       http://services.leapfrog.com/upca/device_content_management\n127.0.0.1       https://services.leapfrog.com/upca/package_management" + "\" > /etc/hosts\n");
              this.my_dftp.run_buffer("#!/bin/sh\necho -e \"" + str + "\" >> /etc/hosts\n");
              this.UpdateLog("Changed hosts file to block leapfrog servers. Reboot required to take effect.");
              break;
            case DialogResult.No:
              this.UpdateLog("Hosts file has not been changed.");
              break;
          }
        }
        else
          this.UpdateLog("Your device doesn't appear to have a hosts file.");
      }
      else
      {
        int num = (int) MessageBox.Show("You must connect first");
      }
    }

    private void btn_dumpCart(object sender, EventArgs e)
    {
      bool flag = false;
      string str1 = "";
      foreach (string dir in this.my_dftp.dir_list("/LF/Cart"))
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray = dir.Split(chArray);
        string str2 = strArray[strArray.Length - 1].Trim();
        if (str2 != "./" && str2 != "../" && str2 != "lib/" && !string.IsNullOrEmpty(str2) && str2.Length < 30)
        {
          if (str1 == "")
          {
            flag = true;
            str1 = str2.Replace("/", "");
          }
          else
            flag = false;
        }
      }
      if (flag)
      {
        string str3 = "";
        string str4 = "";
        string str5 = Encoding.ASCII.GetString(this.my_dftp.cat("/LF/Cart/" + str1 + "/meta.inf"));
        char[] chArray = new char[1]{ '\n' };
        foreach (string str6 in str5.Split(chArray))
        {
          if (str6.StartsWith("PackageID=\""))
            str3 = str6.Replace("PackageID=", "").Replace("\"", "");
          if (str6.StartsWith("Name=\""))
            str4 = str6.Replace("Name=", "").Replace("\"", "");
        }
        if (!string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
        {
          this.UpdateLog("Found cart:" + str4, "done");
          this.UpdateLog("Dumping cart...");
          this.my_dftp.run_buffer("#!/bin/sh\nrm -rf /LF/Bulk/ProgramFiles/mytempdir\n");
          Thread.Sleep(500);
          this.my_dftp.mkdir("/LF/Bulk/ProgramFiles/mytempdir");
          Thread.Sleep(500);
          this.my_dftp.run_buffer("#!/bin/sh\ncp -r /LF/Cart/" + str1 + "/* /LF/Bulk/ProgramFiles/mytempdir\n");
          Thread.Sleep(500);
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nmv /LF/Bulk/ProgramFiles/mytempdir /LF/Bulk/ProgramFiles/{0}\n", (object) str3));
          this.UpdateLog("Dumped cart to device", "done");
        }
        else
          this.UpdateLog("Could not parse cart info", "error", true);
      }
      else
        this.UpdateLog("Could not find cart", "error", true);
    }

    public void ResetUnit()
    {
      if (this.Connected)
      {
        this.UpdateLog("Resetting Leap Pad.....");
        if (this.Device == "LeapPad 2")
        {
          if (this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\n/LF/Base/MfgTest/ResetUnit.sh\n");
            Thread.Sleep(500);
            this.my_dftp.run_buffer("#!/bin/sh\nmkdir /LF/Bulk/Data/Local/All/PAD2-0x1F1E0002-100000\n");
            this.my_dftp.run_buffer("#!/bin/sh\necho \"{\\\"ConnectionReminders\\\": false,\\\"SneakPeaks\\\": false,\\\"isInitialClockSet\\\": false}\" > /LF/Bulk/Data/Local/All/PAD2-0x1F1E0002-100000/UIData.json\n");
            Thread.Sleep(500);
            this.UpdateBatteryLevel();
            this.UpdateLog("Unit Reset", "done", true);
          }
          else if (LFConnect.Properties.Settings.Default.AutoInstallMfgTest)
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LP2.tar";
            if (System.IO.File.Exists(str))
            {
              this.runMfgCommand = nameof (ResetUnit);
              this.installMfgTest(str);
            }
            else
            {
              int num = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LP2.tar");
            }
          }
          else
          {
            int num1 = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LP2.tar");
          }
        }
        else if (this.Device == "LeapPad 1")
        {
          if (this.MfgToolsExists("LF/Base/MadridMfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\n/LF/Base/MadridMfgTest/ResetUnit.sh\n");
            Thread.Sleep(500);
            this.UpdateBatteryLevel();
            this.UpdateLog("Unit Reset", "done", true);
          }
          else if (LFConnect.Properties.Settings.Default.AutoInstallMfgTest)
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LP1.tar";
            if (System.IO.File.Exists(str))
            {
              this.runMfgCommand = nameof (ResetUnit);
              this.installMfgTest(str);
            }
            else
            {
              int num2 = (int) MessageBox.Show("MadridMfgTest is not installed. Please install MfgTest_LP1.tar");
            }
          }
          else
          {
            int num3 = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LP1.tar");
          }
        }
        else if (this.Device == "LeapPad Ultra")
        {
          if (this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\n/LF/Base/MfgTest/ResetUnit.sh\n");
            Thread.Sleep(500);
            this.my_dftp.run_buffer("#!/bin/sh\nrm -f /LF/Bulk/system_rio.cfg\n");
            this.UpdateBatteryLevel();
            this.UpdateLog("Unit Reset", "done", true);
          }
          else if (LFConnect.Properties.Settings.Default.AutoInstallMfgTest)
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LPU.tar";
            if (System.IO.File.Exists(str))
            {
              this.runMfgCommand = nameof (ResetUnit);
              this.installMfgTest(str);
            }
            else
            {
              int num4 = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LPU.tar");
            }
          }
          else
          {
            int num5 = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LPU.tar");
          }
        }
        else
        {
          int num6 = (int) MessageBox.Show("Unknown Device");
        }
      }
      else
        this.UpdateLog("You must connect first", "warning", true);
    }

    public void RunNewCalib()
    {
      if (this.Connected)
      {
        this.UpdateLog("Running Internal Calibration Tool.....");
        if (this.Device == "LeapPad 2")
        {
          if (this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\n/LF/Base/MfgTest/new-calib\n");
            Thread.Sleep(500);
            this.UpdateLog("Ran calibration tool", "done", true);
          }
          else
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LP2.tar";
            if (System.IO.File.Exists(str))
            {
              this.UpdateLog("Calibration script missing", "warning", true);
              this.runMfgCommand = nameof (RunNewCalib);
              this.installMfgTest(str);
            }
            else
            {
              int num = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LP2.tar");
            }
          }
        }
        else if (this.Device == "LeapPad Ultra")
        {
          if (this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\n/LF/Base/MfgTest/new-calib\n");
            Thread.Sleep(500);
            this.UpdateLog("Ran calibration tool", "done", true);
          }
          else
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LPU.tar";
            if (System.IO.File.Exists(str))
            {
              this.UpdateLog("Calibration script missing", "warning", true);
              this.runMfgCommand = nameof (RunNewCalib);
              this.installMfgTest(str);
            }
            else
            {
              int num = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LPU.tar");
            }
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show("This is only available for the LeapPad 2 and Ultra");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("You must connect first");
      }
    }

    public void RunMfgTest()
    {
      if (this.Connected)
      {
        if (this.Device == "LeapPad 2")
        {
          if (this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\ntouch /flags/no8sec\necho \"/LF/Base/MfgTest/LaunchMfgTest.sh\" > /flags/main_app\n");
            Thread.Sleep(500);
            this.UpdateLog("Installed test tools to the system, rebooting the device. To uninstall, use the in app menu");
            this.my_dftp.reboot();
          }
          else
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LP2.tar";
            if (System.IO.File.Exists(str))
            {
              this.runMfgCommand = nameof (RunMfgTest);
              this.installMfgTest(str);
            }
            else
            {
              int num = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LP2.tar");
            }
          }
        }
        else if (this.Device == "LeapPad Ultra")
        {
          if (this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\ntouch /flags/no8sec\necho \"/LF/Base/MfgTest/LaunchMfgTest.sh\" > /flags/main_app\n");
            Thread.Sleep(500);
            this.UpdateLog("Installed test tools to the system, rebooting the device. To uninstall, use the in app menu");
            this.my_dftp.reboot();
          }
          else
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LPU.tar";
            if (System.IO.File.Exists(str))
            {
              this.runMfgCommand = nameof (RunMfgTest);
              this.installMfgTest(str);
            }
            else
            {
              int num = (int) MessageBox.Show("MfgTools is not installed. Please install MfgTest_LPU.tar");
            }
          }
        }
        else if (this.Device == "LeapPad 1")
        {
          if (this.MfgToolsExists("LF/Base/MadridMfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\ntouch /flags/no8sec\necho \"/LF/Base/MadridMfgTest/LaunchMfgTest.sh\" > /flags/main_app\n");
            Thread.Sleep(500);
            this.UpdateLog("Installed test tools to the system, rebooting the device. To uninstall, use the in app menu");
            this.my_dftp.reboot();
          }
          else
          {
            string str = Environment.CurrentDirectory + "\\files\\MfgTest_LP1.tar";
            if (System.IO.File.Exists(str))
            {
              this.runMfgCommand = nameof (RunMfgTest);
              this.installMfgTest(str);
            }
            else
            {
              int num = (int) MessageBox.Show("MadridMfgTest is not installed. Please install MfgTest_LP1.tar");
            }
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show("Unknown Device");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("You must connect first");
      }
    }

    private void installDefaultGreenAppsToolStripMenuItem_Click(object sender, EventArgs e) => this.installAppsFromFolder(Path.Combine(this.curpath, "PRESET\\Green"));

    private void installDefaultPurpleAppsToolStripMenuItem_Click(object sender, EventArgs e) => this.installAppsFromFolder(Path.Combine(this.curpath, "PRESET\\Purple"));

    private void installPrincessAppsToolStripMenuItem_Click(object sender, EventArgs e) => this.installAppsFromFolder(Path.Combine(this.curpath, "PRESET\\Princess"));

    private void installLP1AppsToolStripMenuItem_Click(object sender, EventArgs e) => this.installAppsFromFolder(Path.Combine(this.curpath, "PRESET\\LP1"));

    private void installLP3AppsToolStripMenuItem_Click(object sender, EventArgs e) => this.installAppsFromFolder(Path.Combine(this.curpath, "PRESET\\LP3"));

    public void SetCalibrationData(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        if (this.Device == "LeapPad 2")
        {
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm /flags/pointercal\n"));
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm /flags/set-ts.sh\n"));
          this.my_dftp.run_buffer("#!/bin/sh\nmfgdata set ts 40421 355 -4267004 -148 26143 -4053584 65536\n");
          this.my_dftp.run_buffer("#!/bin/sh\necho \"40421 355 -4267004 -148 26143 -4053584 65536\" > /flags/pointercal\n");
          this.my_dftp.run_buffer("#!/bin/sh\nmfgdata set tspr 4 42 525 5 40 40 40 802 837 856 862 878 900 908 920 941 0 -1 87 -354 103254\n");
          this.my_dftp.run_buffer("#!/bin/sh\necho \"#!/bin/sh\nSYS=/sys/devices/platform/lf2000-touchscreen\n# TSP Version=4\n# TSP Subversion=7\n# Vendor-Number-Res=--1--1\necho 42 > \\$SYS/max_tnt_down\n#2 echo 63 > \\$SYS/max_tnt_down\n#3_7 echo -1 > \\$SYS/max_tnt_down\necho 525 > \\$SYS/min_tnt_up\necho 5 > \\$SYS/max_delta_tnt\n#3_8 echo 20 > \\$SYS/max_delta_tnt\necho 40 > \\$SYS/delay_in_us\necho 40 > \\$SYS/y_delay_in_us\necho 40 > \\$SYS/tnt_delay_in_us\necho 802 837 856 862 878 900 908 920 941 > \\$SYS/pressure_curve\necho 0 > \\$SYS/tnt_mode\necho -1 > \\$SYS/averaging\necho 87 -354 103254 > \\$SYS/tnt_plane\n#2 echo 0 0 0 > \\$SYS/tnt_plane\" > /flags/set-ts.sh\n");
          this.UpdateLog("Calibration Data Set", "done");
        }
        else if (this.Device == "LeapPad Ultra")
        {
          this.UpdateLog("This function is not for a LPU", "error", true);
        }
        else
        {
          if (!(this.Device == "LeapPad 1"))
            return;
          this.UpdateLog("This function is not for a LP1", "error", true);
        }
      }
      else
      {
        int num = (int) MessageBox.Show("You must connect first");
      }
    }

    public void SetCalibrationData2(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        if (this.Device == "LeapPad 2")
        {
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm /flags/pointercal\n"));
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm /flags/set-ts.sh\n"));
          this.my_dftp.run_buffer("#!/bin/sh\nmfgdata set ts 40948 69 -4076046 73 25386 -3797881 65536\n");
          this.my_dftp.run_buffer("#!/bin/sh\necho \"40948 69 -4076046 73 25386 -3797881 65536\" > /flags/pointercal\n");
          this.my_dftp.run_buffer("#!/bin/sh\nmfgdata set tspr 4 27 523 5 1 1 1 694 699 705 713 723 735 749 767 788 0 -1 95 -222 78786\n");
          this.my_dftp.run_buffer("#!/bin/sh\necho \"#!/bin/sh\nSYS=/sys/devices/platform/lf2000-touchscreen\n# TSP Version=4\n# TSP Subversion=7\n# Vendor-Number-Res=--1--1\necho 35 > \\$SYS/max_tnt_down\n#2 echo 52 > \\$SYS/max_tnt_down\n#3_7 echo -1 > \\$SYS/max_tnt_down\necho 523 > \\$SYS/min_tnt_up\necho 5 > \\$SYS/max_delta_tnt\n#3_8 echo 20 > \\$SYS/max_delta_tnt\necho 40 > \\$SYS/delay_in_us\necho 40 > \\$SYS/y_delay_in_us\necho 40 > \\$SYS/tnt_delay_in_us\necho 676 686 703 715 726 732 738 776 787 > \\$SYS/pressure_curve\necho 0 > \\$SYS/tnt_mode\necho -1 > \\$SYS/averaging\necho -42 -431 255049 > \\$SYS/tnt_plane\n#2 echo 0 0 0 > \\$SYS/tnt_plane\" > /flags/set-ts.sh\n");
          this.UpdateLog("Calibration 2 Data Set", "done");
        }
        else if (this.Device == "LeapPad Ultra")
        {
          this.UpdateLog("This function is not for a LPU", "error", true);
        }
        else
        {
          if (!(this.Device == "LeapPad 1"))
            return;
          this.UpdateLog("This function is not for a LP1", "error", true);
        }
      }
      else
      {
        int num = (int) MessageBox.Show("You must connect first");
      }
    }

    public void SetCalibrationData3(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        if (this.Device == "LeapPad 2")
        {
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm /flags/pointercal\n"));
          this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm /flags/set-ts.sh\n"));
          this.my_dftp.run_buffer("#!/bin/sh\nmfgdata set ts 37697 -36 -2906367 -253 27225 -4585429 65536\n");
          this.my_dftp.run_buffer("#!/bin/sh\necho \"37697 -36 -2906367 -253 27225 -4585429 65536\" > /flags/pointercal\n");
          this.my_dftp.run_buffer("#!/bin/sh\nmfgdata set tspr 4 29 524 5 1 1 1 694 699 705 713 723 735 749 767 788 0 -1 -45 -512 254041\n");
          this.my_dftp.run_buffer("#!/bin/sh\necho \"#!/bin/sh\nSYS=/sys/devices/platform/lf2000-touchscreen\n# TSP Version=4\n# TSP Subversion=7\n# Vendor-Number-Res=--1--1\necho 38 > \\$SYS/max_tnt_down\n#2 echo 57 > \\$SYS/max_tnt_down\n#3_7 echo -1 > \\$SYS/max_tnt_down\necho 524 > \\$SYS/min_tnt_up\necho 5 > \\$SYS/max_delta_tnt\n#3_8 echo 20 > \\$SYS/max_delta_tnt\necho 40 > \\$SYS/delay_in_us\necho 40 > \\$SYS/y_delay_in_us\necho 40 > \\$SYS/tnt_delay_in_us\necho 724 725 759 760 762 763 764 765 766 > \\$SYS/pressure_curve\necho 0 > \\$SYS/tnt_mode\necho -1 > \\$SYS/averaging\necho 176 -361 61479 > \\$SYS/tnt_plane\n#2 echo 0 0 0 > \\$SYS/tnt_plane\" > /flags/set-ts.sh\n");
          this.UpdateLog("Calibration 3 Data Set", "done");
        }
        else if (this.Device == "LeapPad Ultra")
        {
          this.UpdateLog("This function is not for a LPU", "error", true);
        }
        else
        {
          if (!(this.Device == "LeapPad 1"))
            return;
          this.UpdateLog("This function is not for a LP1", "error", true);
        }
      }
      else
      {
        int num = (int) MessageBox.Show("You must connect first");
      }
    }

    private void installAppsFromFolder(string appFolder)
    {
      this.folderInstall = appFolder;
      if (Directory.Exists(this.folderInstall))
      {
        this.UpdateLog("Installing apps from " + appFolder);
        this.files = new List<string>((IEnumerable<string>) Directory.GetFiles(appFolder));
        if (this.files.Count <= 0)
          return;
        this.installProgressGroup.Visible = true;
        this.ShowFileInfo();
        this.runMfgCommand = "";
        this.currentfile = 0;
        this.pbFileProgress.Value = 0;
        this.pbFileProgress.Refresh();
        this.progPercent.Text = "0%";
        this.progPercent.Refresh();
        this.totalProgressValue = 0;
        this.totalProgress.Value = this.totalProgressValue;
        this.totalProgress.Refresh();
        this.totalPercent.Text = "0%";
        this.totalPercent.Refresh();
        this.totalfiles = this.files.Count;
        this.setTotalFileSize();
        this.run();
      }
      else
        this.UpdateLog("Folder does not exist! " + appFolder, "error", true);
    }

    private void ExportListIds_Click(object sender, EventArgs e)
    {
      this.UpdateLog("LINEBREAK");
      this.UpdateLog("Export List ID's:");
      this.UpdateLog("LINEBREAK");
      foreach (string dir in this.my_dftp.dir_list("/LF/Bulk/ProgramFiles"))
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray = dir.Split(chArray);
        string input = strArray[strArray.Length - 1].Replace("/", "").Trim();
        if (Regex.Match(input, "([A-Z0-9]+)-0x([A-Za-z0-9]+)-([A-Za-z0-9]+)$", RegexOptions.IgnoreCase).Success)
        {
          string[] appInfo = this.getAppInfo("/LF/Bulk/ProgramFiles/" + input);
          if (appInfo[1] != "NA" && appInfo[1] != "More" && appInfo[1] != "Other")
            this.UpdateLog(appInfo[3] + " - " + appInfo[1] + " - " + appInfo[0]);
        }
      }
      this.UpdateLog("LINEBREAK");
    }

    private void ExportList_Click(object sender, EventArgs e)
    {
      this.UpdateLog("LINEBREAK");
      this.UpdateLog("Export List:");
      this.UpdateLog("LINEBREAK");
      for (int index = 0; index < this.deleteApps.Items.Count; ++index)
        this.UpdateLog(this.deleteApps.Items[index].ToString());
      this.UpdateLog("LINEBREAK");
    }

    private void generateSerialHash(object sender, EventArgs e)
    {
      string serial = Prompt.ShowPinDialog("Enter a serial number", "Generate Serial Hash");
      string flagHashedSerial = this.getFlagHashedSerial(serial);
      this.UpdateLog("Serial: " + serial + "|" + flagHashedSerial);
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      this.my_dftp.backgroundworker = this.backgroundWorker1;
      this.InstallPackage(this.m_filename);
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) => this.UpdateStatus1();

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.lblFilename.Text = "";
      this.lblFilename.Refresh();
      this.UpdateBatteryLevel();
      if (this.cancelAppInstall)
      {
        this.UpdateLog("Installation Cancelled");
        this.completeInstall();
        this.cancelAppInstall = false;
      }
      else if (this.files.Count > 0)
        this.run();
      else
        this.completeInstall();
    }

    private void completeInstall()
    {
      this.isInstalling = false;
      this.pbFileProgress.Value = 0;
      this.pbFileProgress.Refresh();
      this.totalProgress.Value = 100;
      this.totalProgress.Refresh();
      this.progPercent.Text = "0%";
      this.progPercent.Refresh();
      this.HideFileInfo();
      this.getInstalledApps();
      this.EnableMenus();
      this.files.Clear();
      if (this.runMfgCommand != "")
      {
        if (this.runMfgCommand == "ResetUnit")
          this.ResetUnit();
        if (this.runMfgCommand == "RunMfgTest")
          this.RunMfgTest();
        if (this.runMfgCommand == "RunNewCalib")
          this.RunNewCalib();
      }
      if (!(this.folderInstall != string.Empty))
        return;
      this.UpdateLog("Finished installing apps from " + this.folderInstall, "done", true);
      this.folderInstall = "";
    }

    private void run()
    {
      this.isInstalling = true;
      this.DisableMenus();
      string file = this.files[0];
      this.curFileSize = this.fileSizeArray[0];
      this.files.RemoveAt(0);
      this.fileSizeArray.RemoveAt(0);
      this.m_filename = file;
      ++this.currentfile;
      this.onlyfile = Path.GetFileNameWithoutExtension(this.m_filename);
      this.lblFilename.Text = "Copying: " + this.onlyfile;
      this.lblFilename.Refresh();
      this.label3.Text = "File Progress (" + (object) this.currentfile + " of " + (object) this.totalfiles + ")";
      this.curentSize.Text = Form1.BytesToString(this.curFileSize);
      this.curentSize.Refresh();
      this.backgroundWorker1.RunWorkerAsync();
    }

    public void InstallPackage(string filename)
    {
      bool flag1 = false;
      bool flag2 = false;
      this.my_dftp.run_buffer("#!/bin/sh\nrm -rf /LF/Bulk/ProgramFiles/mytempdir\n");
      Thread.Sleep(500);
      this.my_dftp.mkdir("/LF/Bulk/ProgramFiles/mytempdir");
      Thread.Sleep(500);
      this.m_filename = filename;
      this.my_dftp.ipkg(filename, "/LF/Bulk/ProgramFiles/mytempdir");
      string[] strArray1 = Encoding.ASCII.GetString(this.my_dftp.cat("/LF/Bulk/ProgramFiles/mytempdir/meta.inf")).Split('\n');
      string str1 = "";
      string str2 = "ProgramFiles";
      string str3 = "none";
      foreach (string str4 in strArray1)
      {
        if (str4.Contains("Type=\"Music\""))
          str2 = "Music";
        if (str4.Contains("Type=\"MicroDownload\""))
          str2 = "Downloads";
        if (str4.Contains("Type=\"Download\""))
          str2 = "Downloads";
        if (str4.Contains("Name=\"Manufacturing Test\"") && this.Device == "LeapPad 2")
          str2 = "MfgTest2";
        if (str4.Contains("Name=\"Manufacturing Test\"") && this.Device == "LeapPad Ultra")
          str2 = "MfgTestU";
        if (str4.Contains("Name=\"Madrid Manufacturing Test\""))
          str2 = "MfgTest1";
        if (str4.StartsWith("PackageID=\""))
          str1 = str4.Replace("PackageID=", "").Replace("\"", "");
        if (str4.StartsWith("Name=\""))
          str3 = str4.Replace("Name=", "").Replace("\"", "");
        if (str4.StartsWith("PackageID=\"DEAK"))
          flag1 = true;
        if (str4.StartsWith("MDLType=\"UITheme"))
          flag1 = true;
        if (str4.StartsWith("Name=\"MoviePlayerWidget"))
          flag1 = true;
      }
      if (str1 == "")
      {
        this.my_dftp.run_buffer("#!/bin/sh\nrm -rf /LF/Bulk/ProgramFiles/mytempdir\n");
        Thread.Sleep(500);
        this.UpdateLog("Cant find metadata - aborting", "error", true);
      }
      else if (this.ispublic && !flag1)
      {
        this.UpdateLog("This file is not supported", "error", true);
      }
      else
      {
        Thread.Sleep(1000);
        if (str2 == "MfgTest2")
        {
          if (this.Device == "LeapPad 1" || this.Device == "LeapPad Ultra")
          {
            int num1 = (int) MessageBox.Show("This is not the correct test tools.");
          }
          else if (!this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\nmv LF/Bulk/ProgramFiles/mytempdir /LF/Base/MfgTest\n");
            this.UpdateLog("Installed MfgTest", "done");
          }
          else
            this.UpdateLog("- MfgTest is already installed", "warning", true);
        }
        else if (str2 == "MfgTestU")
        {
          if (this.Device == "LeapPad 1" || this.Device == "LeapPad 2")
          {
            int num2 = (int) MessageBox.Show("This is not the correct test tools.");
          }
          else if (!this.MfgToolsExists("LF/Base/MfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\nmv LF/Bulk/ProgramFiles/mytempdir /LF/Base/MfgTest\n");
            this.UpdateLog("Installed MfgTest Ultra", "done");
          }
          else
            this.UpdateLog("- MfgTest is already installed", "warning", true);
        }
        else if (str2 == "MfgTest1")
        {
          if (this.Device == "LeapPad 2" || this.Device == "LeapPad Ultra")
          {
            int num3 = (int) MessageBox.Show("This is not the correct test tools.");
          }
          else if (!this.MfgToolsExists("LF/Base/MadridMfgTest"))
          {
            this.my_dftp.run_buffer("#!/bin/sh\nmv LF/Bulk/ProgramFiles/mytempdir /LF/Base/MadridMfgTest\n");
            this.UpdateLog("Installed MadridMfgTest", "done");
          }
          else
            this.UpdateLog("- MadridMfgTest is already installed", "warning", true);
        }
        else
        {
          if (this.my_dftp.is_dir("/LF/Bulk/ProgramFiles/mytempdir/MOVE_TO_DOWNLOADS"))
          {
            foreach (string dir in this.my_dftp.dir_list("/LF/Bulk/ProgramFiles/mytempdir/MOVE_TO_DOWNLOADS"))
            {
              char[] chArray = new char[1]{ ' ' };
              string[] strArray2 = dir.Split(chArray);
              string str5 = strArray2[strArray2.Length - 1].Trim();
              if (str5 != "./" && str5 != "../" && str5.EndsWith("/"))
              {
                string str6 = str5.Replace("/", string.Empty);
                if (!this.my_dftp.is_dir("/LF/Bulk/Downloads/" + str6))
                {
                  this.my_dftp.run_buffer(string.Format("#!/bin/sh\nmv LF/Bulk/ProgramFiles/mytempdir/MOVE_TO_DOWNLOADS/{0} LF/Bulk/Downloads/{0}\n", (object) str6));
                  this.UpdateLog(" - Installed: " + str6);
                }
                else
                  this.UpdateLog(" - Skipping: " + str6);
              }
            }
            this.my_dftp.run_buffer("#!/bin/sh\nrm -rf /LF/Bulk/ProgramFiles/mytempdir/MOVE_TO_DOWNLOADS\n");
          }
          try
          {
            if (this.my_dftp.exists("/LF/Bulk/ProgramFiles/mytempdir/installcommands.txt"))
            {
              string str7 = Encoding.ASCII.GetString(this.my_dftp.cat("/LF/Bulk/ProgramFiles/mytempdir/installcommands.txt"));
              char[] chArray = new char[1]{ '\n' };
              foreach (string str8 in str7.Split(chArray))
              {
                this.my_dftp.run_buffer("#!/bin/sh\n" + str8.Replace("{path}", "/LF/Bulk/ProgramFiles/mytempdir") + "\n");
                this.UpdateLog("Ran additional command:");
                Thread.Sleep(1500);
              }
              this.my_dftp.run_buffer("#!/bin/sh\nrm /LF/Bulk/ProgramFiles/mytempdir/installcommands.txt\n");
            }
          }
          catch (Exception ex)
          {
          }
          if (!this.my_dftp.is_dir(string.Format("/LF/Bulk/{0}/{1}", (object) str2, (object) str1)))
          {
            Thread.Sleep(1000);
            this.my_dftp.run_buffer(string.Format("#!/bin/sh\nmv LF/Bulk/ProgramFiles/mytempdir LF/Bulk/{0}/{1}\n", (object) str2, (object) str1));
            this.UpdateLog("Installed " + str3, "done");
            flag2 = true;
          }
          else if (LFConnect.Properties.Settings.Default.OverwriteExisting)
          {
            this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm -rf /LF/Bulk/{0}/{1}\n", (object) str2, (object) str1));
            Thread.Sleep(1000);
            while (this.my_dftp.is_dir(string.Format("/LF/Bulk/(0)/{1}", (object) str2, (object) str1)))
              Thread.Sleep(500);
            Thread.Sleep(1000);
            this.my_dftp.run_buffer(string.Format("#!/bin/sh\nmv LF/Bulk/ProgramFiles/mytempdir LF/Bulk/{0}/{1}\n", (object) str2, (object) str1));
            this.UpdateLog("Re-installed " + str3, "done");
            flag2 = true;
          }
          else
            this.UpdateLog("Skipped " + str3);
          if ((this.Device == "LeapPad Ultra" || this.Device == "LeapPad 3" || this.Device == "LeapPad Platinum") && flag2)
          {
            Thread.Sleep(500);
            this.my_dftp.run_buffer(string.Format("#!/bin/sh\nlfpkg2 /LF/Bulk/{0}/{1}/meta.inf\n", (object) str2, (object) str1));
            Thread.Sleep(500);
          }
          this.pbFileProgress.Value = 0;
          this.pbFileProgress.Refresh();
          this.progPercent.Text = "0%";
          this.progPercent.Refresh();
          this.my_dftp.run_buffer("#!/bin/sh\nrm -rf /LF/Bulk/ProgramFiles/mytempdir\n");
        }
      }
    }

    private void UpdateStatus(object sender, EventArgs ea) => this.UpdateStatus1();

    private void UpdateStatus1()
    {
      this.Refresh();
      int num = this.my_dftp.ProgressPercent;
      if (num > 94)
      {
        num = 100;
        this.lblFilename.Text = "Installing " + this.onlyfile;
        this.lblFilename.Refresh();
        this.totalFileSent += this.curFileSize;
      }
      if (num > 0)
      {
        float byteCount1 = (float) this.curFileSize * ((float) num * 0.01f);
        this.fileSent.Text = Form1.BytesToString((long) byteCount1);
        this.fileSent.Refresh();
        if (num < 100)
        {
          long byteCount2 = (long) byteCount1 + this.totalFileSent;
          this.totalFileSizeProgress.Text = Form1.BytesToString(byteCount2);
          this.totalFileSizeProgress.Refresh();
          this.totalProgressValue = (int) (100.0 * ((double) byteCount2 / (double) this.totalFileSize));
        }
        else
        {
          this.totalFileSizeProgress.Text = Form1.BytesToString(this.totalFileSent);
          this.totalFileSizeProgress.Refresh();
          this.totalProgressValue = (int) (100.0 * ((double) this.totalFileSent / (double) this.totalFileSize));
        }
      }
      if (this.totalProgressValue > 100)
        this.totalProgressValue = 100;
      this.pbFileProgress.Value = num;
      this.pbFileProgress.Refresh();
      this.progPercent.Text = num.ToString() + "%";
      this.progPercent.Refresh();
      this.totalProgress.Value = this.totalProgressValue;
      this.totalProgress.Refresh();
      this.totalPercent.Text = this.totalProgressValue.ToString() + "%";
      this.totalPercent.Refresh();
    }

    private void DisableMenus()
    {
      this.leapPadToolStripMenuItem.Enabled = false;
      if (!this.limitSerial)
        this.developToolStripMenuItem.Enabled = false;
      if (!this.deakmenu)
        return;
      this.deaksToolsToolStripMenuItem.Enabled = false;
    }

    private void EnableMenus()
    {
      this.leapPadToolStripMenuItem.Enabled = true;
      if (!this.limitSerial)
        this.developToolStripMenuItem.Enabled = true;
      if (this.ispublic)
        this.developToolStripMenuItem.Visible = false;
      if (!this.deakmenu)
        return;
      this.deaksToolsToolStripMenuItem.Enabled = true;
    }

    private void HideFileInfo() => this.installProgressGroup.Visible = false;

    private void ShowFileInfo()
    {
      this.cancel.Enabled = true;
      this.installProgressGroup.Visible = true;
    }

    private void cancelInstall(object sender, EventArgs e)
    {
      this.UpdateLog("Cancellation in progress. Installation will stop after current process", "warning");
      this.cancelAppInstall = true;
      this.cancel.Enabled = false;
    }

    private void installAppsBrowse(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Multiselect = true;
        int num = (int) openFileDialog.ShowDialog();
        this.setInstallDetails(openFileDialog.FileNames);
      }
      else
        this.UpdateLog("You must connect first");
    }

    private void setInstallDetails(string[] temp)
    {
      int num = 0;
      foreach (string path in temp)
      {
        if (Path.GetExtension(path).ToLower() == ".tar")
        {
          this.files.Add(path);
          ++num;
        }
      }
      if (this.isInstalling)
      {
        this.UpdateLog("Added additional apps to install");
        this.totalfiles += num;
        this.setTotalFileSize();
        this.label3.Text = "File Progress (" + (object) this.currentfile + " of " + (object) this.totalfiles + ")";
      }
      else if (this.files.Count > 0)
      {
        this.installProgressGroup.Visible = true;
        this.ShowFileInfo();
        this.runMfgCommand = "";
        this.currentfile = 0;
        this.pbFileProgress.Value = 0;
        this.pbFileProgress.Refresh();
        this.progPercent.Text = "0%";
        this.progPercent.Refresh();
        this.totalProgressValue = 0;
        this.totalProgress.Value = this.totalProgressValue;
        this.totalProgress.Refresh();
        this.totalPercent.Text = "0%";
        this.totalPercent.Refresh();
        this.totalfiles = this.files.Count;
        this.setTotalFileSize();
        this.run();
      }
      else
        this.UpdateLog("No compatible files were chosen to install", "error");
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      if (!this.Connected)
      {
        this.my_dftp = new dftp();
        if (this.hasDebug())
        {
          this.UpdateLog("Scanning Devices:");
          this.UpdateLog("LINEBREAK");
          this.UpdateLog(this.my_dftp.deviceInfo);
          this.UpdateLog("LINEBREAK");
        }
        if (this.my_dftp.connected)
        {
          this.SetDevice();
          if (this.allowedSerial(this.my_dftp.getSerial()) || this.ispublic)
          {
            this.UpdateLog("Connected to " + this.Device, "done");
            this.UpdateLog("Serial: " + this.my_dftp.getSerial(), "done");
            this.UpdateLog("Firmware: " + this.my_dftp.getFirmware(), "done");
            this.setParentPin();
            if (!this.ispublic)
            {
              if (this.Device == "LeapPad 2" || this.Device == "LeapPad Ultra")
              {
                if (this.MfgToolsExists("LF/Base/MfgTest"))
                  this.UpdateLog("Unit has MfgTest Tools");
                else
                  this.UpdateLog("MfgTest Tools MISSING", "warning", true);
              }
              else if (this.Device == "Leapster Explorer")
              {
                if (this.MfgToolsExists("LF/Base/EmeraldMfgTest"))
                  this.UpdateLog("Unit has EmeraldMfgTest Tools");
                else
                  this.UpdateLog("EmeraldMfgTest Tools MISSING", "warning", true);
              }
              else if (this.Device == "LeapPad 1")
              {
                if (this.MfgToolsExists("LF/Base/MadridMfgTest"))
                  this.UpdateLog("Unit has MadridMfgTest Tools");
                else
                  this.UpdateLog("MadridMfgTest Tools MISSING", "warning", true);
              }
              else if (!(this.Device == "LeapPad 3"))
                this.UpdateLog("Unknown Device", "warning", true);
            }
            this.UpdateLog(" ");
            this.UpdateConnected();
            this.Connected = true;
            this.UpdateBatteryLevel();
            this.getInstalledApps();
            if (!this.hasVideoPlayer)
            {
              this.UpdateLog("Missing Video Player", "warning", true);
              if (LFConnect.Properties.Settings.Default.AutoInstallMoviePlayer)
              {
                string str = Environment.CurrentDirectory + "\\files\\VideoPlayer.tar";
                if (System.IO.File.Exists(str))
                {
                  this.UpdateLog("Installing Video Player");
                  this.installMfgTest(str);
                }
                else
                  this.UpdateLog("Video Player widget must be installed before videos will play. Try updating your firmware through the official connect software.", "warning", true);
              }
            }
            this.my_dftp.OnProgressUpdate += new EventHandler(this.UpdateStatus);
            this.UpdateStatus1();
          }
          else
          {
            this.UpdateLog("Sorry, you are unable to connect to this device", "error", true);
            this.UpdateLog("Serial: " + this.my_dftp.getSerial());
          }
        }
        else
          this.UpdateLog("Can't connect to device!", "error", true);
      }
      else
        this.UpdateLog("You are already connected!", "warning", true);
    }

    private void btnDisconnect_Click(object sender, EventArgs e)
    {
      if (this.Connected)
      {
        this.DisconnectDevice();
      }
      else
      {
        int num = (int) MessageBox.Show("You are not connected!");
      }
    }

    private void installFromInstall_Click(object sender, EventArgs e) => this.installAppsFromFolder(Path.Combine(this.curpath, "install"));

    private void btnDeleteApp_Click(object sender, EventArgs e)
    {
      if (this.deleteApps.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show("Please select an Item first!");
      }
      else
      {
        string str = this.deleteApps.SelectedItem.ToString();
        for (int index = 0; index < this.allAppNames.Count; ++index)
        {
          if (this.allAppTypes[index] + " - " + this.allAppNames[index] == str)
          {
            this.btnDeleteApp.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            this.DeletePackage("/LF/Bulk/ProgramFiles/" + this.allAppPaths[index], this.allAppNames[index]);
            this.deleteApps.Items.Remove((object) (this.allAppTypes[index] + " - " + this.allAppNames[index]));
            this.btnDeleteApp.Enabled = true;
            Cursor.Current = Cursors.Default;
          }
        }
        this.totalApps.Text = this.deleteApps.Items.Count.ToString() + " Apps Installed";
      }
    }

    private void btnDeleteDownload_Click(object sender, EventArgs e)
    {
      if (this.deleteDownloads.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show("Please select an Item first!");
      }
      else
      {
        string str = this.deleteDownloads.SelectedItem.ToString();
        for (int index = 0; index < this.allDownloadNames.Count; ++index)
        {
          if (this.allDownloadTypes[index] + " - " + this.allDownloadNames[index] == str)
          {
            this.btnDeleteDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            this.DeletePackage("/LF/Bulk/Downloads/" + this.allDownloadPaths[index], this.allDownloadNames[index]);
            this.deleteDownloads.Items.Remove((object) (this.allDownloadTypes[index] + " - " + this.allDownloadNames[index]));
            this.btnDeleteDownload.Enabled = true;
            Cursor.Current = Cursors.Default;
          }
        }
      }
    }

    private void btnDownloadDownload_Click(object sender, EventArgs e)
    {
      if (this.deleteDownloads.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show("Please select an Item first!");
      }
      else
      {
        string str = this.deleteDownloads.SelectedItem.ToString();
        for (int index = 0; index < this.allDownloadNames.Count; ++index)
        {
          if (this.allDownloadTypes[index] + " - " + this.allDownloadNames[index] == str)
          {
            this.btnDownloadDownload.Enabled = false;
            this.DownloadPackage(this.allDownloadPaths[index], "/LF/Bulk/Downloads/", this.allDownloadNames[index]);
            this.btnDownloadDownload.Enabled = true;
            this.UpdateLog("Downloaded " + this.allDownloadNames[index], "done");
          }
        }
      }
    }

    private void btnDownloadApp_Click(object sender, EventArgs e)
    {
      if (this.deleteApps.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show("Please select an Item first!");
      }
      else
      {
        string str = this.deleteApps.SelectedItem.ToString();
        for (int index = 0; index < this.allAppNames.Count; ++index)
        {
          if (this.allAppTypes[index] + " - " + this.allAppNames[index] == str)
          {
            this.btnDownloadApp.Enabled = false;
            this.DownloadPackage(this.allAppPaths[index], "/LF/Bulk/ProgramFiles/", this.allAppNames[index]);
            this.btnDownloadApp.Enabled = true;
            this.UpdateLog("Downloaded " + this.allAppNames[index], "done");
          }
        }
      }
    }

    private void btnReboot_Click(object sender, EventArgs e)
    {
      this.my_dftp.reboot();
      this.UpdateLog("Rebooted Device", "done");
      if (!this.Connected)
        return;
      this.DisconnectDevice();
    }

    private void btnDownloadFile_Click(object sender, EventArgs e)
    {
      if (this.downFile.Text.Trim() != "")
      {
        this.DownloadFile(this.downFile.Text);
      }
      else
      {
        int num = (int) MessageBox.Show("Please enter a path " + this.downFile.Text);
      }
    }

    public void DownloadFile(string filepath)
    {
      if (this.Connected)
      {
        if (this.deakmenu)
        {
          if (!this.DownloadPopup)
          {
            this.DownloadPopup = true;
            int num = (int) MessageBox.Show("Please wait, this can take a while");
          }
          string fileName = Path.GetFileName(filepath);
          Cursor.Current = Cursors.WaitCursor;
          this.my_dftp.download_file(Environment.CurrentDirectory + "\\" + this.GetSafeFilename(fileName), filepath);
          Cursor.Current = Cursors.Default;
          this.UpdateLog("Downloaded " + fileName, "done", true);
        }
        else
        {
          int num1 = (int) MessageBox.Show("Sorry this feature is only for Deak");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("You must connect first");
      }
    }

    public void DownloadPackage(string app, string path, string appName)
    {
      if (!this.DownloadPopup)
      {
        this.DownloadPopup = true;
        int num = (int) MessageBox.Show("Please wait, this can take a while");
      }
      Cursor.Current = Cursors.WaitCursor;
      this.my_dftp.run_buffer(string.Format("#!/bin/sh\ncd {0}{1}\ntar -cf ../{1}.tar *\n", (object) path, (object) app));
      this.my_dftp.download_file(Environment.CurrentDirectory + "\\" + this.GetSafeFilename(appName) + ".tar", path + app + ".tar");
      this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm -f {0}{1}.tar\n", (object) path, (object) app));
      this.my_dftp.run_buffer("#!/bin/sh\ncd /\n");
      Cursor.Current = Cursors.Default;
    }

    public void DeletePackage(string path, string name)
    {
      this.my_dftp.run_buffer(string.Format("#!/bin/sh\nrm -rf {0}\n", (object) path));
      this.UpdateBatteryLevel();
      this.UpdateLog("Removed " + name, "done", true);
    }

    public void DisconnectDevice()
    {
      this.my_dftp.disconnect();
      this.pbSpace.Value = 0;
      this.pbBatt.Value = 0;
      this.spaceLeft.Text = "";
      this.spaceTotal.Text = "";
      this.pbFileProgress.Value = 0;
      this.pbFileProgress.Refresh();
      this.totalProgress.Value = 0;
      this.totalProgress.Refresh();
      this.pbSpace.Refresh();
      this.pbBatt.Refresh();
      this.UpdateDisconnected();
      this.UpdateLog(" ");
      this.UpdateLog("Disconnected", "done");
      this.UpdateLog("LINEBREAK");
      this.Connected = false;
    }

    private void checkForUpdates(object sender, EventArgs e)
    {
      string str1 = "NO UPDATE";
      string fileName = "";
      string str2 = "";
      using (WebClient webClient = new WebClient())
      {
        string str3 = this.lpmVersion.Replace(".", string.Empty);
        string s = webClient.DownloadString("http://spiffydirectory.com/v/xml/22/" + str3);
        try
        {
          using (XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(s)))
          {
            xmlReader.ReadToFollowing("has_update");
            str1 = xmlReader.ReadElementContentAsString();
            xmlReader.ReadToFollowing("version_display");
            str2 = xmlReader.ReadElementContentAsString();
            xmlReader.ReadToFollowing("software_url");
            fileName = xmlReader.ReadElementContentAsString();
          }
        }
        catch
        {
        }
      }
      if (str1 == "UPDATE")
      {
        if (MessageBox.Show("An update was found, do you want to load the download page?", "Update Available v" + str2, MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        Process.Start(fileName);
      }
      else
      {
        int num = (int) MessageBox.Show("You have the latest version", "No Update Available");
      }
    }

    private void loadSpiffyHacks(object sender, EventArgs e) => Process.Start("http://spiffyhacks.com");

    public bool allowedSerial(string value)
    {
      if (!this.limitSerial)
        return true;
      for (int index = 0; index < this.allowedSerials.Count; ++index)
      {
        if (this.allowedSerials[index] == value)
          return true;
      }
      return false;
    }

    private static string BytesToString(long byteCount)
    {
      string[] strArray = new string[7]
      {
        "B",
        "KB",
        "MB",
        "GB",
        "TB",
        "PB",
        "EB"
      };
      if (byteCount == 0L)
        return "0" + strArray[0];
      long a = Math.Abs(byteCount);
      int int32 = Convert.ToInt32(Math.Floor(Math.Log((double) a, 1024.0)));
      double num = Math.Round((double) a / Math.Pow(1024.0, (double) int32), 2);
      return ((double) Math.Sign(byteCount) * num).ToString() + strArray[int32];
    }

    private bool hasDebug() => this.debug || LFConnect.Properties.Settings.Default.OutputLevel.ToString() == "Full" || LFConnect.Properties.Settings.Default.OutputLevel.ToString() == "Debug";

    public string[] getAppInfo(string path)
    {
      string[] appInfo = new string[5]
      {
        "NA",
        "NA",
        "NA",
        "NA",
        "NA"
      };
      string str1 = Encoding.ASCII.GetString(this.my_dftp.cat(path + "/meta.inf"));
      char[] chArray = new char[1]{ '\n' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.StartsWith("Name=\""))
        {
          appInfo[0] = str2.Replace("Name=", "");
          appInfo[0] = appInfo[0].Replace("\"", "").Trim();
        }
        if (str2.StartsWith("Category=\""))
        {
          appInfo[1] = str2.Replace("Category=", "");
          appInfo[1] = appInfo[1].Replace("\"", "").Trim();
        }
        if (str2.StartsWith("Type=\""))
        {
          appInfo[2] = str2.Replace("Type=", "");
          appInfo[2] = appInfo[2].Replace("\"", "").Trim();
        }
        if (str2.StartsWith("PackageID=\""))
        {
          appInfo[3] = str2.Replace("PackageID=", "");
          appInfo[3] = appInfo[3].Replace("\"", "").Trim();
        }
        if (str2.StartsWith("MDLType=\""))
        {
          appInfo[4] = str2.Replace("MDLType=", "");
          appInfo[4] = appInfo[4].Replace("\"", "").Trim();
        }
      }
      return appInfo;
    }

    private void clearApps()
    {
      this.deleteApps.Items.Clear();
      this.deleteDownloads.Items.Clear();
      this.allAppPaths.Clear();
      this.allAppNames.Clear();
      this.allAppTypes.Clear();
      this.allDownloadPaths.Clear();
      this.allDownloadNames.Clear();
      this.allDownloadTypes.Clear();
    }

    public string GetSafeFilename(string filename) => string.Join("", filename.Split(Path.GetInvalidFileNameChars()));

    private void setTotalFileSize()
    {
      this.fileSizeArray.Clear();
      this.totalFileSize = 0L;
      this.totalFileSent = 0L;
      foreach (string file in this.files)
      {
        FileInfo fileInfo = new FileInfo(file);
        this.totalFileSize += fileInfo.Length;
        this.fileSizeArray.Add(fileInfo.Length);
      }
      this.fileSize.Text = Form1.BytesToString(this.totalFileSize);
    }

    public bool MfgToolsExists(string path)
    {
      bool flag = false;
      foreach (string dir in this.my_dftp.dir_list(path))
      {
        if (dir.Trim() != "")
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public bool FlagExists(string flag)
    {
      bool flag1 = false;
      foreach (string dir in this.my_dftp.dir_list("/flags"))
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray = dir.Split(chArray);
        if (strArray[strArray.Length - 1].Trim() == flag)
        {
          flag1 = true;
          break;
        }
      }
      return flag1;
    }

    public bool FileExists(string file)
    {
      bool flag = false;
      string[] strArray1 = file.Split('/');
      string str = strArray1[strArray1.Length - 1];
      string path = "/";
      for (int index = 0; index < strArray1.Length - 1; ++index)
        path += strArray1[index];
      foreach (string dir in this.my_dftp.dir_list(path))
      {
        char[] chArray = new char[1]{ ' ' };
        string[] strArray2 = dir.Split(chArray);
        if (strArray2[strArray2.Length - 1].Trim() == str)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private void UpdateBatteryLevel()
    {
      if (!LFConnect.Properties.Settings.Default.updateDetails)
        return;
      this.pbSpace.Value = this.my_dftp.getFreeSpace();
      this.spaceLeft.Text = this.my_dftp.spaceFree;
      this.spaceTotal.Text = this.my_dftp.spaceTotal;
      this.pbBatt.Value = !(this.Device == "LeapPad Ultra") && !(this.Device == "LeapPad 3") ? this.my_dftp.getBatteryLevel() : this.my_dftp.getBatteryLevelUltra();
      this.pbSpace.Refresh();
      this.pbBatt.Refresh();
    }

    public void installMfgTest(string localFile)
    {
      this.files = new List<string>((IEnumerable<string>) new string[1]
      {
        localFile
      });
      this.installProgressGroup.Visible = true;
      this.ShowFileInfo();
      this.currentfile = 0;
      this.pbFileProgress.Value = 0;
      this.pbFileProgress.Refresh();
      this.progPercent.Text = "0%";
      this.progPercent.Refresh();
      this.totalProgressValue = 0;
      this.totalProgress.Value = this.totalProgressValue;
      this.totalProgress.Refresh();
      this.totalPercent.Text = "0%";
      this.totalPercent.Refresh();
      this.totalfiles = this.files.Count;
      this.setTotalFileSize();
      this.run();
    }

    public void setFlagOptions()
    {
      string str1 = "";
      try
      {
        if (!System.IO.File.Exists(this.flagFile))
          return;
        StreamReader streamReader = new StreamReader((Stream) new FileStream(this.flagFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), Encoding.UTF8, true, 128);
        string str2;
        while ((str2 = streamReader.ReadLine()) != null)
        {
          if (str2.Substring(0, 5) == "Mode:")
          {
            str1 = str2.Substring(6).Trim();
            switch (str1)
            {
              case "D3@kHazFu11Acc3ss!":
                this.limitSerial = false;
                this.onlyService = false;
                this.debug = false;
                this.deakmenu = true;
                this.ispublic = false;
                if (this.hasDebug())
                {
                  this.UpdateLog("Setting flag for Deak Access");
                  break;
                }
                break;
              case "ServiceMode!":
                this.limitSerial = false;
                this.onlyService = true;
                this.debug = false;
                this.deakmenu = false;
                this.ispublic = true;
                if (this.hasDebug())
                {
                  this.UpdateLog("Setting flag for Service Mode");
                  break;
                }
                break;
              case "SerialAccess":
                this.limitSerial = true;
                this.onlyService = false;
                this.debug = false;
                this.deakmenu = false;
                this.ispublic = false;
                if (this.hasDebug())
                {
                  this.UpdateLog("Setting flag for Serial Access");
                  break;
                }
                break;
              default:
                this.ispublic = true;
                if (this.hasDebug())
                {
                  this.UpdateLog("Default Access");
                  break;
                }
                break;
            }
          }
          if (str2.Substring(0, 7) == "Serial:" && str1 == "SerialAccess")
          {
            string[] strArray = str2.Substring(8).Trim().Split('|');
            if (strArray.Length == 2)
            {
              string serial = strArray[0];
              if (strArray[1] == this.getFlagHashedSerial(serial))
              {
                if (this.hasDebug())
                  this.UpdateLog(serial + " allowed access");
                this.allowedSerials.Add(serial);
              }
            }
            else
              this.UpdateLog("Serial in " + this.flagFile + " is in the wrong format!", "error", true);
          }
        }
      }
      catch
      {
      }
    }

    public string getFlagHashedSerial(string serial)
    {
      string str1 = "LPmanagerSerialAccess4Deak";
      double num = Math.Floor((double) serial.Length / 2.0);
      string str2 = serial.Substring(0, Convert.ToInt32(num));
      string str3 = serial.Substring(Convert.ToInt32(num));
      string sha1Hash = this.getSha1Hash(str2 + str1 + str3);
      return this.getSha1Hash(serial + sha1Hash);
    }

    public string getSha1Hash(string s)
    {
      using (SHA1Managed shA1Managed = new SHA1Managed())
      {
        byte[] bytes = Encoding.ASCII.GetBytes(s);
        byte[] hash = shA1Managed.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("X2"));
        return stringBuilder.ToString();
      }
    }

    public void UpdateLogOld(string message, string status)
    {
      if (this.statusLog.InvokeRequired)
        this.Invoke((Delegate) new Form1.UpdateLogCallback(this.UpdateLog), (object) message);
      else if (this.statusLog.Text == "")
        this.statusLog.AppendText(message);
      else if (message == "LINEBREAK")
        this.statusLog.AppendText(Environment.NewLine + "---------------------------------------------------------------------------------------------------");
      else
        this.statusLog.AppendText(Environment.NewLine + message);
    }

    public void UpdateLog(string message, string status = "", bool showPrefix = false)
    {
      if (this.statusLog.InvokeRequired)
      {
        this.Invoke((Delegate) new Form1.UpdateLogCallback(this.UpdateLog), (object) message, (object) status, (object) showPrefix);
      }
      else
      {
        this.statusLog.SelectionStart = this.statusLog.Text.Length;
        if (status == "error")
        {
          this.statusLog.SelectionColor = Color.Red;
          if (showPrefix)
            message = "[Error] " + message;
        }
        else if (status == "warning")
        {
          this.statusLog.SelectionColor = Color.OrangeRed;
          if (showPrefix)
            message = "[Warning] " + message;
        }
        else if (status == "info")
        {
          this.statusLog.SelectionColor = Color.Black;
          if (showPrefix)
            message = "[Info] " + message;
        }
        else if (status == "done")
        {
          this.statusLog.SelectionColor = Color.DarkGreen;
          if (showPrefix)
            message = "[Success] " + message;
        }
        else
          this.statusLog.SelectionColor = Color.Black;
        if (this.statusLog.Text == "")
          this.statusLog.AppendText(message);
        else if (message == "LINEBREAK")
          this.statusLog.AppendText(Environment.NewLine + "---------------------------------------------------------------------------------------------------");
        else
          this.statusLog.AppendText(Environment.NewLine + message);
        this.statusLog.SelectionStart = this.statusLog.Text.Length;
        this.statusLog.ScrollToCaret();
      }
    }

    private void showThemeManager(object sender, EventArgs e) => new Form2().Show();

    private void showSettings(object sender, EventArgs e) => new Settings().Show();

    private void showVideoManager(object sender, EventArgs e) => new Form3().Show();

    private void showHelp(object sender, EventArgs e) => new Form4().Show();

    private void showLF3(object sender, EventArgs e) => new LF3().Show();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.pbBatt = new ProgressBar();
      this.label2 = new Label();
      this.pbSpace = new ProgressBar();
      this.label1 = new Label();
      this.pbFileProgress = new ProgressBar();
      this.label3 = new Label();
      this.lblFilename = new Label();
      this.progPercent = new Label();
      this.totalProgress = new ProgressBar();
      this.label5 = new Label();
      this.totalPercent = new Label();
      this.deleteApps = new ListBox();
      this.deleteDownloads = new ListBox();
      this.btnDeleteApp = new Button();
      this.btnDeleteDownload = new Button();
      this.btnDownloadApp = new Button();
      this.btnDownloadDownload = new Button();
      this.exportListButton = new Button();
      this.menuStrip1 = new MenuStrip();
      this.leapPadToolStripMenuItem = new ToolStripMenuItem();
      this.connectToolStripMenuItem = new ToolStripMenuItem();
      this.disconnectToolStripMenuItem = new ToolStripMenuItem();
      this.rebootToolStripMenuItem = new ToolStripMenuItem();
      this.themeMakerToolStripMenuItem1 = new ToolStripMenuItem();
      this.videoMakerToolStripMenuItem = new ToolStripMenuItem();
      this.lF3Menu = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.installAppsFrominstallFolderToolStripMenuItem = new ToolStripMenuItem();
      this.installAppsToolStripMenuItem = new ToolStripMenuItem();
      this.ServiceToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem2 = new ToolStripMenuItem();
      this.toolStripMenuItem3 = new ToolStripMenuItem();
      this.toolStripMenuItem4 = new ToolStripMenuItem();
      this.toolStripMenuItem1 = new ToolStripMenuItem();
      this.developToolStripMenuItem = new ToolStripMenuItem();
      this.calibrateScreenToolStripMenuItem1 = new ToolStripMenuItem();
      this.loadDevModeToolStripMenuItem = new ToolStripMenuItem();
      this.resetUnitToolStripMenuItem1 = new ToolStripMenuItem();
      this.installTestToolsToolStripMenuItem = new ToolStripMenuItem();
      this.toggleMainAppToolStripMenuItem = new ToolStripMenuItem();
      this.dumpCartToolStripMenuItem = new ToolStripMenuItem();
      this.changeHostFileToolStripMenuItem = new ToolStripMenuItem();
      this.deaksToolsToolStripMenuItem = new ToolStripMenuItem();
      this.installDefaultGreenAppsToolStripMenuItem = new ToolStripMenuItem();
      this.installDefaultPurpleAppsToolStripMenuItem = new ToolStripMenuItem();
      this.installPrincessAppsToolStripMenuItem = new ToolStripMenuItem();
      this.installLP1AppsToolStripMenuItem = new ToolStripMenuItem();
      this.installLP3AppsToolStripMenuItem = new ToolStripMenuItem();
      this.setCalibrationDataToolStripMenuItem = new ToolStripMenuItem();
      this.setCalibrationData2ToolStripMenuItem = new ToolStripMenuItem();
      this.setCalibrationData3ToolStripMenuItem = new ToolStripMenuItem();
      this.changeParentPinToolStripMenuItem = new ToolStripMenuItem();
      this.generateSerialHashToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.checkForUpdateToolStripMenuItem = new ToolStripMenuItem();
      this.settingsToolStripMenuItem = new ToolStripMenuItem();
      this.installProgressGroup = new GroupBox();
      this.cancel = new Button();
      this.totalFileSizeProgress = new Label();
      this.curentSize = new Label();
      this.fileSent = new Label();
      this.fileSize = new Label();
      this.groupBox1 = new GroupBox();
      this.totalApps = new Label();
      this.exportListIdButton = new Button();
      this.groupBox2 = new GroupBox();
      this.groupBox3 = new GroupBox();
      this.spaceLeft = new Label();
      this.spaceTotal = new Label();
      this.colorDialog1 = new ColorDialog();
      this.sendCommand = new TextBox();
      this.button1 = new Button();
      this.button2 = new Button();
      this.downFile = new TextBox();
      this.linkLabel1 = new LinkLabel();
      this.statusLog = new RichTextBox();
      this.menuStrip1.SuspendLayout();
      this.installProgressGroup.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.pbBatt.Location = new Point(252, 24);
      this.pbBatt.Name = "pbBatt";
      this.pbBatt.Size = new Size(140, 23);
      this.pbBatt.TabIndex = 3;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(206, 29);
      this.label2.Name = "label2";
      this.label2.Size = new Size(40, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Battery";
      this.pbSpace.Location = new Point(60, 24);
      this.pbSpace.Name = "pbSpace";
      this.pbSpace.Size = new Size(140, 23);
      this.pbSpace.TabIndex = 1;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(16, 29);
      this.label1.Name = "label1";
      this.label1.Size = new Size(38, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Space";
      this.pbFileProgress.Location = new Point(26, 57);
      this.pbFileProgress.Name = "pbFileProgress";
      this.pbFileProgress.Size = new Size(268, 23);
      this.pbFileProgress.TabIndex = 4;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(23, 41);
      this.label3.Name = "label3";
      this.label3.Size = new Size(70, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "File Progress:";
      this.lblFilename.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblFilename.Location = new Point(23, 220);
      this.lblFilename.Name = "lblFilename";
      this.lblFilename.Size = new Size(264, 53);
      this.lblFilename.TabIndex = 6;
      this.lblFilename.TextAlign = ContentAlignment.MiddleCenter;
      this.progPercent.Location = new Point(254, 41);
      this.progPercent.Name = "progPercent";
      this.progPercent.RightToLeft = RightToLeft.No;
      this.progPercent.Size = new Size(41, 13);
      this.progPercent.TabIndex = 14;
      this.progPercent.Text = "0%";
      this.progPercent.TextAlign = ContentAlignment.MiddleRight;
      this.totalProgress.Location = new Point(26, 154);
      this.totalProgress.Name = "totalProgress";
      this.totalProgress.Size = new Size(268, 23);
      this.totalProgress.TabIndex = 15;
      this.label5.AutoSize = true;
      this.label5.Location = new Point(23, 138);
      this.label5.Name = "label5";
      this.label5.Size = new Size(78, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "Total Progress:";
      this.totalPercent.Location = new Point(253, 138);
      this.totalPercent.Name = "totalPercent";
      this.totalPercent.RightToLeft = RightToLeft.No;
      this.totalPercent.Size = new Size(41, 13);
      this.totalPercent.TabIndex = 17;
      this.totalPercent.Text = "0%";
      this.totalPercent.TextAlign = ContentAlignment.MiddleRight;
      this.deleteApps.FormattingEnabled = true;
      this.deleteApps.Location = new Point(12, 38);
      this.deleteApps.Name = "deleteApps";
      this.deleteApps.Size = new Size(281, 160);
      this.deleteApps.Sorted = true;
      this.deleteApps.TabIndex = 22;
      this.deleteDownloads.FormattingEnabled = true;
      this.deleteDownloads.Location = new Point(12, 40);
      this.deleteDownloads.Name = "deleteDownloads";
      this.deleteDownloads.Size = new Size(281, 160);
      this.deleteDownloads.Sorted = true;
      this.deleteDownloads.TabIndex = 25;
      this.btnDeleteApp.Cursor = Cursors.Hand;
      this.btnDeleteApp.Location = new Point(218, 14);
      this.btnDeleteApp.Name = "btnDeleteApp";
      this.btnDeleteApp.Size = new Size(75, 23);
      this.btnDeleteApp.TabIndex = 26;
      this.btnDeleteApp.Text = "Delete";
      this.btnDeleteApp.UseVisualStyleBackColor = true;
      this.btnDeleteApp.Visible = false;
      this.btnDeleteApp.Click += new EventHandler(this.btnDeleteApp_Click);
      this.btnDeleteDownload.Cursor = Cursors.Hand;
      this.btnDeleteDownload.Location = new Point(218, 13);
      this.btnDeleteDownload.Name = "btnDeleteDownload";
      this.btnDeleteDownload.Size = new Size(75, 23);
      this.btnDeleteDownload.TabIndex = 27;
      this.btnDeleteDownload.Text = "Delete";
      this.btnDeleteDownload.UseVisualStyleBackColor = true;
      this.btnDeleteDownload.Visible = false;
      this.btnDeleteDownload.Click += new EventHandler(this.btnDeleteDownload_Click);
      this.btnDownloadApp.Cursor = Cursors.Hand;
      this.btnDownloadApp.Location = new Point(139, 14);
      this.btnDownloadApp.Name = "btnDownloadApp";
      this.btnDownloadApp.Size = new Size(75, 23);
      this.btnDownloadApp.TabIndex = 30;
      this.btnDownloadApp.Text = "Download";
      this.btnDownloadApp.UseVisualStyleBackColor = true;
      this.btnDownloadApp.Visible = false;
      this.btnDownloadApp.Click += new EventHandler(this.btnDownloadApp_Click);
      this.btnDownloadDownload.Cursor = Cursors.Hand;
      this.btnDownloadDownload.Location = new Point(138, 13);
      this.btnDownloadDownload.Name = "btnDownloadDownload";
      this.btnDownloadDownload.Size = new Size(75, 23);
      this.btnDownloadDownload.TabIndex = 31;
      this.btnDownloadDownload.Text = "Download";
      this.btnDownloadDownload.UseVisualStyleBackColor = true;
      this.btnDownloadDownload.Visible = false;
      this.btnDownloadDownload.Click += new EventHandler(this.btnDownloadDownload_Click);
      this.exportListButton.Cursor = Cursors.Hand;
      this.exportListButton.Location = new Point(206, 201);
      this.exportListButton.Name = "exportListButton";
      this.exportListButton.Size = new Size(87, 23);
      this.exportListButton.TabIndex = 34;
      this.exportListButton.Text = "Export List";
      this.exportListButton.UseVisualStyleBackColor = true;
      this.exportListButton.Visible = false;
      this.exportListButton.Click += new EventHandler(this.ExportList_Click);
      this.menuStrip1.Items.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.leapPadToolStripMenuItem,
        (ToolStripItem) this.ServiceToolStripMenuItem,
        (ToolStripItem) this.developToolStripMenuItem,
        (ToolStripItem) this.deaksToolsToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem,
        (ToolStripItem) this.checkForUpdateToolStripMenuItem,
        (ToolStripItem) this.settingsToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(742, 24);
      this.menuStrip1.TabIndex = 35;
      this.menuStrip1.Text = "menuStrip1";
      this.leapPadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.connectToolStripMenuItem,
        (ToolStripItem) this.disconnectToolStripMenuItem,
        (ToolStripItem) this.rebootToolStripMenuItem,
        (ToolStripItem) this.themeMakerToolStripMenuItem1,
        (ToolStripItem) this.videoMakerToolStripMenuItem,
        (ToolStripItem) this.lF3Menu,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.installAppsFrominstallFolderToolStripMenuItem,
        (ToolStripItem) this.installAppsToolStripMenuItem
      });
      this.leapPadToolStripMenuItem.Name = "leapPadToolStripMenuItem";
      this.leapPadToolStripMenuItem.Size = new Size(64, 20);
      this.leapPadToolStripMenuItem.Text = "LeapPad";
      this.connectToolStripMenuItem.Image = (Image) Resources._1413966483_disconnect;
      this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
      this.connectToolStripMenuItem.Size = new Size(241, 22);
      this.connectToolStripMenuItem.Text = "Connect";
      this.connectToolStripMenuItem.Click += new EventHandler(this.btnConnect_Click);
      this.disconnectToolStripMenuItem.Image = (Image) Resources._1413966483_disconnect;
      this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
      this.disconnectToolStripMenuItem.Size = new Size(241, 22);
      this.disconnectToolStripMenuItem.Text = "Disconnect";
      this.disconnectToolStripMenuItem.Visible = false;
      this.disconnectToolStripMenuItem.Click += new EventHandler(this.btnDisconnect_Click);
      this.rebootToolStripMenuItem.Image = (Image) Resources._1413966561_gnome_session_reboot;
      this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
      this.rebootToolStripMenuItem.Size = new Size(241, 22);
      this.rebootToolStripMenuItem.Text = "Reboot System";
      this.rebootToolStripMenuItem.Visible = false;
      this.rebootToolStripMenuItem.Click += new EventHandler(this.btnReboot_Click);
      this.themeMakerToolStripMenuItem1.Image = (Image) Resources._1413966592_gnome_settings_theme;
      this.themeMakerToolStripMenuItem1.Name = "themeMakerToolStripMenuItem1";
      this.themeMakerToolStripMenuItem1.Size = new Size(241, 22);
      this.themeMakerToolStripMenuItem1.Text = "Theme Maker";
      this.themeMakerToolStripMenuItem1.Click += new EventHandler(this.showThemeManager);
      this.videoMakerToolStripMenuItem.Image = (Image) Resources._1413966651_Video_48x48;
      this.videoMakerToolStripMenuItem.Name = "videoMakerToolStripMenuItem";
      this.videoMakerToolStripMenuItem.Size = new Size(241, 22);
      this.videoMakerToolStripMenuItem.Text = "Video Maker";
      this.videoMakerToolStripMenuItem.Click += new EventHandler(this.showVideoManager);
      this.lF3Menu.Name = "lF3Menu";
      this.lF3Menu.Size = new Size(241, 22);
      this.lF3Menu.Text = "LF3 Tool";
      this.lF3Menu.Visible = false;
      this.lF3Menu.Click += new EventHandler(this.showLF3);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(238, 6);
      this.installAppsFrominstallFolderToolStripMenuItem.Name = "installAppsFrominstallFolderToolStripMenuItem";
      this.installAppsFrominstallFolderToolStripMenuItem.Size = new Size(241, 22);
      this.installAppsFrominstallFolderToolStripMenuItem.Text = "Auto install from \"install\" folder";
      this.installAppsFrominstallFolderToolStripMenuItem.Visible = false;
      this.installAppsFrominstallFolderToolStripMenuItem.Click += new EventHandler(this.installFromInstall_Click);
      this.installAppsToolStripMenuItem.Name = "installAppsToolStripMenuItem";
      this.installAppsToolStripMenuItem.Size = new Size(241, 22);
      this.installAppsToolStripMenuItem.Text = "Install....";
      this.installAppsToolStripMenuItem.Visible = false;
      this.installAppsToolStripMenuItem.Click += new EventHandler(this.installAppsBrowse);
      this.ServiceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.toolStripMenuItem3,
        (ToolStripItem) this.toolStripMenuItem4,
        (ToolStripItem) this.toolStripMenuItem1
      });
      this.ServiceToolStripMenuItem.Name = "ServiceToolStripMenuItem";
      this.ServiceToolStripMenuItem.Size = new Size(87, 20);
      this.ServiceToolStripMenuItem.Text = "Service Tools";
      this.ServiceToolStripMenuItem.Visible = false;
      this.toolStripMenuItem2.Image = (Image) Resources._1413967365_add_128;
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(187, 22);
      this.toolStripMenuItem2.Text = "Calibrate Screen";
      this.toolStripMenuItem2.Click += new EventHandler(this.btnNewCalib_Click);
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new Size(187, 22);
      this.toolStripMenuItem3.Text = "Set Calibration Data";
      this.toolStripMenuItem3.Click += new EventHandler(this.SetCalibrationData);
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new Size(187, 22);
      this.toolStripMenuItem4.Text = "Set Calibration Data 2";
      this.toolStripMenuItem4.Click += new EventHandler(this.SetCalibrationData2);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(187, 22);
      this.toolStripMenuItem1.Text = "Set Calibration Data 3";
      this.toolStripMenuItem1.Click += new EventHandler(this.SetCalibrationData3);
      this.developToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.calibrateScreenToolStripMenuItem1,
        (ToolStripItem) this.loadDevModeToolStripMenuItem,
        (ToolStripItem) this.resetUnitToolStripMenuItem1,
        (ToolStripItem) this.installTestToolsToolStripMenuItem,
        (ToolStripItem) this.toggleMainAppToolStripMenuItem,
        (ToolStripItem) this.dumpCartToolStripMenuItem,
        (ToolStripItem) this.changeHostFileToolStripMenuItem
      });
      this.developToolStripMenuItem.Name = "developToolStripMenuItem";
      this.developToolStripMenuItem.Size = new Size(121, 20);
      this.developToolStripMenuItem.Text = "Development Tools";
      this.developToolStripMenuItem.Visible = false;
      this.calibrateScreenToolStripMenuItem1.Image = (Image) Resources._1413967365_add_128;
      this.calibrateScreenToolStripMenuItem1.Name = "calibrateScreenToolStripMenuItem1";
      this.calibrateScreenToolStripMenuItem1.Size = new Size(192, 22);
      this.calibrateScreenToolStripMenuItem1.Text = "Calibrate Screen";
      this.calibrateScreenToolStripMenuItem1.Click += new EventHandler(this.btnNewCalib_Click);
      this.loadDevModeToolStripMenuItem.Image = (Image) Resources._1413967438_utilities_terminal;
      this.loadDevModeToolStripMenuItem.Name = "loadDevModeToolStripMenuItem";
      this.loadDevModeToolStripMenuItem.Size = new Size(192, 22);
      this.loadDevModeToolStripMenuItem.Text = "Load Dev Mode";
      this.loadDevModeToolStripMenuItem.Click += new EventHandler(this.btnToggleDev_Click);
      this.resetUnitToolStripMenuItem1.Image = (Image) Resources._1413966715_Reset;
      this.resetUnitToolStripMenuItem1.Name = "resetUnitToolStripMenuItem1";
      this.resetUnitToolStripMenuItem1.Size = new Size(192, 22);
      this.resetUnitToolStripMenuItem1.Text = "Reset Unit";
      this.resetUnitToolStripMenuItem1.Click += new EventHandler(this.btnReset_Click);
      this.installTestToolsToolStripMenuItem.Image = (Image) Resources._1413966806_Install1;
      this.installTestToolsToolStripMenuItem.Name = "installTestToolsToolStripMenuItem";
      this.installTestToolsToolStripMenuItem.Size = new Size(192, 22);
      this.installTestToolsToolStripMenuItem.Text = "Install / Run Test Tools";
      this.installTestToolsToolStripMenuItem.Visible = false;
      this.installTestToolsToolStripMenuItem.Click += new EventHandler(this.btnMfgTest_Click);
      this.toggleMainAppToolStripMenuItem.Name = "toggleMainAppToolStripMenuItem";
      this.toggleMainAppToolStripMenuItem.Size = new Size(192, 22);
      this.toggleMainAppToolStripMenuItem.Text = "Toggle Main App";
      this.toggleMainAppToolStripMenuItem.Click += new EventHandler(this.enable_disable_mainapp);
      this.dumpCartToolStripMenuItem.Name = "dumpCartToolStripMenuItem";
      this.dumpCartToolStripMenuItem.Size = new Size(192, 22);
      this.dumpCartToolStripMenuItem.Text = "Dump Cart";
      this.dumpCartToolStripMenuItem.Visible = false;
      this.dumpCartToolStripMenuItem.Click += new EventHandler(this.btn_dumpCart);
      this.changeHostFileToolStripMenuItem.Name = "changeHostFileToolStripMenuItem";
      this.changeHostFileToolStripMenuItem.Size = new Size(192, 22);
      this.changeHostFileToolStripMenuItem.Text = "Change Host File";
      this.changeHostFileToolStripMenuItem.Click += new EventHandler(this.changeHostFileToolStripMenuItem_Click);
      this.deaksToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[10]
      {
        (ToolStripItem) this.installDefaultGreenAppsToolStripMenuItem,
        (ToolStripItem) this.installDefaultPurpleAppsToolStripMenuItem,
        (ToolStripItem) this.installPrincessAppsToolStripMenuItem,
        (ToolStripItem) this.installLP1AppsToolStripMenuItem,
        (ToolStripItem) this.installLP3AppsToolStripMenuItem,
        (ToolStripItem) this.setCalibrationDataToolStripMenuItem,
        (ToolStripItem) this.setCalibrationData2ToolStripMenuItem,
        (ToolStripItem) this.setCalibrationData3ToolStripMenuItem,
        (ToolStripItem) this.changeParentPinToolStripMenuItem,
        (ToolStripItem) this.generateSerialHashToolStripMenuItem
      });
      this.deaksToolsToolStripMenuItem.Name = "deaksToolsToolStripMenuItem";
      this.deaksToolsToolStripMenuItem.Size = new Size(81, 20);
      this.deaksToolsToolStripMenuItem.Text = "Deaks Tools";
      this.deaksToolsToolStripMenuItem.Visible = false;
      this.installDefaultGreenAppsToolStripMenuItem.Image = (Image) Resources.LeapPad2_Green_front1;
      this.installDefaultGreenAppsToolStripMenuItem.Name = "installDefaultGreenAppsToolStripMenuItem";
      this.installDefaultGreenAppsToolStripMenuItem.Size = new Size(187, 22);
      this.installDefaultGreenAppsToolStripMenuItem.Text = "Install Green Apps";
      this.installDefaultGreenAppsToolStripMenuItem.Click += new EventHandler(this.installDefaultGreenAppsToolStripMenuItem_Click);
      this.installDefaultPurpleAppsToolStripMenuItem.Image = (Image) Resources._81l5Wx2nt_L1;
      this.installDefaultPurpleAppsToolStripMenuItem.Name = "installDefaultPurpleAppsToolStripMenuItem";
      this.installDefaultPurpleAppsToolStripMenuItem.Size = new Size(187, 22);
      this.installDefaultPurpleAppsToolStripMenuItem.Text = "Install Purple Apps";
      this.installDefaultPurpleAppsToolStripMenuItem.Click += new EventHandler(this.installDefaultPurpleAppsToolStripMenuItem_Click);
      this.installPrincessAppsToolStripMenuItem.Image = (Image) Resources.c26_B0089U3VA4_2_l;
      this.installPrincessAppsToolStripMenuItem.Name = "installPrincessAppsToolStripMenuItem";
      this.installPrincessAppsToolStripMenuItem.Size = new Size(187, 22);
      this.installPrincessAppsToolStripMenuItem.Text = "Install Princess Apps";
      this.installPrincessAppsToolStripMenuItem.Click += new EventHandler(this.installPrincessAppsToolStripMenuItem_Click);
      this.installLP1AppsToolStripMenuItem.Name = "installLP1AppsToolStripMenuItem";
      this.installLP1AppsToolStripMenuItem.Size = new Size(187, 22);
      this.installLP1AppsToolStripMenuItem.Text = "Install LP1 Apps";
      this.installLP1AppsToolStripMenuItem.Click += new EventHandler(this.installLP1AppsToolStripMenuItem_Click);
      this.installLP3AppsToolStripMenuItem.Name = "installLP3AppsToolStripMenuItem";
      this.installLP3AppsToolStripMenuItem.Size = new Size(187, 22);
      this.installLP3AppsToolStripMenuItem.Text = "Install LP3 Apps";
      this.installLP3AppsToolStripMenuItem.Click += new EventHandler(this.installLP3AppsToolStripMenuItem_Click);
      this.setCalibrationDataToolStripMenuItem.Name = "setCalibrationDataToolStripMenuItem";
      this.setCalibrationDataToolStripMenuItem.Size = new Size(187, 22);
      this.setCalibrationDataToolStripMenuItem.Text = "Set Calibration Data";
      this.setCalibrationDataToolStripMenuItem.Click += new EventHandler(this.SetCalibrationData);
      this.setCalibrationData2ToolStripMenuItem.Name = "setCalibrationData2ToolStripMenuItem";
      this.setCalibrationData2ToolStripMenuItem.Size = new Size(187, 22);
      this.setCalibrationData2ToolStripMenuItem.Text = "Set Calibration Data 2";
      this.setCalibrationData2ToolStripMenuItem.Click += new EventHandler(this.SetCalibrationData2);
      this.setCalibrationData3ToolStripMenuItem.Name = "setCalibrationData3ToolStripMenuItem";
      this.setCalibrationData3ToolStripMenuItem.Size = new Size(187, 22);
      this.setCalibrationData3ToolStripMenuItem.Text = "Set Calibration Data 3";
      this.setCalibrationData3ToolStripMenuItem.Click += new EventHandler(this.SetCalibrationData3);
      this.changeParentPinToolStripMenuItem.Name = "changeParentPinToolStripMenuItem";
      this.changeParentPinToolStripMenuItem.Size = new Size(187, 22);
      this.changeParentPinToolStripMenuItem.Text = "Change Parent Pin";
      this.changeParentPinToolStripMenuItem.Visible = false;
      this.changeParentPinToolStripMenuItem.Click += new EventHandler(this.saveParentPin);
      this.generateSerialHashToolStripMenuItem.Name = "generateSerialHashToolStripMenuItem";
      this.generateSerialHashToolStripMenuItem.Size = new Size(187, 22);
      this.generateSerialHashToolStripMenuItem.Text = "Generate Serial Hash";
      this.generateSerialHashToolStripMenuItem.Click += new EventHandler(this.generateSerialHash);
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      this.helpToolStripMenuItem.Click += new EventHandler(this.showHelp);
      this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
      this.checkForUpdateToolStripMenuItem.Size = new Size(113, 20);
      this.checkForUpdateToolStripMenuItem.Text = "Check For Update";
      this.checkForUpdateToolStripMenuItem.Click += new EventHandler(this.checkForUpdates);
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new Size(61, 20);
      this.settingsToolStripMenuItem.Text = "Settings";
      this.settingsToolStripMenuItem.Click += new EventHandler(this.showSettings);
      this.installProgressGroup.Controls.Add((Control) this.cancel);
      this.installProgressGroup.Controls.Add((Control) this.totalFileSizeProgress);
      this.installProgressGroup.Controls.Add((Control) this.curentSize);
      this.installProgressGroup.Controls.Add((Control) this.fileSent);
      this.installProgressGroup.Controls.Add((Control) this.fileSize);
      this.installProgressGroup.Controls.Add((Control) this.pbFileProgress);
      this.installProgressGroup.Controls.Add((Control) this.label3);
      this.installProgressGroup.Controls.Add((Control) this.progPercent);
      this.installProgressGroup.Controls.Add((Control) this.totalProgress);
      this.installProgressGroup.Controls.Add((Control) this.label5);
      this.installProgressGroup.Controls.Add((Control) this.totalPercent);
      this.installProgressGroup.Controls.Add((Control) this.lblFilename);
      this.installProgressGroup.Location = new Point(2, 39);
      this.installProgressGroup.Name = "installProgressGroup";
      this.installProgressGroup.Size = new Size(318, 471);
      this.installProgressGroup.TabIndex = 36;
      this.installProgressGroup.TabStop = false;
      this.installProgressGroup.Text = "Install Progress";
      this.installProgressGroup.Visible = false;
      this.cancel.Location = new Point(204, 326);
      this.cancel.Name = "cancel";
      this.cancel.Size = new Size(91, 23);
      this.cancel.TabIndex = 46;
      this.cancel.Text = "Cancel";
      this.cancel.UseVisualStyleBackColor = true;
      this.cancel.Click += new EventHandler(this.cancelInstall);
      this.totalFileSizeProgress.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.totalFileSizeProgress.Location = new Point(23, 180);
      this.totalFileSizeProgress.Name = "totalFileSizeProgress";
      this.totalFileSizeProgress.Size = new Size(132, 22);
      this.totalFileSizeProgress.TabIndex = 44;
      this.curentSize.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.curentSize.Location = new Point(170, 83);
      this.curentSize.Name = "curentSize";
      this.curentSize.Size = new Size(124, 22);
      this.curentSize.TabIndex = 43;
      this.curentSize.TextAlign = ContentAlignment.TopRight;
      this.fileSent.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.fileSent.Location = new Point(23, 83);
      this.fileSent.Name = "fileSent";
      this.fileSent.Size = new Size(132, 22);
      this.fileSent.TabIndex = 42;
      this.fileSize.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.fileSize.Location = new Point(161, 180);
      this.fileSize.Name = "fileSize";
      this.fileSize.Size = new Size(133, 22);
      this.fileSize.TabIndex = 18;
      this.fileSize.TextAlign = ContentAlignment.TopRight;
      this.groupBox1.Controls.Add((Control) this.totalApps);
      this.groupBox1.Controls.Add((Control) this.exportListIdButton);
      this.groupBox1.Controls.Add((Control) this.deleteApps);
      this.groupBox1.Controls.Add((Control) this.btnDeleteApp);
      this.groupBox1.Controls.Add((Control) this.btnDownloadApp);
      this.groupBox1.Controls.Add((Control) this.exportListButton);
      this.groupBox1.Location = new Point(12, 39);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(308, 230);
      this.groupBox1.TabIndex = 37;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Program Files";
      this.totalApps.AutoSize = true;
      this.totalApps.Location = new Point(9, 206);
      this.totalApps.Name = "totalApps";
      this.totalApps.Size = new Size(82, 13);
      this.totalApps.TabIndex = 36;
      this.totalApps.Text = "0 Apps Installed";
      this.exportListIdButton.Cursor = Cursors.Hand;
      this.exportListIdButton.Location = new Point(113, 201);
      this.exportListIdButton.Name = "exportListIdButton";
      this.exportListIdButton.Size = new Size(87, 23);
      this.exportListIdButton.TabIndex = 35;
      this.exportListIdButton.Text = "Export ID List";
      this.exportListIdButton.UseVisualStyleBackColor = true;
      this.exportListIdButton.Visible = false;
      this.exportListIdButton.Click += new EventHandler(this.ExportListIds_Click);
      this.groupBox2.Controls.Add((Control) this.deleteDownloads);
      this.groupBox2.Controls.Add((Control) this.btnDeleteDownload);
      this.groupBox2.Controls.Add((Control) this.btnDownloadDownload);
      this.groupBox2.Location = new Point(12, 275);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(308, 236);
      this.groupBox2.TabIndex = 38;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Downloads";
      this.groupBox3.Controls.Add((Control) this.spaceLeft);
      this.groupBox3.Controls.Add((Control) this.spaceTotal);
      this.groupBox3.Controls.Add((Control) this.pbSpace);
      this.groupBox3.Controls.Add((Control) this.pbBatt);
      this.groupBox3.Controls.Add((Control) this.label2);
      this.groupBox3.Controls.Add((Control) this.label1);
      this.groupBox3.Location = new Point(326, 446);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(407, 64);
      this.groupBox3.TabIndex = 39;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Details";
      this.spaceLeft.Font = new Font("Microsoft Sans Serif", 6.25f);
      this.spaceLeft.Location = new Point(57, 48);
      this.spaceLeft.Name = "spaceLeft";
      this.spaceLeft.RightToLeft = RightToLeft.No;
      this.spaceLeft.Size = new Size(61, 13);
      this.spaceLeft.TabIndex = 48;
      this.spaceLeft.TextAlign = ContentAlignment.MiddleLeft;
      this.spaceTotal.Font = new Font("Microsoft Sans Serif", 6.25f);
      this.spaceTotal.Location = new Point(135, 48);
      this.spaceTotal.Name = "spaceTotal";
      this.spaceTotal.RightToLeft = RightToLeft.No;
      this.spaceTotal.Size = new Size(65, 13);
      this.spaceTotal.TabIndex = 47;
      this.spaceTotal.TextAlign = ContentAlignment.MiddleRight;
      this.sendCommand.Location = new Point(326, 417);
      this.sendCommand.Name = "sendCommand";
      this.sendCommand.Size = new Size(310, 20);
      this.sendCommand.TabIndex = 40;
      this.button1.Enabled = false;
      this.button1.Location = new Point(642, 417);
      this.button1.Name = "button1";
      this.button1.Size = new Size(91, 23);
      this.button1.TabIndex = 41;
      this.button1.Text = "Send Command";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.btnSendCommand_Click);
      this.button2.Enabled = false;
      this.button2.Location = new Point(642, 391);
      this.button2.Name = "button2";
      this.button2.Size = new Size(91, 23);
      this.button2.TabIndex = 43;
      this.button2.Text = "Download File";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.btnDownloadFile_Click);
      this.downFile.Location = new Point(326, 391);
      this.downFile.Name = "downFile";
      this.downFile.Size = new Size(310, 20);
      this.downFile.TabIndex = 44;
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.linkLabel1.Location = new Point(642, 6);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(91, 18);
      this.linkLabel1.TabIndex = 45;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Spiffy Hacks";
      this.linkLabel1.Click += new EventHandler(this.loadSpiffyHacks);
      this.statusLog.BackColor = Color.White;
      this.statusLog.Location = new Point(326, 39);
      this.statusLog.Name = "statusLog";
      this.statusLog.ReadOnly = true;
      this.statusLog.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.statusLog.Size = new Size(412, 346);
      this.statusLog.TabIndex = 126;
      this.statusLog.Text = "";
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(742, 522);
      this.Controls.Add((Control) this.statusLog);
      this.Controls.Add((Control) this.installProgressGroup);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.downFile);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.sendCommand);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.Name = nameof (Form1);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "LeapPad Manager - By Deak Phreak and is0Mick | v";
      this.Load += new EventHandler(this.Form1_Load);
      this.DragDrop += new DragEventHandler(this.Form1_DragDrop);
      this.DragOver += new DragEventHandler(this.Form1_DragEnter);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.installProgressGroup.ResumeLayout(false);
      this.installProgressGroup.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void UpdateLogCallback(string message, string status = "", bool showPrefix = false);
  }
}
