using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Program
    {
        static void Main(string[] args)
        {
            FileData file = new FileData();
            if (file.Load("./DirectFileTopicDownload.txt"))
            {
                Console.WriteLine("Successful load bitches");
            }
            else
            {
                Console.WriteLine("Failure");
            }
            //
        }
    }
}
