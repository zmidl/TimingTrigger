using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace AutoExecuteDemo
{
   /// <summary>
   /// MainWindow.xaml 的交互逻辑
   /// </summary>
   public partial class MainWindow : Window
   {
      private readonly Timer t;

      private int count = 0;

      private List<DateTime> targets = new List<DateTime>();

      public DateTime current;
      public MainWindow()
      {
         InitializeComponent();
         t = new Timer(this.DoWork, null, Timeout.InfiniteTimeSpan, TimeSpan.FromMinutes(1));
      }

      private void Switch()
      {
         var currerntIndex = this.targets.IndexOf(this.current);
         if (++currerntIndex > this.targets.Count - 1) currerntIndex = 0;
         this.current = this.targets[currerntIndex];
      }

      private void DoWork(object param)
      {
         if (DateTime.Now.ToString("hhmm") == this.current.ToString("hhmm"))
         {
            this.Switch();
            this.count++;
            var flag = this.count % 2 == 0 ? "开" : "关";
            this.Dispatcher.Invoke(() => this.Title += $"{ DateTime.Now}:{flag}--");
         }
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         this.targets = new List<DateTime> { this.TimePicker1.Value.Value, this.TimePicker2.Value.Value, this.TimePicker3.Value.Value, this.TimePicker4.Value.Value };
         this.current = this.targets[0];

         this.t.Change(TimeSpan.Zero, TimeSpan.FromSeconds(1));

      }
   }
}
