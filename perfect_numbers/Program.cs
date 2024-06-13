using System.Threading;
using static perfect_numbers.Number_Checker;


namespace perfect_numbers{
    class Program{
        static List<int> found_numbers = new List<int>();
        static List<int> jobs = new List<int>();

        static void Main(string[] args){
            Start();
        }

        static async void Start(){

            //TAKE USER INPUT
            Console.WriteLine("Enter a target number");
            string input = Console.ReadLine();
            
            int target = Int32.Parse(input);
            int batch_size = 5000;

            //BEGIN MEASURING EXECUTION PERFORMANCE
            Performance_Measure PM = new Performance_Measure();
            PM.Start();

            //ASSIGN EACH BATCH TO THREAD POOL
            for(int i = 0; i < (target / batch_size); i++){
                jobs.Add(i);

                int start = i + (i * batch_size);
                int end = start + batch_size;

                ThreadPool.QueueUserWorkItem(Execute_Job, new {start, end, i});
            }

            //WAIT FOR JOBS
            while(jobs.Count > 0){}

            //STOP MEASURING PERFORMANCE
            PM.Stop();

            Console.WriteLine("found ["+ string.Join(",", found_numbers)+"]");
        }

        static void Execute_Job(Object state){
            int start = (int)state.GetType().GetProperty("start").GetValue(state);
            int end = (int)state.GetType().GetProperty("end").GetValue(state);
            int job_no = (int)state.GetType().GetProperty("i").GetValue(state);

            Console.WriteLine("Job "+job_no+" started. start_number: "+start);
            Number_Checker nc = new Number_Checker(start, end);
            
            nc.Iterate((found) => {
                foreach (int x in found){
                    found_numbers.Add(x);
                }
            });

            jobs.RemoveAll(x => x == job_no);

        }
    }
}