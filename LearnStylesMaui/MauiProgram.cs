#if DEBUG
using Microsoft.Maui.DevFlow.Agent;
#endif
using Microsoft.Extensions.Logging;
using LearnStylesMaui.ViewModels;
using LearnStylesMaui.Views;

namespace LearnStylesMaui
{
	/// <summary>
	/// נקודת הכניסה הראשית לאתחול אפליקציית ה-.NET MAUI ורישום תלויות ב-DI Container.
	/// הבנייה מבוצעת בתבנית Fluent Builder מסודרת המפרידה בין רישום גופנים, שירותים, ViewModels, Views וכלי פיתוח.
	/// </summary>
	public static class MauiProgram
	{
		/// <summary>
		/// יוצר ומגדיר את מופע האפליקציה הראשי (<see cref="MauiApp"/>) באמצעות שרשור פעולות Fluent.
		/// </summary>
		/// <returns>מופע מוכן להרצה של <see cref="MauiApp"/>.</returns>
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.RegisterFonts()
				.RegisterServices()
				.RegisterViewModels()
				.RegisterViews()
				.RegisterDevTools();

			return builder.Build();
		}

		/// <summary>
		/// רושם את הגופנים (Fonts) של האפליקציה וכינוייהם לשימוש ב-XAML וב-Styles.
		/// </summary>
		/// <param name="builder">אובייקט ה-<see cref="MauiAppBuilder"/> שעליו מתבצע הרישום.</param>
		/// <returns>אותו מופע <see cref="MauiAppBuilder"/> להמשך שרשור Fluent.</returns>
		public static MauiAppBuilder RegisterFonts(this MauiAppBuilder builder)
		{
			return builder.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		}

		/// <summary>
		/// רושם את שכבת השירותים (Services / Repositories) ב-DI Container של האפליקציה.
		/// מיועד לשירותי נתונים (כגון SQLite), שירותי רשת או שירותי מערכת.
		/// </summary>
		/// <param name="builder">אובייקט ה-<see cref="MauiAppBuilder"/> שעליו מתבצע הרישום.</param>
		/// <returns>אותו מופע <see cref="MauiAppBuilder"/> להמשך שרשור Fluent.</returns>
		public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
		{
			// כאן רושמים שירותי תשתית ונתונים (למשל AddSingleton עבור שירות מסד נתונים)
			return builder;
		}

		/// <summary>
		/// רושם את כל מחלקות ה-ViewModels של האפליקציה ב-DI Container.
		/// הרישום מתבצע כ-<c>Transient</c> כדי שכל כניסה למסך תקבל מופע נקי או מותאם למחזור חיי הדף.
		/// </summary>
		/// <param name="builder">אובייקט ה-<see cref="MauiAppBuilder"/> שעליו מתבצע הרישום.</param>
		/// <returns>אותו מופע <see cref="MauiAppBuilder"/> להמשך שרשור Fluent.</returns>
		public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
		{
			builder.Services.AddTransient<StylesDemoViewModel>();
			builder.Services.AddTransient<ConvertersDemoViewModel>();
			builder.Services.AddTransient<StudentsDashboardViewModel>();
			builder.Services.AddTransient<StudentExerciseViewModel>();

			return builder;
		}

		/// <summary>
		/// רושם את כל המסכים והדפים (Views / Pages) של האפליקציה ב-DI Container,
		/// כדי שמערכת ה-Shell תוכל ליצור אותם ולהזריק לבנאי שלהם את ה-ViewModel המתאים.
		/// </summary>
		/// <param name="builder">אובייקט ה-<see cref="MauiAppBuilder"/> שעליו מתבצע הרישום.</param>
		/// <returns>אותו מופע <see cref="MauiAppBuilder"/> להמשך שרשור Fluent.</returns>
		public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
		{
			builder.Services.AddTransient<StylesDemoPage>();
			builder.Services.AddTransient<ConvertersDemoPage>();
			builder.Services.AddTransient<StudentsDashboardPage>();
			builder.Services.AddTransient<StudentExercisePage>();

			return builder;
		}

		/// <summary>
		/// רושם כלי פיתוח, לוגים (Logging) וסוכן בדיקות זמן-ריצה (DevFlow) בסביבת <c>DEBUG</c> בלבד.
		/// </summary>
		/// <param name="builder">אובייקט ה-<see cref="MauiAppBuilder"/> שעליו מתבצע הרישום.</param>
		/// <returns>אותו מופע <see cref="MauiAppBuilder"/> להמשך שרשור Fluent.</returns>
		public static MauiAppBuilder RegisterDevTools(this MauiAppBuilder builder)
		{
#if DEBUG
			builder.Logging.AddDebug();
		//	builder.AddMauiDevFlowAgent();
#endif
			return builder;
		}
	}
}
