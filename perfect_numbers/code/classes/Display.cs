

namespace perfect_numbers.code.classes
{
    public class Display
    {
        private List<(int index, string line)> Lines = new List<(int index, string line)>();

        public Display(){
            /*Console.Write("Progress: ");
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\r{i}%");
                Thread.Sleep(100); // Simulate some work
            }
            Console.WriteLine("\nDone!");*/
        }

        public void Update(int index, string newLine){
            if (Lines.Any(lineObj => lineObj.index == index)){
                Lines.Add((index, newLine));
            }else{
                Lines = Lines.Select(
                    lineObj => 
                        (
                            lineObj.index, 
                            (lineObj.index == index) ? newLine : lineObj.line
                        )
                ).ToList();
            }

            Write();
        }

        private void Write(){
            /*Console.Clear();

            foreach (var lineObj in Lines){
                Console.WriteLine(lineObj.line);
            }*/

            
        }

        public void Clear(){
            this.Lines.Clear();
        }
    }
}