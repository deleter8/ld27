using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Deleter.Tenseconds
{
    public class TimeMonitor
    {
        private readonly int _framesToTrack;
        private readonly Stopwatch _watch;
        private long _lastTickCount;
        private long _tickCount;
        private long _totalTickCount;
        private readonly long _tickCountFrequency;
        private readonly float[] _fpsFrames;
        private long _totalFrames;
        private int _fps;
        private int _averageFps;

        public int AverageFps { get { return _averageFps; }}
        
        public long TotalFrames { get { return _totalFrames; } }

        public int CurrentFps {get { return _fps; }}

        public float DeltaTime { get { return (float)_tickCount / _tickCountFrequency; } }

        public float TotalTime { get { return (float) _totalTickCount/_tickCountFrequency; } }

        public TimeMonitor(int framesToTrack)
        {
            _framesToTrack = framesToTrack;
            _fpsFrames = new float[framesToTrack];
            for (int i = 0; i < _fpsFrames.Length; i++)
            {
                _fpsFrames[i] = 0;
            }

            _totalFrames = 0;
            _fps = 0;
            _lastTickCount = 0;
            _averageFps = 0;
            Program.QueryPerformanceFrequency(out _tickCountFrequency);

            _watch = new Stopwatch();
            _watch.Start();
        }

        public void Update()
        {
            long newCount;
            Program.QueryPerformanceCounter(out newCount);
            _tickCount = (int)(newCount - _lastTickCount);
            _lastTickCount = newCount;
            _totalTickCount += _tickCount;

            _fpsFrames[(_totalFrames++) % _framesToTrack] = (float)_tickCount / _tickCountFrequency;
            _fps = (int)(1 / _fpsFrames.Average());
            _averageFps = (int) ((_totalFrames)/(_watch.Elapsed.TotalMilliseconds/1000));
        }
    }
}
