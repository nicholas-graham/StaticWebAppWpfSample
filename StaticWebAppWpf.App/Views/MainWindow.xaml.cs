﻿using Microsoft.Web.WebView2.Wpf;
using StaticWebAppWpf.App.Messaging;
using StaticWebAppWpf.App.Messaging.Interfaces;
using System.ComponentModel;
using System.Windows;

namespace StaticWebAppWpf.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IGreetMessageContract _greetMessageContract;

        public MainWindow(MainWindowViewModel viewModel, IGreetMessageContract greetMessageContract)
        {
            InitializeComponent();
            DataContext = viewModel;
            _greetMessageContract = greetMessageContract;
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            await Program.Shutdown();
            base.OnClosing(e);
        }

        private async void webView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            // attach our messaging contracts to the WebView after it has initialized. 
            if (e.IsSuccess)
            {
                var webView2 = (WebView2)sender;
                var coreWebView2 = webView2.CoreWebView2;
                if (coreWebView2 != null)
                {
                    // Add any WebView2 messaging contracts here.
                    coreWebView2.AddHostObjectToScript(nameof(GreetMessageContract), _greetMessageContract);
                }
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed, shutting down...");
                await Program.Shutdown();
            }
        }
    }
}