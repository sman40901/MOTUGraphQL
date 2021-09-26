# MOTUGraphQL

Create GraphQL project from the scratch, For our tutorial we will call it MOTUGraphQL.
 
Run these commands in command prompt after you get to your desired directory
this will create a solution : dotnet new sln -n MOTUGraphQL  
this will create a .NET core web project : dotnet new web -n MOTUGraphQL
this will add the project to the solution : dotnet sln add MOTUGraphQL
 
Now open the solution you just created in Visual Studio (you can get a community edition for free) 
 Add these NuGet Packages :
 ![image](https://user-images.githubusercontent.com/53153225/109376792-d2ed2700-787b-11eb-8cdb-90300447fb83.png)

Check if launchSettings.json file looks like this, if it is not then it won't start up in IIS server :
 

     {  
       "iisSettings": {  
         "windowsAuthentication": false,  
         "anonymousAuthentication": true,  
         "iisExpress": {  
           "applicationUrl": "http://localhost:51876",  
           "sslPort": 44396  
         }  
       },  
       "profiles": {  
         "IIS Express": {  
           "commandName": "IISExpress",  
           "launchBrowser": true,  
           "environmentVariables": {  
             "ASPNETCORE_ENVIRONMENT": "Development"  
           }  
         },  
         "MOTUGraphQL": {  
           "commandName": "Project",  
           "dotnetRunMessages": "true",  
           "launchBrowser": true,  
           "applicationUrl": "https://localhost:5001;http://localhost:5000",  
           "environmentVariables": {  
             "ASPNETCORE_ENVIRONMENT": "Development"  
           }  
         }  
       }  
     }  

Contents of appsettings.js , this contains SQL server connection string and context name (developer edition is free to use)
 
     {  
       "Logging": {  
         "LogLevel": {  
           "Default": "Information",  
           "Microsoft": "Warning",  
           "Microsoft.Hosting.Lifetime": "Information"  
         }  
       },  
       "AllowedHosts": "*",  
       "ConnectionStrings": {  
         "MotuContext": "Data Source=(local);Database=motu_characters;User ID=sa;Password=;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"  
       }  
     }  
Create a Faction.cs class which is the reflection of table we are going to use 

     [Table("factionMain")]  
        public class Faction  
        {  
            [Key]  
            [Column("factionId")]  
            public int FactionID { get; set; }  

            [Column("baseFaction")]  
            public int BaseFaction { get; set; }  

            [Column("factionName")]  
            public string FactionName { get; set; }  

            [Column("updated")]  
            public DateTime Updated { get; set; }  

            [Column("inserted")]  
            public DateTime Inserted { get; set; }  
        }  
        
        
 Create a MotuContext.cs inherited from DbContext to create our context
 
     public class MotuContext : DbContext  
        {  
            public MotuContext(DbContextOptions<MotuContext> options)  
                 : base(options)  
            {  
            }  

            public DbSet<Faction> Factions { get; set; }  
        }  
        
        
        
 Create a Query.cs class, which serves as our simple query resolver
 
     public class Query  
        {  
            //public IQueryable<Faction> GetFactions([Service] MotuContext context) =>  
            //    context.Factions;  

            public List<Faction> GetFactions(  
                string factionName,  
                int? baseFaction,  
                int? factionID,  
                [Service] MotuContext context)  
            {  
                if(factionName == null  
                    && baseFaction == null  
                    && factionID == null)  
                {  
                    return context.Factions.ToList();  
                }  

                List<Faction> entries = new List<Faction>();  

                if(factionID != null)  
                {  
                    entries.AddRange(  
                       context.Factions  
                       .Where(_ => _.FactionID == factionID).ToList());  
                }  

                if (baseFaction != null)  
                {  
                    entries.AddRange(  
                       context.Factions  
                       .Where(_ => _.BaseFaction == baseFaction).ToList());  
                }  

                if (factionName != null)  
                {  
                    entries.AddRange(  
                        context.Factions  
                        .Where(_ => _.FactionName.ToLower().Contains(factionName.ToLower())).ToList());  
                }  
                return entries;  
            }  
        }  
        
        
 Now we will go to Startup.cs where we will wire up these things
 
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

               services.AddControllers();  

               services.AddDbContext<MotuContext>(options =>  
                      options.UseSqlServer(Configuration.GetConnectionString("MotuContext")));  


               services  
                   .AddRouting()  
                   .AddGraphQLServer()  
                   .AddQueryType<Query>();  
           }  

           // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
           public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  
           {  
               if (env.IsDevelopment())  
               {  
                   app.UseDeveloperExceptionPage();  
               }  

               app.UseRouting();  

               app.UseEndpoints(endpoints =>  
               {  
                   endpoints.MapGraphQL();  
                   endpoints.MapControllers();  
               });  
           }  

       }  
   
   Now you should be able to run and debug the project in IIS Express Server and run GraphQL query
