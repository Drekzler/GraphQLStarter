using CommanderGQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL.Commands
{
    public record AddCommandInput(string HowTo, string CommandLine, int platformId);
}
