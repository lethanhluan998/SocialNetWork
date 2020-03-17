using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Infrastructure.Interfaces;

namespace WebVuiVN.Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly WebVuiVNDbContext _context;
        public EFUnitOfWork(WebVuiVNDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
