using CAN2JSON.Data.Models;
using CAN2JSON.Data.Repository;
using CAN2JSON.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Logic;

public class BatteryCellLogic : IBatteryCellLogic
{
    private readonly IRepository<BatteryCellReading> _repository;

    public BatteryCellLogic(IRepository<BatteryCellReading> repository) => _repository = repository;

    public async Task<BatteryCellReading> GetBatteryCellReadingById(int id)
    {
        if (id == 0)
            throw new Exception($"BatteryCellReading {nameof(id)} needs to be provided");

        BatteryCellReading batteryReading = await _repository.GetAll()
            .FirstOrDefaultAsync(br => br.Id == id) ?? throw new Exception($"{nameof(BatteryCellReading)} with {nameof(id)} not found");;

        return batteryReading;
    }

    public async Task<BatteryCellReading> AddBatteryCellReading(BatteryCellReading batteryReading) => await _repository.AddAsync(batteryReading);

    public async Task<BatteryCellReading> UpdateBatteryCellReading(BatteryCellReading batteryReading)
    {
        return await _repository.UpdateAsync(batteryReading);
    }

    public async Task<BatteryCellReading> DeleteBatteryCellReading(int id)
    {
        if (id == 0)
            throw new Exception($"BatteryCellReading {nameof(id)} needs to be provided");
        
        BatteryCellReading? batteryReading = await _repository.GetAll().FirstOrDefaultAsync(br => br.Id == id);
        
        if(batteryReading == null)
            throw new Exception($"BatteryCellReading does not exist");
        
        return await _repository.DeleteAsync(batteryReading);
    }

}