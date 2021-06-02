﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdvancedAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource cts = new CancellationTokenSource();

        private void ReportProgress(object sender, ProgressReportModel e)
        {
            dashboardProgress.Value = e.PercentageComplete;
            PrintResults(e.SitesDownloaded);
        }

        private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = await DemoMethods.RunDownloadParallelAsyncV2(progress);
            PrintResults(results);

            watch.Stop();
            var elaspedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elaspedMs}";
        }

        private void cancelOperation_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = DemoMethods.RunDownloadParallelSync();
            PrintResults(results);

            watch.Stop();
            var elaspedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elaspedMs}";
        }

        private async void excuteAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var results = await DemoMethods.RunDownloadAsync(progress, cts.Token);
                PrintResults(results);
            }
            catch (OperationCanceledException)
            {
                resultsWindow.Text = $"The async download was cancelled. {Environment.NewLine}";
            }

            watch.Stop();
            var elaspedMs = watch.ElapsedMilliseconds;

            resultsWindow.Text += $"Total execution time: {elaspedMs}";
        }

        private void PrintResults(List<WebsiteDataModel> result)
        {
            resultsWindow.Text = "";
            foreach (var item in result)
            {
                resultsWindow.Text += $"{item.WebsiteUrl} downloaded: {item.WebsiteData.Length} characters long. {Environment.NewLine}";
            }
        }
    }
}