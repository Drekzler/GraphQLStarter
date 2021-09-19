using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Description("Represents any executable command");

            descriptor.Field(x => x.Platform)
                .ResolveWith<Resolver>(x => x.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform to which the command belongs");

        }

        private class Resolver
        {
            public Platform GetPlatform(Command command, [ScopedService] AppDbContext context)
            {
                return context.Platforms.Where(x => x.Id == command.PlatformId).SingleOrDefault();
            }

        }
    }
}
