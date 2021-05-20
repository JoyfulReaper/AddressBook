using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using AddressBookDataAccess.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AddressBookMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IAddressRepository>(r => new AddressRepository(
                Configuration.GetConnectionString("AddressBook"), new SqliteDataAccess()
                ));

            // To fix later
            // First arg to be replaced

            SQLiteCommand.Execute(@"CREATE TABLE IF NOT EXISTS [People] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [FirstName] text NOT NULL
, [LastName] text NOT NULL
); CREATE UNIQUE INDEX[People_sqlite_autoindex_People_1] ON[People]([Id] ASC);

CREATE TABLE [PhoneNumbers] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [PersonId] bigint NOT NULL
, [Number] bigint NOT NULL
, [IsPrimary] bigint NOT NULL
, CONSTRAINT [FK_PhoneNumbers_0_0] FOREIGN KEY ([PersonId]) REFERENCES [People] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE UNIQUE INDEX [PhoneNumbers_sqlite_autoindex_PhoneNumbers_1] ON [PhoneNumbers] ([Id] ASC);

CREATE TABLE [EmailAddresses] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [PersonId] bigint NOT NULL
, [EmailAddress] text NOT NULL
, [IsPrimary] bigint NOT NULL
, CONSTRAINT [FK_EmailAddresses_0_0] FOREIGN KEY ([PersonId]) REFERENCES [People] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE UNIQUE INDEX [EmailAddresses_sqlite_autoindex_EmailAddresses_1] ON [EmailAddresses] ([Id] ASC);

CREATE TABLE [Addresses] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [PersonId] bigint NOT NULL
, [StreetAddress] text NOT NULL
, [City] text NOT NULL
, [Suburb] text NOT NULL
, [State] text NOT NULL
, [PostCode] text NOT NULL
, [IsMailAddress] bigint NOT NULL
, [IsPrimary] bigint NOT NULL
, CONSTRAINT [FK_Addresses_0_0] FOREIGN KEY ([PersonId]) REFERENCES [People] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE UNIQUE INDEX [Addresses_sqlite_autoindex_Addresses_1] ON [Addresses] ([Id] ASC);", SQLiteExecuteType.NonQuery, Configuration.GetConnectionString("AddressBook"));
}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
