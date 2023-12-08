﻿using Microsoft.EntityFrameworkCore;
using SignalR_App.Application.Repositories;
using SignalR_App.Application.Services.Abstracts;
using SignalR_App.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_App.Application.Services.Concretes
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking, int> _bookingRepository;

        public BookingService(IRepository<Booking, int> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _bookingRepository.GetAll().ToListAsync();
        }
    }
}