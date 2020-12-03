﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class CreateDBCommand : IRequest<IRemsDbContext>
    {
        public string FileName { get; set; }        
    }

    public class CreateDBCommandHandler : IRequestHandler<CreateDBCommand, IRemsDbContext>
    {
        private readonly IRemsDbFactory _factory;

        public CreateDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<IRemsDbContext> Handle(CreateDBCommand request, CancellationToken cancellationToken) 
            => Task.Run(() => Handler(request));

        private IRemsDbContext Handler(CreateDBCommand request)
        {
            File.Delete(request.FileName);
            var context = _factory.Create(request.FileName);

            return context;
        }
    }
}
