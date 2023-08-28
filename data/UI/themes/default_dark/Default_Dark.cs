using System.Drawing;

namespace Spectral_Launcher.Themes
{
    public class Default_Dark: Theme_Base
    {
        public override string ThemeName() => "Default - Dark";
        public override string ThemeType() => "Dark";
        public override string ThemeCurator() => "Spectral Games";
        public override Color Primary_Colour() => ColorTranslator.FromHtml("#181f26");
        public override Color Secondary_Colour() => ColorTranslator.FromHtml("#102438");
        public override Color Tertiary_Colour() => ColorTranslator.FromHtml("#171545");
        public override Color Trim_Colour() => ColorTranslator.FromHtml("#1b5361");
        public override bool IsInternal() => true;
        public override string RootFolder() => "data/UI/themes";
        public override string ThemeFolder() => "default_dark";
        public override string Primary_BackgroundPicture() => "backgroundA.png";
        public override string Secondary_BackgroundPicture() => "backgroundB.png";
        public override string Tertiary_BackgroundPicture() => "backgroundC.png";
        public override string Small_LoadingIcon() => "backgrounda.png";
        public override string Large_LoadingIcon() => "backgrounda.png";
    }
}
