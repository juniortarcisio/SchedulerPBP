using System.Collections.Generic;
using System.Linq;

namespace PBPS
{
    public class Process
    {
        public char ProcessID { get; set; }

        public int ArrivalTime { get; set; }
        public int CPUBurst { get; set; }
        public byte Priority { get; set; }



        public List<Execution> Executions = new List<Execution>();

        public int TotalExecuted
        {
            get
            {
                return Executions.Sum(x => x.Burst);
            }
        }

        public int TurnAroundTimeTT
        {
            get
            {
                if (Executions.Count() == 0)
                    return 0;

                var lastExecution = Executions.OrderBy(x => x.FinishTime).Last();

                return lastExecution.FinishTime - ArrivalTime;
            }
        }

        public int ResponseTimeRT
        {
            get
            {
                if (Executions.Count() == 0)
                    return 0;

                var firstExecution = Executions.OrderBy(x => x.StartTime).First();

                return firstExecution.StartTime - ArrivalTime;
            }
        }


        public int WaitingTimeWT
        {
            get
            {
                if (Executions.Count() == 0)
                    return 0;

                //This code sum all spare time between the sequencial executions
                int sumWT = 0;

                for (int i = 0; i < Executions.Count(); i++)
                {
                    if (i == 0)
                        continue;

                    sumWT += Executions[i].StartTime - Executions[i-1].FinishTime;
                }

                return ResponseTimeRT + sumWT;
            }
        }

        public Process(char id, int at, int bt, byte priority)
        {
            ProcessID = id;
            ArrivalTime = at;
            CPUBurst = bt;
            Priority = priority;
        }
    }
}
