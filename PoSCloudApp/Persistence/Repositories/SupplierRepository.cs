﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
{
    public class SupplierRepository:ISupplierRepository
    {
        private PosDbContext _context;

        public SupplierRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier GetSupplierById(int id)
        {
            return _context.Suppliers.Find(id);
        }

        public void AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
        }

        public void UpdateSupplier(int id, Supplier supplier)
        {
            _context.Suppliers.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Modified;
        }

        public void DeleteSupplier(int id)
        {
            var supplier = new Supplier { Id = id };
            _context.Suppliers.Attach(supplier);
            _context.Entry(supplier).State = EntityState.Deleted;
        }
    }
}