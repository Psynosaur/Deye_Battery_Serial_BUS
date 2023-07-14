using CAN2JSON.Data.Models;

namespace CAN2JSON.Interfaces;

public interface IBatteryLogic
{
    /// <summary>
    /// Add an BatteryReading
    /// </summary>
    /// <param name="batteryReading"></param>
    /// <returns></returns>
    Task<BatteryReading> AddBatteryReading(BatteryReading batteryReading);

    /// <summary>
    /// Get an BatteryReading by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BatteryReading> GetBatteryReadingById(int id);

    /// <summary>
    /// Update an BatteryReading
    /// </summary>
    /// <param name="batteryReading"></param>
    /// <returns></returns>
    Task<BatteryReading> UpdateBatteryReading(BatteryReading batteryReading);

    /// <summary>
    /// Delete an BatteryReading by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BatteryReading> DeleteBatteryReading(int id);
}