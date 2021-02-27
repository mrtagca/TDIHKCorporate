using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(TDIHKCorporate.Startup))]

namespace TDIHKCorporate
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //ConfigureAuth(app);

            // Hangfire sunucu bağlantı
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);


            // Hangfire dashboard
            app.UseHangfireDashboard("/Jobs", new DashboardOptions
            {
                //Authorization = new[] { new HangfireAuthorization() }
            });

            // Hangfire sunucu kullan
            app.UseHangfireServer();



            //haftaiçi hergün 9-10 arası dakikada bir bülten yüklendi mi diye kontrol edecek. "* 14-15 * * 1-5"
            //RecurringJob.AddOrUpdate(() => new JobHelper().NewsletterSend(), "* 9-11 * * 1-5", TimeZoneInfo.Local);

        }
    }
}
