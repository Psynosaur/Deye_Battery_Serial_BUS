using CAN2JSON.Data.Models;

namespace CAN2JSON.Interfaces;

public interface IBatteryCellLogic
{
    /// <summary>
    /// Add an BatteryCellReading
    /// </summary>
    /// <param name="batteryReading"></param>
    /// <returns></returns>
    Task<BatteryCellReading> AddBatteryCellReading(BatteryCellReading batteryReading);

    /// <summary>
    /// Get an BatteryCellReading by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BatteryCellReading> GetBatteryCellReadingById(int id);

    /// <summary>
    /// Update an BatteryCellReading
    /// </summary>
    /// <param name="batteryReading"></param>
    /// <returns></returns>
    Task<BatteryCellReading> UpdateBatteryCellReading(BatteryCellReading batteryReading);

    /// <summary>
    /// Delete an BatteryCellReading by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BatteryCellReading> DeleteBatteryCellReading(int id);
}