using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using Watchster.Application;
using Watchster.DataAccess;
using Watchster.MovieImporter.Job;
using Watchster.MovieImporter.Settings;

namespace Watchster.MovieImporter.Extensions
{
    public static class MovieImporterServiceCollectionExtensions
    {

        public static IServiceCollection AddMovieImporter(this IServiceCollection services, IConfiguration configuration)
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

                const string movieImporterGroupKey = "MovieImporter";
                JobKey jobKey = new("MovieImporterJob", movieImporterGroupKey);
                TriggerKey triggerKey = new("MovieImporterTrigger", movieImporterGroupKey);

                int minutesRerunInterval = quartzSettings.RerunUnitOfMeasureIsInHours ?
                            quartzSettings.RerunInHours * 60 : quartzSettings.RerunInMinutest;

                var startTime = new DateTimeOffset(quartzSettings.StartAt ?? DateTime.Now);

                quartzConfigurator.ScheduleJob<MovieImporterJob>(trigger => trigger.WithIdentity(triggerKey)
                    .WithSimpleSchedule(options => options.WithIntervalInMinutes(minutesRerunInterval)
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
