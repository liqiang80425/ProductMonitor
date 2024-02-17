using ProductMonitor.Models;
using ProductMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// 视图模型构造函数
        /// </summary>
        public MainWindowVM()
        {
            #region 初始化环境监控数据
            EnviromentList = new List<EnviromentModel>();

            EnviromentList.Add(new EnviromentModel { EnItemName = "光照(Lux)", EnItemValue = 123 });
            EnviromentList.Add(new EnviromentModel { EnItemName = "噪音(db)", EnItemValue = 55 });
            EnviromentList.Add(new EnviromentModel { EnItemName = "温度(℃)", EnItemValue = 80 });
            EnviromentList.Add(new EnviromentModel { EnItemName = "湿度(%)", EnItemValue = 43 });
            EnviromentList.Add(new EnviromentModel { EnItemName = "PM2.5(m³)", EnItemValue = 20 });
            EnviromentList.Add(new EnviromentModel { EnItemName = "硫化氢(PPM)", EnItemValue = 15 });
            EnviromentList.Add(new EnviromentModel { EnItemName = "氮气(PPM)", EnItemValue = 18 });
            #endregion

            #region 初始化报警列表

            AlarmList = new List<AlarmModel>();
            AlarmList.Add(new AlarmModel { Num = "01", Msg = "设备温度过高", Time = "2023-11-23 18:34:56", Duration = 7 });
            AlarmList.Add(new AlarmModel { Num = "02", Msg = "车间温度过高", Time = "2023-12-08 20:40:59", Duration = 10 });
            AlarmList.Add(new AlarmModel { Num = "03", Msg = "设备转速过快", Time = "2024-01-05 12:24:34", Duration = 12 });
            AlarmList.Add(new AlarmModel { Num = "04", Msg = "设备气压偏低", Time = "2024-02-04 19:58:00", Duration = 90 });
            #endregion

            #region 初始化设备监控
            DeviceList = new List<DeviceModel>();
            DeviceList.Add(new DeviceModel { DeviceItem = "电能(Kw.h)", Value = 60.8 });
            DeviceList.Add(new DeviceModel { DeviceItem = "电压(V)", Value = 390 });
            DeviceList.Add(new DeviceModel { DeviceItem = "电流(A)", Value = 5 });
            DeviceList.Add(new DeviceModel { DeviceItem = "压差(kpa)", Value = 13 });
            DeviceList.Add(new DeviceModel { DeviceItem = "温度(℃)", Value = 36 });
            DeviceList.Add(new DeviceModel { DeviceItem = "振动(mm/s)", Value = 4.1 });
            DeviceList.Add(new DeviceModel { DeviceItem = "转速(r/min)", Value = 2600 });
            DeviceList.Add(new DeviceModel { DeviceItem = "气压(kpa)", Value = 0.5 });
            #endregion

            #region 初始化雷达数据 
            RaderList = new List<RaderModel>();
            RaderList.Add(new RaderModel { ItemName = "排烟风机", Value = 90 });
            RaderList.Add(new RaderModel { ItemName = "客梯", Value = 30.00 });
            RaderList.Add(new RaderModel { ItemName = "供水机", Value = 34.89 });
            RaderList.Add(new RaderModel { ItemName = "喷淋水泵", Value = 69.59 });
            RaderList.Add(new RaderModel { ItemName = "稳压设备", Value = 20 });

            #endregion

            #region 初始化人员缺岗信息
            StuffOutWorkList = new List<StuffOutWorkModel>();
            StuffOutWorkList.Add(new StuffOutWorkModel { StuffName = "张晓婷", Position = "技术员", OutWorkCount = 123 });
            StuffOutWorkList.Add(new StuffOutWorkModel { StuffName = "李晓", Position = "操作员", OutWorkCount = 23 });
            StuffOutWorkList.Add(new StuffOutWorkModel { StuffName = "王克俭", Position = "技术员", OutWorkCount = 134 });
            StuffOutWorkList.Add(new StuffOutWorkModel { StuffName = "陈家栋", Position = "统计员", OutWorkCount = 143 });
            StuffOutWorkList.Add(new StuffOutWorkModel { StuffName = "杨过", Position = "技术员", OutWorkCount = 12 });

            #endregion

            #region 初始化车间列表 
            WorkShopList = new List<WorkShopModel>();
            WorkShopList.Add(new WorkShopModel { WorkShopName = "贴片车间", WorkingCount = 32, WaitCount = 8, WrongCount = 4, StopCount = 0 });
            WorkShopList.Add(new WorkShopModel { WorkShopName = "封装车间", WorkingCount = 20, WaitCount = 8, WrongCount = 4, StopCount = 0 });
            WorkShopList.Add(new WorkShopModel { WorkShopName = "焊接车间", WorkingCount = 68, WaitCount = 8, WrongCount = 4, StopCount = 0 });
            WorkShopList.Add(new WorkShopModel { WorkShopName = "贴片车间", WorkingCount = 68, WaitCount = 8, WrongCount = 4, StopCount = 0 });

            #endregion
        }

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

        #region 时间 日期 
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
        #endregion

        #region 计数
        /// <summary>
        /// 机台总数
        /// </summary>
        private string _MachineCount = "02981";

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
        #endregion

        #region 环境监控数据
        /// <summary>
        /// 环境监控数据
        /// </summary>
        private List<EnviromentModel> _EnviromentList;

        /// <summary>
        /// 环境监控数据
        /// </summary>
        public List<EnviromentModel> EnviromentList
        {
            get { return _EnviromentList; }
            set
            {
                _EnviromentList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EnviromentList"));
                }
            }
        }

        #endregion

        #region 报警属性

        /// <summary>
        /// 报警集合
        /// </summary>
        private List<AlarmModel> _AlarmList;

        /// <summary>
        /// 报警集合
        /// </summary>
        public List<AlarmModel> AlarmList
        {
            get { return _AlarmList; }
            set
            {
                _AlarmList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AlarmList"));
                }
            }
        }
        #endregion

        #region 设备集合属性
        /// <summary>
        /// 设备集合
        /// </summary>
        private List<DeviceModel> _DeviceList;

        /// <summary>
        /// 设备集合
        /// </summary>
        public List<DeviceModel> DeviceList
        {
            get { return _DeviceList; }
            set
            {
                _DeviceList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DeviceList"));
                }
            }
        }
        #endregion


        #region 雷达数据属性
        /// <summary>
        /// 雷达
        /// </summary>
        private List<RaderModel> _RaderList;

        /// <summary>
        /// 雷达
        /// </summary>
        public List<RaderModel> RaderList
        {
            get { return _RaderList; }
            set
            {
                _RaderList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RaderList"));
                }
            }
        }

        #endregion

        #region 缺岗员工属性

        /// <summary>
        /// 缺岗员工
        /// </summary>
        private List<StuffOutWorkModel> _StuffOutWorkList;

        /// <summary>
        /// 缺岗员工
        /// </summary>
        public List<StuffOutWorkModel> StuffOutWorkList
        {
            get { return _StuffOutWorkList; }
            set
            {
                _StuffOutWorkList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StuffOutWorkList"));
                }
            }
        }
        #endregion

        #region 车间属性
        /// <summary>
        /// 车间
        /// </summary>
        private List<WorkShopModel> _WorkShopList;

        /// <summary>
        /// 车间
        /// </summary>
        public List<WorkShopModel> WorkShopList
        {
            get { return _WorkShopList; }
            set { _WorkShopList = value; }
        }

        #endregion

    }
}
