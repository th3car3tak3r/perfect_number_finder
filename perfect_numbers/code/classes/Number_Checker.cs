

namespace perfect_numbers{
    class Number_Checker{
        public int start { get; }
        public int end { get; }

        private int display_index = 1000;

        public Number_Checker(int start, int end){
            this.start = start;
            this.end = end;
        }

        public void Iterate(Action<List<int>> callback){
            List<int> found = new List<int>();


            for(int i = start; i <= end; i++){
                int? x = is_perfect(i);

                if (x != null){
                    found.Add(x.Value);
                }
            }

            callback(found);
        }

        private int? is_perfect(int number){

            List<int> divisibles = new List<int>();

            for(int i = 1; i < number; i++){
                if(number % i == 0){
                    divisibles.Add(i);
                }
            }

            int sum = divisibles.Sum();

            return number != 0 && number == sum ? number : null;
        }
    }
}