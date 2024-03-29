﻿using CommunityToolkit.Maui;
using InputKit.Shared.Controls;
using UraniumUI;

namespace BirthdayGenie
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddMaterialIconFonts();

                });

            builder.Services.AddCommunityToolkitDialogs();
            return builder.Build();
        }
    }
}
