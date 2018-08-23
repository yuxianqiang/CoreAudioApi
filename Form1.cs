using System;
using System.Windows.Forms;

namespace CoreAudioApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MMDevice device;

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化设备
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            device = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        }

        /// <summary>
        /// 获取当前音量
        /// </summary>
        public int CurrentVolume
        {
            get => Convert.ToInt32(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100.0f);
        }
        /// <summary>
        /// 设置音量
        /// </summary>
        public int SetVolume
        {
            get => CurrentVolume;
            set
            {
                if (value < 0) device.AudioEndpointVolume.MasterVolumeLevelScalar = 0.0f;
                else if (value > 100) device.AudioEndpointVolume.MasterVolumeLevelScalar = 100.0f;
                else device.AudioEndpointVolume.MasterVolumeLevelScalar = value / 100.0f;
            }
        }
        /// <summary>
        /// 定时器更新并显示当前音量和峰值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "当前音量："+ CurrentVolume;

            //使用Convert.ToInt32转换没有误差
            progressBar1.Value = Convert.ToInt32(device.AudioMeterInformation.MasterPeakValue * 100.0f);
            progressBar2.Value = Convert.ToInt32(device.AudioMeterInformation.PeakValues[0] * 100.0f);
            progressBar3.Value = Convert.ToInt32(device.AudioMeterInformation.PeakValues[1] * 100.0f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //每点击一次，音量加10
            SetVolume += 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //每点击一次，音量减10
            SetVolume -= 10;
        }
    }
}
