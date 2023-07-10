// Decompiled with JetBrains decompiler
// Type: LFConnect.Program
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LFConnect
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      try
      {
        foreach (Process process in Process.GetProcessesByName("Monitor"))
          process.Kill();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
