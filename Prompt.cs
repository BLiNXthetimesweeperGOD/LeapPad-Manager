// Decompiled with JetBrains decompiler
// Type: Prompt
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Windows.Forms;

public static class Prompt
{
  public static string ShowPinDialog(string text, string caption)
  {
    Form prompt = new Form();
    prompt.Width = 500;
    prompt.Height = 150;
    prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
    prompt.Text = caption;
    prompt.StartPosition = FormStartPosition.CenterScreen;
    Label label1 = new Label();
    label1.Left = 50;
    label1.Top = 20;
    label1.Text = text;
    Label label2 = label1;
    TextBox textBox1 = new TextBox();
    textBox1.Left = 50;
    textBox1.Top = 50;
    textBox1.Width = 400;
    TextBox textBox2 = textBox1;
    Button button1 = new Button();
    button1.Text = "Ok";
    button1.Left = 350;
    button1.Width = 100;
    button1.Top = 70;
    Button button2 = button1;
    button2.Click += (EventHandler) ((sender, e) => prompt.Close());
    prompt.Controls.Add((Control) textBox2);
    prompt.Controls.Add((Control) button2);
    prompt.Controls.Add((Control) label2);
    prompt.AcceptButton = (IButtonControl) button2;
    int num = (int) prompt.ShowDialog();
    return textBox2.Text;
  }
}
