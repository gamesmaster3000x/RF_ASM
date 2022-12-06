namespace RedFoxVM {
    /// <summary>
    /// Entry point class for RF_ASM.
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            Computer computer = new Computer(2, 1024, new byte[] {234,213,1,23,16,45}, 32, 16, 256);
            computer.TriggerClock();
        }
    }
}