using enums;

namespace Services
{
    public class TimeService
    {
        private int dayCount = 1;
        private Time time = Time.NIGHT;
        
        public void ToggleTimeCycle()
        {
            switch (time)
            {
                case Time.DAY:
                    time = Time.VOTING;
                    break;
                case Time.VOTING:
                    time = Time.NIGHT;
                    break;
                case Time.NIGHT:
                    time = Time.DAY;
                    dayCount++;
                    break;
            }
        }
        
        public int GetDayCount()
        {
            return dayCount;
        }
        
        public Time GetTime()
        {
            return time;
        }
    }
}