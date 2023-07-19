namespace PractProj_ASP.Root
{
    public class Logic
    {
        private static Logic instance;

        private Logic()
        {
               
        }
        public static Logic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logic();
                }
                return instance;
            }
        }
    }
}
