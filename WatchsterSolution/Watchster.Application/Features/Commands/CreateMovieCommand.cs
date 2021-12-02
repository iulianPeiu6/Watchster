﻿using MediatR;
using System;
using System.Collections.Generic;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class CreateMovieCommand : IRequest<Guid>
    {
        public int TMDbId { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public string Overview { get; set; }
    }
}