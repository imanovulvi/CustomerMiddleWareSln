using System.Net.Http;

namespace ConsoleApp1
{
    public delegate void RequestDelegate(string context);

    public interface IMiddleWare 
    {
        public void Invoke(string context, RequestDelegate next);
    }

    public class TestMW1 : IMiddleWare
    {
        public void Invoke(string context, RequestDelegate next)
        {
            Console.WriteLine("1");
            next.Invoke("next2");
            int a = 1;
        }
    }
    public class TestMW2 : IMiddleWare
    {
        public void Invoke(string context, RequestDelegate next)
        {
            Console.WriteLine("2");
            next.Invoke(" next3");
            int a = 1;
        }
    }

    public class TestMW3 : IMiddleWare
    {
        public void Invoke(string context, RequestDelegate next)
        {
            Console.WriteLine("3");
            next("next");
            int a = 5 + 5;
        }
    }

    public class WebApp
    {
        int count = 0;
        List<IMiddleWare> list=new List<IMiddleWare>();

        public void Next(string context) 
        {
            count++;
            if (count<list.Count)
            {
                list[count].Invoke("", next);
            }
        
        }
        RequestDelegate next;
        public void UseMiddleware<T>() where T : class, IMiddleWare, new()
        {

            IMiddleWare m = new T();
            list.Add(m);    
            //next = d;
            //m.Invoke("httpcontext", next);
        }

        public void RunInvocations()
        {
            next = Next; 
            list[0].Invoke("httpcontext", next);

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            WebApp appMW = new WebApp();
            appMW.UseMiddleware<TestMW1>();
            appMW.UseMiddleware<TestMW2>();
            appMW.UseMiddleware<TestMW3>();
            appMW.RunInvocations();
            Console.ReadLine();
        }
    }
}
