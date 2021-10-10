using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RlktElsStatistics
{
    public enum RunType
    {
        Every_Second,
        Every_Minute,
        Every_Hour,
    }

    class RunTask
    {
        public RunTask()
        {
            lastRunTick = 0;
        }

        public RunTask(string name_, RunType type_) : this()
        {
            type = type_;
            name = name_;
        }

        public RunType type { get; set; }
        public string name { get; set; }

        virtual public void Run() { }

        //
        public bool CanRun()
        {
            int nInterval = 0;
            switch (type)
            {
                case RunType.Every_Second:
                    nInterval = 1000;
                    break;
                case RunType.Every_Minute:
                    nInterval = 60 * 1000;
                    break;
                case RunType.Every_Hour:
                    nInterval = 60 * 60 * 1000;
                    break;
            }

            if (Environment.TickCount > lastRunTick)
            {
                lastRunTick = Environment.TickCount + nInterval;
                return true;
            }

            return false;
        }


        int lastRunTick { get; set; }
    }

}
