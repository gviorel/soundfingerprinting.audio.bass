﻿namespace SoundFingerprinting.Audio.Bass
{
    using System;
    using System.Collections.Generic;

    using Un4seen.Bass;
    using Un4seen.Bass.AddOn.Tags;

    internal interface IBassServiceProxy : IDisposable
    {
        void RegisterBass(string email, string registrationKey);

        bool BassLoadMe(string targetPath);

        bool BassMixLoadMe(string targetPath);

        int GetVersion();

        int GetMixerVersion();

        IDictionary<int, string> PluginLoadDirectory(string path);

        bool Init(int deviceNumber, int sampleRate, BASSInit flags);

        bool SetConfig(BASSConfig config, int value);

        bool SetConfig(BASSConfig config, bool value);

        bool RecordInit(int deviceNumber);

        string GetLastError();

        int GetRecordingDevice();

        int CreateStream(string pathToAudioFile, BASSFlag flags);

        int CreateStreamFromUrl(string urlToResource, BASSFlag flags);

        int StartRecording(int sampleRate, int numberOfChannels, BASSFlag flags);

        bool StartPlaying(int stream);

        int CreateMixerStream(int sampleRate, int channels, BASSFlag flags);

        bool CombineMixerStreams(int mixerStream, int stream, BASSFlag flags);

        bool ChannelSetPosition(int stream, double seekToSecond);

        bool ChannelSetAttribute(int stream, BASSAttribute attribute, float value);

        int ChannelGetData(int stream, float[] buffer, int lengthInBytes);

        double ChannelGetLengthInSeconds(int stream);

        bool FreeStream(int stream);

        bool PluginFree(int number);

        bool BassFree();

        TAG_INFO GetTagsFromFile(string pathToFile);
    }
}
