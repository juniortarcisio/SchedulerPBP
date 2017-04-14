namespace PBPS
{
    public class Execution
    {
        public int StartTime { get; set; }
        public bool Finished;
        public int Burst { get; set; }

        public int FinishTime
        {
            get
            {
                return StartTime + Burst;
            }
        }

        public Process Process;

        public Execution(Process process)
        {
            Finished = false;
            Burst = 0;
            Process = process;
        }

    }
}
