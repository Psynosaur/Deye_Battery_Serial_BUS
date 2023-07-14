using CAN2JSON.Data.Models;

namespace CAN2JSON.Interfaces;

public interface IBmsLogic
{
    /// <summary>
    /// Add a BmsReading
    /// </summary>
    /// <param name="bmsReading"></param>
    /// <returns></returns>
    Task<BmsReading> AddBmsReading(BmsReading bmsReading);

    /// <summary>
    /// Get a BmsReading by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BmsReading> GetBmsReadingById(int id);

    /// <summary>
    /// Update a BmsReading
    /// </summary>
    /// <param name="bmsReading"></param>
    /// <returns></returns>
    Task<BmsReading> UpdateBmsReading(BmsReading bmsReading);

    /// <summary>
    /// Delete a BmsReading by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteBmsReading(int id);
}