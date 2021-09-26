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

        //use query :
        //    query{motuCharacters
        //    {
        //      faction
        //     name
        //    }
        //    }

        public List<MotuCharacter> GetMotuCharacters(
                      string name,
                      int? faction,
                      int? id,
                      string leader,

                     [Service] MotuContext context)
        {
            if (name == null
                && faction == null
                && id == null
                 && leader == null)
            {
                return context.MotuCharacters.ToList();
            }

            List<MotuCharacter> entries = new List<MotuCharacter>();

            if (faction != null)
            {
                entries.AddRange(
                   context.MotuCharacters
                   .Where(_ => _.Faction == faction).ToList());
            }

            if (id != null)
            {
                entries.AddRange(
                   context.MotuCharacters
                   .Where(_ => _.ID == id).ToList());
            }

            if (name != null)
            {
                entries.AddRange(
                    context.MotuCharacters
                    .Where(_ => _.Name.ToLower().Contains(name.ToLower())).ToList());
            }

            if (leader != null)
            {
                entries.AddRange(
                    context.MotuCharacters
                    .Where(_ => _.Leader.ToLower().Contains(leader.ToLower())).ToList());
            }
            return entries;
        }

    }
}
