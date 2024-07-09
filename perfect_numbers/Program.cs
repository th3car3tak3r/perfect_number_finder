using System.Threading;
using perfect_numbers.code.classes;


namespace perfect_numbers{
    class Program{
        static List<int> found_numbers = new List<int>();
        static List<Int64> jobs = new List<Int64>();

        static void Main(string[] args){
            Start();
        }

        static async void Start(){
            //TAKE USER INPUT
            Console.WriteLine("Enter a target number (type 0 for no limit)");
            string input = Console.ReadLine();
            
            int target = Int32.Parse(input);
            int batch_size = 5000;

            //BEGIN MEASURING EXECUTION PERFORMANCE
            Performance_Measure PM = new Performance_Measure();
            PM.Start();

            var tasks = new List<Task>();
            for (int i = 0; i < (target / batch_size); i++) {
                int start = i + (i * batch_size);
                int end = start + batch_size;

                tasks.Add(Task.Run(() => Execute_Job(new { start, end, i })));
            }

            Task.WaitAll(tasks.ToArray()); // Wait for all tasks to finish

            //STOP MEASURING PERFORMANCE
            PM.Stop();

            Console.WriteLine("found ["+ string.Join(",", found_numbers)+"]");

            Console.ReadLine(); //KEEP CONSOLE OPEN
        }

        static void Execute_Job(Object state){
            int start = (int)state.GetType().GetProperty("start").GetValue(state);
            int end = (int)state.GetType().GetProperty("end").GetValue(state);
            int job_no = (int)state.GetType().GetProperty("i").GetValue(state);

            //Console.WriteLine("Job "+job_no+" started. checking "+start+" - "+end);

            Number_Checker nc = new Number_Checker(start, end);
            
            nc.Iterate(
                //OUTPUT FOUND WHEN COMPLETE
                (found) => {
                    foreach (int x in found){
                        found_numbers.Add(x);
                    }
                }
            );

            jobs.RemoveAll(x => x == job_no);
            Console.WriteLine("jobs: "+jobs.Count());

        }
    }
}