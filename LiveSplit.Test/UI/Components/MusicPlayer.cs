using System;

using NAudio.Wave;

namespace LiveSplit.CupheadMusic.Music
{
    public class MusicPlayer : IDisposable
    {
        private WaveOutEvent output;
        private AudioFileReader audioFile;
        private LoopStream loopStream;
        private float volume = 1.0f;

        public float Volume
        {
            get => volume;
            set
            {
                volume = Math.Max(0f, Math.Min(1f, value));

                try
                {
                    if (audioFile != null)
                        audioFile.Volume = volume;
                }
                catch
                {
                    // Ignore volume setting failures on audio file
                }

                try
                {
                    if (output != null)
                        output.Volume = volume;
                }
                catch
                {
                    // Some output drivers may not support volume property; ignore.
                }
            }
        }

        public string CurrentFile
        {
            get;
            private set;
        }

        public bool IsPlaying
        {
            get
            {
                return output != null &&
                       output.PlaybackState == PlaybackState.Playing;
            }
        }

        public void PlayLooping(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            /*
             * Don't restart the same song if it is already playing.
             */
            if (string.Equals(
                CurrentFile,
                filePath,
                StringComparison.OrdinalIgnoreCase) &&
                IsPlaying)
            {
                return;
            }

            /*
             * Stop anything currently playing.
             */
            Stop();

            try
            {
                audioFile = new AudioFileReader(filePath);

                loopStream = new LoopStream(audioFile);

                output = new WaveOutEvent();

                output.Init(loopStream);

                output.Play();

                CurrentFile = filePath;
            }
            catch
            {
                Stop();
                throw;
            }
        }

        public void Stop()
        {
            if (output != null)
            {
                output.Stop();
                output.Dispose();
                output = null;
            }

            if (loopStream != null)
            {
                loopStream.Dispose();
                loopStream = null;
            }

            /*
             * LoopStream disposes the AudioFileReader.
             */
            audioFile = null;

            CurrentFile = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }

    internal class LoopStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return sourceStream.WaveFormat;
            }
        }

        public override long Length
        {
            get
            {
                return sourceStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return sourceStream.Position;
            }

            set
            {
                sourceStream.Position = value;
            }
        }

        public override int Read(
            byte[] buffer,
            int offset,
            int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(
                    buffer,
                    offset + totalBytesRead,
                    count - totalBytesRead);

                if (bytesRead == 0)
                {
                    sourceStream.Position = 0;
                }
                else
                {
                    totalBytesRead += bytesRead;
                }
            }

            return totalBytesRead;
        }

        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                sourceStream.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}