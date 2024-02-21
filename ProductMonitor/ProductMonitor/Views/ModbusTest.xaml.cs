using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductMonitor.Views
{
    /// <summary>
    /// ModbusTest.xaml 的交互逻辑
    /// </summary>
    public partial class ModbusTest : Window
    {
        public ModbusTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成校验码
        /// </summary>
        /// <param name="value"></param>
        /// <param name="poly"></param>
        /// <param name="crcInit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private List<byte> CRC16(List<byte> value, ushort poly = 0xA001, ushort crcInit = 0xFFFF)
        {
            if (value == null || !value.Any())
            {
                throw new ArgumentException("");
            }

            //运算
            ushort crc = crcInit;
            for (int i = 0; i < value.Count; i++)
            {
                crc = (ushort)(crc ^ (value[i]));
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ poly) : (ushort)(crc >> 1);
                }
            }

            byte hi = (byte)((crc & 0xFF00) >> 8);//高位置
            byte lo = (byte)(crc & 0x00FF);//低位置

            List<byte> buffer = new List<byte>();
            buffer.AddRange(value);
            buffer.Add(lo);
            buffer.Add(hi);

            return buffer;
        }

        /// <summary>
        /// 读线圈状态(功能码01)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadCoilsStatus(object sender, RoutedEventArgs e)
        {
            //1、自己组装报文 了解 面试
            //2、通过modbus组件调用  用

            #region 自己组装报文 了解 面试

            ////1、组装发送报文
            ////2、发送(创建串口 打开串口 写)
            ////3、 解析接收的数据
            //ushort startAddr = 0;//线圈起始地址
            //ushort readLen = 10;//长度

            ////组装请求报文
            //List<byte> command = new List<byte>();
            //command.Add(0x01);//1号地址
            //command.Add(0x01);//功能码  读线圈状态

            ////起始地址
            //command.Add(BitConverter.GetBytes(startAddr)[1]);//起始地址高位
            //command.Add(BitConverter.GetBytes(startAddr)[0]);//起始地址低位

            ////读取数量
            //command.Add(BitConverter.GetBytes(readLen)[1]);//读取数量高位
            //command.Add(BitConverter.GetBytes(readLen)[0]);//读取数量低位

            ////CRC
            //command = CRC16(command);

            ////发送 Rtu（串口） SerialPort
            //using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            //{
            //    serialPort.Open();//打开串口

            //    serialPort.Write(command.ToArray(), 0, command.Count());

            //    //接收响应报文并解析
            //    byte[] respBytes = new byte[serialPort.BytesToRead];
            //    serialPort.Read(respBytes, 0, respBytes.Length);//丢数据包 线程

            //    //校验 校验位
            //    List<byte> respList = new List<byte>(respBytes);// respBytes.ToList();
            //    respList.RemoveRange(0, 3);
            //    respList.RemoveRange(respList.Count - 2, 2);

            //    respList.Reverse();//反转

            //    var respStrList = respList.Select(m => Convert.ToString(m, 2)).ToList();

            //    var result = "";
            //    foreach (string item in respStrList)
            //    {
            //        result += item.PadLeft(8, '0');
            //    }

            //    //字符串反转
            //    result = new string(result.ToArray().Reverse<char>().ToArray());
            //    result = result.Length > readLen ? result.Substring(0, readLen) : result;

            //    MessageBox.Show(result);
            //}
            #endregion

            #region 通过modbus组件调用读取
            ushort startAddr = 0;//线圈起始地址
            ushort readLen = 10;//长度

            using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            {
                serialPort.Open();
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);
                bool[] result = master.ReadCoils(1, startAddr, readLen);
            }
            #endregion
        }

        /// <summary>
        /// 读保持寄存器 功能码03
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadHoldRegiter(object sender, RoutedEventArgs e)
        {
            //1、自己组装报文 了解 面试
            //2、通过modbus组件调用  用

            #region 自己组装报文 了解 面试
            //int startAddr = 0;
            //int readLen = 10;

            ////请求
            //List<byte> command = new List<byte>();
            //command.Add(0x01);//1号从站
            //command.Add(0x03);//功能码  读保持型寄存器

            ////起始地址
            //command.Add(BitConverter.GetBytes(startAddr)[1]);//起始地址高位
            //command.Add(BitConverter.GetBytes(startAddr)[0]);//起始地址低位

            ////读取数量
            //command.Add(BitConverter.GetBytes(readLen)[1]);//起始地址高位
            //command.Add(BitConverter.GetBytes(readLen)[0]);//起始地址低位

            ////CRC
            //command = CRC16(command);

            ////发送 Rtu（串口） SerialPort
            //using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            //{
            //    serialPort.Open();

            //    serialPort.Write(command.ToArray(), 0, command.Count());

            //    //接收响应报文并解析
            //    byte[] respBytes = new byte[serialPort.BytesToRead];
            //    serialPort.Read(respBytes, 0, respBytes.Length);

            //    //校验 校验位
            //    List<byte> respList = new List<byte>(respBytes);// respBytes.ToList();
            //    respList.RemoveRange(0, 3);
            //    respList.RemoveRange(respList.Count - 2, 2);


            //    ////单精度
            //    //byte[] data = new byte[2];
            //    //for (int i = 0; i < readLen; i++)
            //    //{
            //    //    data[0] = respList[i * 2 + 1];
            //    //    data[1] = respList[i * 2];

            //    //    //根据两个字节转成实际数字
            //    //    var result = BitConverter.ToUInt16(data);
            //    //}

            //    //float
            //    byte[] data = new byte[4];
            //    for (int i = 0; i < readLen / 2; i++)
            //    {
            //        data[0] = respList[i * 4 + 3];
            //        data[1] = respList[i * 4 + 2];
            //        data[2] = respList[i * 4 + 1];
            //        data[3] = respList[i * 4];

            //        //根据两个字节转成实际数字
            //        var result = BitConverter.ToSingle(data);
            //
            //}
            #endregion

            #region 通过modbus组件调用  用
            ushort startAddr = 0;
            ushort readLen = 10;
            using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            {
                serialPort.Open();
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);
                ushort[] result = master.ReadHoldingRegisters(1, startAddr, readLen);
            }
            #endregion
        }

        /// <summary>
        /// 写保持寄存器 一个值 功能码16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWriteHoldRegiter(object sender, RoutedEventArgs e)
        {
            //单精度 一个值占用1个寄存器
            //float  一个值占用2个寄存器
            #region 自己组装报文 了解 面试
            //int startAddr = 3;
            //int writeLen = 2;//寄存器个数
            //float value = 84.5f;

            ////请求
            //List<byte> command = new List<byte>();
            //command.Add(0x01);//1号从站
            //command.Add(0x10);//功能码  写多个保持型寄存器

            ////写入地址
            //command.Add(BitConverter.GetBytes(startAddr)[1]);//起始地址高位
            //command.Add(BitConverter.GetBytes(startAddr)[0]);//起始地址低位

            ////写入数量
            //command.Add(BitConverter.GetBytes(writeLen)[1]);//寄存器数量高8位
            //command.Add(BitConverter.GetBytes(writeLen)[0]);//寄存器数量低8位

            ////第一个
            //List<byte> valueBytesList = new List<byte>(BitConverter.GetBytes(value));
            //valueBytesList.Reverse();
            //command.Add((byte)valueBytesList.Count);
            //command.AddRange(valueBytesList);

            ////CRC
            //command = CRC16(command);

            ////发送 Rtu（串口） SerialPort
            //using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            //{
            //    serialPort.Open();

            //    serialPort.Write(command.ToArray(), 0, command.Count());
            //}
            #endregion

            #region  通过modbus组件调用  用
            ushort startAddr = 6;
            using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            {
                serialPort.Open();
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);
                ushort value = 97;
                master.WriteSingleRegister(1, startAddr, value);//单个寄存器 相当于功能码06
            }
            #endregion

        }

        /// <summary>
        /// 写保持寄存器 多个值 功能码16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWriteHoldRegiterMul(object sender, RoutedEventArgs e)
        {
            #region 自己组装报文 了解 面试
            //int startAddr = 6;
            //int writeLen = 4;//寄存器个数
            //float[] values = new float[] { 123.7f, 560.9f };

            ////请求
            //List<byte> command = new List<byte>();
            //command.Add(0x01);//1号从站
            //command.Add(0x10);//功能码  写多个保持型寄存器

            ////写入地址
            //command.Add(BitConverter.GetBytes(startAddr)[1]);//起始地址高位
            //command.Add(BitConverter.GetBytes(startAddr)[0]);//起始地址低位

            ////写入数量
            //command.Add(BitConverter.GetBytes(writeLen)[1]);//起始地址高位
            //command.Add(BitConverter.GetBytes(writeLen)[0]);//起始地址低位

            //List<byte> valuesBytesList = new List<byte>();
            //for (int i = 0; i < values.Length; i++)
            //{
            //    var tempList = new List<byte>(BitConverter.GetBytes(values[i]));
            //    tempList.Reverse();
            //    valuesBytesList.AddRange(tempList);
            //}

            //command.Add((byte)valuesBytesList.Count);
            //command.AddRange(valuesBytesList);

            ////CRC
            //command = CRC16(command);

            ////发送 Rtu（串口） SerialPort
            //using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            //{
            //    serialPort.Open();

            //    serialPort.Write(command.ToArray(), 0, command.Count());
            //}
            #endregion

            #region  通过modbus组件调用  用
            ushort startAddr = 1;
            using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
            {
                serialPort.Open();
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);
                ushort[] values = new ushort[] { 123, 560 };
                master.WriteMultipleRegisters(1, startAddr, values);//单个寄存器  功能编码16
            }
            #endregion
        }
    }
}
