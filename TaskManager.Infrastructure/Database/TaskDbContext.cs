using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Database;

public class TaskDbContext : IdentityDbContext<IdentityUser>
{
    public TaskDbContext(DbContextOptions options) : base(options)
    {
    }
}