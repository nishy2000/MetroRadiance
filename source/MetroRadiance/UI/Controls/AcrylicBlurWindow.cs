﻿using System;
using System.Windows;
using System.Windows.Media;
using MetroRadiance.Platform;

namespace MetroRadiance.UI.Controls
{
	public class AcrylicBlurWindow : BlurWindow
	{
		private static bool IsAcrylicBlurEnabled { get; }

		static AcrylicBlurWindow()
		{
			IsAcrylicBlurEnabled = WindowsVersion.OSVersion.Build >= 17004;
		}

		protected internal override void HandleThemeChanged()
		{
			if (WindowsTheme.HighContrast.Current)
			{
				this.ToHighContrast();
			}
			else if (!WindowsVersion.Is10)
			{
				this.ToCompatibility();
			}
			else if (!WindowsTheme.Transparency.Current)
			{
				this.ToDefault();
			}
			else if (IsAcrylicBlurEnabled)
			{
				this.ToAcrylicBlur();
			}
			else
			{
				this.ToBlur();
			}
		}

		private void ToAcrylicBlur()
		{
			this.GetColors(out var background, out var foreground);

			background.A = (byte)(background.A * this.BlurOpacity);
			WindowComposition.EnableAcrylicBlur(this, background, this.BordersFlag);
			this.ChangeProperties(Color.FromArgb(1, 0, 0, 0), foreground, Colors.Transparent, new Thickness());
		}
	}
}
