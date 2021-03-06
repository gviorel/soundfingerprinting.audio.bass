namespace SoundFingerprinting.Audio.Bass
{
    public class BassSoundCaptureService : ISoundCaptureService
    {
        private readonly IBassServiceProxy proxy;
        private readonly IBassStreamFactory streamFactory;
        private readonly IBassResampler bassResampler;

        public BassSoundCaptureService()
            : this(
                BassServiceProxy.Instance,
                new BassStreamFactory(BassServiceProxy.Instance),
                new BassResampler(
                    BassServiceProxy.Instance,
                    new BassStreamFactory(BassServiceProxy.Instance),
                    new SamplesAggregator()))
        {
            // no op
        }

        internal BassSoundCaptureService(IBassServiceProxy proxy, IBassStreamFactory streamFactory, IBassResampler bassResampler)
        {
            this.proxy = proxy;
            this.streamFactory = streamFactory;
            this.bassResampler = bassResampler;
        }

        public float[] ReadMonoSamples(int sampleRate, int secondsToRecord)
        {
            const int DefaultResamplerQuality = 4;
            if (!IsRecordingSupported())
            {
                throw new BassException("No recording device could be found un running machine");
            }

            int stream = streamFactory.CreateStreamFromMicrophone(sampleRate);
            return bassResampler.Resample(stream, sampleRate, secondsToRecord, 0, DefaultResamplerQuality, mixerStream => new ContinuousStreamSamplesProvider(new BassSamplesProvider(proxy, mixerStream)));
        }

        private bool IsRecordingSupported()
        {
            return proxy.GetRecordingDevice() != -1;
        }
    }
}