namespace RedFoxVM.Components
{
    internal class Stack
    {
        private Word[] data;
        private int pointer = 0;

        public Stack(int size)
        {
            data = new Word[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = new Word(Computer.DataWidth);
            }
        }

        public void Push(Word word)
        {
            data[pointer++] = new Word((word).Bytes);
        }

        public Word Pop()
        {
            return new Word(data[--pointer].Bytes);
        }

        public Word Peek()
        {
            return new Word(data[pointer - 1].Bytes);
        }
    }
}
