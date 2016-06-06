using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace GameEngine.Core
{
    public class AudioManager : IDisposable
    {
        private WaveOut m_OutputDevice;
        private MixingSampleProvider m_Mixer;

        public AudioManager(int sampleRate, int channelCount)
        {
            m_OutputDevice = new WaveOut();

            m_Mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            m_Mixer.ReadFully = true;
            m_Mixer.RemoveAllMixerInputs();

            m_OutputDevice.Init(m_Mixer);
            m_OutputDevice.Play();
        }

        public void Dispose()
        {
            m_OutputDevice.Stop();
            m_OutputDevice.Dispose();
        }

        public void PlayAudio(Audio audio)
        {
            m_Mixer.AddMixerInput(ConvertToRightChannelCount(audio));
        }

        public void StopAudio(Audio audio)
        {
            m_Mixer.RemoveMixerInput(ConvertToRightChannelCount(audio));
        }

        public void SetVolume(float volume)
        {
            m_OutputDevice.Volume = volume;
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == m_Mixer.WaveFormat.Channels)
            {
                return input;
            }

            if (input.WaveFormat.Channels == 1 && m_Mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }

            Debug.LogError("No such channel conversion currently exists.");
            return input;
        }
    }

}
