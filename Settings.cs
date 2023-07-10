// Decompiled with JetBrains decompiler
// Type: LFConnect.Settings
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LFConnect
{
  public class Settings : Form
  {
    private IContainer components = (IContainer) null;
    private Label themeInfo;
    private Label label1;
    private Label label2;
    private Label label3;
    private ComboBox comboBox1;
    private ComboBox comboBox2;
    private ComboBox comboBox3;
    private ComboBox comboBox4;
    private Button Cancel;
    private Button Save;
    private ComboBox comboBox5;
    private Label label5;
    private ComboBox comboBox6;
    private Label label4;

    public Settings()
    {
      this.InitializeComponent();
      this.loadSettings();
    }

    private void saveSettings(object sender, EventArgs e)
    {
      LFConnect.Properties.Settings.Default.OverwriteExisting = this.comboBox1.Text == "Yes";
      LFConnect.Properties.Settings.Default.AutoInstallMoviePlayer = this.comboBox2.Text == "Yes";
      LFConnect.Properties.Settings.Default.AutoInstallMfgTest = this.comboBox3.Text == "Yes";
      LFConnect.Properties.Settings.Default.OutputLevel = this.comboBox4.Text;
      LFConnect.Properties.Settings.Default.LimitApps = this.comboBox5.Text == "Yes";
      LFConnect.Properties.Settings.Default.updateDetails = this.comboBox6.Text == "Yes";
      LFConnect.Properties.Settings.Default.Save();
      this.Close();
    }

    private void loadSettings()
    {
      if (LFConnect.Properties.Settings.Default.OverwriteExisting)
        this.comboBox1.Text = "Yes";
      else
        this.comboBox1.Text = "No";
      if (LFConnect.Properties.Settings.Default.AutoInstallMoviePlayer)
        this.comboBox2.Text = "Yes";
      else
        this.comboBox2.Text = "No";
      if (LFConnect.Properties.Settings.Default.AutoInstallMfgTest)
        this.comboBox3.Text = "Yes";
      else
        this.comboBox3.Text = "No";
      this.comboBox4.Text = LFConnect.Properties.Settings.Default.OutputLevel.ToString();
      if (LFConnect.Properties.Settings.Default.LimitApps)
        this.comboBox5.Text = "Yes";
      else
        this.comboBox5.Text = "No";
      if (LFConnect.Properties.Settings.Default.updateDetails)
        this.comboBox6.Text = "Yes";
      else
        this.comboBox6.Text = "No";
    }

    private void Cancel_Click(object sender, EventArgs e) => this.Close();

    private void saveSettings()
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Settings));
      this.themeInfo = new Label();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.comboBox1 = new ComboBox();
      this.comboBox2 = new ComboBox();
      this.comboBox3 = new ComboBox();
      this.comboBox4 = new ComboBox();
      this.Cancel = new Button();
      this.Save = new Button();
      this.comboBox5 = new ComboBox();
      this.label5 = new Label();
      this.comboBox6 = new ComboBox();
      this.label4 = new Label();
      this.SuspendLayout();
      this.themeInfo.AutoSize = true;
      this.themeInfo.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.themeInfo.Location = new Point(12, 25);
      this.themeInfo.Name = "themeInfo";
      this.themeInfo.Size = new Size(171, 20);
      this.themeInfo.TabIndex = 43;
      this.themeInfo.Text = "Overwrite Existing Files";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(12, 64);
      this.label1.Name = "label1";
      this.label1.Size = new Size(297, 20);
      this.label1.TabIndex = 44;
      this.label1.Text = "Auto install movie player widget if missing";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(12, 106);
      this.label2.Name = "label2";
      this.label2.Size = new Size(252, 20);
      this.label2.TabIndex = 45;
      this.label2.Text = "Auto install mfgtest tools if missing";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(12, 148);
      this.label3.Name = "label3";
      this.label3.Size = new Size(121, 20);
      this.label3.TabIndex = 46;
      this.label3.Text = "Log output level";
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[2]
      {
        (object) "Yes",
        (object) "No"
      });
      this.comboBox1.Location = new Point(332, 24);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(121, 21);
      this.comboBox1.TabIndex = 47;
      this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Items.AddRange(new object[2]
      {
        (object) "Yes",
        (object) "No"
      });
      this.comboBox2.Location = new Point(332, 63);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new Size(121, 21);
      this.comboBox2.TabIndex = 48;
      this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox3.FormattingEnabled = true;
      this.comboBox3.Items.AddRange(new object[2]
      {
        (object) "Yes",
        (object) "No"
      });
      this.comboBox3.Location = new Point(332, 105);
      this.comboBox3.Name = "comboBox3";
      this.comboBox3.Size = new Size(121, 21);
      this.comboBox3.TabIndex = 49;
      this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox4.FormattingEnabled = true;
      this.comboBox4.Items.AddRange(new object[4]
      {
        (object) "Default",
        (object) "Limited",
        (object) "Debug",
        (object) "Full"
      });
      this.comboBox4.Location = new Point(332, 147);
      this.comboBox4.Name = "comboBox4";
      this.comboBox4.Size = new Size(121, 21);
      this.comboBox4.TabIndex = 50;
      this.Cancel.Location = new Point(394, 284);
      this.Cancel.Name = "Cancel";
      this.Cancel.Size = new Size(75, 23);
      this.Cancel.TabIndex = 51;
      this.Cancel.Text = "Cancel";
      this.Cancel.UseVisualStyleBackColor = true;
      this.Cancel.Click += new EventHandler(this.Cancel_Click);
      this.Save.Location = new Point(313, 284);
      this.Save.Name = "Save";
      this.Save.Size = new Size(75, 23);
      this.Save.TabIndex = 53;
      this.Save.Text = "Save";
      this.Save.UseVisualStyleBackColor = true;
      this.Save.Click += new EventHandler(this.saveSettings);
      this.comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox5.FormattingEnabled = true;
      this.comboBox5.Items.AddRange(new object[2]
      {
        (object) "Yes",
        (object) "No"
      });
      this.comboBox5.Location = new Point(332, 188);
      this.comboBox5.Name = "comboBox5";
      this.comboBox5.Size = new Size(121, 21);
      this.comboBox5.TabIndex = 55;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(12, 189);
      this.label5.Name = "label5";
      this.label5.Size = new Size(131, 20);
      this.label5.TabIndex = 54;
      this.label5.Text = "Limit apps shown";
      this.comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox6.FormattingEnabled = true;
      this.comboBox6.Items.AddRange(new object[2]
      {
        (object) "Yes",
        (object) "No"
      });
      this.comboBox6.Location = new Point(332, 233);
      this.comboBox6.Name = "comboBox6";
      this.comboBox6.Size = new Size(121, 21);
      this.comboBox6.TabIndex = 57;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(12, 234);
      this.label4.Name = "label4";
      this.label4.Size = new Size(115, 20);
      this.label4.TabIndex = 56;
      this.label4.Text = "Update Details";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(482, 319);
      this.Controls.Add((Control) this.comboBox6);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.comboBox5);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.Save);
      this.Controls.Add((Control) this.Cancel);
      this.Controls.Add((Control) this.comboBox4);
      this.Controls.Add((Control) this.comboBox3);
      this.Controls.Add((Control) this.comboBox2);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.themeInfo);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Settings);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (Settings);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
