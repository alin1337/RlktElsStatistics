using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RlktElsStatistics
{
    class Program
    {
        //Open Sql Connection
        SqlLib sqlLib = new SqlLib();

        //Tasks 
        List<RunTask> runTasks = new List<RunTask>();

        void CreateTasks()
        {
            runTasks.Add(new OnlineStats("Online Players Statistics", RunType.Every_Minute));    //Online player stats
        }

        public void Run()
        {
            CreateTasks();

            while (true)
            {
                //Run tasks
                foreach (var obj in runTasks)
                {
                    if (obj.CanRun())
                        obj.Run();
                }

                //Sleep 1sec
                Thread.Sleep(1000);
            }
        }


        /// <summary>
        /// Main method
        /// </summary>
        static void Main(string[] args)
        {
            //Run The Program
            Program program = new Program();
            program.Run();
        }
    }
}
