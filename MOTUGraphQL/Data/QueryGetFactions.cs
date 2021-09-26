using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOTUGraphQL.Data
{
    public partial class Query
    {
        // use this to get to graphql endpoint
        // https://localhost:44394/graphql/
        // it takes some time

        public List<Faction> GetFactions(
            string factionName,
            int? baseFaction,
            int? factionID,
            [Service] MotuContext context)
        {
            if (factionName == null
                && baseFaction == null
                && factionID == null)
            {
                return context.Factions.ToList();
            }

            List<Faction> entries = new List<Faction>();

            if (factionID != null)
            {
                entries.AddRange(
                   context.Factions
                   .Where(_ => _.FactionID == factionID).ToList());
            }

            if (baseFaction != null)
            {
                entries.AddRange(
                   context.Factions
                   .Where(_ => _.BaseFaction == baseFaction).ToList());
            }

            if (factionName != null)
            {
                entries.AddRange(
                    context.Factions
                    .Where(_ => _.FactionName.ToLower().Contains(factionName.ToLower())).ToList());
            }
            return entries;
        }
    }
}
