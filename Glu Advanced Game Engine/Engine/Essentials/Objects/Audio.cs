using System;
using System.Collections.Generic;
using System.Linq;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace GameEngine
{
    public class Audio : IDisposable, ISampleProvider
    {
        private float[] m_OriginalData;
        private float[] m_Data;
        private long m_CurrentPosition;

        private float m_Volume = 1.0f;
        private bool m_IsLooping = false;

        private WaveFormat m_WaveFormat;
        public WaveFormat WaveFormat
        {
            get { return m_WaveFormat; }
        }

        public Audio(string filePath)
        {
            LoadAudio("../../Assets/" + filePath);
        }

        private void LoadAudio(string filePath)
        {
            using (AudioFileReader audioFileReader = new AudioFileReader(filePath))
            {
                //Make sure all audio uses the same samplerate
                WaveToSampleProvider convStream = new WaveToSampleProvider(new MediaFoundationResampler(new SampleToWaveProvider(audioFileReader),
                                                                                                        WaveFormat.CreateIeeeFloatWaveFormat(44100, 2))
                { ResamplerQuality = 60 });


                m_WaveFormat = convStream.WaveFormat;

                List<float> wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                float[] readBuffer = new float[convStream.WaveFormat.SampleRate * convStream.WaveFormat.Channels];

                int samplesRead;
                while ((samplesRead = convStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                {
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                }

                m_OriginalData = wholeFile.ToArray();
                m_Data = m_OriginalData;
            }
        }

        public void Dispose()
        {

        }

        public void SetVolume(float volume)
        {
            //We'll just do this manually otherwise finding a way out of all these conversion classes (see LoadAudio) will be a nightmare.
            //As long as we don't use this to fadeout we'll be fine.
            for (int i = 0; i < m_OriginalData.Length; ++i)
            {
                m_Data[i] = m_OriginalData[i] * volume;
            }
        }

        public void SetLooping(bool looping)
        {
            m_IsLooping = looping;
        }

        public float GetVolume()
        {
            return m_Volume;
        }

        public bool IsLooping()
        {
            return m_IsLooping;
        }

        //ISampleProvider
        public int Read(float[] buffer, int offset, int count)
        {
            long availableSamples = m_Data.Length - m_CurrentPosition;

            //Looping
            if (availableSamples <= 0 && m_IsLooping == true)
            {
                m_CurrentPosition = 0;
                availableSamples = m_Data.Length - m_CurrentPosition;
            }

            long samplesToCopy = Math.Min(availableSamples, count);

            Array.Copy(m_Data, m_CurrentPosition, buffer, offset, samplesToCopy);

            m_CurrentPosition += samplesToCopy;

            return (int)samplesToCopy;
        }
    }

}
