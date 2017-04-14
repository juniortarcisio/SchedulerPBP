using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PBPS
{
    public partial class Form1 : Form
    {
        List<Process> list;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Test();
        }

        private void Test()
        {
            List<Process> list = new List<Process>();

            list.Add(new Process('A', 1, 2, 1));
            list.Add(new Process('B', 1, 2, 1));
            list.Add(new Process('C', 1, 2, 1));
            list.Add(new Process('D', 1, 2, 1));
            list.Add(new Process('E', 1, 2, 1));

            gridExecutions.DataSource = list;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            ReadValues();
            ProcessScheduler();
            PrintValues();
        }

        private void ReadValues()
        {
            int at, bt;
            byte priority;

            list = new List<Process>();

            int.TryParse(txtArrivalTime1.Text, out at);
            int.TryParse(txtCPUBurst1.Text, out bt);
            byte.TryParse(txtPriority1.Text, out priority);
            list.Add(new Process('A', at, bt, priority));

            int.TryParse(txtArrivalTime2.Text, out at);
            int.TryParse(txtCPUBurst2.Text, out bt);
            byte.TryParse(txtPriority2.Text, out priority);
            list.Add(new Process('B', at, bt, priority));

            int.TryParse(txtArrivalTime3.Text, out at);
            int.TryParse(txtCPUBurst3.Text, out bt);
            byte.TryParse(txtPriority3.Text, out priority);
            list.Add(new Process('C', at, bt, priority));

            int.TryParse(txtArrivalTime4.Text, out at);
            int.TryParse(txtCPUBurst4.Text, out bt);
            byte.TryParse(txtPriority4.Text, out priority);
            list.Add(new Process('D', at, bt, priority));

            int.TryParse(txtArrivalTime5.Text, out at);
            int.TryParse(txtCPUBurst5.Text, out bt);
            byte.TryParse(txtPriority5.Text, out priority);
            list.Add(new Process('E', at, bt, priority));
        }

        private void ProcessScheduler()
        {
            SchedulerPBP scheduler = new SchedulerPBP();
            scheduler.Process(list);
        }

        private void PrintValues()
        {
            var r = from p in list
                    from e in p.Executions
                    select new { 
                        e.Process.ProcessID,
                        e.StartTime,
                        e.FinishTime
                    };

            r = r.OrderBy(w => w.StartTime);

            gridExecutions.AutoGenerateColumns = true;
            gridExecutions.DataSource = r.ToList();


            txtTurnAroundTime1.Text = list[0].TurnAroundTimeTT.ToString();
            txtTurnAroundTime2.Text = list[1].TurnAroundTimeTT.ToString();
            txtTurnAroundTime3.Text = list[2].TurnAroundTimeTT.ToString();
            txtTurnAroundTime4.Text = list[3].TurnAroundTimeTT.ToString();
            txtTurnAroundTime5.Text = list[4].TurnAroundTimeTT.ToString();

            txtTurnAroundTimeTotal.Text = list.Sum(x => x.TurnAroundTimeTT).ToString();
            txtTurnAroundTimeAverage.Text = (list.Sum(x => x.TurnAroundTimeTT) / 5f).ToString();


            txtResponseTime1.Text = list[0].ResponseTimeRT.ToString();
            txtResponseTime2.Text = list[1].ResponseTimeRT.ToString();
            txtResponseTime3.Text = list[2].ResponseTimeRT.ToString();
            txtResponseTime4.Text = list[3].ResponseTimeRT.ToString();
            txtResponseTime5.Text = list[4].ResponseTimeRT.ToString();

            txtResponseTimeTotal.Text = list.Sum(x => x.ResponseTimeRT).ToString();
            txtResponseTimeAvg.Text = (list.Sum(x => x.ResponseTimeRT) / 5f).ToString();


            txtWaitingTime1.Text = list[0].WaitingTimeWT.ToString();
            txtWaitingTime2.Text = list[1].WaitingTimeWT.ToString();
            txtWaitingTime3.Text = list[2].WaitingTimeWT.ToString();
            txtWaitingTime4.Text = list[3].WaitingTimeWT.ToString();
            txtWaitingTime5.Text = list[4].WaitingTimeWT.ToString();

            txtWaitingTimeTotal.Text = list.Sum(x => x.WaitingTimeWT).ToString();
            txtWaitingTimeAverage.Text = (list.Sum(x => x.WaitingTimeWT) / 5f).ToString();
        }
    }
}
