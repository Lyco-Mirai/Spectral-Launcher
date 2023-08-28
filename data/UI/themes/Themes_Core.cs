using System.Drawing;

namespace Spectral_Launcher.Themes
{
    public abstract class UITheme
    {
        public abstract string ThemeName();
        public abstract string ThemeType();
        public abstract string ThemeCurator();
        public abstract Color Primary_Colour();
        public abstract Color Secondary_Colour();
        public abstract Color Tertiary_Colour();
        public abstract Color Trim_Colour();
        public abstract bool IsInternal();
        public abstract string RootFolder();
        public abstract string ThemeFolder();
        public abstract string Primary_BackgroundPicture();
        public abstract string Secondary_BackgroundPicture();
        public abstract string Tertiary_BackgroundPicture();
        public abstract string Small_LoadingIcon();
        public abstract string Large_LoadingIcon();
    }
    public abstract class Theme_Base: UITheme
    {
    }
}
