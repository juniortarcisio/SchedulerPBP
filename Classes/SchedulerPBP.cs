using System;
using System.Collections.Generic;
using System.Linq;

namespace PBPS
{
    public class SchedulerPBP
    {
        public void Process(List<Process> listProcess)
        {
            int totalTime;
            Process currentProcess;
            Process nextProcess;
            Execution execution;

            totalTime = 0;
            currentProcess = null;

            //I did it with a loop because I'm not finding a better solution at the moment, it's not near of an optimal solution... but it works (I think)
            //The loop condition is, while exists process where total executed haven't reach the total execution yet...
            while (listProcess.Where(x => x.TotalExecuted < x.CPUBurst).Count() > 0)
            {
                //Select process which weren't already full processed and time of execution has already arrived
                var pendentProcess = listProcess.Where(x => x.TotalExecuted < x.CPUBurst
                                                         && x.ArrivalTime <= totalTime);

                //If there's not process running go to next time
                if (pendentProcess.Count() == 0)
                {
                    totalTime++;
                    continue;
                }

                //Get the higher priority process 
                nextProcess = pendentProcess.OrderBy(x => x.Priority).First();

                //If the next priority process is different from the current
                if (currentProcess != null && currentProcess.ProcessID != nextProcess.ProcessID)
                {
                    //finishes the current process execution 
                    execution = currentProcess.Executions.Where(x => !x.Finished).First();
                    execution.Finished = true;
                }

                //Set the next process as the new current process
                currentProcess = nextProcess;

                if (currentProcess.Executions.Where(x => !x.Finished).Count() > 0)
                {
                    execution = currentProcess.Executions.Where(x => !x.Finished).First();
                }
                else
                {
                    execution = new Execution(currentProcess);
                    execution.StartTime = totalTime;
                    currentProcess.Executions.Add(execution);
                }

                execution.Burst++;
                totalTime++;
            }
        }

    }
}
