﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore_Services
{
    public class BookWorkerService : BackgroundService
    {

        private ILogger<BookWorkerService> _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        private Context _dbContext;

        public BookWorkerService(ILogger<BookWorkerService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            var scope = _scopeFactory.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<Context>();

            await base.StartAsync(cancellationToken);
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);

        }
        public override void Dispose()
        {
            base.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_dbContext == null)
                {
                    var scope = _scopeFactory.CreateScope();
                    _dbContext = scope.ServiceProvider.GetRequiredService<Context>();
                }


                var Avaiblerecords = await _dbContext.Books
                                                     .Where(book => !book.Avaible).ToListAsync();

              


                foreach (var record in Avaiblerecords)
                {
                    

                    record.Avaible = true;
                }

                if (_dbContext.ChangeTracker.HasChanges())
                    await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Worker Runing");



                await Task.Delay(3000, stoppingToken);

            }


        }
    }
}
