using System;

namespace Common
{
    [Serializable]
    public class Timer
    {
        private float _timeRemain;
        private float _timeWait;

        public Timer(float timeWait)
        {
            _timeWait = timeWait;
        }

        public void SetTimeWait(float time)
        {
            _timeWait = time;
        }

        public bool UpdateAndIsCheck(float time)
        {
            Update(time);
            return IsCheck();
        }

        public bool IsCheck()
        {
            return _timeRemain >= _timeWait;
        }

        public void Update(float time)
        {
            _timeRemain += time;
        }

        public void Reset()
        {
            _timeRemain = 0;
        }
    }
}