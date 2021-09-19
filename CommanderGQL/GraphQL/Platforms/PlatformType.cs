using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GrapQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Description("Represents any software or service that has a command line interface");
            descriptor.Field(x => x.LicenseKey).Ignore();

            descriptor
            .Field(x => x.Commands)
            .ResolveWith<Resolvers>(p => p.GetCommands(default, default))
            .UseDbContext<AppDbContext>()
            .Description("this is the lsit of available commands for this platform");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(x => x.PlatformId == platform.Id);
            }
        }

    }
}