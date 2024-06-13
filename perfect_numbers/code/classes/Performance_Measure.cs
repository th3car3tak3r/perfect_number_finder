using System;
using System.Diagnostics;


namespace perfect_numbers{
    class Performance_Measure{
        private Stopwatch watch;

        public void Start(){
            this.watch = Stopwatch.StartNew();
        }

        public void Stop(){
            this.watch.Stop();

            // Get the elapsed time in milliseconds
            long elapsedMs = this.watch.ElapsedMilliseconds;

            Console.WriteLine($"Elapsed time: {elapsedMs} ms");
        }
    }
}