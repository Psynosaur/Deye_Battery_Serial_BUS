using CAN2JSON.Data.Context;
using CAN2JSON.Data.Models;
using CAN2JSON.Data.Repository;
using CAN2JSON.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Logic;

public class BmsLogic : IBmsLogic
{
    private readonly IRepository<BmsReading> _repository;
    
    public BmsLogic(
        IRepository<BmsReading> repository)
    {
        _repository = repository;
    }
    
    public async Task<BmsReading> GetBmsReadingById(int id)
    {
        if (id == 0)
            throw new Exception($"Book {nameof(id)} needs to be provided");

        BmsReading bookModel = await _repository.GetAll()
                                   .Include(b => b.BatteryReadings)
                                   .FirstOrDefaultAsync(bms => bms.Id == id)
                               ?? throw new Exception($"{nameof(BmsReading)} with {nameof(id)} not found");

        return bookModel;
    }

    public async Task<BmsReading> AddBmsReading(BmsReading bmsReading) => await _repository.AddAsync(bmsReading);
    

    public async Task<BmsReading> UpdateBmsReading(BmsReading bmsReading) => await _repository.UpdateAsync(bmsReading);

    public async Task DeleteBmsReading(int id)
    {
        if (id == 0)
            throw new Exception($"BmsReading {nameof(id)} needs to be provided");


        BmsReading? book = await _repository.GetAll()
                .Include(b => b.BatteryReadings)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                throw new Exception($"BmsReading does not exist");
            await _repository.DeleteAsync(book);
        }
    }

