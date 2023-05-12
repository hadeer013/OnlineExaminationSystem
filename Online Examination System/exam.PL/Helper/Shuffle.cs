namespace exam.PL.Helper
{
    public class Shuffle
    {
        public static void shuffle(string[] arr)
        {

            Random rand = new Random();

            for (int i = arr.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                string temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }
}
