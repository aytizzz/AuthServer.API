using AuthServer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Data.Configurations.UserApp
{
    public class UserAppConfiguration : IEntityTypeConfiguration<Core.Entities.UserApp>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.UserApp> builder)
        {
            builder.Property(x => x.City).HasMaxLength(40);
        }
    }
}
