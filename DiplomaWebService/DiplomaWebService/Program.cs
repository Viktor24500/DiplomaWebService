namespace DiplomaWebService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var config = new ConfigurationBuilder()
					 .AddJsonFile("appsettings.json", optional: false)
					 .Build();

			builder.Services.AddDistributedMemoryCache(); // Required for session state
			builder.Services.AddSession(options =>
			{
				double expirationTime = config.GetValue<double>("TokenExpirationTime");
				options.IdleTimeout = TimeSpan.FromMinutes(expirationTime); // Set session timeout
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseSession();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
