using System;
using CSharpUtilityExtensions.Extensions;

namespace CSharpUtilityExtensions.Classes
{
    public class Timer
    {
        private double _time;
        private double _counter;

        public double counter { get => _counter; private set => _counter = Math.Min(Math.Max(_counter, 0), time); }

        public bool isFinished { get; private set; }

        public double time
        {
            get => _time;

            set => _time = Math.Max(value, 0);
        }

        public Timer()
        {
        }

        public Timer(double time) : this()
        {
            this._time = Math.Max(time, 0);
            counter = 0;
            isFinished = false;
        }

        public bool Update(double step)
        {
            if (isFinished)
            {
                counter = time;
                return true;
            }
            counter += step;
            if (counter >= time)
            {
                isFinished = true;
                counter = time;
            }
            else
                isFinished = false;

            return isFinished;
        }

        public bool Reverse(double step)
        {
            if (isFinished)
            {
                isFinished = false;
            }

            counter -= step;
            counter = Math.Max(counter, 0f);

            return counter.CloseTo(0f);
        }

        public void Reset()
        {
            counter = 0f;
            isFinished = false;
        }

        public void Finish()
        {
            counter = time;
            isFinished = true;
        }
    }
}