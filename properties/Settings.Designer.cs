﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spectral_Launcher.properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string accountName {
            get {
                return ((string)(this["accountName"]));
            }
            set {
                this["accountName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string accountPassword {
            get {
                return ((string)(this["accountPassword"]));
            }
            set {
                this["accountPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Spectral_Launcher.Localization.Language desiredLanguage {
            get {
                return ((global::Spectral_Launcher.Localization.Language)(this["desiredLanguage"]));
            }
            set {
                this["desiredLanguage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Spectral_Launcher.Themes.UITheme desiredUITheme {
            get {
                return ((global::Spectral_Launcher.Themes.UITheme)(this["desiredUITheme"]));
            }
            set {
                this["desiredUITheme"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Spectral_Launcher.Localization.Language defaultLanguage {
            get {
                return ((global::Spectral_Launcher.Localization.Language)(this["defaultLanguage"]));
            }
            set {
                this["defaultLanguage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Spectral_Launcher.Themes.UITheme defaultUITheme {
            get {
                return ((global::Spectral_Launcher.Themes.UITheme)(this["defaultUITheme"]));
            }
            set {
                this["defaultUITheme"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Spectral_Launcher.Localization.Language[] avalibleLanguages {
            get {
                return ((Spectral_Launcher.Localization.Language[])(this["avalibleLanguages"]));
            }
            set {
                this["avalibleLanguages"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Spectral_Launcher.Themes.UITheme[] avalibleUIThemes {
            get {
                return ((Spectral_Launcher.Themes.UITheme[])(this["avalibleUIThemes"]));
            }
            set {
                this["avalibleUIThemes"] = value;
            }
        }
    }
}