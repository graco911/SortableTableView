namespace App1
{
    public class CarProducer
    {
        public CarProducer(int logoRes, string name)
        {
            Logo = logoRes;
            Name = name;
        }

        public int Logo { get; private set; }

        public string Name { get; private set; }
    }
}