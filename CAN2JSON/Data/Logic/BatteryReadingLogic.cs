using CAN2JSON.Data.Models;
using CAN2JSON.Data.Repository;
using CAN2JSON.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Logic;

public class BatteryReadingLogic : IBatteryLogic
{
    private readonly IRepository<BatteryReading> _repository;

    public BatteryReadingLogic(IRepository<BatteryReading> repository) => _repository = repository;

    public async Task<BatteryReading> GetBatteryReadingById(int id)
    {
        if (id == 0)
            throw new Exception($"BatteryReading {nameof(id)} needs to be provided");

        BatteryReading batteryReading = await _repository.GetAll()
            .FirstOrDefaultAsync(br => br.Id == id) ?? throw new Exception($"{nameof(BatteryReading)} with {nameof(id)} not found");;

        return batteryReading;
    }

    public async Task<BatteryReading> AddBatteryReading(BatteryReading batteryReading) => await _repository.AddAsync(batteryReading);

    public async Task<BatteryReading> UpdateBatteryReading(BatteryReading batteryReading)
    {
        return await _repository.UpdateAsync(batteryReading);
    }

    public async Task<BatteryReading> DeleteBatteryReading(int id)
    {
        if (id == 0)
            throw new Exception($"BatteryReading {nameof(id)} needs to be provided");
        
        BatteryReading? batteryReading = await _repository.GetAll().FirstOrDefaultAsync(br => br.Id == id);
        
        if(batteryReading == null)
            throw new Exception($"BatteryReading does not exist");
        
        return await _repository.DeleteAsync(batteryReading);
    }

}