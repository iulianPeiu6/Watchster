using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using Watchster.Application;
using Watchster.DataAccess;
using Watchster.MovieRecommenderNotifier.Job;
using Watchster.MovieRecommenderNotifier.Settings;

namespace Watchster.MovieRecommenderNotifier.Extensions
{
    public static class MovieRecommenderNotifierServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieRecommenderNotifier(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication(configuration);

            services.AddDataAccess(configuration);

            var quartzSettings = configuration.GetSection("QuartzSettings").Get<QuartzSettings>();
            services.AddQuartz(quartzSettings);

            return services;
        }

        private static IServiceCollection AddQuartz(this IServiceCollection services, QuartzSettings quartzSettings)
        {
            services.AddQuartz(quartzConfigurator =>
            {
                quartzConfigurator.UseMicrosoftDependencyInjectionJobFactory();

                const string movieImporterGroupKey = "MovieRecommenderNotifier";
                JobKey jobKey = new("MovieRecommenderNotifierJob", movieImporterGroupKey);
                TriggerKey triggerKey = new("MovieRecommenderNotifierTrigger", movieImporterGroupKey);

                var hoursRerunInterval = quartzSettings.RerunUnitOfMeasureIsInDays ?
                            quartzSettings.RerunInDays * 24 : quartzSettings.RerunInHours;

                var startTime = new DateTimeOffset(quartzSettings.StartAt ?? DateTime.Now);

                quartzConfigurator.ScheduleJob<MovieRecommenderNotifierJob>(trigger => trigger.WithIdentity(triggerKey)
                    .WithSimpleSchedule(options => options.WithIntervalInHours(hoursRerunInterval)
                        .WithMisfireHandlingInstructionFireNow()
                        .RepeatForever())
                    .StartAt(startTime),
                    job => job.WithIdentity(jobKey)
                        .StoreDurably(false));
            });

            services.AddQuartzHostedService(quartzOptions => quartzOptions.WaitForJobsToComplete = true);
            return services;
        }
    }
}
