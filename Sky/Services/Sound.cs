using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Sky.Services
{
    class Sound
    {
        private WaveIn waveIn;
        public List<byte> audioBytes = new List<byte>(new byte[] { });
        public void StartRecord()
        {
            try
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.WaveFormat = new WaveFormat(48000, 16, 1);
                waveIn.StartRecording();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void StopRecord()
        {
            waveIn.StopRecording();
        }
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            audioBytes.AddRange(e.Buffer);
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