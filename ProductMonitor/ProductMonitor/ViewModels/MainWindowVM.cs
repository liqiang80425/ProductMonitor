using ProductMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProductMonitor.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 监控用户控件
        /// </summary>
        private UserControl _MonitorUC;

        /// <summary>
        /// 监控用户控件
        /// </summary>
        public UserControl MonitorUC
        {
            get
            {
                if (_MonitorUC == null)
                {
                    _MonitorUC = new MonitorUC();
                }
                return _MonitorUC;
            }
            set
            {
                _MonitorUC = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MonitorUC"));
                }
            }
        }

        /// <summary>
        /// 时间 小时:分钟
        /// </summary>
        public string TimeStr
        {
            get
            {
                return DateTime.Now.ToString("HH:mm");
            }
        }

        /// <summary>
        /// 日期 年-月-日
        /// </summary>
        public string DateStr
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 星期
        /// </summary>
        public string WeekStr
        {
            get
            {
                int index = (int)DateTime.Now.DayOfWeek;

                string[] week = new string[7] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

                return week[index];
            }
        }

        /// <summary>
        /// 机台总数
        /// </summary>
        private string _MachineCount="02981";

        /// <summary>
        /// 机台总数
        /// </summary>
        public string MachineCount
        {
            get { return _MachineCount; }
            set
            {
                _MachineCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MachineCount"));
                }
            }
        }

        /// <summary>
        /// 生产计数
        /// </summary>
        private string _ProductCount = "16403";

        /// <summary>
        /// 生产计数
        /// </summary>
        public string ProductCount
        {
            get { return _ProductCount; }
            set
            {
                _ProductCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ProductCount"));
                }
            }
        }

        /// <summary>
        /// 不良计数
        /// </summary>
        private string _BadCount = "0403";

        /// <summary>
        /// 不良计数
        /// </summary>
        public string BadCount
        {
            get { return _BadCount; }
            set
            {
                _BadCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BadCount"));
                }
            }
        }
    }
}
