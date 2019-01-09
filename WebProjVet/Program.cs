using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebProjVet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            var minhaCultura = new CultureInfo("pt-BR");
            minhaCultura.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            minhaCultura.DateTimeFormat.ShortTimePattern = "HH:mm";
            minhaCultura.NumberFormat.NumberDecimalDigits = 2;
            minhaCultura.NumberFormat.NumberGroupSeparator = "_";
            minhaCultura.NumberFormat.NumberDecimalSeparator = ",";

            //System.Console.WriteLine(string.Format(minhaCultura, "{0:N}", 43239.11));

            //string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", 43239.11));

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
