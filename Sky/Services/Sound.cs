using NAudio.Wave;
using Sky.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sky.Services
{
    class Sound
    {
        private WaveIn waveIn;
        private WaveFileWriter writer = null;
        public List<byte> audioBytes = new List<byte>(new byte[] { });
        public void StartRecord()
        {
            var outputFolder = AppDomain.CurrentDomain.BaseDirectory;
            var outputFilePath = Path.Combine(outputFolder, "recorded.wav");
            try
            {
                Console.WriteLine("Start Recording");
                waveIn = new WaveIn();
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += waveIn_RecordingStopped;
                waveIn.WaveFormat = new WaveFormat(48000, 16, 1);
                waveIn.StartRecording();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            audioBytes.AddRange(e.Buffer);
            //writer.Write(e.Buffer, 0, e.BytesRecorded);
        }
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            writer?.Dispose();
            writer = null;
        }
        public void StopRecord()
        {
            waveIn.StopRecording();
        }
        public static void PlayAudio(byte[] content)
        {
            WaveOut waveOut = new WaveOut();
            IWaveProvider provider = new RawSourceWaveStream(
                             new MemoryStream(content), new WaveFormat(48000, 16, 1));
            waveOut.Init(provider);
            waveOut.Play();
        }
    }
}
