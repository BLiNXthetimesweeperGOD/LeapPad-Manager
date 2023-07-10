// Decompiled with JetBrains decompiler
// Type: LFConnect.Properties.Settings
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LFConnect.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("1")]
    [UserScopedSetting]
    public Decimal videoID
    {
      get => (Decimal) this[nameof (videoID)];
      set => this[nameof (videoID)] = (object) value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("1")]
    [UserScopedSetting]
    public Decimal themeID
    {
      get => (Decimal) this[nameof (themeID)];
      set => this[nameof (themeID)] = (object) value;
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("True")]
    public bool OverwriteExisting
    {
      get => (bool) this[nameof (OverwriteExisting)];
      set => this[nameof (OverwriteExisting)] = (object) value;
    }

    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool AutoInstallMoviePlayer
    {
      get => (bool) this[nameof (AutoInstallMoviePlayer)];
      set => this[nameof (AutoInstallMoviePlayer)] = (object) value;
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("False")]
    public bool AutoInstallMfgTest
    {
      get => (bool) this[nameof (AutoInstallMfgTest)];
      set => this[nameof (AutoInstallMfgTest)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Default")]
    public string OutputLevel
    {
      get => (string) this[nameof (OutputLevel)];
      set => this[nameof (OutputLevel)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool LimitApps
    {
      get => (bool) this[nameof (LimitApps)];
      set => this[nameof (LimitApps)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool updateDetails
    {
      get => (bool) this[nameof (updateDetails)];
      set => this[nameof (updateDetails)] = (object) value;
    }
  }
}
