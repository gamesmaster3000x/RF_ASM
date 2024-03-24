namespace RedFoxVM.Components
{
    internal class Keyboard
    {
        private readonly byte lane;
        private readonly byte register;
        private readonly byte flagIn;
        private readonly byte flagOut;

        private Register dataRegister = new Register(Computer.DataWidth);

        public Keyboard(byte lane)
        {
            this.lane = lane;
            register = (byte)(64 + lane);
            flagIn = (byte)(128 + lane);
            flagOut = (byte)(192 + lane);
        }

        public void Clock()
        {
            if (Computer.processor.flags[flagOut])
            {
                Computer.processor.flags[flagOut] = false;
            }
            if (Console.KeyAvailable)
            {
                dataRegister.Byte = Convert.ToByte(Console.ReadKey());
                Computer.processor.flags[flagOut] = true;
            }
        }
    }
}
