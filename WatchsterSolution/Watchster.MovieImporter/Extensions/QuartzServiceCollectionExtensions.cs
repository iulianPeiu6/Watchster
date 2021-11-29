using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using Watchster.MovieImporter.Job;
using Watchster.MovieImporter.Settings;

namespace Watchster.MovieImporter.Extensions
{
    public static class QuartzServiceCollectionExtensions
    {
        public static IServiceCollection AddQuartz(this IServiceCollection services, QuartzSettings quartzSettings)
        {
            services.AddQuartz(quartzConfigurator =>
            {
                quartzConfigurator.UseMicrosoftDependencyInjectionJobFactory();

                const string movieImporterGroupKey = "MovieImporter";
                JobKey jobKey = new("MovieImporterJob", movieImporterGroupKey);
                TriggerKey triggerKey = new("MovieImporterTrigger", movieImporterGroupKey);

                int minutesRerunInterval = quartzSettings.RerunUnitOfMeasureIsInHours ?
                            quartzSettings.RerunInHours * 60 : quartzSettings.RerunInMinutest;

                quartzConfigurator.ScheduleJob<MovieImporterJob>(trigger => trigger.WithIdentity(triggerKey)
                    .WithSimpleSchedule(options => options.WithIntervalInMinutes(minutesRerunInterval)
                        .WithMisfireHandlingInstructionFireNow()
                        .RepeatForever())
                    .StartAt(new DateTimeOffset(quartzSettings.StartAt ?? DateTime.Now)),
                    job => job.WithIdentity(jobKey)
                        .StoreDurably(false));
            });

            services.AddQuartzHostedService(quartzOptions => quartzOptions.WaitForJobsToComplete = true);
            return services;
        }
    }
}
