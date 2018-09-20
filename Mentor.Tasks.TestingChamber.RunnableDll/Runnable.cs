namespace Mentor.Tasks.TestingChamber.RunnableDll
{
    public class Runnable : IRunnable
    {
        public int Run(int input)
        {
            return input;
        }
    }
    public interface IRunnable
    {
        int Run(int input);
    }
}
