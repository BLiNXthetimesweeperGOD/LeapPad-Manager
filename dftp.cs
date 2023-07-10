// Decompiled with JetBrains decompiler
// Type: LFConnect.dftp
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Helper.IO;
using SCSI;
using SCSI.Block;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LFConnect
{
  internal class dftp
  {
    public EventHandler OnProgressUpdate;
    public int ProgressPercent = 0;
    public ulong totalspace = 0;
    public ulong freespace = 0;
    public string spaceTotal = "";
    public string spaceFree = "";
    public string deviceInfo = "";
    private Win32FileStream stream;
    public BackgroundWorker backgroundworker = (BackgroundWorker) null;
    private string m_device = "\\\\.\\PhysicalDrive2";
    private BlockDevice currentDrive;
    private int m_request_large = 10240;
    private int m_request_small = 512;
    public bool connected = false;
    public string sendrtnResult = "";

    public void UpdateProgress() => this.OnProgressUpdate((object) this, EventArgs.Empty);

    public dftp()
    {
      this.m_device = this.findDevice();
      if (this.m_device == "")
      {
        this.connected = false;
      }
      else
      {
        try
        {
          this.stream = new Win32FileStream(this.m_device, FileAccess.ReadWrite, FileShare.None, FileMode.Open, FileOptions.None);
          this.currentDrive = new BlockDevice((ISCSIPassThrough) new SPTI(this.stream.SafeFileHandle, false), false);
          this.connected = true;
        }
        catch (Exception ex)
        {
          this.connected = false;
        }
      }
    }

    private string findDeviceOld()
    {
      Process process = new Process();
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.Start();
      string[] strArray = process.StandardOutput.ReadToEnd().Split('\n');
      string str1 = "";
      foreach (string str2 in strArray)
      {
        if (str2.Contains("LeapFrog"))
        {
          str1 = str2;
          break;
        }
      }
      return str1 != "" ? str1.Split(' ')[0] : throw new Exception("Cant find the device :(");
    }

    private string findDevice()
    {
      string path = "";
      for (int index = 0; index < 35; ++index)
      {
        try
        {
          path = string.Format("\\\\.\\PhysicalDrive{0}", (object) index);
          dftp dftp1 = this;
          dftp1.deviceInfo = dftp1.deviceInfo + Environment.NewLine + path;
          Win32FileStream win32FileStream = new Win32FileStream(path, FileAccess.ReadWrite, FileShare.None, FileMode.Open, FileOptions.None);
          string vendorIdentification = new BlockDevice((ISCSIPassThrough) new SPTI(win32FileStream.SafeFileHandle, false), false).Inquiry().VendorIdentification;
          win32FileStream.Close();
          dftp dftp2 = this;
          dftp2.deviceInfo = dftp2.deviceInfo + " - ID: " + vendorIdentification;
          if (vendorIdentification.Contains("LeapFro"))
            break;
        }
        catch (Exception ex)
        {
          dftp dftp = this;
          dftp.deviceInfo = dftp.deviceInfo + Environment.NewLine + path + " ERROR: " + ex.ToString();
        }
      }
      return path;
    }

    public int send(byte[] data) => this.send(data, true);

    public int send(byte[] data, bool small)
    {
      int length1 = 0;
      int length2 = data.Length;
      int num1 = 0;
      int num2 = 0;
      try
      {
        int length3;
        uint num3;
        ushort num4;
        if (small)
        {
          length3 = this.m_request_small;
          num3 = 32U;
          num4 = (ushort) 1;
        }
        else
        {
          length3 = this.m_request_large;
          num3 = 64U;
          num4 = (ushort) 20;
        }
        Write10Command command = new Write10Command();
        command.LBA = num3;
        command.TransferLength = num4;
        int num5 = 0;
        for (int sourceIndex = 0; sourceIndex < length2; sourceIndex += length3)
        {
          byte[] numArray = new byte[length3];
          length1 = length3;
          if (data.Length - sourceIndex < length3)
            length1 = data.Length - sourceIndex;
          Array.Copy((Array) data, sourceIndex, (Array) numArray, 0, length1);
          num2 = numArray.Length;
          this.currentDrive.Write10(command, numArray, 0);
          SenseData senseData;
          do
            ;
          while ((senseData = this.currentDrive.RequestSense()).SenseKey == SenseKey.NotReady && senseData.AdditionalSenseCode == AdditionalSenseCode.LogicalUnitNotReady && senseData.AdditionalSenseCodeQualifier == (AdditionalSenseCodeQualifier) 7);
          if (!small)
          {
            this.ProgressPercent = (int) (100.0 * ((double) sourceIndex / (double) length2));
            if (this.ProgressPercent % 5 == 0 && this.ProgressPercent != num5)
            {
              this.backgroundworker.ReportProgress(this.ProgressPercent);
              num5 = this.ProgressPercent;
            }
            if (!this.receive(true).Contains("103 CONT"))
            {
              Console.WriteLine("NOT 103");
              break;
            }
          }
        }
        if (!small)
          this.sendrtn(string.Format("101 EOF:{0}", (object) (num2 - length1)));
      }
      catch
      {
      }
      return num1;
    }

    public string receive() => this.receive(true);

    public string receive(bool small)
    {
      int num1 = 0;
      uint num2;
      ushort num3;
      if (small)
      {
        num1 = this.m_request_small;
        num2 = 32U;
        num3 = (ushort) 1;
      }
      else
      {
        num1 = this.m_request_large;
        num2 = 64U;
        num3 = (ushort) 20;
      }
      string str1 = "";
      Read10Command command = new Read10Command();
      command.TransferLength = num3;
      command.LBA = num2;
      string str2;
      do
      {
        byte[] bytes = this.currentDrive.Read10(command);
        SenseData senseData;
        do
          ;
        while ((senseData = this.currentDrive.RequestSense()).SenseKey == SenseKey.NotReady && senseData.AdditionalSenseCode == AdditionalSenseCode.LogicalUnitNotReady && senseData.AdditionalSenseCodeQualifier == (AdditionalSenseCodeQualifier) 7);
        str2 = Encoding.ASCII.GetString(bytes);
        if (str2 == "")
          goto label_9;
      }
      while (str2.Contains("102 BUSY"));
      return str1 + str2;
label_9:
      return str1;
    }

    public bool sendrtn(string cmd)
    {
      this.sendrtnResult = "";
      bool flag1 = false;
      List<string> stringList = new List<string>();
      this.send(Encoding.ASCII.GetBytes(cmd), true);
      string str1 = "";
      flag1 = false;
      string str2;
      do
      {
        str2 = this.receive();
        str1 += str2;
      }
      while (!str2.Contains("200 OK") && !str2.Contains("503 Bad response:") && !str2.Contains("552 Path already exists:"));
      if (!(str1 != ""))
        Console.Write("Receiving error.");
      bool flag2 = false;
      if (str1.Contains("200 OK"))
      {
        this.sendrtnResult = str1.Replace("200 OK", "");
        flag2 = true;
      }
      return flag2;
    }

    public int upload_buffer(byte[] buf, string rpath)
    {
      try
      {
        rpath = rpath.Replace("\\", "/");
        this.sendrtn(string.Format("STOR {0}", (object) rpath));
        return this.send(buf, false);
      }
      catch
      {
        throw new Exception("Failed to upload file.");
      }
    }

    public bool run_buffer(string buf)
    {
      try
      {
        if (!buf.StartsWith("#!/bin/sh"))
          throw new Exception("File does not appear to be valid shell script, missing shebag line.");
        if (this.sendrtn("RUN"))
        {
          buf = buf.Replace("\r", "");
          this.send(Encoding.ASCII.GetBytes(buf), false);
        }
        do
        {
          this.sendrtn("GETS SCRIPT_RUNNING");
        }
        while (this.sendrtnResult.Contains("SCRIPT_RUNNING=1"));
        return true;
      }
      catch
      {
        throw new Exception("Failed to run script.");
      }
    }

    public byte[] download_buffer(string path)
    {
      string str1 = "";
      string s = "";
      ulong length = 0;
      bool flag = false;
      path = path.Replace("\\", "/");
      this.send(Encoding.ASCII.GetBytes(string.Format("RETR {0}", (object) path)), true);
      string str2 = this.receive().Replace("\0", "");
      if (str2.Contains("200 OK"))
      {
        s = "";
        str1 = "";
        flag = false;
      }
      string str3;
      do
      {
        string str4 = this.receive(false);
        if (!str4.ToLower().Contains("500 unknown command"))
        {
          s += str4;
          str3 = this.receive();
          if (str3.Contains("101 EOF"))
            goto label_7;
        }
        else
          goto label_2;
      }
      while (str3.Contains("103 CONT"));
      goto label_5;
label_2:
      flag = true;
      goto label_7;
label_5:
      flag = true;
label_7:
      if (str2.Contains("200 OK:"))
        length = (ulong) Convert.ToUInt32(str2.Replace("200 OK:", ""));
      else
        s = s.TrimEnd(new char[1]);
      if (flag)
        throw new Exception("Error receiving");
      byte[] destinationArray = new byte[length];
      Array.Copy((Array) Encoding.ASCII.GetBytes(s), (Array) destinationArray, (int) length);
      return destinationArray;
    }

    public void disconnect()
    {
      if (this.stream == null)
        return;
      this.send(Encoding.ASCII.GetBytes("DCON"));
      this.currentDrive.Dispose();
    }

    public void reboot() => this.run_buffer("#!/bin/sh\nreboot\n");

    public void reboot_usbmode()
    {
      this.send(Encoding.ASCII.GetBytes("UPD8"));
      this.send(Encoding.ASCII.GetBytes("NOOP"));
      this.send(Encoding.ASCII.GetBytes("DCON"));
    }

    public string getInfo()
    {
      this.send(Encoding.ASCII.GetBytes("INFO"), true);
      return this.receive(true);
    }

    public string getSerial()
    {
      string serial = "";
      this.send(Encoding.ASCII.GetBytes("GETS SERIAL"), true);
      string[] strArray = this.receive(true).Split('\n');
      if (strArray[0].Contains("SERIAL="))
        serial = strArray[0].Replace("SERIAL=", "").ToString().Replace("\"", "");
      return serial;
    }

    public string getFirmware() => Encoding.ASCII.GetString(this.cat("/etc/version")).Replace("\n", "").Replace("\r", "").Replace("\t", "");

    public int getFreeSpace()
    {
      int freeSpace = 0;
      this.send(Encoding.ASCII.GetBytes("GETS FREESPACE"), true);
      string[] strArray = this.receive(true).Split('\n');
      if (strArray[0].Contains("FREESPACE="))
      {
        this.freespace = Convert.ToUInt64(strArray[0].Replace("FREESPACE=", ""));
        this.send(Encoding.ASCII.GetBytes("GETS TOTALSPACE"), true);
        this.totalspace = Convert.ToUInt64(this.receive(true).Split('\n')[0].Replace("TOTALSPACE=", ""));
        freeSpace = (int) (100.0 * ((double) this.freespace / (double) this.totalspace));
        this.spaceTotal = dftp.GetSizeReadable(this.totalspace);
        this.spaceFree = dftp.GetSizeReadable(this.freespace);
      }
      return freeSpace;
    }

    public int getBatteryLevelUltra()
    {
      int batteryLevelUltra = 0;
      int num1 = 3650;
      int num2 = 4250;
      int num3 = num2 - num1;
      this.send(Encoding.ASCII.GetBytes("GETS BATTERYLEVEL"), true);
      string[] strArray = this.receive(true).Split('\n');
      if (strArray[0].Contains("BATTERYLEVEL="))
      {
        int int32 = Convert.ToInt32(strArray[0].Replace("BATTERYLEVEL=", ""));
        if (int32 > num2)
          batteryLevelUltra = 100;
        if (int32 < num1)
          batteryLevelUltra = 0;
        if (int32 > num1 && int32 < num2)
          batteryLevelUltra = 100 * (int32 - num1) / num3;
      }
      return batteryLevelUltra;
    }

    public int getBatteryLevel()
    {
      int batteryLevel = 0;
      int num1 = 3800;
      int num2 = 6000;
      int num3 = num2 - num1;
      this.send(Encoding.ASCII.GetBytes("GETS BATTERYLEVEL"), true);
      string[] strArray = this.receive(true).Split('\n');
      if (strArray[0].Contains("BATTERYLEVEL="))
      {
        int num4 = Convert.ToInt32(strArray[0].Replace("BATTERYLEVEL=", ""));
        if (num4 < num1)
          num4 = num2;
        batteryLevel = 100 * (num4 - num1) / num3;
      }
      return batteryLevel;
    }

    public void run_script(string path)
    {
      if (!File.Exists(path))
        return;
      this.run_buffer(Encoding.ASCII.GetString(File.ReadAllBytes(path)));
    }

    public bool exists(string path)
    {
      this.sendrtn(string.Format("LIST {0}", (object) path));
      return !this.sendrtnResult.StartsWith("550");
    }

    public bool is_dir(string path)
    {
      this.sendrtn(string.Format("LIST {0}", (object) path));
      string[] strArray = this.sendrtnResult.Split('\n');
      return strArray.Length > 1 && strArray[0].StartsWith("D");
    }

    public string[] dir_list(string path)
    {
      this.sendrtn(string.Format("LIST {0}", (object) path));
      return this.sendrtnResult.Split('\n');
    }

    public void rm_i(string path) => this.sendrtn(string.Format("RM {0}", (object) path));

    public void rmdir(string path) => this.sendrtn(string.Format("RMD {0}", (object) path));

    public void mkdir(string path) => this.sendrtn(string.Format("MKD {0}", (object) path));

    public void ipkg2(string meta) => this.sendrtn(string.Format("IPKG2 {0}", (object) meta));

    public void download_file(string lpath, string rpath)
    {
      try
      {
        string path = lpath;
        byte[] buffer = this.download_buffer(rpath);
        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        fileStream.Write(buffer, 0, buffer.Length);
        fileStream.Close();
      }
      catch
      {
        throw new Exception("Error downloading file");
      }
    }

    public void upload_file(string lpath, string rpath)
    {
      try
      {
        this.upload_buffer(File.ReadAllBytes(lpath), rpath);
      }
      catch
      {
        throw new Exception("Error Uploading file");
      }
    }

    public byte[] cat(string path) => this.download_buffer(path);

    public void ipkg(string lpath, string rpath) => this.IPKG_buffer(File.ReadAllBytes(lpath), rpath);

    public int IPKG_buffer(byte[] buf, string rpath)
    {
      try
      {
        rpath = rpath.Replace("\\", "/");
        this.sendrtn(string.Format("IPKG {0}", (object) rpath));
        return this.send(buf, false);
      }
      catch
      {
        throw new Exception("Failed to upload file.");
      }
    }

    public static string GetSizeReadable(ulong i)
    {
      string str1 = i < 0UL ? "-" : "";
      double num1 = i < 0UL ? 0.0 : (double) i;
      string str2;
      double num2;
      if (i >= 1152921504606846976UL)
      {
        str2 = "EB";
        num2 = (double) (i >> 50);
      }
      else if (i >= 1125899906842624UL)
      {
        str2 = "PB";
        num2 = (double) (i >> 40);
      }
      else if (i >= 1099511627776UL)
      {
        str2 = "TB";
        num2 = (double) (i >> 30);
      }
      else if (i >= 1073741824UL)
      {
        str2 = "GB";
        num2 = (double) (i >> 20);
      }
      else if (i >= 1048576UL)
      {
        str2 = "MB";
        num2 = (double) (i >> 10);
      }
      else
      {
        if (i < 1024UL)
          return i.ToString(str1 + "0 B");
        str2 = "KB";
        num2 = (double) i;
      }
      double num3 = num2 / 1024.0;
      return str1 + num3.ToString("0.## ") + str2;
    }
  }
}
